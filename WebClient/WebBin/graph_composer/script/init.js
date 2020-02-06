(function()
{
    "use strict";
    var urlGameServer = parent.APPIQUERYURL;

    // TODO: grab this via wiki login system (parameter to the iframe)
    var sessionKey = 'dfhyudifjkdshfklsdgoiysdoihgdsfhdlkf';

    /** @type {DatasetGraph} The dataset graph class */
    var graph;
    /** @type {Object} list of available saved graphs */
    var graphList = { };
    var datasets =
        {
            'Air':      [ 'Oxygen', 'Humidity', 'CO2', 'Nitrogen', 'CFCs', 'Polutants', 'Stinkiness' ],
            'Building': [ 'CO2 Production', 'Bricks Used', "Land Coverage", "Pubs & Bars" ],
            'Cow':      [ 'Births', 'Deaths', 'Hunger', 'Population: Adults', 'Population: All', 'Population: Juveniles', 'Distance travelled', 'Grass Consumed', 'Moos/hr' ],
            'Marmot':   [ 'Births', 'Deaths', 'Hunger', 'Population', 'Hoar Count' ],
            'Soil':     [ 'Moisture', 'Toxins', 'Nutrients', 'Worms/m²' ],
            'Tree':     [ 'Population', 'Harvested', 'Planted', 'Deaths', 'Happiness' ],
            'Wolf':     [ 'Births', 'Deaths', 'Hunger', 'Population', 'Average Tooth Length' ]
        };

    var reload = false;

    var savedGraphs = false;

    function timeBar(isSingle) {

        var selector = $(".ui-slider-handle[aria-labelledby=end-label], .graph-range-day-right, .ui-slider-input#end");
        var start, end;

        $(".ui-slider-handle[aria-labelledby=start-label]").change();

        if(isSingle) {

            $('#graph-time #end').slider('disable');
            selector.hide();

            //bar graph
            start = graph.time_start;
            end = graph.time_end;

            // if(start != end){
            // 	start = end;
            // }
            // $('#graph-time #start').attr({'min': end, 'max': end});
            //	$('#graph-time #end').attr({'min': end, 'max': end});

            $(".ui-slider-handle").css("pointer-events", "auto")
            $(".ui-slider-track").css("background-color","#6cb351");
            // $(".ui-slider-bg.ui-btn-active").addClass("green-bar")

            // if(getURLParameter("page") == "dashboard"){
            // 	frontpageGraphs("bar");
            // }
        } else {
            $('#graph-time #end').slider('enable');

            selector.show();

            start = graph.time_start;
            end = graph.time_end;
            $('#graph-time #start').attr({'min': start, 'max': end});
            $('#graph-time #end').attr({'min': start, 'max': end});

            $(".ui-slider-track").css("background-color","transparent");

            // if(getURLParameter("page") == "dashboard"){
            // 	frontpageGraphs("line")
            // }
        }

        graph.setTimeRange(start, end);
        graph.dataToQuery();
        // updateTimeRange()

        reload = true;

        // $(".ui-slider-bg.ui-btn-active").removeClass("green-bar")
    }

    function UpdateGraphData() {
        var currentGraphs = $('#law-add-graph #graph-data', parent.document.body).val();

        if(currentGraphs && currentGraphs.length) {

            currentGraphs = JSON.parse(currentGraphs);

            var min = $('[data-type=range]#start').attr('min');
            var max = $('[data-type=range]#end').attr('max');

            currentGraphs.time_start = parseFloat(min);
            currentGraphs.time_end = parseFloat(max);

            $('#law-add-graph #graph-data', parent.document.body).val(JSON.stringify(currentGraphs));
        }
    }

    function sub_item(elm, ev){
        elm.bind("click", function(e){
            $("#loader2").fadeIn();
            e.stopPropagation();
            ev.stopPropagation();
            var step = graph.getGraphicStep();
            graph.clear();

            var data = $(this).data("graph");
            if(step < 0.1){
                var time_min =  (Math.floor(data.TimeMin*100)/100).toFixed(2);
                var time_max =  (Math.floor(data.TimeMax*100)/100).toFixed(2);
            }else{
                var time_min =  (Math.floor(data.TimeMin*100)/100).toFixed(1);
                var time_max =  (Math.floor(data.TimeMax*100)/100).toFixed(1);
            }
            var graphs = data.Keys;

            graph.setTimeRange(time_min, time_max);
            graphs.forEach(function(element){
                graph.addDataset(element.join('/'))
                graph.dataToQuery();
            });

            //setting the graph type
            if(time_min == time_max){
                var style = {'width': '100%', 'margin-left': '0', 'display': 'block' };
                $(".ui-rangeslider-sliders .ui-slider-bg").css(style);

                $(".ui-block-c a").trigger('click');

            }else{
                $(".ui-block-a a").trigger('click');
            }
            $('#load-saved-graphs').attr("data-start", time_min);
            $('#load-saved-graphs').attr("data-end", time_max);
            updateTimeRange([time_min,time_max]);
        })
    }

    function onClickGraphType() {
        var type = $(this).attr('id');
        graph.setType(type);
        frontpageGraphs(type);
    }

    function frontpageGraphs(type) {
        var isFrontPage = (getURLParameter('page') == 'dashboard') ? true : false;

        if(isFrontPage) {

            //changing the graph composer design for front page
            $(".unselect-categories").css("display", "none");
            $('#subheader').css({'height': '50px', 'font-size':'30px'});
            $(' #data-list, .ui-listview').hide();
            // $('.ui-slider-input').css('font-size', "12px");
            //$('#footer .ui-rangeslider input:not(.ui-rangeslider-first)').css('width', '45px');
            //$('#footer .ui-rangeslider input').css('margin-right', '7px');
            //$('#footer .ui-rangeslider input, .graph-range-day-right, .graph-range-day-left').css('font-size', '11px');
            //$('.graph-range-day-right').css('margin-right', "43px");

            var graphName = getURLParameter('graph');

            var url = urlGameServer + "/frontpage";

            $.get(url, function(data, status){

                var categories = [],
                    timeEnd = 0,
                    timeStart = 0;

                data = data.Graphs;
                for(var i = 0; i < data.length; i++) {
                    if(data[i].Graph.TimeMax > 1) {
                        timeStart = (Math.floor(data[i].Graph.TimeMin*10)/10).toFixed(1);
                        timeEnd = (Math.floor(data[i].Graph.TimeMax*10)/10).toFixed(1);
                    } else {
                        timeStart = (Math.floor(data[i].Graph.TimeMin*100)/100).toFixed(2);
                        timeEnd = (Math.floor(data[i].Graph.TimeMax*100)/100).toFixed(2);
                    }

                    if(timeStart == timeEnd) {
                        timeStart = 0;
                    }

                    var obj = { name: data[i].Name, category: data[i].Graph.Categories, Keys: data[i].Graph.Keys, time_start: timeStart, time_end: timeEnd };
                    categories.push(obj);
                }

                if(graphName == "skills") {

                    $('#subheader').html("<span class='localize' translate-key='119'>Player Skill Progress</span>");

                    graph.setTimeRange(categories[0].time_start, categories[0].time_end);

                    for(var i = 0; i < categories[0].Keys.length; i++) {
                        graph.addDataset(categories[0].Keys[i].join("/"));
                    }

                    $('#graph-time #end, #graph-time #start').attr('max',categories[0].time_end);
                    $('#graph-time #end').val(categories[0].time_end);

                    if(typeof type == "undefined" || type == "bar") {
                        // if(categories[0].time_start == categories[0].time_end){
                        graph.setType('column');
                        $(".ui-block-c").trigger("click");
                        // graph.setTimeRange(0,0);
                        // graph.setTimeRange(parseFloat(categories[0].time_start),parseFloat(categories[0].time_end));
                        var range = {min: categories[0].time_start, max: categories[0].time_end, step: graph.getGraphicStep()};
                        $("#graph-time #start").attr(range).val(range.min).slider("refresh");
                        // }
                    } else {
                        graph.setTimeRange(categories[0].time_start,categories[0].time_end);
                        var range = {min: categories[0].time_start, max: categories[0].time_end, step: graph.getGraphicStep()};
                        $("#graph-time #start").attr(range).val(range.min).slider("refresh");
                        $("#graph-time #end").attr(range).val(range.max).slider("refresh");
                    }
                } else if(graphName == "populations") {
                    $('#subheader').html("<span class='localize' translate-key='118'>Populations</span>");
                    graph.setTimeRange(categories[1].time_start, categories[1].time_end);
                    for(var i = 0; i < categories[1].Keys.length; i++) {
                        //console.log(categories[1].Keys[i]);
                        graph.addDataset(categories[1].Keys[i].join("/"));
                    }

                    $('#graph-time #end, #graph-time #start').attr('max',categories[1].time_end);
                    $('#graph-time #end').val(categories[1].time_end);

                    if(typeof type == "undefined" || type == "bar") {
                        // if(categories[1].time_start == categories[1].time_end){
                        graph.setType('column');
                        $(".ui-block-c").trigger("click");
                        // graph.setTimeRange(categories[1].time_start,categories[1].time_end);
                        // graph.setTimeRange(parseFloat(categories[1].time_start),parseFloat(categories[1].time_end));
                        // var range = {min: 0, max: parseFloat(categories[1].time_end), step: graph.getGraphicStep()};
                        var range = {min: categories[1].time_start, max: categories[1].time_end, step: graph.getGraphicStep()};
                        $("#graph-time #start").attr(range).val(range.min).slider("refresh");
                        // }
                    } else {
                        graph.setTimeRange(categories[1].time_start,categories[1].time_end);
                        var range = {min: categories[1].time_start, max: categories[1].time_end, step: graph.getGraphicStep()};
                        $("#graph-time #start").attr(range).val(range.min).slider("refresh");
                        $("#graph-time #end").attr(range).val(range.max).slider("refresh");
                    }
                } else if(graphName == "pollution") {
                    $('#subheader').html("<span class='localize' translate-key='120'>Pollution Progress</span>");
                    graph.setTimeRange(categories[1].time_start, categories[1].time_end);
                    for(var i = 0; i < categories[1].Keys.length; i++) {
                        graph.addDataset(categories[1].Keys[i].join("/"));
                    }

                    $('#graph-time #end, #graph-time #start').attr('max',categories[1].time_end);
                    $('#graph-time #end').val(categories[1].time_end);

                    if(typeof type == "undefined" || type == "bar") {
                        // if(categories[1].time_start == categories[1].time_end) {
                        graph.setType('column');
                        $(".ui-block-c").trigger("click");
                        // graph.setTimeRange(parseFloat(categories[1].time_start),parseFloat(categories[1].time_end));
                        var range = {min: categories[1].time_start, max: categories[1].time_end, step: graph.getGraphicStep()};
                        $("#graph-time #start").attr(range).val(range.min).slider("refresh");
                        // } else {
                        // 	graph.setTimeRange(0,parseFloat(categories[1].time_end));
                        // 	var range = {min: 0, max: parseFloat(categories[1].time_end), step: graph.getGraphicStep()};
                        // 	$("#graph-time #start").attr(range).val(0).slider("refresh");
                        // 	$("#graph-time #end").attr(range).val(range.max).slider("refresh");
                        // }
                    } else {
                        graph.setTimeRange(categories[1].time_start,categories[1].time_end);
                        var range = {min: categories[1].time_start, max: parseFloat(categories[1].time_end), step: graph.getGraphicStep()};
                        $("#graph-time #start").attr(range).val(0).slider("refresh");
                        $("#graph-time #end").attr(range).val(range.max).slider("refresh");
                    }
                }
            });
        }
    }

    function onClickGraphSave() {
        var name = $('input#graph-name').val();
        if(name.length < 2) {
            Popups.info("Enter a valid title before saving. Your graph must have a unique title.");
            return false;
        }
        graph.name = name;

        for( var key in graphList ) {
            if ( key != graph.id && graphList[key].toUpperCase() == name.toUpperCase() ) {
                Popups.info( "<span class='localize' translate-key='121'>A graph with that title already exists! Choose a unique title for your graph.</span>" );
                return false;
            }
        }

        if( !graph.save(updateGraphList) ) {
            // can't save
            Popups.info("<span class='localize' translate-key='122'>Please add a dataset to your graph before saving.</span>");
            return false;
        }
        return true;
    }

    function onClickGraphCopy() {
        promptToSaveChanges(function(){
            graph.id = null;
            graph.modified = false;
            $('#graph-name').val("Copy of " + $('#graph-name').val());
            Popups.info("<span class='localize' translate-key='123'>Graph copied. Type a unique name and save it.</span>", "good");
        });
    }

    function onClickGraphNew() {
        promptToSaveChanges(function(){
            graph.id = null;
            graph.clear();
            graph.modified = false;
            $("#graph-name").val('');
            updateSelectedDatasets();
        });
    }

    function promptToSaveChanges( onProceed ) {
        if(graph.modified) {
            var fields = {
                header: "Graph Modified",
                subtext: $("#graph-name").val(),
                body: "<span class='localize' translate-key='124'>Do you want to save your changes before proceeding?</span>",
                btn0: "<span class='localize' translate-key='125'>Don't Save</span>",
                btn1: "<span class='localize' translate-key='18'>Save</span>"
            };
            Popups.prompt(fields, function(btn){
                if( btn == "btn1" && !onClickGraphSave() ) return; // save failed
                onProceed();
            });
        } else onProceed();
    }

    function activateTimeBar(type) {
        if(getURLParameter("frontpage") == null && graph.modified) {
            var range = {min: graph.time_start, max: graph.time_end, step: graph.getGraphicStep()};
            switch(type) {
                case "bar":
                    $('#graph-time #start').attr(range).slider("enable");
                    break;
                case "line":
                    $('#graph-time #end').attr(range).slider("enable");
                    $('#graph-time #start').attr(range).slider("enable");
                    break;
            }
        }
    }

    function updateTimeRange(timeData) {
        timeData = timeData || false;

        if(getURLParameter('page') == 'dashboard') {
            return false;
        }
        //Popups.syncOpen("Loading...");
        window.parent.$('#page').addClass('hide-graph')

        $.ajax({
            url: urlGameServer + "/datasets/timerange",
            data: {},
            success: function( data, status, xhr ) {
                if(timeData) updateTimeRangeByData(timeData);
                else updateTimeRangeByData(data);
            },
            complete: function() {
                if(savedGraphs) {

                    if(graph.time_start == graph.time_end && savedGraphs) {

                        var min = parseFloat($('#load-saved-graphs').attr("data-start"));
                        var max = parseFloat($('#load-saved-graphs').attr("data-end"));

                        graph.setTimeRange(0,max);

                        $("#graph-time #start").attr({"min": 0, "max": max}).val(max).slider("refresh");

                        var style = {'width': '100%', 'margin-left': '0', 'display': 'block' };
                        $(".ui-rangeslider-sliders .ui-slider-bg").css(style);

                        return false;
                    } else {

                        var min = parseFloat(0);
                        var max = parseFloat($('#load-saved-graphs').attr("data-end"));
                        graph.setTimeRange(min,max);
                        $("#graph-time #start").attr({min: min,max: max}).slider("refresh");
                        $("#graph-time #end").attr({min: min,max: max}).slider("refresh");
                    }
                } else if(graph.time_start == graph.time_end) {
                    var style = {'width': '100%', 'margin-left': '0', 'display': 'block' };
                    $(".ui-rangeslider-sliders .ui-slider-bg").css(style);
                }
                //Popups.syncClose();
                window.parent.$('#page').removeClass('hide-graph');
                if(reload) {
                    $(".ui-slider-handle:first").css('left','0');
                    $(".ui-slider-handle:last").css('left','100%');

                    $(".ui-rangeslider-sliders .ui-slider-bg").each(function(){
                        var style = {'width': '100%', 'margin-left': '0', 'display': 'block' };
                        $(this).css(style);
                    })

                    var start = (graph.time_start == 0) ? 0 : parseFloat(graph.time_start).toFixed(1);
                    var end = (graph.time_end == 0) ? 0 : parseFloat(graph.time_end).toFixed(1);

                    $('#graph-time #start').val(start);
                    if(graph.getType() != "column") {
                        $('#graph-time #end').val(end);
                    }

                    graph.time_start = start;
                    graph.time_end = end;
                    reload = false;
                }
            }
        });

        function updateTimeRangeByData(data) {
            if(graph.time_start == graph.time_end) {
                activateTimeBar("bar");
            } else {
                activateTimeBar("line");
            }
            var range = { min: data[0], max: data[1], step: .1};
            // range.max = Math.round(range.max * 1000) / 1000;

            /*if time is less than 1 day: step => 0.01 otherwise: step => 0.1 */
            range.step = (data[1] > 1) ? 0.1 : 0.01;
            // range.step = 0.01;
            if(data[1] > 1) {
                range.max = (Math.floor(range.max*10)/10).toFixed(1);
            } else {
                range.max = (Math.floor(range.max*100)/100).toFixed(2);
            }

            graph.setTimeRange( range.min, range.max );
            if(savedGraphs) {
                var min = $('#load-saved-graphs').attr("data-start");
                var max = $('#load-saved-graphs').attr("data-end");
                $('#graph-time #start').attr(range).slider('refresh');
                $('#graph-time #end').attr(range).slider('refresh');
                return false;
            }
            $('#graph-time #start').attr(range).slider('refresh');
            $('#graph-time #end').attr(range).slider('refresh');
        }
    }

    function createDatasetList( data, category ) {
        var attrItemList = {'class':'ui-listview','data-role':'listview','data-shadow':'false','data-inset':'false','data-corners':'false','style':'margin-right:0'};
        var attrCategory = {'data-role':'collapsible','data-iconpos':'left','data-shadow':'false','data-corners':'false','data-collapsed-icon':'carat-r','data-expanded-icon':'carat-d','data-mini':'false','data-collapsed':'true','data-inset':'false'};
        var list = $("<ul></ul>").attr(attrItemList);
        for( var n in data ) {
            if (typeof data[n] != "object") continue;
            if(data[n].KeyFragment) {
                list.append("<li data-icon='plus'><a class='miniListItem ui-icon-plus ui-btn ui-btn-icon-right' title='"+data[n].DisplayName+"'data-parent='"+category+"' data-fragment='"+data[n].KeyFragment+"' href='#'>" + data[n].DisplayName + "</a></li>");
            } else {
                // var test = "<li data-icon='plus'><a class='miniListItem' title='"+data[n]+"'data-parent='"+category+"' href='#'>" + data[n] + "</a></li>");
                var test = $("<div><h2> "+ data[n].DisplayName +"<span class='count nested'></span> </h2></div>").attr(attrCategory);
                var ul = createDatasetList(data[n], category+"/"+n);
                test.append(ul);
                list.append(test);
            }                                      4
        }
        return list;
    }

    function updateDatasetList() {
        // $('#footer .ui-rangeslider input').css('width', '45px');
        var data = datasets;
        $.ajax({
            url: urlGameServer + "/datasets/treelist",
            data: { session: sessionKey },
            success: function( data, status, xhr ) {
                var container = $('#data-list');
                var attrCategory = { 'data-role': 'collapsible', 'data-iconpos': 'left', 'data-shadow': 'false', 'data-corners': 'false',
                    'data-collapsed-icon': 'carat-r', 'data-expanded-icon': 'carat-d', 'data-mini': 'false', 'data-collapsed': 'true',
                    'data-inset': 'false' };

                /* NESTED CATEGORIES Testing  */
                var category1 = ["Population", "Population Rate",  "Death By Being Eaten"];
                var category2 = [ { Cedar: ["Population", "Population Rate" ] } ];
                var category3 = [ { Plants: [{ Cedar: ["Population", "Population Rate"] }] } ];

                // Please make this line comment in order to disable the testing mode for the nested categories
                // data = {Bunchgrass: category1, Plant: category2, Species: category3 };
                /*


                                var category = [];
                                var obj = {};
                                for(var i=0;i<data.length;i++){

                                    if (-1 == category.indexOf(data[i].Key[0]) ) {
                                        category.push(data[i].Key[0]);
                                        obj[data[i].Key[0]] = [];
                                    }
                                    if(data[i].Key.length == 3){
                                        obj[data[i].Key[0]].push(data[i]["Key"][1]+"/"+data[i]["Key"][2]);
                                    } else {
                                        obj[data[i].Key[0]].push(data[i]["Key"][1]);
                                    }

                                }

                                data = obj;

                                for( var category in data ) {
                                    var cat = $("<div><h2>"+category+" <span class='count'></span></h2></div>").attr(attrCategory);
                */

                for (var i = 0; i < data.length; i++) {
                    var cat = $("<div><h2>"+data[i].DisplayName+" <span class='count'></span></h2></div>").attr(attrCategory);
                    container.append(cat);
                    var obj = {};
                    var k = 0;
                    createObjByDataset(data[i].Children,obj,k);
                    var list = createDatasetList(obj, data[i].KeyFragment);
                    cat.append(list);
                    list.listview().listview("refresh");
                    $('li>a', list).click( onClickDataset );
                }

                container.collapsibleset().collapsibleset( "refresh" );
                $('.ui-collapsible h2 a', container).addClass('ui-nodisc-icon ui-alt-icon');
                $("[data-role='collapsible']").collapsible({
                    collapse: function( event, ui ) {
                        $(this).children().next().slideUp();
                    },
                    expand: function( event, ui ) {
                        $(this).children().next().hide();
                        $(this).children().next().slideDown();
                    }
                });
                updateGroupCount( $('.ui-collapsible', container) );

                //jscroll(); // load scroll bar script after content loaded
            }
        });
    }
    function createObjByDataset(data, object,k){
        for (var i = 0; i < data.length; i++) {
            if(data[i].Children.length>0){
                k = 0;
                var childObj = {DisplayName:data[i].DisplayName};
                object[data[i].KeyFragment] = childObj;
                createObjByDataset(data[i].Children,childObj,k);
            }else{
                object[k] = {KeyFragment:data[i].KeyFragment,DisplayName:data[i].DisplayName};
                k++;
            }
        }
    }
    function onClickDataset() {
        var menu = $(this);
        var group = menu.parents('div.ui-collapsible');
        var name = getDatasetFromMenu(menu);
        if ( menu.hasClass("ui-icon-plus") ) {
            graph.addDataset(name, function(series){
                var color = series.color;
                menu.css( { color: color } );
                menu.removeClass("ui-icon-plus").addClass("ui-icon-minus");
                updateGroupCount(group);
            });
        } else {
            graph.removeDataset(name);
            menu.css( { color: '' } );
            menu.removeClass("ui-icon-minus").addClass("ui-icon-plus");
            updateGroupCount(group);
        }

        graph.dataToQuery(); //Update hidden input on index with user's graph selection - live
        updateTimeRange();
    }

    function getDatasetFromMenu( menu ) {
        var group = menu.parents('div.ui-collapsible');
        var cat = menu.data('parent').trim();
        var fragment = menu.data("fragment");
        if (fragment)
            cat += "/" + fragment.trim();
        return cat;
    }

    function updateSelectedDatasets() {
        // Go through the datasets and select
        var dsets = $('#data-list li>a');
        dsets.each(function() {
            var menu = $(this);
            var name = getDatasetFromMenu(menu);
            var color = graph.getColor(name);
            if(color) {
                menu.css({color: color});
                menu.removeClass("ui-icon-plus").addClass("ui-icon-minus");
            } else {
                menu.css({color: ''});
                menu.removeClass("ui-icon-minus").addClass("ui-icon-plus");
            }
        });
        updateGroupCount( $("#panel-datasets .ui-collapsible") );
    }

    function updateGroupCount( groups ) {
        groups.each(function() {
            var group = $(this);
            var countSelected = group.find('.ui-icon-minus').length;
            $(group.find('h2 span.count')[0]).text( countSelected ? "("+countSelected+")" : "" );
        });
    }

    function updateGraphList() {
        // making available edit graph functionality
        setTimeout(function(){

            /* show graph name on the input */
            $('#graph-name').val(graph.name);

            var currentGraphs = $('#law-add-graph #graph-data', parent.document.body).val();
            var time_start, time_end;

            if(typeof currentGraphs == "string" && currentGraphs != "") {
                time_start = (JSON.parse(currentGraphs)).time_start;
                time_end = (JSON.parse(currentGraphs)).time_end;
                graph.setTimeRange(time_start,time_end);
                /* including already made graphs to the new ones*/

                currentGraphs = ((JSON.parse(currentGraphs)).categories) ? (JSON.parse(currentGraphs).categories) : (JSON.parse(currentGraphs).Categories);
                /* the case when user have added one graph */
                if(typeof currentGraphs == "string") {

                    graph.addDataset(currentGraphs);
                    /* the case when user have added more than one graphs */
                } else if(typeof currentGraphs == "object") {

                    if(currentGraphs.length == 1) {

                        graph.addDataset(currentGraphs);
                    } else {
                        for(var i = 0; i < currentGraphs.length; i++) {
                            graph.addDataset(currentGraphs[i]);

                            /* loading categories */
                            var parentCategory = currentGraphs[i].split('/')[0];
                            var item = currentGraphs[i].split('/')[1];

                            loadGraphCategory(parentCategory, item);
                        }
                    }
                }
            }
            /* saved graph type */
            var graphType = "line";

            if(typeof time_start != "undefined" && typeof time_end != "undefined") {
                if(time_start == time_end) {
                    graphType = "column";
                }
            }
            if(graphType != null) {
                graph.setType(graphType);
            }
            if(graphType == "column") {
                $(".ui-block-c").trigger('click');
            } else {
                $(".ui-block-a").trigger('click');
            }

            var range = { min: parseFloat(time_start), max: parseFloat(time_end), step: .1};
            range.step = graph.getGraphicStep();

            $('#graph-time #start').attr(range).slider('refresh');
            $('#graph-time #end').attr(range).slider('refresh');
        }, 1000);
    }

    function loadGraphCategory(parent, child) {
        var container = $('#data-list');

        /* finding saved graphs and loading them under data column */
        $('li>a', container).each(function(){
            if($(this).data('parent') == parent && $(this).html() == child ) {
                $(this).trigger('click');
            }
        })
    }

    function unselectCategories(){

        var container = $('#data-list');

        /* finding saved graphs and loading them under data column */
        $('li>a', container).each(function(){
            if($(this).hasClass('ui-icon-minus')) {
                $(this).trigger('click');
            }
        });
    }

    function onClickGraphOpen()	{
        var idGraph = $(this).parent().attr('id');
        //console.log( "Open Graph: " + idGraph );
        $('#panel-graphs').panel("close");

        promptToSaveChanges(function(){
            graph.extendedOptions = {
                chart: {
                    style: {
                        fontFamily: 'Dosis'
                    },
                    backgroundColor: null,
                    spacingBottom: 20,
                    spacingTop: 10,
                    spacingLeft: 0,
                    spacingRight: 10,
                    marginBottom: 110,
                    marginTop: 25,
                    marginLeft: 65,
                    marginRight: 30
                },
                title: {
                    style: {
                        color: '#434348',
                        fontSize: '25px'
                    }
                },
                legend: {
                    itemHiddenStyle: '#4f7b3f',
                    itemStyle: {
                        fontSize: 15
                    }
                },
                yAxis: {
                    gridLineColor: '#4f7b3f',
                    labels: {
                        style: {
                            fontSize: '15px',
                            color: '#434348'
                        }
                    },
                    lineColor: '#4f7b3f',
                    minorGridLineColor: '#4f7b3f',
                    tickColor: '#4f7b3f'
                },
                xAxis: {
                    gridLineColor: '#4f7b3f',
                    labels: {
                        style: {
                            fontSize: '15px',
                            color: '#434348'
                        }
                    },
                    lineColor: '#4f7b3f',
                    minorGridLineColor: '#4f7b3f',
                    tickColor: '#4f7b3f',
                    title: {
                        offset: 25,
                        style: {
                            color: '#434348',
                            fontSize:'17px'
                        }
                    }
                },
                navigation: {
                    buttonOptions: {
                        symbolStroke: '#434348',
                        theme: {
                            fill: '#75B25E'
                        }
                    },
                    menuStyle: {
                        border: '1px solid #477D32',
                        background: '#000000'
                    }
                }
            };

            graph.load(idGraph, function( success ){
                if (!success) return;
                // Update the GUI to match graph properties
                $("#graph-name").val( graph.name );
                $("#graph-type a").removeClass('ui-btn-active').filter("#"+graph.getType()).addClass('ui-btn-active'); // select active type
                $('#graph-time #start').val(graph.time_start).slider('refresh');
                $('#graph-time #end').val(graph.time_end).slider('refresh');
                updateSelectedDatasets();
            });
        });
    }

    function onClickGraphDelete() {
        var idGraph = $(this).parent().attr('id');
        //console.log( "Delete Graph: " + idGraph );
        var fields = {
            header: 'Delete Graph?',
            body: 'Are you sure you want to remove this graph?',
            subtext: 'This action cannot be undone.',
            btn0: 'Cancel',
            btn1: 'Delete'
        };
        Popups.prompt( fields, function(btn) {
            if(btn == 'btn1') {
                var panel = $('#panel-graphs').addClass('ui-loading');
                $('ul#list', panel).html("");
                // call ajax function to remove the graph
                $.ajax( { url: "../ajax.php", data: { op: "graph-delete", id: idGraph, session: sessionKey }, complete: updateGraphList } );
            }
        });
    }

    function getURLParameter(name) {
        return decodeURIComponent((new RegExp('[?|&]' + name + '=' + '([^&;]+?)(&|#|;|$)').exec(location.search)||[,""])[1].replace(/\+/g, '%20'))||null
    }

    $(document).on("pagebeforeshow", '#graph_composer', function(event){
        var control = true;

        $.ajax( {
            url: urlGameServer + "/datasets/timerange",
            success: function( data, status, xhr ) {
                if(data[1] < 0.01) {
                    $('.ui-grid-c>.ui-block-a').css({'display':'none'});
                    $('.ui-grid-c>.ui-block-c').css({'width':'100%'});
                }
            }
        });

        if(getURLParameter('show') == 'lines') {
            frontpageGraphs('line');
        } else if(getURLParameter('page') == 'dashboard') {
            //Temp turn off front page graphs
            //frontpageGraphs();
        } else {
            if(typeof graph == "undefined") {
                $('#graph-time #end').slider('disable');
                $('#graph-time #start').slider('disable');
            }
            updateDatasetList();
            updateGraphList();
        }
    });

    //switch to bar graph
    $(document).on("click", ".ui-block-c", function(e){
        if($(this).find("a").hasClass("ui-btn-active")) {
            return false;
        }
        if (e.originalEvent !== undefined) {
            $("#loader2").fadeIn();
        }
        $('.ui-block-a').removeClass('active');
        $('.ui-block-a').find('a').removeClass('ui-btn-active');
        $(this).addClass('active');
        $(this).find('a').addClass('ui-btn-active');
        timeBar(true);
        updateTimeRange();
    });

    //switch to line graph
    $(document).on("click",".ui-block-a", function(e){
        if($(this).find("a").hasClass("ui-btn-active")) {
            return false;
        }
        if (e.originalEvent !== undefined) {
            $("#loader2").fadeIn();
        }
        $('.ui-block-c').removeClass('active');
        $('.ui-block-c').find('a').removeClass('ui-btn-active');
        $(this).addClass('active');
        $(this).find('a').addClass('ui-btn-active');
        timeBar(false);
        UpdateGraphData();
        updateTimeRange();
    });

    $(document).on("keypress", "#start, #end", function(){
        if( $(this).val() === "" ) $(this).val(0);
        $(this).trigger("slidestop");
    })
    $(document).on("change", "#graph-time #start", function(){
        $('#graph-time #start').mousemove(function(e){
            if(e.clientX < 50){
                $(this).trigger("mouseup");
            }
        });
    });

    $(document).on("change", "#graph-time #end", function(){
        $('#graph-time #end').mousemove(function(e){
            if(e.clientX < 50){
                $(this).trigger("mouseup");
            }
        });
    });

    $(document).on("click", ".unselect-categories", function(){
        $('#graph-categories > li').removeClass("opened");
        $('#graph-categories').empty();
        graph.clear();
        unselectCategories();
        $("#saved-graphs").slideUp();
        $('#load-saved-graphs').removeClass("opened");
        $('#load-saved-graphs > a').removeClass("ui-icon-carat-d").addClass("ui-icon-carat-r");
        savedGraphs = false;
        timeBar(true);
    });

    $(document).on("click",'#load-saved-graphs > a', function(){
        var opened = $("#saved-graphs").css("display") == "block";
        if( opened ) {
            $('#load-saved-graphs > a').removeClass("ui-icon-carat-d").addClass("ui-icon-carat-r");
            $("#saved-graphs").slideUp();
            return;
        }
        $(".subcategories").slideUp();
        savedGraphs = true;
        if(!$(this).hasClass("opened")) {
            unselectCategories();
        }
        $(this).addClass("opened");

        var container = $('#load-saved-graphs');
        $('#graph-categories').empty();

        //getting the graph data
        $.get(urlGameServer + "/datasets/graphs", function(data,status){

            if(status == "success") {

                var arr = [];
                //grouping data by categories
                data.forEach(function(item){

                    //getting the current category name
                    var cat = item.Category;
                    //storing the graphs of each category
                    var items = [];

                    //checking if category items are already pushed to array
                    var find = arr.find(function(x){
                        return x[0].Category == cat;
                    });

                    //push category data to array
                    if(!find && item.Graph.Keys.length) {
                        for(var i = 0; i < data.length; i++) {
                            if(data[i].Category == cat) {
                                var category = [];
                                category.Category = data[i].Category;
                                category.Name = data[i].Name;
                                category.data = [];
                                var name = data[i].Name;

                                var find_items = items.find(function(x){
                                    return x.Name == data[i].Name;
                                });

                                var category_items = [];
                                if(!find_items) {
                                    for(var j = 0; j < data.length; j++) {
                                        if(data[j].Category == cat && data[j].Name == name) {
                                            category_items.push(data[j]);
                                        }
                                    }

                                    category.data = category_items;
                                    items.push(category);
                                }
                            }
                        }
                        arr.push(items);
                        items = [];
                    }
                });

                $('#load-saved-graphs > #saved-graphs').empty();

                //createing the category list
                var item = $("<ul style='list-style-type:none;text-align:left; float:right;' id='graph-categories'></ul>")

                //adding the category items to the list
                arr.forEach(function(obj,i){

                    var elem = $("<li data-category='"+i+"'><a style='margin:0' title='"+obj[0].Category+"' class='ui-collapsible-heading-toggle ui-btn ui-icon-carat-r ui-btn-icon-left ui-btn-inherit ui-nodisc-icon ui-alt-icon'>"+obj[0].Category+"</a></li>");

                    item.append(elem);

                });

                container.find("#saved-graphs").append(item);
            }

            container.find("#saved-graphs").slideToggle(function(){
                var elm = $('#load-saved-graphs > a');
                if(elm.hasClass("ui-icon-carat-r")) {
                    elm.removeClass("ui-icon-carat-r");
                    elm.addClass("ui-icon-carat-d");
                } else {
                    elm.removeClass("ui-icon-carat-d");
                    elm.addClass("ui-icon-carat-r");

                    $('#graph-categories > li').each(function(){
                        $(this).slideUp();
                    })
                }
            });

            //loading the second layer of categoires
            // $(document).on("click","#graph-categories > li",function(ev){
            $("#graph-categories > li").bind("click", function(ev){
                ev.stopPropagation();
                if($(this).hasClass("opened")){
                    $(this).removeClass("opened");
                }else{
                    $("#saved-graphs li").removeClass("opened")
                    $(this).addClass("opened");
                }

                $('#saved-graphs > li a').removeClass("ui-icon-carat-d").addClass("ui-icon-carat-r");

                $('#saved-graphs li').each(function(){
                    $(this).find("a").removeClass("ui-icon-carat-d").addClass("ui-icon-carat-r");
                })

                $(this).siblings().each(function(){
                    $(this).find(".subcategories").slideUp();
                })


                if($(this).find(".subcategories").length != 0){
                    $(this).find(".subcategories").remove();
                    // open = false;
                    // $(this).find(".subcategories").slideUp();
                    // $(this).find(".subcategories").slideToggle();
                    //return false;
                }

                if($(this).find("a").hasClass("ui-icon-carat-r")){
                    $(this).find("a").removeClass("ui-icon-carat-r").addClass("ui-icon-carat-d");
                }else{
                    $(this).find("a").removeClass("ui-icon-carat-d").addClass("ui-icon-carat-r");
                }

                var subCategories = $("<ul class='subcategories'></ul>");
                var open = $(this).hasClass("opened");
                if( open ) {
                    $(subCategories).attr("style", "display:none;");
                }

                var id = $(this).data("category");
                arr[id].forEach(function(sub, j){

                    if(arr[id][j].data.length == 1){
                        var data = JSON.stringify(arr[id][j].data[0].Graph);
                        var li = $("<li data-graph='"+data+"' class='sub-item' data-subcategory = '"+j+"'><a title='"+sub.Name+"'>"+sub.Name+"</a></li>");
                    }else{
                        var li = $("<li data-category = '"+id+"' data-subcategory = '"+j+"' class='sub-items'><a style='margin:0' title='"+sub.Name+"' class='ui-collapsible-heading-toggle ui-btn ui-icon-carat-r ui-btn-icon-left ui-btn-inherit ui-nodisc-icon ui-alt-icon'>"+sub.Name+"</a></li>");
                    }
                    subCategories.append(li);
                })

                $("#saved-graphs li[data-category='"+id+"']").find("ul").empty();
                $("#saved-graphs li[data-category='"+id+"']").append(subCategories);
                /*if($(this).find(".subcategories").innerHeight() > 350){
                    $(this).find(".subcategories").css("height", "350px");
                    $(this).find(".subcategories").css("overflow-y", "scroll");
                };*/

                if(open){
                    $(this).find(".subcategories").slideDown();
                }else{
                    $(this).find("a").removeClass("ui-icon-carat-d").addClass("ui-icon-carat-r");
                    $(this).find(".subcategories").slideUp();
                }

                $(".subcategories > .sub-items").bind("click", function(e){
                    e.stopPropagation();
                    ev.stopPropagation();
                    if($(this).hasClass("opened")){
                        $(this).removeClass("opened");
                    }else{
                        $(this).parent().find('li').removeClass("opened")
                        $(this).addClass("opened");
                    }

                    $(this).parent().find('li a').removeClass("ui-icon-carat-d").addClass("ui-icon-carat-r");

                    $(this).parent().find('li').each(function(){
                        $(this).find("a").removeClass("ui-icon-carat-d").addClass("ui-icon-carat-r");
                    })

                    $(this).siblings().each(function(){
                        $(this).find(".subcategories-item").slideUp();
                    })


                    if($(this).find(".subcategories-item").length != 0){
                        $(this).find(".subcategories-item").remove();
                    }

                    if($(this).find("a").hasClass("ui-icon-carat-r")){
                        $(this).find("a").removeClass("ui-icon-carat-r").addClass("ui-icon-carat-d");
                    }else{
                        $(this).find("a").removeClass("ui-icon-carat-d").addClass("ui-icon-carat-r");
                    }

                    var subCategories_item = $("<ul class='subcategories-item'></ul>");
                    var open = $(this).hasClass("opened");
                    if( open ) {
                        $(subCategories_item).attr("style", "display:none;");
                    }

                    var id = $(this).data("category");
                    var subcategory_id = $(this).data("subcategory");
                    arr[id][subcategory_id].data.forEach(function(sub, j){
                        var data = JSON.stringify(sub.Graph),
                            keys = sub.Graph.Keys[0],
                            key = keys.length-1,
                            name = keys[key];
                        var li = $("<li data-graph='"+data+"' class='sub-item' data-subcategory = '"+j+"'><a title='"+name+"'>"+name+"</a></li>");

                        subCategories_item.append(li);
                    })

                    $(".sub-items[data-subcategory='"+subcategory_id+"']").find("ul").empty();
                    $(".sub-items[data-subcategory='"+subcategory_id+"']").append(subCategories_item);


                    if(open){
                        $(this).find(".subcategories-item").slideDown();
                    }else{
                        $(this).find("a").removeClass("ui-icon-carat-d").addClass("ui-icon-carat-r");
                        $(this).find(".subcategories-item").slideUp();
                    }
                    sub_item($(".subcategories-item .sub-item"),ev);
                })
                sub_item($(".subcategories .sub-item"),ev);
                //dynamically adding the graph names

            })
        });
    });

    $(document).on( "pageshow", '#graph_composer', function(event){
        // Determine the height of the graph inside the page
        var ht = $('body').height();
        ht -= $('[data-role=header]').height();
        ht -= $('[data-role=footer]').height();
        $('#graph').css({ 'height': 340 +"px" });

        // Create the graph class
        graph = new DatasetGraph('#graph');
        // Handle changing the graph type
        $('#graph-type a').click( onClickGraphType );

        // Handle save/copy/new buttons
        $('a#btn-save').click( onClickGraphSave );
        $('a#btn-copy').click( onClickGraphCopy );
        $('a#btn-new' ).click( onClickGraphNew );

        // Handle modification of graph name
        $('#graph-name').change( function() { graph.modified = true; } );

        $('#graph-time #start').on("slidestart", function(){
            $(".ui-rangeslider-sliders .ui-slider-bg").each(function(){
                var style = {'width': '100%', 'margin-left': '0', 'display': 'block' };
                $(this).css(style);
            });
        });

        $(".ui-slider-bg").on("mousedown", function(){
            $(this).on("mousemove", function(){
                var style = {'width': '100%', 'margin-left': '0', 'display': 'block' };
                $(".ui-rangeslider-sliders .ui-slider-bg").css(style);
            })
            // $(".ui-rangeslider-sliders .ui-slider-bg").attr("disabled", "disabled")
            // $(".ui-rangeslider-sliders .ui-slider-bg").each(function(){
            //  	var style = {'width': '100%', 'margin-left': '0', 'display': 'block' };
            //   	$(this).css(style);
            // })
        })

        // Handle changing of the time rangeslider
        $('#graph-time #start').on('slidestop', function() {
            var style = {'width': '100%', 'margin-left': '0', 'display': 'block' };
            if(graph.time_start == graph.time_end){
                $(".ui-rangeslider-sliders .ui-slider-bg").css(style);
            }

            graph.setTimeRange( parseFloat($(this).val())/*.toFixed(2)*/ , graph.time_end );
            graph.dataToQuery();
        } );
        $('#graph-time #end').on('slidestop', function() {
            var style = {'width': '100%', 'margin-left': '0', 'display': 'block' };
            if(graph.time_start == graph.time_end){
                $(".ui-rangeslider-sliders .ui-slider-bg").css(style);
            }

            graph.setTimeRange( graph.time_start, parseFloat($(this).val())/*.toFixed(2)*/);
            graph.dataToQuery();
        } );
        var style = {'width': '100%', 'margin-left': '0', 'display': 'block' };
        if(graph.time_start == graph.time_end){
            $(".ui-rangeslider-sliders .ui-slider-bg").css(style);
        }
        updateTimeRange();



        if(settings) {
            setTimeout(function(){
                var TimeMin = settings.TimeMin;
                var TimeMax = settings.TimeMax;

                for(var i in settings.Keys) {
                    graph.addDataset(settings.Keys[i].join('/'));
                }

                if(TimeMax == TimeMin) {
                    TimeMin = 0;
                    graph.setTimeRange(TimeMin, TimeMax);
                    graph.setType('column');
                    //console.log($(".ui-block-c"));
                    $(".ui-block-c a").trigger("click");
                    var range = {min: TimeMin, max: TimeMax, step: graph.getGraphicStep()};
                    $("#graph-time #start").attr(range).val(range.min).slider("refresh");
                } else {
                    graph.setTimeRange(TimeMin,TimeMax)
                    var range = {min: TimeMin, max: TimeMax, step: graph.getGraphicStep()};
                    $(".ui-block-a a").trigger("click");
                    $("#graph-time #start").attr(range).val(range.min).slider("refresh")
                    $("#graph-time #end").attr(range).val(range.max).slider("refresh")
                }
            },1000);
        }
    })
})();


// Copied from MDN documentation for older browsers
if (!String.prototype.trim) {
    (function(){
        // Make sure we trim BOM and NBSP
        var rtrim = /^[\s\uFEFF\xA0]+|[\s\uFEFF\xA0]+$/g;
        String.prototype.trim = function () {
            return this.replace(rtrim, "");
        }
    })();
}