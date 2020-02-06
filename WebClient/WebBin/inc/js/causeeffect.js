;(function(){
	var CauseEffect = function(){
		this.data = []
		this.timeRange = undefined
		this.simulation = undefined
		this.play = undefined
		this.pause = true
		this.ctrl = false
		this.animationTime = 5	// sec
		this.graph = {}
		this.width = 0
		this.height = 0
		this.maps = []
		this.graphs = []
		this.focusedcat = undefined
		this.focused = undefined
		this.strokeWidth = 1
		this.correctWidth = {}
		this.tooltipHide = true;
		this.timeDisabled = true;
		this.attrIndex = 1;
		this.updateMapsInfo_timeout = undefined;
		this.nodeSize = {
			max: 500,
			min: 210,
			size: 210
		}
	}

	/*------------	Loading Data Start ------------*/
		CauseEffect.prototype.loadGraphs = function(){
			var _this = this,
				data = new Object();

			Promise.resolve({
				data : {
					url : APPIQUERYURL + "/datasets/list",
					data : {},
					type : 'GET'
				},
				this : this
			}).then(this.PromiseAjax)
			.then(function(obj){
				_this.graphs = obj.data;
			})
			.catch(function(err){
				variables.showError(err);
			});
		}
		CauseEffect.prototype.loadNodes = function(obj){
			return new Promise(function(resolve,reject){
				obj.data = {
					url : APPIQUERYURL + "/api/v1/worldlayers/layers/"+obj.api.layer+"?minX="+obj.api.x1+"&minY="+obj.api.y1+"&maxX="+obj.api.x2+"&maxY="+obj.api.y2,
					data : {},
					type : 'GET'
				};
				resolve(obj);
			});
		}
		CauseEffect.prototype.loadConnections = function(obj){
			return new Promise(function(resolve,reject){
				obj.this.graph.nodes = obj.data;
				obj.data = {
					url : APPIQUERYURL + "/api/v1/worldlayers/relationships/"+obj.api.layer+"?minX="+obj.api.x1+"&minY="+obj.api.y1+"&maxX="+obj.api.x2+"&maxY="+obj.api.y2,
					data : {},
					type : 'GET'
				};
				resolve(obj);
			});
		}
	 /*------------	Loading Data End ------------*/

	/*------------	Getting Data, Create Graph and Show ------------*/
		CauseEffect.prototype.drawGraph = function(layer, area){
			area = area || false;
			var _this = this;
			if( area == false || true ) {
				area = {
					min: {x:0,y:0},
					max: {x:20,y:20}
				}
			}

			_this.loadGraphs();

			Promise.resolve({
				data : {},
				api: {
					layer: layer,
					x1: area.min.x,
					y1: area.min.y,
					x2: area.max.x,
					y2: area.max.y
				},
				this : _this
			})
			.then(_this.loadNodes).then(_this.PromiseAjax)
			.then(_this.loadConnections).then(_this.PromiseAjax)
			.then(_this.resolveData)
			.then(_this.initSVG)
			.catch(function(err){
				// $("#layerselect").prop("disabled", false).focus();
				$("#causeeffectcontainer").html('<h1>This Layer not found.</h1><h1>View the global effects that shape this world, and how they influence each other.</h1><h1>Use the time bar at the bottom to scroll through history.</h1>');
				variables.showError(err);
			});
		}

	/*------------	Constructing Data ------------*/
		CauseEffect.prototype.resolveData = function(obj){
			return new Promise(function(resolve,reject){
				obj.this.graph.conns = obj.data;

				// helping functions
				// add node's level to levels array, if (it already exists and old_level != new_level) => level = 1
				add = function(id,s){if(id!=_this.focused) if(levels[id] == undefined)levels[id] = s; else if(levels[id]!=s)levels[id]=1}
				// find node's id with LayerName
				find = function(id){for(i in obj.nodes)if(obj.nodes[i].LayerName==id)return obj.nodes[i].id;}
				// convert "R 1, G 0, B 0, A 1" => "rgba(1, 0, 0, 1)"
				textToColor = function(t){
					var rgba = 'rgba('+t['R']+','+t['G']+','+t['B']+','+t['A']+')';
					return rgba;
				}
				var _this = obj.this;

				var focused = _this.focused;
				var levels = {};
				levels[focused] = 1;

				obj.this.graph.conns = obj.data;
				obj.nodes = [];
				obj.icons = [];
				obj.connections = [];

				var nodes = obj.this.graph.nodes;
				var conns = obj.this.graph.conns;

				var count = 0;
				for( i in nodes ) {
					var node = nodes[i];
					if( nodes[i].LayerName != focused ) {
						// if( nodes.length <= 15 ) {
							_this.appendMap(node);
						// } else {
						// 	_this.appendMap(node, "image");
						// }
					}
					node.id = ++count;
					node.class = "node";
					obj.nodes.push(node);
				}


				for( i in conns ) {
					var conn = conns[i];
					conn.id = ++count;
					conn.class = "conn";
					obj.nodes.push(conn);

					var line = {};
					if( conn.Output != undefined ){
						line.source = conn.id;
						line.connection = conn.Name;
						line.name = conn.Name.replace(/[()]/g, "").replace(conn.Output + ", ", "").replace(", " + conn.Output, "");
						for(var k in conn.HiddenInputs) {
							line.name = line.name.replace(conn.HiddenInputs[k] + ", ", "").replace(", " + conn.HiddenInputs[k], "");
						}
						add(conn.Output,2);
						line.target = find(conn.Output);
						line.Tooltip = conn.Tooltip;
						line.color = textToColor(conn.Inputs[0].Color);
						line.LayerName = (conn.Name + conn.Output).replace(/[ -<>,()]/g, "");
						line.arrow = 1;
						obj.connections.push(line);
					}

					if( conn.Inputs != undefined ){
						for(j in conn.Inputs) {

							line = {};
							line.target = conn.id;
							line.connection = conn.Name;
							line.name = conn.Name.replace(/[()]/g, "").replace(conn.Output + ", ", "").replace(", " + conn.Output, "");
							for(var k in conn.HiddenInputs) {
								line.name = line.name.replace(conn.HiddenInputs[k] + ", ", "").replace(", " + conn.HiddenInputs[k], "");
							}
							line.source = find(conn.Inputs[j].Name);
							line.Tooltip = conn.Tooltip;
							line.color = textToColor(conn.Inputs[j].Color);
							line.LayerName = (conn.Inputs[j].Name + conn.Name).replace(/[ -<>,()]/g, "");
							line.arrow = conn.Inputs[j].DoubleEnded?2:0;
							line.icon = parseInt(Math.random()*17+1);
							obj.connections.push(line);
							if( conn.Output == undefined || conn.Inputs[j].Name != conn.Output ) {
								add(conn.Inputs[j].Name,0);
								obj.icons.push(line);
							}

							if( conn.Inputs[j].Name == focused && conn.Inputs[j].Name != conn.Output ) {
								for(l in conn.Inputs) {
									levels[conn.Inputs[l].Name] = 1;
								}
								if( conn.Output != undefined && conn.Output != focused  ) {
									levels[conn.Output] = 2;
								}
							}

						}
					}
				}

				var verCount = 0;
				var ver = 0;
				var horCount = [0,0,0];
				var hor = [0,0,0];
				var interval = {
					x: [0,0,0],
					y: [0,0,0]
				};
				for( i in levels ) {
					horCount[ levels[i] ]++;
				}

				if( horCount[0] ) verCount++;
				if( horCount[1] ) verCount++;
				if( horCount[2] ) verCount++;

				switch( verCount ) {
					case 3: ver = 210; break;
					case 2: ver = 330; break;
					case 1:case 0:default: ver = 500; break;
				}

				var maxHorCount = Math.max( horCount[0], horCount[1], horCount[2] );
				hor = 4000 / (3*maxHorCount);

				_this.nodeSize.size = Math.max(_this.nodeSize.min, Math.min(_this.nodeSize.max, ver, hor))

				var maxWidth = _this.nodeSize.size * ( maxHorCount + 1 ) * 5 / 4;

				var width = Math.max(maxWidth, 2000);
				var height = (maxHorCount>7&&verCount==3)?2000:verCount==1?700:1200;

				switch( verCount ) {
					case 3: interval.y = [0,(height-65-_this.nodeSize.size)/2,height-95-_this.nodeSize.size]; break;
					case 2: interval.y = [0, (horCount[0]?height-95-_this.nodeSize.size:0), height-95-_this.nodeSize.size]; break;
					case 1:case 0:default: interval.y = [30,30,30]; break;
				}

				pos = [0, 0, 0];
				steps = [0, 0, 0];

				for( i in levels ) {
					steps[levels[i]]++;
				}


				for(i in steps){
					steps[i] = parseInt((width - _this.nodeSize.size) / steps[i]);
				}

				for(var i in obj.nodes) {
					if( obj.nodes[i].class == "conn" ) continue;
					var j = levels[obj.nodes[i].LayerName];
					obj.nodes[i].fx = (pos[ j ]++ + 0.5) * steps[ j ];
					obj.nodes[i].fy = interval.y[j];
				}
				$("#causeeffectcontainer").css("min-height", "715px")
				$("#causeeffectcontainer svg,#causeeffectcontainer h1").remove();

				$("#causeeffectcontainer").append('<svg xmlns="http://www.w3.org/2000/svg" viewBox="0 0 '+width+' '+height+'" id="causeeffectsvg"></svg><svg xmlns="http://www.w3.org/2000/svg" viewBox="0 0 '+width+' '+height+'" id="causeeffectsvg1"></svg>');
				var realHeight = parseInt($("#causeeffectcontainer").css("height")) - 100;
				var realWidth = realHeight * width / 1000;

				$("#causeeffectsvg").css("width", realWidth+"px");
				$("#causeeffectsvg1").css("width", realWidth+"px");
				$("#causeeffectcontainer").css("width", realWidth+"px");

				$("#causeeffectcontainer").css("min-height", parseInt($("#causeeffectsvg").css("height"))+70+"px")

				$(".mapInfo").show();
				_this.updateMapsInfo();

				_this.timeDisabled = false;
				$(".map-progress-bar-wrapper").show();
				$("#slider").slider({disabled: false});

				resolve(obj);
			});
		}

	/*------------	Draw SVG(Graph) ------------*/
		CauseEffect.prototype.initSVG = function(obj){
			var _this = obj.this;
			var svg = d3.select("svg#causeeffectsvg");
			var svg1 = d3.select("svg#causeeffectsvg1");
			var focused = _this.focused;

			_this.width = svg.attr("viewBox").split(" ")[2] - svg.attr("viewBox").split(" ")[0];
			_this.height = svg.attr("viewBox").split(" ")[3] - svg.attr("viewBox").split(" ")[1];
			var realWidth = parseInt($("#causeeffectsvg").css("width"));
			var realHeight = parseInt($("#causeeffectsvg").css("height"));
			var kx = _this.width/realWidth;
			var ky = _this.height/realHeight;

			_this.simulation = d3.forceSimulation()
				.force("link", d3.forceLink().id(function(d){return d.id}))
				.force("charge", d3.forceManyBody())
	            .force("collide",d3.forceCollide(_this.nodeSize.size + 30))
				.force("center", d3.forceCenter(_this.width / 2, _this.height / 2));

			// _this.pausePlay();
			var defs = svg.append('svg:defs')

			var link = svg.append("g")
				.attr("class", "connections")
				.selectAll("line")
				.data(obj.connections)
				.enter().append("line")
				.attr("class", "connection")
				.attr("data-layername", function(d){return d.LayerName})
				.attr("data-connection", function(d){return d.connection})
				.attr("stroke", function(d){return d.color})
				.attr('marker-end', function(d){
					if( d.arrow == 1 ) return 'url(#marker_'+d.LayerName+')'
				}).attr('marker-start', function(d){
					if( d.arrow == 2 ) return 'url(#marker_'+d.LayerName+')'
				})
			var link1 = svg1.append("g")
				.attr("class", "connections")
				.selectAll("line")
				.data(obj.connections)
				.enter().append("line")
				.attr("class", "connection connection-line")
				.attr("data-layername", function(d){return d.LayerName})
				.attr("data-connection", function(d){return d.connection})
				.attr("stroke", "transparent")
			var icon = svg.append("g")
				.attr("class", "icons")
				.selectAll("foreignObject.icon")
				.data(obj.icons)
				.enter().append("foreignObject")
				.attr("class", "icon")
				.attr("rx", "10")
				.attr("ry", "10")
				.attr("width", 32*kx)
				.attr("height", 32*ky)
			icon.append("xhtml:div")
				.style("width", 32*kx+"px")
				.style("height", 32*ky+"px")
				.style("background-size", (138*kx)+"px "+(176*ky)+"px")
				.style("background-position-x", function(d){return "-"+((1.3+((d.icon-1)%4)*34.3)*kx)+"px"})
				.style("background-position-y", function(d){return "-"+((2+parseInt((d.icon-1)/4)*34.9)*ky)+"px"})
			var icon1 = svg1.append("g")
				.attr("class", "icons")
				.selectAll("rect.icon1")
				.data(obj.icons)
				.enter().append("rect")
				.attr("class", "icon1")
				.attr("rx", "10")
				.attr("ry", "10")
				.attr("width", 32*kx)
				.attr("height", 32*ky)
				.attr("style", "fill:transparent")
				.attr("data-layername", function(d){return d.LayerName})
				.attr("data-connection", function(d){return d.connection})


			var marker = defs.selectAll('marker')
				.data(obj.connections)
				.enter()
				.append('svg:marker')
					.attr('id', function(d){return 'marker_' + d.LayerName})
					.attr('markerHeight', 5)
					.attr('markerWidth', 5)
					.attr('markerUnits', 'strokeWidth')
					.attr('orient', 'auto')
					.attr('refX', function(d){return d.arrow==1?4:-6})
					.attr('refY', 0)
					.attr('viewBox', '-5 -5 10 10')
					.append('svg:path')
						.attr('d', function(d){return d.arrow==1?'M 0,0 m -5,-5 L 5,0 L -5,5 Z':'M 0,0 m 5,5 L -5,0 L 5,-5 Z'})
						.attr('fill', 'white');

			var node = svg.append("g")
				.attr("class", "nodes")
				.selectAll("rect")
				.data(obj.nodes)
				.enter().append("rect")
				// .call(d3.drag()
				// 	.on("start", _this.startDrag)
				// 	.on("drag", _this.dragging)
				// 	.on("end", _this.stopDrag))
				.attr("class", function(d){return d.class+" blue"})
				.attr("data-id", function(d){return d.id})

			var texts = svg1.append("g")
				.attr("class", "texts")
				.selectAll(".texts foreignObject")
				.data(_this.graph.nodes)
				.enter().append("foreignObject")
				.attr("class", function(d){return ((d.LayerName==focused)?"worldlayer-focused ":"")+"text_part"})
				.attr("data-category", function(d){return d.Category})
				.attr("data-layername", function(d){return d.LayerName})
				.style("width", _this.nodeSize.size)		// for Chrome
				.style("height", 67)						// for Chrome
				.attr("width", _this.nodeSize.size)			// for Firefox
				.attr("height", 67);						// for Firefox
			var tooltip = svg1.append("g")
				.attr("class", "tooltips")
				.selectAll(".tooltips foreignObject")
				.data(obj.connections.concat(_this.graph.nodes))
				.enter().append("foreignObject")
				.attr("class", "mytooltip")
				.attr("data-layername", function(d){return d.LayerName})
				.style("display", "none")
				.attr("display", "none")
				.style("width", function(d){return _this.textwidth((d.class=="node"?d.LayerDisplayName:d.name), "tooltipWidth")+"px"})
				.attr("width", function(d){return _this.textwidth((d.class=="node"?d.LayerDisplayName:d.name), "tooltipWidth")})
				// .attr("height", "")
				.append("xhtml:div");

			// texts.append("xhtml:div").attr("class", "graph").text(function(d){return _this.getGraph(d.LayerName)})
			texts.append("xhtml:div").attr("class", "name").text(function(d){return d.LayerDisplayName}).style('min-height','33.5px').attr('min-height','33.5px')
			texts.append("xhtml:div").attr("class", "summary").text(function(d){return d.Summary}).style('min-height','33.5px').attr('min-height','33.5px')
			// texts.append("xhtml:div").attr("class", "toltip").text(function(d){return d.Tooltip})

			tooltip.append("xhtml:h1").html(function(d){return d.class=="node"?d.LayerDisplayName:d.name})
			tooltip.append("xhtml:span").html(function(d){return d.Tooltip})
			// link.append("title").text(function(d){return d.Tooltip})
			// node.append("title").text(function(d){return d.Tooltip})

			_this.simulation.nodes(obj.nodes).on("tick", ticked);
			_this.simulation.force("link").links(obj.connections);

			setTimeout(_this.simulation.stop,2000);

			function ticked() {
				node.attr("x", function(d){
						if(d.class == "node") d.x = Math.max(10, Math.min( _this.width - _this.nodeSize.size, d.x) );
						// else d.x = Math.max(0, Math.min( _this.width - 50, d.x) );
						else {
							if( d.Output != undefined ) {
								for(var i = 0; i < obj.connections.length; i++) {
									if( obj.connections[i].target.LayerName == d.Output ) {
										d.fx = obj.connections[i].target.x + _this.nodeSize.size / 2 + 5;
										d.x = d.fx;
									}
								}
							} else {
								d.fx = _this.width;
								d.x = d.fx;
							}
						}
						return d.x;
					}).attr("y", function(d){
						if(d.class == "node") d.y = Math.max(10, Math.min( _this.height - _this.nodeSize.size, d.y) );
						// else d.y = Math.max(0, Math.min( _this.height - 50, d.y) );
						else {
							if( d.Output != undefined ) {
								for(var i = 0; i < obj.connections.length; i++) {
									if( obj.connections[i].target.LayerName == d.Output ) {
										d.fy = obj.connections[i].target.y - 70;
										d.y = d.fy;
									}
								}
							} else {
								d.fy = _this.height;
								d.y = d.fy;
							}
						}
						return d.y;
					}).attr("data-pos", _this.updateMapsPos);

				link.attr("x1", function(d){return _this.getCoords(d,'x1')})
					.attr("y1", function(d){return _this.getCoords(d,'y1')})
					.attr("x2", function(d){return _this.getCoords(d,'x2')})
					.attr("y2", function(d){return _this.getCoords(d,'y2')});

				link1.attr("x1", function(d){return _this.getCoords(d,'x1')})
					 .attr("y1", function(d){return _this.getCoords(d,'y1')})
					 .attr("x2", function(d){return _this.getCoords(d,'x2')})
					 .attr("y2", function(d){return _this.getCoords(d,'y2')});

				icon.style("transform", function(d){return "translate("+(_this.getCoords(d,'mx')-16*kx)+"px,"+(_this.getCoords(d,'my')-16*ky)+"px)"});	// for Chrome
				icon.attr("transform", function(d){return "translate("+(_this.getCoords(d,'mx')-16*kx)+","+(_this.getCoords(d,'my')-16*ky)+")"});			// for Firefox
				icon1.style("transform", function(d){return "translate("+(_this.getCoords(d,'mx')-16*kx)+"px,"+(_this.getCoords(d,'my')-16*ky)+"px)"});	// for Chrome
				icon1.attr("transform", function(d){return "translate("+(_this.getCoords(d,'mx')-16*kx)+","+(_this.getCoords(d,'my')-16*ky)+")"});		// for Firefox

				// for Chrome
				texts.style("transform", function(d){if(_this.correctWidth[d.LayerName]==undefined) _this.textwidth(d, "nodeName"); return "translate("+(d.x+5.5)+"px,"+(d.y+9)+"px)"});
				// for Firefox
				texts.attr("transform", function(d){if(_this.correctWidth[d.LayerName]==undefined) _this.textwidth(d, "nodeName"); return "translate("+(d.x+5.5)+","+(d.y+9)+")"});

			}
			_this.strokeWidth = (parseInt((_this.nodeSize.size-1)/100)+1);
			$("#causeeffectcontainer .connections line").css("stroke-width", _this.strokeWidth + "px");
			setTimeout(function(){$("html, body").stop().animate({scrollTop:$("#causeeffectsvg").height()/2 - 150,scrollLeft:$("#causeeffectsvg").width()/2 - 540}, 1000, 'swing')}, 500);
			// $("#layerselect").prop("disabled", false).focus()
		}

	/*------------	Functions For Stoping Animation Before 5sec	Start ------------*/
		// CauseEffect.prototype.pauseAnimation = function(){
			// var _this = causeeffect;
			// var nodes = _this.nodes;
			// if( !_this.pause ) {
			// 	for(i in nodes) {
			// 		nodes[i].fx = nodes[i].x;
			// 		nodes[i].fy = nodes[i].y;
			// 	}
			// 	_this.pause = true;
			// }
		// }
		// CauseEffect.prototype.pausePlay = function( play ){
			// play = play || 0;
			// var _this = this;
			// var nodes = _this.nodes;
			// if( _this.ctrl == true ) {
			// 	_this.pause = false;
			// 	clearTimeout(_this.play);
			// 	_this.play = undefined;
			// } else if( play == 1 ) {
			// 	for(i in nodes) {
			// 		nodes[i].fx = null;
			// 		nodes[i].fy = null;
			// 	}
			// 	_this.pause = false;
			// 	clearTimeout(_this.play);
			// 	_this.play = undefined;
			// } else if( play == 2) {
			// 	_this.play = setTimeout(_this.pauseAnimation, _this.animationTime*1000);
			// } else if( play == 0) {
			// 	for(i in nodes) {
			// 		nodes[i].fx = null;
			// 		nodes[i].fy = null;
			// 	}
			// 	_this.pause = false;
			// 	_this.play = setTimeout(_this.pauseAnimation, _this.animationTime*1000);
			// }
		// }
	 /*------------	Functions For Stoping Animation Before 5sec	End ------------*/

	// CauseEffect.prototype.getGraph = function(layer){
		// var _this = this;
		// // for(i in _this.graphs) {
		// // 	for(j in _this.graphs[i]) {
		// // 		var graph = _this.graphs[i][j].split("/");
		// // 		for(k in graph) {
		// // 			if( layer == graph[k] ){
		// // 				console.log("this: ", i + ", " + j + ", " + k + ": " + graph[k] + ":::::" + graph);
		// // 			}
		// // 		}
		// // 	}
		// // }
		// var graphs = {
		// 	"Terrain": '{4,1,1,9,6,4,3,3,8,0}',
		// 	"WaterSpread": '{9,4,7,4,4,9,1,0,0,5}',
		// 	"FertileGround": '{1,1,9,4,5,7,3,8,5,0}',
		// 	"Camas": '{5,3,6,7,9,7,2,2,3,0}',
		// 	"Birch": '{0,6,6,0,0,8,3,1,2,2}',
		// 	"SoilMoisture": '{2,1,3,7,7,9,3,6,3,1}',
		// 	"Temperature": '{7,1,7,5,6,1,5,5,1,3}',
		// 	"DesertBiome": '{4,0,8,0,4,2,6,8,4,2}',
		// 	"TundraBiome": '{6,2,9,9,4,3,4,8,3,5}',
		// 	"ForestBiome": '{7,4,1,3,9,6,1,6,6,5}',
		// 	"GrasslandBiome": '{1,8,5,1,5,6,4,0,0,3}',
		// 	"WetlandBiome": '{6,3,9,1,9,4,8,8,5,3}'
		// }
		// return graphs[layer];
	// }

	/*------------	Add Maps Function	------------*/
		CauseEffect.prototype.appendMap = function(node, stat){
			stat = stat || false;
			var _this = this;
			var i = node.LayerName;
			$("#causeeffectcontainer").prepend('<div id="map_'+i+'" class="maps_div'+(stat=="image"?" image_map":"")+'"></div>');
			$("#map_"+i).append('<div class="map-'+i+'" data-id="'+i+'"><input type="hidden" class="map-data" value=""><div></div></div>');

			$(".map-"+i+" div").html( $("#mapcontent").html() );

			var view = $(".map-"+i+" .view-map")[0];

			view.mapdata = $(".map-"+i+" .map-data")[0];
			view.settings = {
				"layerSelected": i,
				"pause": true,
				"flat": true,
				"shareCamera": true
			};
			view.OnPositionUpdated = function(pos) {
				$(".map-"+i+" .map-position-text").html(pos.x + ", " + pos.y);
			};
			if( stat == false ) {
				ECO.MINIMAP.addView(view);
			} else if( stat == "selectedLayer" ) {
				//Test code for map coordinates
				ECO.MINIMAP.addView(view);
				var borderSize = 0;
				var area = ECO.MINIMAP.getWorldArea(view, borderSize);
				_this.drawGraph(i, area);
			};

			_this.maps.push(i);
		}

	/*------------	Remove Maps Function	------------*/
		CauseEffect.prototype.deleteMap = function(LayerName){
			var _this = this;

			var view = $(".map-"+LayerName+" .view-map");
			ECO.MINIMAP.removeView(view);
			$("#map_"+LayerName).remove();
		}

	/*------------	Getting Arrows Start and End Coordinats Function	------------*/
		CauseEffect.prototype.getCoords = function(d,r){
			r = r || false;
			var dx = d.target.x-d.source.x,
				dy = d.target.y-d.source.y;

			var	w = causeeffect.nodeSize.size + 10,
				h = causeeffect.nodeSize.size + 80;

			var X1 = d.source.x;
			var Y1 = d.source.y;
			var X2 = d.target.x;
			var Y2 = d.target.y;

			var b = 0;

			if( d.source.class == "node" ) {
				X1 = d.source.x + ((dy>h-5-b||dy<-25-b)?w/2:(dx>25+b)?w+b:-b);
				Y1 = d.source.y + ((dy<h-5-b&&dy>-25-b)?h/2:(dy>25+b)?h+b:-b);
				// X2 += 25;
				// Y2 += 25;
			} else if( d.target.class == "node" ) {
				// X1 += 25;
				// Y1 += 25;
				X2 = d.target.x + ((dy>25+b||dy<-h+5+b)?w/2:(dx>25+b)?-b:w+b);
				Y2 = d.target.y + ((dy<25+b&&dy>-h+5+b)?h/2:(dy>25+b)?-b:h+b);
			} else {
				X1 = d.source.x + ((dy>h+b||dy<-h-b)?w/2:(dx>w+b)?w+b:-b);
				Y1 = d.source.y + ((dy<h+b&&dy>-h-b)?h/2:(dy>h+b)?h+b:-b);
				X2 = d.target.x + ((dy>h+b||dy<-h-b)?w/2:(dx>w+b)?-b:w+b);
				Y2 = d.target.y + ((dy<h+b&&dy>-h-b)?h/2:(dy>h+b)?-b:h+b);
			}

			switch(r) {
				case '1':	return [X1,Y1];
				case '2':	return [X2,Y2];
				case 'x1':	case 'X1':	return X1;
				case 'x2':	case 'X2':	return X2;
				case 'y1':	case 'Y1':	return Y1;
				case 'y2':	case 'Y2':	return Y2;
				case 'x':	case 'X':	return [X1,X2];
				case 'y':	case 'Y':	return [Y1,Y2];
				case 'm':	case 'M':	return [(X1+X2)/2,(Y1+Y2)/2];
				case 'Mx':	case 'mx':	case 'MX':	case 'mX':	return (X1+X2)/2;
				case 'My':	case 'my':	case 'MY':	case 'mY':	return (Y1+Y2)/2;
				case false:	default:	return [[X1,Y1],[(X1 + X2)/2,(Y1 + Y2)/2],[X2,Y2]];
			}
		}

	CauseEffect.prototype.updateMapsPos = function(d) {
		var _this = causeeffect;
		if( d.class != "node" || $("#causeeffectsvg").length == 0) return;
		var i = d.LayerName;
		var realWidth = $("#causeeffectsvg").width();
		var realHeight = $("#causeeffectsvg").height();

		var viewBox = $("#causeeffectsvg")[0].attributes[_this.attrIndex].value.split(" ");

		var factionWidth = viewBox[2] - viewBox[0];
		var factionHeight = viewBox[3] - viewBox[1];

		var kx = realWidth/factionWidth;
		var ky = realHeight/factionHeight;

		var width = _this.nodeSize.size*kx;
		var height = _this.nodeSize.size*ky;

		d.x = Math.max(5, Math.min( factionWidth - width, d.x) );
		d.y = Math.max(5, Math.min( factionHeight - height, d.y) );

		$("#map_"+i).css({"top":((d.y + 74)*ky) + "px","left":((d.x + 6)*kx) + "px"});
		$('#map_'+i+' .map-control-panel').css({'width': width+'px', "height": height+'px'});
		$('#map_'+i+' .view-map').css({'width': width+'px', "height": height+'px'});
		$('#map_'+i+'').css({'width': width+'px', "height": height+'px'});
	}

	/*------------	Node Draging Functionality Start	------------*/
		// CauseEffect.prototype.startDrag = function(d,index,array) {
			// causeeffect.pausePlay(1);
			// $(this).attr("class", $(this).attr("class").replace("blue", "yellow"));
			// if(!d3.event.active)
			// 	causeeffect.simulation.alphaTarget(0.3).restart()
			// d.fx = d.x
			// d.fy = d.y
		// }
		// CauseEffect.prototype.dragging = function(d){
			// d.fx = d.ax != undefined? Math.max(d.ax - 20, Math.min(d.ax + 20, d3.event.x)) : d3.event.x
			// d.fy = d.ay != undefined? Math.max(d.ay - 20, Math.min(d.ay + 20, d3.event.y)) : d3.event.y
		// }
		// CauseEffect.prototype.stopDrag = function(d){
			// causeeffect.pausePlay(2);
			// $(this).attr("class", $(this).attr("class").replace("yellow", "blue"));
			// if(!d3.event.active) causeeffect.simulation.alphaTarget(0)
			// if(!causeeffect.ctrl) {
			// 	d.fx = null
			// 	d.fy = null
			// }
		// }
	 /*------------	Node Draging Functionality End	------------*/

	/*------------	Time Speed Range Start	------------*/
		CauseEffect.prototype.initSpeedRange = function(){
			$("#range-speed").slider({
				orientation: "vertical",
				min: -1,
				value: 0.5,
				step: 0.01,
				max: 1,
				slide: causeeffect.slideTimeRangeSpeed
			});
		}
		CauseEffect.prototype.slideTimeRangeSpeed = function(e,item){
			var value = parseFloat(item.value).toFixed(2);
			$(".range-control2>span").html(value + "<br>days / sec");
			$(".view-map").each(function(index,item){
				item.settings.playSpeed = value;
			})
		}
	 /*------------	Time Speed Range Start	------------*/

	/*------------	Time Range Start	------------*/
		CauseEffect.prototype.initTimeBar = function(){

			$.ajax({
				url: APPIQUERYURL + "/api/v1/map/map.json",
				data: {},
				success: function (data, status, xhr) {
					$("#slider").slider({
						// range: "min",
						// range: "max",
						// range: true,
						disabled: true,
						min: 0,
						value: 0,
						step: 0.01,
						// values: [0, data.WorldTime],
						max: data.WorldTime,
						change: causeeffect.changeTimeRange,
						slide: causeeffect.slideTimeRange,
						create: causeeffect.slideTimeRange
					});
				}
			});
		}
		CauseEffect.prototype.changeTimeRange = function(t,item){
			var value = parseFloat($(this).slider("value"));
			var max = parseFloat($(this).slider("option", "max"));
			// var v1 = item.values[0],v2 = item.values[1];

			$('.view-map').each(function(index,item){
				// item.settings.timeStart = v1;
				item.settings.timeEnd = max;
				item.settings.currentTime = value;
			});

			$("#timeRange .display-time.time1").html(causeeffect.secTotime(value,3));
			$("#timeRange .map-progress-bar").attr("value", parseFloat(value / max).toFixed(2));
		}
		CauseEffect.prototype.slideTimeRange = function(e,item){
			e = e || false;
			item = item || false;
			var value = (item&&!$.isEmptyObject(item))?item.value:$("#slider").slider("option", "value");
			var max = $("#slider").slider("option", "max");
			if( item == undefined || $.isEmptyObject(item) ) {
				$("#timeRange .display-time.time2").html(causeeffect.secTotime(max,3));
			}

			$('.view-map').each(function(index,item){
				item.settings.timeEnd = max;
				item.settings.currentTime = value;
			});
			$("#timeRange .display-time.time1").html(causeeffect.secTotime(value,3));
			$("#timeRange .map-progress-bar").attr("value", parseFloat(value / max).toFixed(2));
		}
	 /*------------	Time Range End	------------*/

	/*------------	Loading Layers Select Options	------------*/
		CauseEffect.prototype.loadLayers = function(){
			var _this = this,
				data = new Object();

			Promise.resolve({
				data : {
					url : APPIQUERYURL + "/api/v1/worldlayers/layers",
					data : {},
					type : 'GET'
				},
				this : this
			}).then(this.PromiseAjax)
			.then(function(obj){
				_this.layers = obj.data;
				var d = obj.data;
				for(i in d) {
					$("#categoryselect").append('<option value="'+i+'" >'+d[i].Category+'</option>');
				}
			})
			.catch(function(err){
				variables.showError(err);
			});
		}

	/*------------	Update Maps Information Text in Header	------------*/
		CauseEffect.prototype.updateMapsInfo = function(){
			var _this = this,
				data = new Object();
			var area = ECO.MINIMAP.getWorldArea($("#map_"+_this.focused+" .view-map")[0], 0);

			Promise.resolve({
				data : {
					url : APPIQUERYURL + "/api/v1/worldlayers/relationships/areadescription?minX="+area.min.x+"&minY="+area.min.y+"&maxX="+area.max.x+"&maxY="+area.max.y,
					data : {},
					type : 'GET'
				},
				this : this
			}).then(this.PromiseAjax)
			.then(function(obj){
				return new Promise(function(resolve,reject){
					$(".mapInfo").html(obj.data);
					resolve(obj);
				});
			})
			.then(function(obj){
				return new Promise(function(resolve,reject){
					obj.data = {
						// url : APPIQUERYURL + "/api/v1/worldlayers/layers/"+_this.focused+"?minX="+area.min.x+"&minY="+area.min.y+"&maxX="+area.max.x+"&maxY="+area.max.y,
						url : APPIQUERYURL + "/api/v1/worldlayers/layers/"+_this.focused+"?minX=0&minY=0&maxX=20&maxY=20",
						data : {},
						type : 'GET'
					};
					resolve(obj);
				});
			}).then(this.PromiseAjax)
			.then(function(obj){
				return new Promise(function(resolve,reject){
					for(i in obj.data) {
						var d = obj.data[i];
						$("#causeeffectsvg1 .texts foreignObject[data-layername='"+d.LayerName+"'] .summary").html(d.Summary);
						$("#causeeffectsvg1 .tooltips foreignObject[data-layername='"+d.LayerName+"'] span").html(d.Tooltip);
					}
				});
			})
			.catch(function(err){
				variables.showError(err);
			});
		}

	/*------------	Helping Functions Start	------------*/
		CauseEffect.prototype.secTotime = function(s,i) {
			i = i || 0;
			var d = Math.ceil(s/86400);
			var t = new Date(null);
			t.setSeconds(s);
			try {
				t = t.toISOString().substr(11,5);
			} catch(e){
				t = '';
			}
			switch(i) {
				case 1: return d;
				case 2: return t;
				case 3: return "<span class='localize' translate-key='38'>Day</span> "+d+"<br>"+t;
				case 0:
				default: return [d,t];
			}
		}
		CauseEffect.prototype.PromiseAjax = function(obj){
			return new Promise(function(resolve,reject){
				$.ajax({
					url: obj.data.url,
					data: obj.data.data,
					type: obj.data.type,
					contentType: "application/json",
					success: function(data){
						obj.data = data
						return resolve(obj);
					},
					error : function(err){
						return reject(err);
					}
				});
			});
		}
		CauseEffect.prototype.textwidth = function(d, state){
			state = state || false;
			var _this = this;
			var width = -1;
			if( state == "nodeName" ) {
				$("#get-string-width").attr("class", state);
				$("#get-string-width").html(d.LayerDisplayName);
				width = parseInt($("#get-string-width").width());
				_this.correctWidth[d.LayerName] = width;
				if( width > _this.nodeSize.size ){
					$(".text_part[data-layername='"+d.LayerName+"'] div").css({"font-size":"20px","line-height":"19px"});
				}
			} else if( state == "tooltipWidth") {

				var realWidth = $("#causeeffectsvg").width();
				var viewBox = $("#causeeffectsvg")[0].attributes[_this.attrIndex].value.split(" ");
				var factionWidth = viewBox[2] - viewBox[0];
				var kx = factionWidth/realWidth;

				$("#get-string-width").attr("class", state);
				$("#get-string-width").html(d);
				width = parseInt($("#get-string-width").width()) * kx;

			}
			return Math.max(400, width);
		}
	 /*------------	Helping Functions End	------------*/

	CauseEffect.prototype.bindEvent = function(){
		var _this = this;
		$(document).on("click", "#causeeffectcontainer .item span", function(){
			var graph_part = $(this).parent().find('.graph_part');

			$(".graph_part.active").slideUp().removeClass("active");

			if( $(graph_part).css("display") == "block" ) $(graph_part).slideUp();
			else $(graph_part).addClass("active").css({'margin-top':'-50px'}).slideDown();
		})
		$(document).on("click", "#timeRange .range-control", function(){
			if( _this.timeDisabled ) return;
			var elem = $(this).find(".fa");
			$(elem).toggleClass("fa-pause").toggleClass("fa-play");
			if( $(elem).hasClass("fa-play") ) {
				clearInterval(_this.timeRange);
			} else {
				clearInterval(_this.timeRange);
				_this.timeRange = setInterval(function(){
					var step = parseFloat($("#range-speed").slider('option', 'value')).toFixed(2);
					var time = parseFloat($("#slider").slider("value"))+step*8640;
					var max = parseFloat($("#slider").slider("option", "max"));
					if( time >= max) time = 0;
					$("#slider").slider({value: time})
				},100);

			}
			$('.view-map').each(function(index,item){
				item.settings.pause = $(elem).hasClass("fa-play");
			});
		})
		// $(document).on("click", ".text_part", function(){
			// $('#graphModal').modal('toggle');
			// var layer = $(this).find('.name').html();
			// $("#graph-playerskill").attr("src", "graph_composer/index.html?page=dashboard&graph="+layer+"&graphcomposer=false&show=lines");
		// })
		$(document).on("click", ".range-control2 span", function(){
			if($(".speed-range-container").css("display") == 'none') {
				$(".speed-range-container").fadeIn();
			} else {
				$(".speed-range-container").fadeOut();
			}
		})
		// $(document).on("click", ".map-toggle-2d", function(){
			// if($(this).data('type') == 3){
			// 	$(this).val('3D');
			// 	$(this).data('type',2);
			// }else if($(this).data('type') == 2){
			// 	$(this).val('2D');
			// 	$(this).data('type',3);
			// }
			// $(this).parent()[0].toggle3D();
		// })
		// $(document).on("keydown", function(e){_this.ctrl = e.keyCode === 17})
		// $(document).on("keyup", function(e){_this.ctrl = _this.ctrl&&e.keyCode !== 17})
		$(window).on("resize", function(){if(_this.simulation==undefined)return;_this.simulation.restart();setTimeout(_this.simulation.stop,2000)})
		$(document).on("change", "#layerselect", function(){
			// $("#layerselect").prop("disabled", true);
			$("#causeeffectcontainer").html('');
			_this.focused = $(this).val();
			this.correctWidth = {};
			while(_this.maps.length)_this.deleteMap(_this.maps.pop());
			$("#mapcontent").load( '../map.html', function(){
				_this.appendMap({LayerName: _this.focused}, "selectedLayer");
			});
		})
		$(document).on("change", "#categoryselect", function(){
			$("#layerselect").html('<option selected disabled>Select Layer</option>');
			var i = $(this).val();
			var d = _this.layers[i];
			_this.focusedcat = d.Category;
			for(var j in d.List) {
				var layer = d.List[j];
				$("#layerselect").append('<option value="'+layer.LayerName+'" data-summary="'+layer.Summary+'" data-cat="'+i+'" data-tooltip="'+layer.Tooltip+'" >'+layer.LayerDisplayName+'</option>');
			}
			$("#layerselect").css({"display":"inline-block"});
			var w = $("#categoryselect").width()+$("#layerselect").width()+670;
			//$(".instructions").css({"width": "calc(100% - " + w + "px)","padding":(w<=930?14:4)+"px 0"});
		})
		$(document).on("mouseover", ".connection, .text_part, .icon1", function(evt){
			var LayerName = $(this).attr("data-layername");
			_this.tooltipHide = true;
			var realWidth = $("#causeeffectsvg").width();
			var realHeight = $("#causeeffectsvg").height();
			var viewBox = $("#causeeffectsvg")[0].attributes[_this.attrIndex].value.split(" ");
			var factionWidth = viewBox[2] - viewBox[0];
			var factionHeight = viewBox[3] - viewBox[1];
			var kx = factionWidth/realWidth;
			var ky = factionHeight/realHeight;

			$("#causeeffectsvg line[data-active=true]").css("stroke", "white");
			$("line[data-active=true]").attr("data-active", false).css("stroke-width", _this.strokeWidth+"px");
			$("marker path[data-active=true]").attr("data-active", false).css("fill", "white");
			$(".icon1").css("stroke-width", 0);
			$(".mytooltip").hide();

			if( evt.target.nodeName == "line" || evt.target.nodeName == "rect" ) {
				var connection = $(this).attr("data-connection").replace(/[ -<>,()]/g, "");
				$("line[data-layername*='"+connection+"'").attr("data-active", true);
				$("#causeeffectsvg line[data-layername*='"+connection+"'").css("stroke", "#1A460A");
				$("marker[id*='"+connection+"'] path").attr("data-active", true).css("fill", "#1A460A");
				$(".icon1[data-connection*='"+$(this).attr("data-connection")+"']").css({"stroke-width":"4px", "stroke":"#1A460A"});
			}
			var coords = $("#causeeffectsvg1")[0].getBoundingClientRect()
			var x = (270 - (coords.x || coords.left))*kx;
			var y = (187 - (coords.y || coords.top ))*ky;
			$(".mytooltip[data-layername='"+LayerName+"']").fadeIn(100);

			// x = Math.min( x, factionWidth - $(".mytooltip[data-layername='"+LayerName+"'] div").width() - 50 );
			// y = Math.min( y, factionHeight - $(".mytooltip[data-layername='"+LayerName+"'] div").height() - 50 );

			// for Chrome
			$(".mytooltip[data-layername='"+LayerName+"']").css("transform","translate("+x+"px,"+y+"px)");
			// for Firefox
			$(".mytooltip[data-layername='"+LayerName+"']").attr("transform","translate("+x+","+y+")").attr("height", $(".mytooltip[data-layername='"+LayerName+"'] div").height()+44);

			$(".maps_div").css("z-index", 2);
		})
		$(document).on("mouseover, mousemove", ".connection, .text_part, .mytooltip, .icon1", function(){
			_this.tooltipHide = false;
		})
		$(document).on("mouseout", ".mytooltip", function(){
			var elem = this;
			_this.tooltipHide = true;
			setTimeout(function(){
				if( _this.tooltipHide ) {
					$("#causeeffectsvg line[data-active=true]").css("stroke", "white");
					$("line[data-active=true]").attr("data-active", false).css("stroke-width", _this.strokeWidth+"px");
					$("marker path[data-active=true]").attr("data-active", false).css("fill", "white");
					$(elem).fadeOut(100, function(){$(".maps_div").css("z-index", 11)});
					$(".icon1").css("stroke-width", 0);
				}
			}, 100);
		})
		$(document).on("mouseout", ".connection, .text_part, .icon1", function(evt){
			_this.tooltipHide = true;
			var elem = this;
			var LayerName = $(elem).attr("data-layername");

			setTimeout(function(){
				if( _this.tooltipHide ) {

					if( evt.target.nodeName == "line" || evt.target.nodeName == "rect" ) {
						var connection = $(elem).attr("data-connection").replace(/[ -<>,()]/g, "");
						$("line[data-layername*='"+connection+"']").attr("data-active", false).css("stroke-width", _this.strokeWidth+"px");
						$("#causeeffectsvg line[data-layername*='"+connection+"']").css("stroke", "white");
						$("marker[id*='"+connection+"'] path").attr("data-active", false).css("fill", "white");
						$(".icon1").css("stroke-width", 0);
					}

					$(".mytooltip[data-layername='"+LayerName+"']").fadeOut(100, function(){$(".maps_div").css("z-index", 11);});
				}
			}, 100);
		})
		$(document).on("click", ".image_map", function(){
			$(this).removeClass("image_map");
			var view = $(this).find(".view-map")[0];
			view.settings.camPos = $(".maps_div:not(.image_map) .view-map")[0].settings.camPos;
			ECO.MINIMAP.addView(view);
		})
		$(document).on("click", ".text_part .name", function(){
			var cat = $(this).parent().attr("data-category");
			var id = 0;
			for(var i in _this.layers) {
				if( _this.layers[i].Category == cat ) {
					id = i;
				}
			}
			$("#categoryselect").val( id ).change();
			$("#layerselect").val( $(this).parent().attr("data-layername") ).change();
		})
	}

	CauseEffect.prototype.init = function(){
		var _this = this;
		if( !(/*@cc_on!@*/false || !!document.documentMode) && !!window.StyleMedia ) {
			_this.attrIndex = 2;
		}

		_this.bindEvent();
		$(document).ready(function(){
			_this.initSpeedRange();
			_this.initTimeBar();
			_this.loadLayers();
		})
	}

	window.causeeffect = new CauseEffect();
	causeeffect.init();

})()