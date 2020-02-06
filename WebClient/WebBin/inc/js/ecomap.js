(function (global, factory) {
  typeof exports === 'object' && typeof module !== 'undefined' ? factory(exports) :
  typeof define === 'function' && define.amd ? define(['exports'], factory) :
  (factory((global.ECO = global.ECO || {})));
}(this, (function (exports) { 'use strict';

	// Generic functions
	var bitsToNum = function (ba) {
	    return ba.reduce(function (s, n) { return s * 2 + n; }, 0);
	};

	var byteToBitArr = function (bite) {
	    var a = [];
	    for (var i = 7; i >= 0; i--) {
	        a.push(!!(bite & (1 << i)));
	    }
	    return a;
	};

	// Stream
	/**
	 * @constructor
	 */ // Make compiler happy.
	var Stream = function (data) {
	    this.data = data;
	    this.len = this.data.length;
	    this.pos = 0;

	    this.readByte = function () {
	        if (this.pos >= this.data.length) {
	            throw new Error('Attempted to read past end of stream.');
	        }
	        return data.charCodeAt(this.pos++) & 0xFF;
	    };

	    this.readBytes = function (n) {
	        var bytes = [];
	        for (var i = 0; i < n; i++) {
	            bytes.push(this.readByte());
	        }
	        return bytes;
	    };

	    this.read = function (n) {
	        var s = '';
	        for (var i = 0; i < n; i++) {
	            s += String.fromCharCode(this.readByte());
	        }
	        return s;
	    };

	    this.readUnsigned = function () { // Little-endian.
	        var a = this.readBytes(2);
	        return (a[1] << 8) + a[0];
	    };

	    this.readInt = function () {
	        var a = this.readBytes(4);
	        return (a[3] << 24) + (a[2] << 16) + (a[1] << 8) + a[0];
	    };

	    this.peek = function () {
	        return data.charCodeAt(this.pos) & 0xFF;
	    };
	};

	var lzwDecode = function (minCodeSize, data) {
	    // TODO: Now that the GIF parser is a bit different, maybe this should get an array of bytes instead of a String?
	    var pos = 0; // Maybe this streaming thing should be merged with the Stream?

	    var readCode = function (size) {
	        var code = 0;
	        for (var i = 0; i < size; i++) {
	            if (data.charCodeAt(pos >> 3) & (1 << (pos & 7))) {
	                code |= 1 << i;
	            }
	            pos++;
	        }
	        return code;
	    };

	    var output = [];

	    var clearCode = 1 << minCodeSize;
	    var eoiCode = clearCode + 1;

	    var codeSize = minCodeSize + 1;

	    var dict = [];

	    var clear = function () {
	        dict = [];
	        codeSize = minCodeSize + 1;
	        for (var i = 0; i < clearCode; i++) {
	            dict[i] = [i];
	        }
	        dict[clearCode] = [];
	        dict[eoiCode] = null;

	    };

	    var code;
	    var last;

	    while (true) {
	        last = code;
	        code = readCode(codeSize);

	        if (code === clearCode) {
	            clear();
	            continue;
	        }
	        if (code === eoiCode) break;

	        if (code < dict.length) {
	            if (last !== clearCode) {
	                dict.push(dict[last].concat(dict[code][0]));
	            }
	        } else {
	            if (code !== dict.length) throw new Error('Invalid LZW code.');
	            dict.push(dict[last].concat(dict[last][0]));
	        }
	        output.push.apply(output, dict[code]);

	        if (dict.length === (1 << codeSize) && codeSize < 12) {
	            // If we're at the last code and codeSize is 12, the next code will be a clearCode, and it'll be 12 bits long.
	            codeSize++;
	        }
	    }

	    // I don't know if this is technically an error, but some GIFs do it.
	    //if (Math.ceil(pos / 8) !== data.length) throw new Error('Extraneous LZW bytes.');
	    return output;
	};

	// The actual parsing; returns an object with properties.
	var parseGIF = function (st, handler) {
	    handler || (handler = {});

	    // LZW (GIF-specific)
	    var parseCT = function (entries) { // Each entry is 3 bytes, for RGB.
	        var ct = [];
	        for (var i = 0; i < entries; i++) {
	            ct.push(st.readBytes(3));
	        }
	        return ct;
	    };

	    var readSubBlocks = function () {
	        var size, data;
	        data = '';
	        do {
	            size = st.readByte();
	            data += st.read(size);
	        } while (size !== 0);
	        return data;
	    };

	    var parseHeader = function () {
	        var hdr = {};
	        hdr.sig = st.read(3);
	        hdr.ver = st.read(3);
	        if (hdr.sig !== 'GIF') throw new Error('Not a GIF file.'); // XXX: This should probably be handled more nicely.

	        hdr.width = st.readUnsigned();
	        hdr.height = st.readUnsigned();

	        var bits = byteToBitArr(st.readByte());
	        hdr.gctFlag = bits.shift();
	        hdr.colorRes = bitsToNum(bits.splice(0, 3));
	        hdr.sorted = bits.shift();
	        hdr.gctSize = bitsToNum(bits.splice(0, 3));

	        hdr.bgColor = st.readByte();
	        hdr.pixelAspectRatio = st.readByte(); // if not 0, aspectRatio = (pixelAspectRatio + 15) / 64

	        if (hdr.gctFlag) {
	            hdr.gct = parseCT(1 << (hdr.gctSize + 1));
	        }
	        handler.hdr && handler.hdr(hdr);
	    };

	    var parseExt = function (block) {
	        var parseGCExt = function (block) {
	            var blockSize = st.readByte(); // Always 4

	            var bits = byteToBitArr(st.readByte());
	            block.reserved = bits.splice(0, 3); // Reserved; should be 000.
	            block.disposalMethod = bitsToNum(bits.splice(0, 3));
	            block.userInput = bits.shift();
	            block.transparencyGiven = bits.shift();

	            block.delayTime = st.readUnsigned();

	            block.transparencyIndex = st.readByte();

	            block.terminator = st.readByte();

	            handler.gce && handler.gce(block);
	        };

	        var parseComExt = function (block) {
	            if (st.peek() == 4)
	            {
	                st.readByte();
	                block.comment = st.readInt();
	                st.readByte();
	            }
	            else
	                block.comment = parseFloat(readSubBlocks());

	            handler.com && handler.com(block);
	        };

	        var parsePTExt = function (block) {
	            // No one *ever* uses this. If you use it, deal with parsing it yourself.
	            var blockSize = st.readByte(); // Always 12
	            block.ptHeader = st.readBytes(12);
	            block.ptData = readSubBlocks();
	            handler.pte && handler.pte(block);
	        };

	        var parseAppExt = function (block) {
	            var parseNetscapeExt = function (block) {
	                var blockSize = st.readByte(); // Always 3
	                block.unknown = st.readByte(); // ??? Always 1? What is this?
	                block.iterations = st.readUnsigned();
	                block.terminator = st.readByte();
	                handler.app && handler.app.NETSCAPE && handler.app.NETSCAPE(block);
	            };

	            var parseUnknownAppExt = function (block) {
	                block.appData = readSubBlocks();
	                // FIXME: This won't work if a handler wants to match on any identifier.
	                handler.app && handler.app[block.identifier] && handler.app[block.identifier](block);
	            };

	            var blockSize = st.readByte(); // Always 11
	            block.identifier = st.read(8);
	            block.authCode = st.read(3);
	            switch (block.identifier) {
	                case 'NETSCAPE':
	                    parseNetscapeExt(block);
	                    break;
	                default:
	                    parseUnknownAppExt(block);
	                    break;
	            }
	        };

	        var parseUnknownExt = function (block) {
	            block.data = readSubBlocks();
	            handler.unknown && handler.unknown(block);
	        };

	        block.label = st.readByte();
	        switch (block.label) {
	            case 0xF9:
	                block.extType = 'gce';
	                parseGCExt(block);
	                break;
	            case 0xFE:
	                block.extType = 'com';
	                parseComExt(block);
	                break;
	            case 0x01:
	                block.extType = 'pte';
	                parsePTExt(block);
	                break;
	            case 0xFF:
	                block.extType = 'app';
	                parseAppExt(block);
	                break;
	            default:
	                block.extType = 'unknown';
	                parseUnknownExt(block);
	                break;
	        }
	    };

	    var parseImg = function (img) {
	        var deinterlace = function (pixels, width) {
	            // Of course this defeats the purpose of interlacing. And it's *probably*
	            // the least efficient way it's ever been implemented. But nevertheless...

	            var newPixels = new Array(pixels.length);
	            var rows = pixels.length / width;
	            var cpRow = function (toRow, fromRow) {
	                var fromPixels = pixels.slice(fromRow * width, (fromRow + 1) * width);
	                newPixels.splice.apply(newPixels, [toRow * width, width].concat(fromPixels));
	            };

	            // See appendix E.
	            var offsets = [0, 4, 2, 1];
	            var steps = [8, 8, 4, 2];

	            var fromRow = 0;
	            for (var pass = 0; pass < 4; pass++) {
	                for (var toRow = offsets[pass]; toRow < rows; toRow += steps[pass]) {
	                    cpRow(toRow, fromRow);
	                    fromRow++;
	                }
	            }

	            return newPixels;
	        };

	        img.leftPos = st.readUnsigned();
	        img.topPos = st.readUnsigned();
	        img.width = st.readUnsigned();
	        img.height = st.readUnsigned();

	        var bits = byteToBitArr(st.readByte());
	        img.lctFlag = bits.shift();
	        img.interlaced = bits.shift();
	        img.sorted = bits.shift();
	        img.reserved = bits.splice(0, 2);
	        img.lctSize = bitsToNum(bits.splice(0, 3));

	        if (img.lctFlag) {
	            img.lct = parseCT(1 << (img.lctSize + 1));
	        }

	        img.lzwMinCodeSize = st.readByte();

	        var lzwData = readSubBlocks();

	        img.pixels = lzwDecode(img.lzwMinCodeSize, lzwData);

	        if (img.interlaced) { // Move
	            img.pixels = deinterlace(img.pixels, img.width);
	        }

	        handler.img && handler.img(img);
	    };

	    var parseBlock = function () {
	        var block = {};
	        block.sentinel = st.readByte();

	        switch (String.fromCharCode(block.sentinel)) { // For ease of matching
	            case '!':
	                block.type = 'ext';
	                parseExt(block);
	                break;
	            case ',':
	                block.type = 'img';
	                parseImg(block);
	                break;
	            case ';':
	                block.type = 'eof';
	                handler.eof && handler.eof(block);
	                break;
	            default:
	                throw new Error('Unknown block: 0x' + block.sentinel.toString(16)); // TODO: Pad this with a 0.
	        }

	        if (block.type !== 'eof') setTimeout(parseBlock, 0);
	    };

	    var parse = function () {
	        parseHeader();
	        setTimeout(parseBlock, 0);
	    };

	    parse();
	};

	// BEGIN_NON_BOOKMARKLET_CODE
	if (typeof exports !== 'undefined') {
	    exports.Stream = Stream;
	    exports.parseGIF = parseGIF;
	}
	// END_NON_BOOKMARKLET_CODE

	function GIFDECODER(name, OnFinish, filter) {

	    var my = {};

	    var handler;
	    var canvas = document.createElement('canvas');
	    var ctx = canvas.getContext('2d');
	    var tmpCanvas = document.createElement('canvas');
	    var transparency = null;
	    var delay = null;
	    var disposalMethod = null;
	    var lastDisposalMethod = null;
	    var frame = null;
	    var frameCount = 0;
	    var tid = 0;


	    //gifStorage = {};
	    my.parseName = name;

	    my.clean = function (name) {
	        if (!GIFMANAGER.gifStorage[name]) return;
	        for (var i = 0; i < GIFMANAGER.gifStorage[name].length; i++) {
	            GIFMANAGER.gifStorage[name][i].dispose();
	        }
	        delete GIFMANAGER.gifStorage[name];
	    };

	    this.onFinish = null;

	    my.loadImage = function (name) {
	        this.parseName = name;
	        frame = null;
	        frames = [];
	        frameCount = 0;
	        var xhr = new XMLHttpRequest();
	        xhr.open("GET", "Layers/" + name + ".gif", true);
	        xhr.overrideMimeType('text/plain; charset=x-user-defined');
	        //xhr.responseType = "arraybuffer";

	        xhr.onload = function (e) {
	            var arrayBuffer = xhr.responseText; // Note: not xhr.responseText
	            parseGIF(new Stream(arrayBuffer), my.handler);
	        };

	        xhr.send(null);

	    };

	    var clear = function () {
	        transparency = null;
	        delay = null;
	        lastDisposalMethod = disposalMethod;
	        disposalMethod = null;
	        frame = null;
	    };

	    var hdr;
	    var doHdr = function (_hdr) {
	        hdr = _hdr;

	        console.log("Starting to parse " + my.parseName);

	        canvas.width = hdr.width;
	        canvas.height = hdr.height;

	        tmpCanvas.width = hdr.width;
	        tmpCanvas.height = hdr.height;
	    };

	    var doGCE = function (gce) {
	        my.pushFrame();
	        clear();
	        transparency = gce.transparencyGiven ? gce.transparencyIndex : null;
	        delay = gce.delayTime;
	        disposalMethod = gce.disposalMethod;
	    };

	    my.pushFrame = function () {
	        if (!frame) return;

	        var tcanvas = document.createElement('canvas');
	        tcanvas.width = tmpCanvas.width;
	        tcanvas.height = tmpCanvas.height;
	        var tctx = tcanvas.getContext('2d');
	        tctx.drawImage(tmpCanvas, 0, 0, tmpCanvas.width, tmpCanvas.height);

	        var texture = new THREE.Texture(tcanvas);
	        texture.minFilter = filter;
	        texture.magFilter = filter;
	        //texture.wrapS = THREE.RepeatWrapping;
	        //texture.wrapT = THREE.RepeatWrapping;
	        texture.needsUpdate = true;
	        texture.flipY = false;

	        GIFMANAGER.gifStorage[my.parseName].push(texture);

	        frameCount++;
	    };

	    var doImg = function (img) {
	        if (!frame) frame = tmpCanvas.getContext('2d');
	        var ct = img.lctFlag ? img.lct : hdr.gct;

	        var cData = frame.getImageData(img.leftPos, img.topPos, img.width, img.height);

	        img.pixels.forEach(function (pixel, i) {
	            if (transparency !== pixel) {
	                cData.data[i * 4 + 0] = ct[pixel][0];
	                cData.data[i * 4 + 1] = ct[pixel][1];
	                cData.data[i * 4 + 2] = ct[pixel][2];
	                cData.data[i * 4 + 3] = 255;
	            } else {
	                if (lastDisposalMethod === 2 || lastDisposalMethod === 3) {
	                    cData.data[i * 4 + 3] = 0;
	                } else {
	                }
	            }
	        });
	        frame.putImageData(cData, img.leftPos, img.topPos);
	        ctx.putImageData(cData, img.leftPos, img.topPos);
	    };

	    var doNothing = function () { };

	    var doComment = function (cblock) {
	        GIFMANAGER.gifTimes[my.parseName].push(cblock.comment);
	    };

	    my.handler = {
	        hdr: doHdr,
	        gce: doGCE,
	        com: doComment,
	        app: {
	            NETSCAPE: doNothing
	        },
	        img: doImg,
	        eof: function (block) {
	            my.pushFrame();
	            if (OnFinish) OnFinish(my.parseName);
	            //         spinner.style.opacity = 0;
	        }
	    };

	    my.loadImage(my.parseName);

	    return my;
	}

	var GIFMANAGER = (function() {
	    var my = {};
	    my.gifStorage = {};
	    my.gifTimes = {};

	    my.giftextures = function(name, onFinish, filter = THREE.LinearFilter) {
	        if(!my.gifStorage[name]) {
	            //parseName = name;
	            my.gifStorage[name] = [];
	            my.gifTimes[name] = [];
	            var tempLoader = new GIFDECODER(name, onFinish?onFinish:my.OnGifLoaded, filter);
	            //tempLoader.loadImage(name);
	            //this.loadImage(name);
	        } else if(onFinish){
	            onFinish(name);
	        }
	        return my.gifStorage[name];
	    };

	    my.OnGifLoaded = null;

	    return my;
	}());

	function GenChunk(xSize, zSize, worldX, worldZ) {
	    var geometry = new THREE.InstancedBufferGeometry();

	    var v = new THREE.BufferAttribute(new Float32Array((xSize+1) * (zSize+1) * 4 * 4), 4);
	    var t = new THREE.BufferAttribute(new Uint16Array(xSize * zSize * 18), 1);

	    function p(x, z) {
	        return 4 * ((xSize+1) * z + x);
	    }
	 
	    // z
	    // ^ p   p+1
	    // |
	    // | 3   2     7   6
	    // | 
	    // | 0   1     4   5
	    // +------------> x

	    var i = 0;
	    var ti = 0;
	    var iUp = 0;
	    for (var z = 0; z <= zSize; z++) {
	        for (var x = 0; x <= xSize; x++) {
	            //index = p(x, z);
	            v.setXYZW(i,     x*100,      50, z*100, 50 );
	            v.setXYZW(i + 1, x*100 + 100,  -50, z*100, 50);
	            v.setXYZW(i + 2, x*100 + 100,  -50, z*100 + 100, -50);
	            v.setXYZW(i + 3, x*100,      50, z*100 + 100, -50);

	            if (x < xSize && z < zSize) {
	                t.setXYZ(ti, i + 0, i + 2, i + 1);
	                t.setXYZ(ti + 3, i + 0, i + 3, i + 2);

	                //if (x < xSize) {
	                t.setXYZ(ti + 6, i + 1, i + 2, i + 7);
	                t.setXYZ(ti + 9, i + 1, i + 7, i + 4);
	                //}

	                //if (z + 1 < zSize) {
	                iUp = p(x, z + 1);
	                t.setXYZ(ti + 12, i + 2, i + 3, iUp);
	                t.setXYZ(ti + 15, i + 2, iUp, iUp + 1);
	                //}
	                ti += 18;
	            }
	            i += 4;
	        }
	    }

	    geometry.setIndex(t);
	    geometry.addAttribute('position', v);

	    var d = Math.round(worldX / xSize);
	    var instances = d*d;
	    var o = new THREE.InstancedBufferAttribute(new Float32Array(instances * 3), 3, 1);
	    i = 0;
	    for (var z = 0; z < d; z++) {
	        for (var x = 0; x < d; x++) {
	            o.setXYZ(i++, (x * xSize * 100) - (worldX * 50), 0.001, (z * zSize * 100) - (worldZ * 50));
	        }
	    }
	    geometry.addAttribute("translate", o);
	    // threejs isn't setting this for some reason and nothing renders w/o it
	    geometry.maxInstancedCount = instances;
	    geometry.boundingSphere = new THREE.Sphere(new THREE.Vector3(0, 0, 0), worldX * 1000);

	    return geometry;
	}

	var rawFragmentShader = "precision highp float;\nprecision highp int;\nuniform float time;\nuniform vec2 resolution;\nuniform sampler2D map;\nuniform sampler2D heat;\nuniform sampler2D features;\nuniform sampler2D terrain;\n#ifdef DRAWING\nuniform sampler2D drawmap;\n#endif\nuniform vec3 tint;\nvarying vec2 vUv;\nvarying vec3 vColor;\nvoid main(void) {\n#ifdef LOOKUP\n    gl_FragColor = vec4(vUv, 0.0, vColor.r);\n#else\n    vec3 mColor = texture2D(map, vUv).rgb;\n#ifdef HEATMAP\n    float a = mColor.r > 0.2 ? 1.0 : (mColor.r + 0.8);\n    vec2 uv = vec2(mColor.r, mColor.g);\n    mColor = vColor - texture2D(heat, uv).rgb;\n#endif\n#ifdef FMAP\n    #ifdef HEATMAP\n    mColor = (mColor * a) + ((1.0 - a) * (vColor - (vec3(1, 1, 1) - texture2D(terrain, vUv).rgb)));\n    #else\n    mColor = vColor * mColor;\n    #endif\n    mColor = mColor * texture2D(features, vUv).rgb;\n#endif\n#ifdef DRAWING\n    vec4 drawing = texture2D(drawmap, vUv);\n    mColor = (drawing.r > 0.0) ? (vec3(1, 1, 1) - mColor) : mColor;\n#endif\n    gl_FragColor = vec4(mColor * tint, 0.9);\n#endif\n}";

	var curvedVertex = "precision highp float;\nprecision highp int;\n#define VERTEX_TEXTURES\nuniform mat4 modelMatrix;\nuniform mat4 projectionMatrix;\nuniform mat4 viewMatrix;\nuniform mat4 modelViewMatrix;\nuniform vec4 worldSize;\nuniform vec3 cameraPosition;\nattribute vec4 position;\nattribute vec3 translate;\nvarying vec2 vUv;\nvarying vec3 vColor;\nuniform sampler2D height;\n#ifdef LOOKUP\nuniform vec3 tint;\n#endif\nvec3 slerp(vec3 p0, vec3 p1, float t)\n{\n    float cosHalfTheta = dot(p0, p1);\n    float halfTheta = acos(cosHalfTheta);\n    float sinHalfTheta = sqrt(1.0 - cosHalfTheta * cosHalfTheta);\n    float ratioA = sin((1.0 - t) * halfTheta) / sinHalfTheta;\n    float ratioB = sin(t * halfTheta) / sinHalfTheta;\n    return p0 * ratioA + p1 * ratioB;\n}\nvec4 curveVertexFixed(vec4 vertex)\n{\n    float radius = worldSize.x / 3.14159;    vec3 center = vec3(cameraPosition.x, -radius, cameraPosition.z);\n    vec4 vv = modelMatrix * vertex;\n    if (cameraPosition.x - vv.x > worldSize.x)\n        vv.x = vv.x + worldSize.z;\n    else if (cameraPosition.x - vv.x < -worldSize.x)\n        vv.x = vv.x - worldSize.z;\n    if (cameraPosition.z - vv.z > worldSize.y)\n        vv.z = vv.z + worldSize.w;\n    else if (cameraPosition.z - vv.z < -worldSize.y)\n        vv.z = vv.z - worldSize.w;\n    vec3 toVertex = vv.xyz - center;\n    vec3 v1 = vec3(0, toVertex.y, 0);    vec3 xz = vec3(toVertex.x, 0, toVertex.z);\n    vec3 v2 = normalize(xz) * abs(toVertex.y);\n    float d = length(toVertex.xz);\n    float c = 2.0 * 3.14159 * abs(radius);\n    float t = clamp(d / (c / 4.0), 0.0, 2.0);\n    vec3 s = slerp(v1, v2, t);\n    vec3 expected = center + s;\n    vv.xyz = expected;\n    vv = viewMatrix*vv;\n    return projectionMatrix * vv;\n}\nvoid main()\n{\n    vec3 pos = position.xyz + translate;\n    vUv = (pos.xz + worldSize.xy) / worldSize.zw;\n#ifdef GRID\n    pos.y = texture2D(height, fract(vUv + position.yw / worldSize.zw)).r * 25500.0;\n    float y2 = texture2D(height, fract(vUv - position.yw / worldSize.zw)).r * 25500.0;\n    float y3 = texture2D(height, fract(vUv - vec2(position.y, -position.w) / worldSize.zw)).r * 25500.0;\n    float y4 = texture2D(height, fract(vUv - vec2(-position.y, position.w) / worldSize.zw)).r * 25500.0;\n    if (y2 > pos.y || y3 > pos.y || y4 > pos.y)\n        vColor = vec3(0.7, 0.7, 0.7);\n    else\n        vColor = vec3(1.0, 1.0, 1.0);\n#endif\n#ifdef FLAT\n#ifdef WRAP\n    vec4 vv = modelMatrix * vec4(pos, 1.0);\n    if (cameraPosition.x - vv.x > worldSize.x)\n        vv.x = vv.x + worldSize.z;\n    else if (cameraPosition.x - vv.x > worldSize.x * 0.9)\n        vv.y = -10000.0;\n    else if (cameraPosition.x - vv.x < -worldSize.x)\n        vv.x = vv.x - worldSize.z;\n    else if (cameraPosition.x - vv.x < -worldSize.x * 0.9)\n        vv.y = -10000.0;\n    if (cameraPosition.z - vv.z > worldSize.y)\n        vv.z = vv.z + worldSize.w;\n    else if (cameraPosition.z - vv.z > worldSize.y * 0.9)\n        vv.y = -10000.0;\n    else if (cameraPosition.z - vv.z < -worldSize.y)\n        vv.z = vv.z - worldSize.w;\n    else if (cameraPosition.z - vv.z < -worldSize.y * 0.9)\n        vv.y = -10000.0;\n    vec4 mvPosition = viewMatrix * vv;\n#else\n    vec4 mvPosition = viewMatrix* vec4(pos, 1.0);\n#endif\n    gl_Position = projectionMatrix * mvPosition;\n#else\n    gl_Position = curveVertexFixed(vec4(pos, 1.0));\n#endif\n}\n";

	var vertexShader = "uniform vec4 worldSize;\nvarying vec2 vUv;\nvarying vec3 vColor;\nvec3 slerp(vec3 p0, vec3 p1, float t)\n{\n    float cosHalfTheta = dot(p0, p1);\n    float halfTheta = acos(cosHalfTheta);\n    float sinHalfTheta = sqrt(1.0 - cosHalfTheta * cosHalfTheta);\n    float ratioA = sin((1.0 - t) * halfTheta) / sinHalfTheta;\n    float ratioB = sin(t * halfTheta) / sinHalfTheta;\n    return p0 * ratioA + p1 * ratioB;\n}\nvec4 curveVertexFixed(vec4 vertex)\n{\n    float radius = worldSize.x / 3.14159;    vec3 center = vec3(cameraPosition.x, -radius, cameraPosition.z);\n    vec4 vv = modelMatrix * vertex;\n    if (cameraPosition.x - vv.x > worldSize.x)\n        vv.x = vv.x + worldSize.z;\n    else if (cameraPosition.x - vv.x < -worldSize.x)\n        vv.x = vv.x - worldSize.z;\n    if (cameraPosition.z - vv.z > worldSize.y)\n        vv.z = vv.z + worldSize.w;\n    else if (cameraPosition.z - vv.z < -worldSize.y)\n        vv.z = vv.z - worldSize.w;\n    vec3 toVertex = vv.xyz - center;\n    vec3 v1 = vec3(0, toVertex.y, 0);    vec3 xz = vec3(toVertex.x, 0, toVertex.z);\n    vec3 v2 = normalize(xz) * abs(toVertex.y);\n    float d = length(toVertex.xz);\n    float c = 2.0 * 3.14159 * abs(radius);\n    float t = clamp(d / (c / 4.0), 0.0, 2.0);\n    vec3 s = slerp(v1, v2, t);\n    vec3 expected = center + s;\n    vv.xyz = expected;\n    vv = viewMatrix*vv;\n    return projectionMatrix * vv;\n}\nvoid main()\n{\n    vColor = color;\n#ifdef FLAT\n#ifdef WRAP\n    vec4 vv = modelMatrix * vec4(position, 1.0);\n    if (cameraPosition.x - vv.x > worldSize.x)\n        vv.x = vv.x + worldSize.z;\n    else if (cameraPosition.x - vv.x > worldSize.x * 0.9)\n        vv.y = -10000.0;\n    else if (cameraPosition.x - vv.x < -worldSize.x)\n        vv.x = vv.x - worldSize.z;\n    else if (cameraPosition.x - vv.x < -worldSize.x * 0.9)\n        vv.y = -10000.0;\n    if (cameraPosition.z - vv.z > worldSize.y)\n        vv.z = vv.z + worldSize.w;\n    else if (cameraPosition.z - vv.z > worldSize.y * 0.9)\n        vv.y = -10000.0;\n    else if (cameraPosition.z - vv.z < -worldSize.y)\n        vv.z = vv.z - worldSize.w;\n    else if (cameraPosition.z - vv.z < -worldSize.y * 0.9)\n        vv.y = -10000.0;\n    vec4 mvPosition = viewMatrix * vv;\n#else\n    vec4 mvPosition = modelViewMatrix * vec4(position, 1.0);\n#endif\n    gl_Position = projectionMatrix * mvPosition;\n#else\n    gl_Position = curveVertexFixed(vec4(position, 1.0));\n#endif\n    vUv = (position.xz + worldSize.xy) / worldSize.zw;\n}\n";

	var fragmentShader = "uniform float time;\nuniform vec2 resolution;\nuniform sampler2D map;\nuniform sampler2D heat;\nuniform sampler2D features;\nuniform vec3 tint;\nvarying vec2 vUv;\nvarying vec3 vColor;\nvoid main(void) {\n\tgl_FragColor = vec4(tint, 0.9);\n}\n";

	var worldLayerFragment = "precision highp float;\nprecision highp int;\nuniform float time;\nuniform vec2 resolution;\nuniform sampler2D renderTex;\nuniform sampler2D map;\nuniform sampler2D heat;\nuniform sampler2D features;\nuniform sampler2D terrain;\nuniform vec3 waterColor;\n#ifdef DRAWING\nuniform sampler2D drawmap;\n#endif\nuniform vec3 tint;\nvarying vec2 vUv;\nvoid main(void) {\n    vec4 inputData = texture2D(renderTex, vUv);\n    vec2 uv = inputData.xy;\n    float underWater = inputData.z;\n    vec3 vColor = inputData.www ;\n    vec3 mColor = texture2D(map, uv).rgb;\n#ifdef DRAWING\n    vec4 drawing = texture2D(drawmap, uv);\n\tdrawing = texture2D(heat, vec2(drawing.r, 0));\n\tmColor = vColor - (vec3(1, 1, 1) - drawing.rgb);\n\t#ifdef FMAP\n\t\tmColor = mColor * texture2D(features, uv).rgb;\n\t#endif\n\tvec3 c = mix(mColor * tint, vec3(0.0, 0.0, 0.1), underWater * 0.7);\n#else\n\t#ifdef HEATMAP\n\t\tfloat a = mColor.r > 0.2 ? 1.0 : (mColor.r + 0.8);\n\t\tvec2 uv2 = vec2(mColor.r, mColor.g);\n\t#endif\n\t#ifdef FMAP\n\t\t#ifdef HEATMAP\n\t\tmColor = vColor - texture2D(heat, uv2).rgb;\n\t\tmColor = (mColor * a) + ((1.0 - a) * (vColor - (vec3(1, 1, 1) - texture2D(terrain, uv).rgb)));\n\t\t#else\n\t\tmColor = vColor * mColor;\n\t\t#endif\n\t\tmColor = mColor * texture2D(features, uv).rgb;\n\t#else\n\t\tmColor = vColor * texture2D(heat, uv2).rgb;\n\t\t#ifdef HEATMAP\n\t\t\tunderWater *= 0.7;\n\t\t#endif\n\t#endif\n\t\tvec3 c = mix(mColor * tint, waterColor, underWater);\n#endif\n    gl_FragColor = vec4(c, step(0.5, inputData.w));\n}";

	var worldLayerVertex = "precision highp float;\nuniform mat4 projectionMatrix;\nuniform mat4 modelViewMatrix;\nattribute vec2 uv;\nattribute vec3 position;\nvarying vec2 vUv;\nvoid main()\n{\n\tvUv = uv;\n\tgl_Position = projectionMatrix * modelViewMatrix * vec4( position, 1.0 );\n}\n";

	//'use strict'

	//exports.byteLength = byteLength
	//exports.toByteArray = toByteArray
	//exports.fromByteArray = fromByteArray

	var lookup = [];
	var revLookup = [];
	var Arr = typeof Uint8Array !== 'undefined' ? Uint8Array : Array;

	var code = 'ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789+/';
	for (var i = 0, len = code.length; i < len; ++i) {
	  lookup[i] = code[i];
	  revLookup[code.charCodeAt(i)] = i;
	}

	revLookup['-'.charCodeAt(0)] = 62;
	revLookup['_'.charCodeAt(0)] = 63;

	function placeHoldersCount (b64) {
	  var len = b64.length;
	  if (len % 4 > 0) {
	    throw new Error('Invalid string. Length must be a multiple of 4')
	  }

	  // the number of equal signs (place holders)
	  // if there are two placeholders, than the two characters before it
	  // represent one byte
	  // if there is only one, then the three characters before it represent 2 bytes
	  // this is just a cheap hack to not do indexOf twice
	  return b64[len - 2] === '=' ? 2 : b64[len - 1] === '=' ? 1 : 0
	}

	function toByteArray (b64) {
	  var i, j, l, tmp, placeHolders, arr;
	  var len = b64.length;
	  placeHolders = placeHoldersCount(b64);

	  arr = new Arr(len * 3 / 4 - placeHolders);

	  // if there are placeholders, only get up to the last complete 4 chars
	  l = placeHolders > 0 ? len - 4 : len;

	  var L = 0;

	  for (i = 0, j = 0; i < l; i += 4, j += 3) {
	    tmp = (revLookup[b64.charCodeAt(i)] << 18) | (revLookup[b64.charCodeAt(i + 1)] << 12) | (revLookup[b64.charCodeAt(i + 2)] << 6) | revLookup[b64.charCodeAt(i + 3)];
	    arr[L++] = (tmp >> 16) & 0xFF;
	    arr[L++] = (tmp >> 8) & 0xFF;
	    arr[L++] = tmp & 0xFF;
	  }

	  if (placeHolders === 2) {
	    tmp = (revLookup[b64.charCodeAt(i)] << 2) | (revLookup[b64.charCodeAt(i + 1)] >> 4);
	    arr[L++] = tmp & 0xFF;
	  } else if (placeHolders === 1) {
	    tmp = (revLookup[b64.charCodeAt(i)] << 10) | (revLookup[b64.charCodeAt(i + 1)] << 4) | (revLookup[b64.charCodeAt(i + 2)] >> 2);
	    arr[L++] = (tmp >> 8) & 0xFF;
	    arr[L++] = tmp & 0xFF;
	  }

	  return arr
	}

	function tripletToBase64 (num) {
	  return lookup[num >> 18 & 0x3F] + lookup[num >> 12 & 0x3F] + lookup[num >> 6 & 0x3F] + lookup[num & 0x3F]
	}

	function encodeChunk (uint8, start, end) {
	  var tmp;
	  var output = [];
	  for (var i = start; i < end; i += 3) {
	    tmp = (uint8[i] << 16) + (uint8[i + 1] << 8) + (uint8[i + 2]);
	    output.push(tripletToBase64(tmp));
	  }
	  return output.join('')
	}

	function fromByteArray (uint8) {
	  var tmp;
	  var len = uint8.length;
	  var extraBytes = len % 3; // if we have 1 byte left, pad 2 bytes
	  var output = '';
	  var parts = [];
	  var maxChunkLength = 16383; // must be multiple of 3

	  // go through the array every three bytes, we'll deal with trailing stuff later
	  for (var i = 0, len2 = len - extraBytes; i < len2; i += maxChunkLength) {
	    parts.push(encodeChunk(uint8, i, (i + maxChunkLength) > len2 ? len2 : (i + maxChunkLength)));
	  }

	  // pad the end with zeros, but make sure to not forget the extra bytes
	  if (extraBytes === 1) {
	    tmp = uint8[len - 1];
	    output += lookup[tmp >> 2];
	    output += lookup[(tmp << 4) & 0x3F];
	    output += '==';
	  } else if (extraBytes === 2) {
	    tmp = (uint8[len - 2] << 8) + (uint8[len - 1]);
	    output += lookup[tmp >> 10];
	    output += lookup[(tmp >> 4) & 0x3F];
	    output += lookup[(tmp << 2) & 0x3F];
	    output += '=';
	  }

	  parts.push(output);

	  return parts.join('')
	}

	function PlotMap(width, height, type, mapping, wrapS, wrapT, magFilter, minFilter, anisotropy, format) {

	    //if ( type === undefined && format === THREE.DepthFormat ) type = THREE.UnsignedShortType;
	    //if ( type === undefined && format === THREE.DepthStencilFormat ) type = THREE.UnsignedInt248Type;
	    var canvas = document.createElement('canvas');
	    canvas.width = width;
	    canvas.height = height;

	    THREE.Texture.call(this, canvas, mapping, wrapS, wrapT, magFilter, minFilter, format, type, anisotropy);

	    this.ctxt = canvas.getContext('2d');
	    this.ctxt.webkitImageSmoothingEnabled = false;

	    this.zoneColor = "#000000";
	    this.ctxt.fillStyle = this.zoneColor;
	    this.ctxt.strokeStyle = this.zoneColor;
	    this.ctxt.miterLimit = 0;
	    this.ctxt.lineJoin = "bevel";
	    this.erasing = false;

	    this.plotSize = 1;

	    this.magFilter = magFilter !== undefined ? magFilter : THREE.NearestFilter;
	    this.minFilter = minFilter !== undefined ? minFilter : THREE.NearestFilter;

	    this.generateMipmaps    = false;
	    this.Reset();
	    this.lastData = this.ctxt.getImageData(0, 0, this.image.width, this.image.height);
	}

	PlotMap.prototype = Object.create(THREE.Texture.prototype);
	PlotMap.prototype.constructor = PlotMap;

	PlotMap.prototype.SetColor = function (color) {
	    this.zoneColor = color;
	    this.ctxt.fillStyle = color;
	    this.ctxt.strokeStyle = color;
	};

	PlotMap.prototype.SetPixel = function (vec, size=1) {
	    this.ctxt.fillRect(vec.x - size, vec.y - size, 1 + size * 2, 1 + size * 2);
	    this.ctxt.beginPath();
	    this.ctxt.moveTo(vec.x + 0.5, vec.y + 0.5);
	    this.needsUpdate = true;
	};

	// canvas lineto uses anti-aliasing that sets incorrect district IDs... using Bresenham line drawing instead
	PlotMap.prototype.DrawLine = function (start, end, size = 1) {
	    if (start.distanceTo(end) > this.image.width / 2) {
	        // TODO: figure out the right direction and do some wrapping or draw 2 line segments
	        this.SetPixel(end, size);
	    } else {
	        var vec = start.clone();
	        var dx = Math.abs(end.x-vec.x);
	        var dy = Math.abs(end.y-vec.y);
	        var sx = (vec.x < end.x) ? 1 : -1;
	        var sy = (vec.y < end.y) ? 1 : -1;
	        var err = dx-dy;

	        while(true){
	            this.SetPixel(vec, size);

	            if ((vec.x==end.x) && (vec.y==end.y)) break;
	            var e2 = 2*err;
	            if (e2 >-dy){ err -= dy; vec.x  += sx; }
	            if (e2 < dx){ err += dx; vec.y  += sy; }
	        }
	    }
	};

	PlotMap.prototype.SetRect = function (a, b) {
	    this.mask[a.x + a.y * this.width / 5] = 255;
	    var min = new THREE.Vector2((a.x < b.x) ? a.x : b.x, (a.y < b.y) ? a.y : b.y);
	    var size = new THREE.Vector2(Math.abs(a.x - b.x), Math.abs(a.y - b.y));

	    this.ctxt.fillRect(5 * min.x + 2.5, 5 * min.y + 2.5, size.x * 5, size.y * 5);
	    this.needsUpdate = true;
	};

	PlotMap.prototype.SetEraseMode = function (x) {
	    this.erasing = x;
	    if (this.erasing) {
	        this.ctxt.fillStyle = "#000000";
	        this.ctxt.strokeStyle = "#000000";
	    } else {
	        this.ctxt.fillStyle = this.zoneColor;
	        this.ctxt.strokeStyle = this.zoneColor;
	    }
	};

	PlotMap.prototype.Reset = function () {
	    this.ctxt.fillStyle = "#000000";
	    this.ctxt.fillRect(0, 0, this.image.width, this.image.height);
	    this.ctxt.fillStyle = this.zoneColor;
	    this.needsUpdate = true;
	};

	PlotMap.prototype.AntiAntiAlias = function () {
	    var data = this.ctxt.getImageData(0, 0, this.image.width, this.image.height);
	    for (var y = 0; y < data.data.length; y++) {
	        data.data[y] = data.data[y] > 128 ? 255 : 0;
	    }
	    this.ctxt.putImageData(data, 0, 0);
	    this.ctxt.needsUpdate = true;
	};

	PlotMap.prototype.GetArray = function () {
	    var data = this.ctxt.getImageData(0, 0, this.image.width, this.image.height);
	    var mask = new Uint8Array(data.data.length / 4);
	    //Packing to array w/ 1-byte per pixel
	    for (var i = 0; i < mask.length; i++) {
	        mask[i] = data.data[i * 4];
	    }
	    var tmp = fromByteArray(mask);
	    return tmp;
	};

	PlotMap.prototype.LoadArray = function (b64MaskString) {
	    // unpack to 1bpp array
	    var mask = toByteArray(b64MaskString);
	    var data = this.ctxt.getImageData(0, 0, this.image.width, this.image.height);
	    
	    for (var i = 0; i < mask.length; i++) {
	        data.data[(i * 4)] = mask[i];
	        data.data[(i * 4) + 1] = mask[i];
	        data.data[(i * 4) + 1] = mask[i];
	    }
	    this.ctxt.putImageData(data, 0, 0);
	    this.needsUpdate = true;
	};

	PlotMap.prototype.ApplyDistrictColors = function (colorsArray) {
	    var data = this.ctxt.getImageData(0, 0, this.image.width, this.image.height);
	    for (var i = 0; i < data.data.length; i += 4) {
	        var districtOffset = data.data[i] * 4;
	        for (var j = 0; j < 4; j++)
	            data.data[i + j] = colorsArray[districtOffset + j];
	    }
	    this.ctxt.putImageData(data, 0, 0);
	    this.ctxt.needsUpdate = true;
	};

	PlotMap.prototype.LoadDistrictMap = function (districtData) {
	    this.metadata = districtData.DistrictMetadata;
	    var colorsArray = new Uint8Array(256 * 4);
	    colorsArray[0] = 255; // 0 is white / "no district"
	    colorsArray[1] = 255;
	    colorsArray[2] = 255;
	    colorsArray[3] = 255;
	    for (var i = 0; i < districtData.DistrictMetadata.length; i++) {
	        var id = districtData.DistrictMetadata[i].ID;
	        colorsArray[id*4  ] = districtData.DistrictMetadata[i].R;
	        colorsArray[id*4+1] = districtData.DistrictMetadata[i].G;
	        colorsArray[id*4+2] = districtData.DistrictMetadata[i].B;
	        colorsArray[id*4+3] = 255;
	    }
	    if( districtData.DistrictMap != undefined )
	        this.LoadArray(districtData.DistrictMap);
	    this.ApplyDistrictColors(colorsArray);
	};

	function DistrictColors(type, mapping, wrapS, wrapT, magFilter, minFilter, anisotropy, format) {
	    var width = 256;
	    var height = 1;

	    var canvas = document.createElement('canvas');
	    canvas.width = width;
	    canvas.height = height;

	    THREE.Texture.call(this, canvas, mapping, wrapS, wrapT, magFilter, minFilter, format, type, anisotropy);

	    this.ctxt = canvas.getContext('2d');
	    this.ctxt.webkitImageSmoothingEnabled = false;

	    this.magFilter = magFilter !== undefined ? magFilter : THREE.NearestFilter;
	    this.minFilter = minFilter !== undefined ? minFilter : THREE.NearestFilter;

	    this.zoneColor = "#FFFFFF";
	    this.ctxt.fillStyle = this.zoneColor;
	    this.ctxt.strokeStyle = this.zoneColor;
	    this.ctxt.miterLimit = 0;
	    this.ctxt.lineJoin = "bevel";
	    this.erasing = false;


	    this.generateMipmaps    = false;
	    this.Reset();
	    this.lastData = this.ctxt.getImageData(0, 0, this.image.width, this.image.height);
	}

	DistrictColors.prototype = Object.create(THREE.Texture.prototype);
	DistrictColors.prototype.constructor = DistrictColors;

	DistrictColors.prototype.SetPixel = function (vec, color) {
	    var id = this.ctxt.createImageData(1, 1);
	    id.data[0] = color.r;
	    id.data[1] = color.g;
	    id.data[2] = color.b;
	    id.data[3] = color.a;
	    this.ctxt.putImageData(id, vec.x, vec.y);
	    this.needsUpdate = true;
	};

	DistrictColors.prototype.SetDistrictColorFromStyle = function (id, style) {
	    this.ctxt.fillStyle = style;
	    this.ctxt.fillRect(id, 0, 1, 1);
	    this.needsUpdate = true;
	};

	DistrictColors.prototype.Reset = function () {
	    this.ctxt.fillStyle = "#FF0000";
	    this.ctxt.fillRect(0, 0, this.image.width, this.image.height);
	    this.ctxt.fillStyle = this.zoneColor;
	    this.needsUpdate = true;
	};

	//import { RenderPass } from '../node_modules/three/src/Three';

	if (!Detector.webgl) Detector.addGetWebGLMessage();

	var MINIMAP = (function () {
	    var my = {
	        initialized: false,
	        zoning: false,
	        zoomSetsDrawSize: false,
	        drawSize: 1,
	        get drawZone() { return my.zoning; },
	        set drawZone(x) { my.setZoning(x); },
	        get eraseMode() { return drawmap.erasing; },
	        set eraseMode(x) { drawmap.SetEraseMode(x); },
	        editView: null
	    };

	    var scenes = [], views = [], t, canvas, renderer;
	    var heatmap = new THREE.TextureLoader().load('inc/images/heatmapinvert.png');
	    var featuremap, drawmap, districtMap, proposedDistrictMap, districtColorLookup;
	    var terrainmap = new THREE.TextureLoader().load('Layers/TerrainLatest.gif');
	    terrainmap.generateMipMaps = false;
	    terrainmap.magFilter = THREE.NearestFilter;
	    terrainmap.minFilter = THREE.NearestFilter;
	    terrainmap.flipY = false;

	    heatmap.generateMipMaps = false;
	    heatmap.magFilter = THREE.LinearFilter;
	    heatmap.minFilter = THREE.LinearFilter;

	    var waterMat;
	    var clock;

	    var worldWidth = 200, worldDepth = 200,
	        worldHalfWidth = worldWidth / 2, worldHalfDepth = worldDepth / 2;

	    var heightMap, heightHistoryLoaded = false, terrainHistoryLoaded = false;

	    my.init = function () {
	        var texture = new THREE.TextureLoader().load('Layers/HeightMapLatest.gif', function (texture) {
	            views = $('.view-map:visible');
	            heightMap = texture;
	            heightMap.generateMipMaps = false;
	            heightMap.flipY = false;
	            heightMap.minFilter = THREE.NearestFilter;

	            if (views.length && my.waterLevel > 0)
	                finishInit(texture);
	        });


	        setupLayers();
	        document.addEventListener('mousemove', onDocumentMouseMove, false);
	    };

	    var geometry, geometry2, data;

	    function finishInit(hmap) {
	        canvas = document.getElementById('c');

	        renderer = new THREE.WebGLRenderer({ canvas: canvas, antialias: true, alpha: true });
	        renderer.setClearColor(0xffffff, 0);
	        renderer.setPixelRatio(window.devicePixelRatio);


	        worldWidth = hmap.image.width;
	        worldDepth = hmap.image.height;
	        worldHalfWidth = worldWidth / 2;
	        worldHalfDepth = worldDepth / 2;
	        data = generateHeight(hmap);


	        setupPlots();
	        drawmap = new PlotMap(worldWidth / 5, worldDepth / 5);
	        if (my.zoneString) {
	            //TODO: enable map viewing
	            drawmap.LoadArray(my.zoneString);
	        }

	        districtColorLookup = new DistrictColors();

	        geometry = GenChunk(40, 40, worldWidth, worldDepth);

	        geometry2 = new THREE.PlaneGeometry(worldWidth * 100, worldDepth * 100, 100, 100);
	        geometry2.rotateX(- Math.PI / 2);

	        my.initialized = true;

	        for (var n = 0; n < views.length; n++) {
	            my.addView(views[n]);
	        }

	        clock = new THREE.Clock(true);
	        animate();
	    }

	    var flatShaderDefines = { GRID: true, FLAT: true, WRAP: true, FMAP: true };
	    var curveShaderDefines = { GRID: true, FMAP: true };
	    var waterFlatShaderDefines = { FLAT: true, WRAP: true };
	    var waterCurveShaderDefines = {};
	    var sharedRenderTarget, sharedFirstView;

	    my.addView = function (view) {
	        view.forceUpdate = false;
	        if (!view.settings) view.settings = {};
	        if (undefined === view.settings.layerSelected) view.settings.layerSelected = "Terrain";
	        if (undefined === view.settings.frameNum) view.settings.frameNum = -1;
	        if (undefined === view.settings.heightFrameNum) view.settings.heightFrameNum = -1;
	        if (undefined === view.settings.terrainFrameNum) view.settings.terrainFrameNum = -1;
	        if (undefined === view.settings.timeStart) view.settings.timeStart = 0.0;
	        if (undefined === view.settings.timeEnd) view.settings.timeEnd = my.serverTime;
	        if (undefined === view.settings.playSpeed) view.settings.playSpeed = 0.5;
	        if (undefined === view.settings.currentTime) view.settings.currentTime = my.serverTime;
	        if (undefined === view.settings.flat) view.settings.flat = false;
	        if (undefined === view.settings.pause) view.settings.pause = true;

	        if (view.OnTimeUpdated)
	            view.OnTimeUpdated(view.settings.currentTime);

	        if (!my.initialized)
	            finishInit(heightMap);

	        var rect = view.getBoundingClientRect();

	        if (view.settings.shareCamera) {
	            if (sharedRenderTarget === undefined) {
	                var pars = { minFilter: THREE.LinearFilter, magFilter: THREE.LinearFilter, format: THREE.RGBAFormat, type: THREE.FloatType };
	                sharedRenderTarget = new THREE.WebGLRenderTarget(rect.width, rect.height, pars);
	                sharedFirstView = view;
	            }
	            view.depthRenderTarget = sharedRenderTarget;
	        } else {
	            var pars = { minFilter: THREE.LinearFilter, magFilter: THREE.LinearFilter, format: THREE.RGBAFormat, type: THREE.FloatType };
	            view.depthRenderTarget = new THREE.WebGLRenderTarget(rect.width, rect.height, pars);
	        }

	        var scene = new THREE.Scene();
	        //scene.fog = new THREE.FogExp2( 0xffffff, 0.00005 );

	        var uniforms2 = {
	            tint: { value: new THREE.Color(1.0, 1.0, 1.0) },
	            waterColor: { value: new THREE.Color(0.0, 0.3, 0.7) },
	            time: { value: 1.0 },
	            worldSize: { value: new THREE.Vector4(worldHalfWidth * 100, worldHalfDepth * 100, worldWidth * 100, worldDepth * 100) },
	            heat: { value: heatmap },
	            features: { value: featuremap },
	            drawmap: { value: drawmap },
	            terrain: { value: terrainmap },
	            map: { value: null },
	            height: { value: heightMap },
	            renderTex: { value: view.depthRenderTarget.texture },
	        };

	        var material = new THREE.RawShaderMaterial({
	            defines: view.settings.flat ? flatShaderDefines : curveShaderDefines,
	            uniforms: uniforms2,
	            vertexShader: curvedVertex,
	            fragmentShader: rawFragmentShader
	        });

	        view.positionLookupShader = new THREE.RawShaderMaterial({
	            defines: view.settings.flat ? { GRID: true, FLAT: true, WRAP: true, LOOKUP: true } : { GRID: true, LOOKUP: true },
	            uniforms: uniforms2,
	            vertexShader: curvedVertex,
	            fragmentShader: rawFragmentShader
	        });

	        var layerShader = new THREE.RawShaderMaterial({
	            defines: view.settings.flat ? flatShaderDefines : curveShaderDefines,
	            uniforms: uniforms2,
	            vertexShader: worldLayerVertex,
	            fragmentShader: worldLayerFragment
	        });
	        layerShader.transparent = true;
	        //layerShader.opacity = 0.5;
	        layerShader.blending = "NormalBlending";

	        view.mainMat = layerShader;
	        if (my.zoneString)
	            view.mainMat.defines.DRAWING = true;

	        if (view.settings.gradient !== undefined) {
	            delete view.mainMat.defines.FMAP;
	            var gradient = new THREE.TextureLoader().load(view.settings.gradient, function(texture) {
	                gradient = texture;
	                gradient.generateMipMaps = false;
	                gradient.magFilter = THREE.LinearFilter;
	                gradient.minFilter = THREE.LinearFilter;
	                view.mainMat.uniforms.heat.value = gradient;
	                view.mainMat.needsUpdate = true;
	            });
	        }
	        if (view.settings.noWater !== undefined && view.settings.noWater)
	            view.mainMat.uniforms.waterColor.value = new THREE.Color(0.0, 0.0, 0.1);

	        my.setLayer(view.settings.layerSelected, view);

	        var renderLayer = new THREE.ShaderPass(layerShader);
	        renderLayer.renderToScreen = true;


	        var mesh = new THREE.Mesh(geometry, view.positionLookupShader); //material);

	        var waterUniforms = {
	            tint: { value: new THREE.Color(0.0, 0.0, 1.0) },
	            time: { value: 1.0 },
	            worldSize: { value: new THREE.Vector4(worldHalfWidth * 100, worldHalfDepth * 100, worldWidth * 100, worldDepth * 100) },
	            heat: { value: null },
	            features: { value: null },
	            terrain: { value: null },
	            map: { value: null }
	        };

	        waterMat = new THREE.ShaderMaterial({
	            defines: view.settings.flat ? waterFlatShaderDefines : waterCurveShaderDefines,
	            uniforms: waterUniforms,
	            vertexShader: vertexShader,
	            fragmentShader: fragmentShader,
	            vertexColors: THREE.VertexColors
	        });
	        waterMat.transparent = true;
	        waterMat.blending = THREE["AdditiveBlending"];
	        view.waterMat = waterMat;

	        view.toggle3D = function () {
	            view.settings.flat = !view.settings.flat;
	            [view.mainMat, view.waterMat, view.positionLookupShader].forEach(function (mat) {
	                if (view.settings.flat) {
	                    mat.defines.FLAT = true;
	                    mat.defines.WRAP = true;
	                } else {
	                    delete mat.defines.FLAT;
	                    delete mat.defines.WRAP;
	                }
	                mat.needsUpdate = true;
	            });
	        };

	        var plane = new THREE.Mesh(geometry2, waterMat);
	        plane.translateY((my.waterLevel * 100.0)); //.1 from getY scaling, 100 from block size, 10 to put it on top

	        //var box = new THREE.BoxBufferGeometry(500, 9000, 500, 10, 1, 10);
	        //var boxMesh = new THREE.Mesh(box, waterMat);
	        //scene.add(boxMesh);
	        //scene.userData.selectBox = boxMesh;

	        scene.add(plane);

	        mesh.layers.set(2); // terrain layer
	        scene.add(mesh);

	        var ambientLight = new THREE.AmbientLight(0xcccccc);
	        scene.add(ambientLight);

	        var directionalLight = new THREE.DirectionalLight(0xffffff, 2);
	        directionalLight.position.set(1, 1, 0.5).normalize();
	        scene.add(directionalLight);

	        scene.userData.view = view;

	        if (view.camera)
	            scene.userData.camera = view.camera;
	        else {
	            var camera = new THREE.PerspectiveCamera(45, rect.width / rect.height, 1000, 400000);
	            camera.position.y = getY(worldHalfWidth, worldHalfDepth) * 100 + 10000;
	            scene.userData.camera = camera;
	            view.camera = camera;
	        }
	        var camera = scene.userData.camera;
	        camera.layers.enable(2);

	        //var renderPass = new THREE.RenderPass( scene, camera );

	        //var copyPass = new THREE.ShaderPass(THREE.CopyShader);
	        //copyPass.renderToScreen = true;

	        view.composer = new THREE.EffectComposer(renderer);
	        //view.composer.addPass(renderPass);
	        //view.composer.addPass(copyPass);
	        view.composer.addPass(renderLayer);

	        var controls = new THREE.OrbitControls(camera, view);
	        controls.enableRotate = false;
	        controls.mouseButtons = { ORBIT: THREE.MOUSE.RIGHT, ZOOM: THREE.MOUSE.MIDDLE, PAN: THREE.MOUSE.LEFT };
	        controls.maxDistance = my.waterLevel * 100 + worldWidth * 95;
	        controls.minDistance = my.waterLevel * 110;
	        view.camera.position.y = (controls.maxDistance + controls.minDistance) / 2;

	        if (view.settings.camPos) {
	            camera.position.copy(view.settings.camPos);
	            controls.target.copy(view.settings.camPos); // prevent orbiting around zero
	            controls.target.y = 0;
	        }

	        scene.userData.controls = controls;
	        view.settings.camPos = camera.position;

	        scene.userData.lastCamPosition = new THREE.Vector3(0, 0, 0);
	        scene.userData.lastRenderPosition = new THREE.Vector3(0, -100000, 0);
	        scene.userData.frameNum = 0;

	        scenes.push(scene);

	        if (my.editView == null) my.editView = view;
	    };


	    my.lastMouseScene = null;

	    var depthMaterial, effectComposer, depthRenderTarget, sharedRenderTarget;
	    //     var ssaoPass;
	    //     var postprocessing = { enabled: true, renderMode: 0 }; // renderMode: 0('framebuffer'), 1('onlyAO')


	    function initPostprocessing(scene, camera, rect) {

	        //                 // Setup render pass
	        //                 //var renderPass = new THREE.RenderPass( scene, camera );

	        //                 // Setup depth pass
	        //                 //depthMaterial = new THREE.MeshDepthMaterial();

	        //         var uniforms2 = {
	        //             tint:        { value: new THREE.Color(1.0,1.0,1.0) },
	        //             time:       { value: 1.0 },
	        //             worldSize:    { value: new THREE.Vector4(worldHalfWidth*100,worldHalfDepth*100,worldWidth*100, worldDepth*100) },
	        //             heat:       { value: heatmap },
	        //             features:       { value: featuremap },
	        //             map: { value: null },
	        //             height: { value: heightMap }
	        //         };
	        //                 var depthMaterial = new THREE.RawShaderMaterial({
	        //                     defines: scene.userData.view.settings.flat ? flatShaderDefines : curveShaderDefines,
	        //             uniforms:         uniforms2,
	        //             vertexShader: rawVertexShader,
	        //             fragmentShader: customShaders["CustomDepthFrag"],
	        //             //vertexColors:     THREE.VertexColors

	        //         });
	        //                 depthMaterial.depthPacking = THREE.RGBADepthPacking;
	        //                 depthMaterial.blending = THREE.NoBlending;

	        var pars = { minFilter: THREE.LinearFilter, magFilter: THREE.LinearFilter, format: THREE.RGBAFormat, type: THREE.FloatType };
	        depthRenderTarget = new THREE.WebGLRenderTarget(rect.width, rect.height, pars);

	        // Setup SSAO pass
	        //                 ssaoPass = new THREE.ShaderPass( THREE.SSAOShader );
	        //                 ssaoPass.renderToScreen = true;
	        //                 //ssaoPass.uniforms[ "tDiffuse" ].value will be set by ShaderPass
	        //                 ssaoPass.uniforms[ "tDepth" ].value = depthRenderTarget.texture;
	        //                 ssaoPass.uniforms[ 'size' ].value.set( rect.width, rect.height );
	        //                 ssaoPass.uniforms[ 'cameraNear' ].value = camera.near;
	        //                 ssaoPass.uniforms[ 'cameraFar' ].value = camera.far;
	        //                 ssaoPass.uniforms[ 'onlyAO' ].value = ( postprocessing.renderMode == 1 );
	        //                 ssaoPass.uniforms[ 'aoClamp' ].value = 0.7;
	        //                 ssaoPass.uniforms[ 'lumInfluence' ].value = 0.0;

	        // Add pass to effect composer
	        //                 effectComposer = new THREE.EffectComposer( renderer );
	        //                 //effectComposer.addPass( renderPass );
	        //                 effectComposer.addPass( ssaoPass );

	    }



	    my.removeView = function (view) {
	        if (scenes.length == 1 && scenes[0].userData.view == view)
	            scenes = [];
	        else {
	            for (var i = 0; i < scenes.length; i++) {
	                if (scenes[i].userData.view == view || scenes[i].userData.view.id == view) {
	                    scenes[i].userData.controls.dispose();
	                    scenes[i].userData.camera = null;
	                    delete scenes[i];
	                    scenes.splice(i, 1);
	                    break;
	                }
	            }
	        }
	    };

	    my.removeDeadViews = function () {
	        for (var i = 0; i < scenes.length; i++) {
	            if (!jQuery.contains(document, scenes[i].userData.view)) {
	                scenes[i].userData.controls.dispose();
	                scenes[i].userData.camera = null;
	                delete scenes[i];
	                scenes.splice(i, 1);
	                i--;
	            }
	        }
	    };

	    function generateHeight(hmap) {
	        var canvas = document.createElement('canvas');
	        canvas.width = hmap.image.width;
	        canvas.height = hmap.image.height;
	        var context = canvas.getContext('2d');

	        var size = hmap.image.width * hmap.image.height, data = new Float32Array(size);

	        context.drawImage(hmap.image, 0, 0);
	        var imgd = context.getImageData(0, 0, hmap.image.width, hmap.image.height);
	        var pix = imgd.data;

	        data = [];
	        var j = 0;
	        for (var i = 0, n = pix.length; i < n; i += 4) {
	            data[j++] = pix[i];
	        }

	        return data;
	    }


	    function getY(x, z) {
	        x = (x + worldWidth) % worldWidth;
	        z = (z + worldWidth) % worldWidth;
	        return (data[x + z * worldWidth] * 0.1) | 0;
	    }

	    // 4 tiles in game = 1 tile here
	    var getWorldPos = function (position) {
	        return new THREE.Vector2(
	            Math.round((worldHalfWidth + position.x / 100)),
	            Math.round((worldHalfWidth - position.z / 100))
	        );
	    };

	    function updateSize() {

	        var width = canvas.clientWidth;
	        var height = canvas.clientHeight;

	        if (canvas.width !== width || canvas.height != height) {

	            renderer.setSize(width, height, false);

	        }

	    }

	    function animate() {

	        render();
	        requestAnimationFrame(animate);

	    }

	    var mouseX, mouseY;
	    function onDocumentMouseMove(event) {
	        mouseX = event.clientX;
	        mouseY = event.clientY;
	        //if(scenes.length < 1) return;
	        //var rect = scenes[0].userData.view.getBoundingClientRect();
	        //scenes[0].userData.view.OnInfoUpdated((mouseX - rect.left) + " , " + (mouseY - rect.top));
	    }

	    function GetFrameNum(name, frameNum, view) {
	        if (GIFMANAGER.gifTimes[name] != undefined && GIFMANAGER.gifTimes[name].length > 0) {
	            var currentTime = view.settings.currentTime;
	            var playSpeed = view.settings.playSpeed;
	            var myTimes = GIFMANAGER.gifTimes[name];
	            var maxFrame = myTimes.length;

	            if (frameNum < 0)
	                frameNum = maxFrame - 1;

	            while (frameNum < maxFrame - 1 && currentTime > myTimes[frameNum])
	                frameNum++;
	            while (frameNum > 0 && currentTime < myTimes[frameNum])
	                frameNum--;
	        }
	        return frameNum;
	    }

	    var pos, mouseHitWorld = false;
	    function render() {

	        updateSize();

	        renderer.setClearColor(0xffffff, 0);
	        renderer.setScissorTest(false);
	        renderer.clear();

	        var myRect = canvas.getBoundingClientRect();

	        var deltaTime = clock.getDelta();
	        if (deltaTime > 5) deltaTime = 1.0; // probably debugging

	        scenes.forEach(function (scene) {

	            var view = scene.userData.view;
	            var s = view.settings;

	            if (s.shareCamera && sharedFirstView != view) {
	                s.currentTime = sharedFirstView.settings.currentTime;
	                s.pause = sharedFirstView.settings.pause;
	                s.playSpeed = sharedFirstView.settings.playSpeed;
	            }

	            // wait until unpaused to load history data
	            if (!s.pause && GIFMANAGER.gifStorage["HeightMap"] === undefined) {
	                GIFMANAGER.giftextures("HeightMap", function(x) { heightHistoryLoaded = true;
	                    GIFMANAGER.giftextures("Terrain", function(x) { terrainHistoryLoaded = true; }, THREE.NearestFilter);
	                }, THREE.NearestFilter);
	            }

	            var rect = view.getBoundingClientRect();
	            var cam = scene.userData.camera;
	            // check if it's offscreen. If so skip it
	            if (rect.bottom < 0 || rect.top > renderer.domElement.clientHeight ||
	                rect.right < 0 || rect.left > renderer.domElement.clientWidth) {
	                return;  // it's off screen
	            }
	            // set the viewport
	            var width = rect.right - rect.left;
	            var height = rect.bottom - rect.top;
	            var left = rect.left - myRect.left;
	            var top = rect.top;
	            renderer.setViewport(left, top, width, height);
	            renderer.setScissor(left, top, width, height);
	            if (view.lastWidth != width || view.lastHeight != height) {
	                cam.aspect = width / height;
	                cam.updateProjectionMatrix();
	                view.forceUpdate = true;
	            }
	            view.lastWidth = width;
	            view.lastHeight = height;

	            if (isNaN(s.currentTime))
	                s.currentTime = 0.0;

	            var materialChanged = false, heightChanged = false;
	            var lastTime = s.currentTime, prevFrameNum = s.frameNum, prevHeightFrameNum = s.heightFrameNum, prevTerrainFrameNum = s.terrainFrameNum;

	            if (!s.pause && s.playSpeed != 0.0) {
	                s.currentTime += ((view.loading ? 0.2 : s.playSpeed) * 86400) * deltaTime;
	            }

	            if (s.currentTime > s.timeEnd) {
	                s.currentTime = s.timeStart;
	            } else if (s.currentTime < s.timeStart) {
	                s.currentTime = s.timeEnd;
	            }

	            if (lastTime != s.currentTime || view.forceUpdate) {
	                if (view.timeText)
	                    view.timeText.innerText = s.currentTime / 3600.0;
	                if (view.OnTimeUpdated)
	                    view.OnTimeUpdated(s.currentTime);

	                s.frameNum = GetFrameNum(s.layerSelected, s.frameNum, view);
	                if (heightHistoryLoaded)
	                    s.heightFrameNum = GetFrameNum("HeightMap", s.heightFrameNum, view);
	                if (terrainHistoryLoaded)
	                    s.terrainFrameNum = GetFrameNum("Terrain", s.terrainFrameNum, view);

	                if (s.currentTime != my.serverTime && GIFMANAGER.gifStorage[s.layerSelected] == undefined) {
	                    my.setLayer(s.layerSelected, view);
	                }

	                if (GIFMANAGER.gifStorage[s.layerSelected] && prevFrameNum != s.frameNum) {
	                    view.mainMat.uniforms.map.value = GIFMANAGER.giftextures(s.layerSelected)[s.frameNum];
	                    materialChanged = true;
	                }

	                if (heightHistoryLoaded && prevHeightFrameNum != s.heightFrameNum) {
	                    view.mainMat.uniforms.height.value = GIFMANAGER.giftextures("HeightMap")[s.heightFrameNum];
	                    materialChanged = true;
	                    heightChanged = true;
	                }

	                if (s.layerSelected != "Terrain" && terrainHistoryLoaded && prevTerrainFrameNum != s.terrainFrameNum) {
	                    view.mainMat.uniforms.terrain.value = GIFMANAGER.giftextures("Terrain")[s.terrainFrameNum];
	                    materialChanged = true;
	                }

	                if (materialChanged)
	                    view.mainMat.needsUpdate = true;
	            }

	            var mouseViewX = mouseX - rect.left;
	            var mouseViewY = rect.height - (mouseY - rect.top);

	            pos = getWorldPos(s.camPos);

	            if ((mouseViewX > 0 && mouseViewY > 0 && mouseViewX < rect.width && mouseViewY < rect.height) || view.firstRender === undefined || heightChanged || view.forceUpdate) {
	                view.forceUpdate = false;
	                //TODO: switch to UInt8 and pack/unpack 16-bit ints
	                var read = new Float32Array(4);
	                renderer.readRenderTargetPixels(view.depthRenderTarget, mouseViewX, mouseViewY, 1, 1, read);
	                mouseHitWorld = read[3] >= 0.7; // AO is 0.7-1.0
	                if (mouseHitWorld) {
	                    pos.x = Math.floor(read[0] * worldWidth);
	                    pos.y = worldDepth - Math.floor(read[1] * worldDepth);
	                }
	                if (cam.position.distanceTo(scene.userData.lastRenderPosition) > 0.5) { //should remain valid until camera moves
	                    //scene.overrideMaterial = view.positionLookupShader;
	                    //cam.layers.disable(0); // disable default stuff, only show terrain
	                    renderer.setClearColor(0x000000, 0);
	                    renderer.clearTarget(view.depthRenderTarget);
	                    renderer.render(scene, cam, view.depthRenderTarget, true);
	                    if (!debugKeyX) {
	                        scene.overrideMaterial = null;
	                        //cam.layers.enable(0);
	                    }
	                    scene.userData.lastRenderPosition.copy(cam.position);
	                    if (view.settings.shareCamera) {
	                        scenes.forEach (function (s) {
	                            var v = s.userData.view;
	                            if (s.userData.view.settings.shareCamera) {
	                                s.userData.lastRenderPosition.copy(cam.position);
	                                s.userData.camera.position.copy(cam.position);
	                            }
	                        });
	                    }
	                }
	                my.lastMouseScene = scene;
	                view.firstRender = false;
	            }

	            if (view.OnPositionUpdated) {
	                if (pos != view.worldPos) {
	                    view.OnPositionUpdated(pos);
	                    view.worldPos = pos;
	                    if (view.OnInfoUpdated) {
	                        var lookup = getPlotNum(pos.x, pos.y);
	                        var plotName = "\n";
	                        if (plotLookup[lookup] !== undefined)
	                            plotName = plotNameLookup[plotLookup[lookup]];
	                        if (view.plotName != plotName) {
	                            view.OnInfoUpdated(plotName);
	                            view.plotName = plotName;
	                        }
	                    }
	                }
	            }

	            //if (debugKeyZ) {
	                //cam.layers.disable(0);
	            //} else {
	                //cam.layers.enable(0);
	            //}

	            renderer.setClearColor(0x38562d, 1); // Map Background Color
	            renderer.setScissorTest(true);

	            //if (debugKeyX) {
	                //renderer.render(scene, cam);
	            //} else {
	                view.composer.passes[0].uniforms.renderTex.value = view.depthRenderTarget.texture;
	                view.composer.setSize(width, height);
	                view.composer.render(0.1);
	            //}

	            //if(++frameNum == GIFMANAGER.giftextures.length) frameNum = 0;
	            scene.userData.lastCamPosition.copy(cam.position);
	            scene.userData.controls.update();


	            // shader magically wraps 1 tile around map, need to keep camera within bounds
	            if (cam.position.x > worldHalfWidth * 100)
	                cam.position.x -= worldWidth * 100;
	            else if (cam.position.x < -worldHalfWidth * 100)
	                cam.position.x += worldWidth * 100;
	            if (cam.position.z > worldHalfDepth * 100)
	                cam.position.z -= worldDepth * 100;
	            else if (cam.position.z < -worldHalfDepth * 100)
	                cam.position.z += worldDepth * 100;

	            //             ssaoPass.uniforms[ 'cameraNear' ].value = cam.near;
	            //             ssaoPass.uniforms[ 'cameraFar' ].value = cam.far;
	            scene.userData.controls.target.copy(cam.position); // fix target to prevent insanity
	            scene.userData.controls.target.y = 0;


	            if (view.cameraText) {
	                view.cameraText.innerText = JSON.stringify(s);
	            }
	            if (scene.userData.view.mapdata) {
	                view.mapdata.value = JSON.stringify(s);
	            }

	        });

	        t++;

	    }


	    var urlGameServer = parent.APPIQUERYURL;
	    my.layerNames = ["Camas", "Elk"];
	    var layerSelected = "Fireweed";
	    my.waterLevel = -1;
	    my.serverTime = 10000.0;
	    function setupLayers() {
	        $.ajax({
	            url: "/api/v1/map/map.json",
	            data: {},
	            success: function (data, status, xhr) {
	                my.layerNames = data.LayerNames;
	                my.layerNames.unshift("Districts");
	                my.layerNames.unshift("Terrain");
	                my.fillLayerSelects();
	                my.waterLevel = data.WaterLevel;
	                my.serverTime = data.WorldTime;
	                plots = data.Plots;
	                if (heightMap !== undefined)
	                    finishInit(heightMap);
	            }
	        });
	    }
	    var plots;
	    var plotLookup = {};
	    var plotNameLookup = [];

	    var featureContext;
	    var debugKeyX = false;
	    var debugKeyZ = false;

	    my.setZoning = function (enabled) {
	        //TODO: setup/disable drawing
	        my.zoning = enabled;
	        if (my.zoning) {
	            my.editView.addEventListener('mousedown', onMouseDown, false);
	            my.editView.mainMat.defines.DRAWING = true;
	            my.editView.mainMat.uniforms.heat.value = districtColorLookup;
	            my.editView.mainMat.needsUpdate = true;
	            //document.getElementById('map-zonemap').appendChild(drawmap.image);
	        } else {
	            drawmap.Reset();
	            delete my.editView.mainMat.defines.DRAWING;
	            my.editView.mainMat.needsUpdate = true;
	            my.editView.mainMat.uniforms.heat.value = heatmap;
	            my.editView.removeEventListener('mousedown', onMouseDown, false);
	            //document.getElementById('map-zonemap').removeChild(drawmap.image);
	        }
	    };

	    var mouseState = 0;
	    var dragStart, dragPos;
	    function onMouseDown(event) {
	        if (event.button === THREE.MOUSE.RIGHT && mouseHitWorld) {
	            mouseState = 1;
	            dragStart = new THREE.Vector2(Math.floor(pos.x / 5), Math.floor(pos.y / 5));
	            if (my.zoomSetsDrawSize)
	                my.drawSize = Math.floor(10 * my.editView.camera.position.y / (worldWidth * 90));
	            drawmap.SetPixel(dragStart, my.drawSize);
	            document.addEventListener('mousemove', onMouseMove, false);
	            document.addEventListener('mouseup', onMouseUp, false);
	        }
	    }

	    function onMouseMove(event) {
	        if (mouseState === 1 && mouseHitWorld) {
	            dragPos = new THREE.Vector2(Math.floor(pos.x / 5), Math.floor(pos.y / 5));
	            if (dragStart != dragPos) {
	                drawmap.DrawLine(dragStart, dragPos, my.drawSize);
	                dragStart = dragPos;
	            }
	        }
	    }

	    function onMouseUp(event) {
	        if (mouseState !== 0) {
	            mouseState = 0;
	            document.removeEventListener('mousemove', onMouseMove, false);
	            document.removeEventListener('mouseup', onMouseUp, false);
	        }
	    }

	    window.addEventListener('keydown', onKeyDown, false);
	    function onKeyDown(event) {
	    if (event.keyCode == 88) { // x
	    debugKeyX = !debugKeyX;
	    }
	    if (event.keyCode == 90) { // x
	    debugKeyZ = !debugKeyZ;
	    }
	    }

	    my.GetZone = function () {
	        if (my.drawZone)
	            return drawmap.GetArray();
	        else
	            return "";
	    };

	    my.SetZone = function (b64string) {
	        if (this.initialized)
	            drawmap.LoadArray(b64string);
	        else
	            my.zoneString = b64string;
	    };

	    my.LoadProposedDistricts = function (districtData) {
	        if (proposedDistrictMap === undefined)
	            proposedDistrictMap = new PlotMap(worldWidth / 5, worldDepth / 5);
	        if (proposedDistrictMap.metadata !== undefined)
	            return;
	        proposedDistrictMap.LoadDistrictMap(districtData);
	        var select = $('.map-layer-select');
	        var option = $('<option></option>').attr("value", "ProposedDistricts").text("ProposedDistricts");
	        $(select).prepend(option);
	    };

	    function draw(v) {
	        drawmap.SetPixel(v, my.drawSize);
	        //featureContext.fillStyle = "#CC5555";
	        //featureContext.fillRect(5 * Math.floor(v.x/ 5), 5 * Math.floor(v.y / 5), 5, 5);
	        //featuremap.needsUpdate = true;
	    }

	    my.paintDistrict = function paintDistrict(id) {
	        drawmap.SetColor("rgb(" + id + "," + id + "," + id +")");
	    };

	    my.setDistrictColor = function setDistrictColor(id, color) {
	        districtColorLookup.SetDistrictColorFromStyle(id, color);
	    };

	    function setupPlots() {
	        var tcanvas = document.createElement('canvas');
	        tcanvas.width = worldWidth;
	        tcanvas.height = worldDepth;
	        var tctx = tcanvas.getContext('2d');
	        featureContext = tctx;
	        //tctx.drawImage( tmpCanvas, 0, 0, tmpCanvas.width, tmpCanvas.height );
	        tctx.fillStyle = "#FFFFFF";
	        tctx.fillRect(0, 0, tcanvas.width, tcanvas.height);
	        tctx.fillStyle = "#BBBBBB";
	        tctx.strokeStyle = "#555555";
	        tctx.lineWidth = 2;
	        //         tctx.shadowColor = "#333333";
	        //         tctx.shadowBlur = 1;

	        var i = 0;
	        for (var prop in plots) {
	            plotNameLookup[i] = prop;
	            for (var p in plots[prop]) {
	                var v = plots[prop][p];
	                tctx.fillRect(v.x, v.y, 5, 5);
	                //tctx.strokeRect(v.x,v.y,5,5);
	                plotLookup[getPlotNum(v.x, v.y)] = i;
	            }
	            i++;
	        }

	        featuremap = new THREE.Texture(tcanvas);
	        featuremap.minFilter = THREE.NearestFilter;
	        featuremap.magFilter = THREE.NearestFilter;
	        //texture.wrapS = THREE.RepeatWrapping;
	        //texture.wrapT = THREE.RepeatWrapping;
	        featuremap.needsUpdate = true;
	        featuremap.flipY = true;
	    }

	    function getPlotNum(x, y) {
	        return Math.floor(x / 5) + ((Math.floor(y / 5) * worldWidth) / 5);
	    }


	    my.editViewNum = 0; //think this should usually be the last?

	    my.setLayer = function (layerName, viewRef = undefined) {
	        var view = viewRef ? viewRef : scenes[scenes.length - 1].userData.view;
	        view.settings.layerSelected = layerName;
	        view.forceUpdate = true;
	        if (view.mainMat) {
	            if (layerName == "Terrain") {
	                view.mainMat.uniforms.map.value = terrainmap;
	                delete view.mainMat.defines.HEATMAP;
	            } else if (layerName == "Districts") {
	                if (districtMap === undefined) {
	                    districtMap = new PlotMap(worldWidth / 5, worldDepth / 5);
	                    $.ajax({
	                        url: "/api/v1/laws/districts",
	                        data: {},
	                        success: function (data, status, xhr) {
	                            districtMap.LoadDistrictMap(data);
	                        }
	                    });
	                }
	                view.mainMat.uniforms.map.value = districtMap;
	                delete view.mainMat.defines.HEATMAP;
	            } else if (layerName == "ProposedDistricts") {
	                if (proposedDistrictMap === undefined)
	                    proposedDistrictMap = new PlotMap(worldWidth / 5, worldDepth / 5);
	                view.mainMat.uniforms.map.value = proposedDistrictMap;
	                delete view.mainMat.defines.HEATMAP;
	            } else {
	                view.mainMat.defines.HEATMAP = true;
	                if (GIFMANAGER.gifStorage[layerName] === undefined) {

	                    view.settings.frameNum = -1;
	                    view.loading = true;
	                    if (view.OnInfoUpdated)
	                        view.OnInfoUpdated("Loading Layer...");

	                    if (view.settings.currentTime == my.serverTime && view.settings.pause) {
	                        new THREE.TextureLoader().load('Layers/' + layerName + 'Latest.gif', function (texture) {
	                            texture.generateMipMaps = false;
	                            texture.flipY = false;
	                            texture.magFilter = THREE.LinearFilter;
	                            texture.minFilter = THREE.LinearFilter;
	                            view.mainMat.uniforms.map.value = texture;
	                            view.loading = false;
	                        });
	                    } else {
	                        view.settings.currentTime = 0;
	                        GIFMANAGER.giftextures(layerName, function (name) {
	                            //scenes[my.editViewNum].userData.view.settings.layerSelected = name;
	                            if (name == view.settings.layerSelected) {
	                                view.loading = false;
	                                if (view.OnInfoUpdated)
	                                    view.OnInfoUpdated("");
	                            }
	                        }); //start fetching the texture
	                    }
	                }
	            }
	            view.mainMat.needsUpdate = true;
	        }
	    };

	    my.fillLayerSelects = function (sell = false) {
	        var layerSelected = '';
	        if (sell) {
	            var sel = $(sell + ' .map-layer-select');
	        } else {
	            var sel = $('.map-layer-select');
	        }

	        $.each(sel, function (key, select) {
	            $.each(my.layerNames, function (key, value) {
	                var option = $('<option></option>').attr("value", value).text(value);
	                if ($(select).attr('data-val') == value) {
	                    option.attr("selected", true);
	                }
	                $(select).append(option);
	            });
	            // select.value = guiParams.layerSelected;
	        });
	    };

	    function screenToWorldPos(view, viewX, viewY) {
	        var read = new Float32Array(4);
	        renderer.readRenderTargetPixels(view.depthRenderTarget, viewX, viewY, 1, 1, read);
	        var hitWorld = read[3] >= 0.7; // AO is 0.7-1.0
	        if (hitWorld)
	            return new THREE.Vector2(Math.floor(read[0] * worldWidth), worldDepth - Math.floor(read[1] * worldDepth));
	        return null;
	    }

	    my.getWorldArea = function(view, borderSize) {
	        var result = { 
	            min: screenToWorldPos(view, borderSize, borderSize),
	            max: screenToWorldPos(view, view.lastWidth - borderSize, view.lastHeight - borderSize) 
	        };
	        if (result.min == null || result.max == null) {
	            result.min = new THREE.Vector2(0, 0);
	            result.max = new THREE.Vector2(worldWidth - 1, worldDepth - 1);
	        }
	        return result;
	    };

	    return my;
	}());

	window.onload = MINIMAP.init;

	exports.MINIMAP = MINIMAP;

	Object.defineProperty(exports, '__esModule', { value: true });

})));
//# sourceMappingURL=ecomap.js.map
