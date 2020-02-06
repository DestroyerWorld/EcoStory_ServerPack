/** @type {string} URL to the game server */

var urlGameServer = parent.APPIQUERYURL;

var DatasetGraph = function( selector )
{
	// ------ public members ------
	/** @type {string} unique ID of the current graph (null if none loaded) */
	this.id = null;
	this.loadingFadeOutIn = 0;

	/** @type {string} name of the current graph */
	this.name = '';

	/** @type {number} start of graph time */
	this.time_start = 0;
	this.time_end = 10000;

	/** @type {boolean} TRUE when the graph has been changed */
	this.modified = false;

	/** @type {Object} Set this to extend the default chart options */
	this.extendedOptions = { };

	// ------ private members ------
	var chart;
	var chartType = 'line';
	var chartColors = [ '#386bae','#429757','#d97d3a','#cb222c','#6a4e97','#958b44','#db64ae','#84ace1','#7bdd93','#f1959b','#ab92d2','#dbcc64' ];
	var element = $(selector);

	/* @type {float} craphic step */
	var graphicStep = 0.01;

	var sessionKey = 'xyzpdq'; // TODO: implement user authentication

	var graph = this;

	// For the graph viewer: if the Popups manager doesn't exist, then create stubs that do nothing.
	var Popups = ('Popups' in window) ? window.Popups :
		{ syncOpen: function(){}, syncClose: function(){}, info: function(){} };


	// ------ public methods ------

	/**
	 * Add a new dataset with the given name to the chart
	 * @param name {string} The dataset name to load and add to the graph
	 * @param onAdded {function} [optional] Function to call on load success: callback(series)
	 * @param color {string} [optional] Color for the series (omit for default)
	 */
	this.addDataset = function( name, onAdded, color )
	{
		var series = chart ? chart.get(name) : null;
		if ( series == null )
		{
			var displayName = "";
			var placeholder = {};

			if (name.indexOf('/')>0){
				var name2 = name.split('/');
				displayName = name2[name2.length-1];
                placeholder = { name: name, data: [], id: name, type: chartType };
			}else {
                placeholder = { name: name, data: [], id: name, type: chartType };
			}
			//console.log(placeholder);
            //var placeholder = { name: name, data: [], id: name, type: chartType };

			if (color) placeholder.color = color;
			if (chart)
				series = chart.addSeries(placeholder, false);
			else {
				chart = setup(placeholder);
				series = chart.get(name);
			}
		}
		this.modified = true;
		// Load it from the server
		loadDataset( series, onAdded );

	}

	/**
	 * Remove the given dataset from the chart
	 */
	this.getGraphicStep = function()
	{
		//if time is greater than 1.0 timeline allows one decimals
		if(this.time_end > 1){
			graphicStep = 0.1;
		} else {
			graphicStep = 0.01;
			}
		return graphicStep;
	}

	/**
	 * Remove the given dataset from the chart
	 */
	this.removeDataset = function( name )
	{
		var series = chart ? chart.get(name) : null;
		series && series.remove();
		this.modified = true;

		//dataToQuery() //send updated data to query
	}


	/**
	 * Remove all datasets from the graph
	 */
	this.clear = function()
	{
		if (chart)
		{
			for(var i = chart.series.length-1; i >= 0; i--)
				chart.series[i].remove();
			this.modified = true;
		}
	}

	/**
	 * Return the color used to render a series.
	 * @return {string} The color string or could be null if the series is invalid.
	 */
	this.getColor = function( name )
	{
		var series = chart ? chart.get(name) : null;
		return series ? series.color : null;
	}


	/**
	 * Set the type of chart to display
	 * @param type {string} 'column', 'line', or 'spline'
	 */
	this.setType = function( type )
	{
		if ( type != chartType )
		{
			this.modified = chart != null;
			chartType = type;
			if (chart)
			{
				for( var i in chart.series )
					chart.series[i].update( { 'type': type } );
			}
		}
	}

	/**
	 * Return the chart type
	 */
	this.getType = function()
	{
		return chartType;
	}

	/**
	 * Sets the new time range for the data and reloads new data from the server
	 */
	this.setTimeRange = function( time_low, time_high )
	{
		if(typeof chart != typeof undefined){
			this.loadingFadeOutIn += chart.series.length - 1;
		}
		if(chartType == 'column'){

			this.time_start = (typeof time_low =='undefined') ? parseFloat(0) : parseFloat(time_low);
			this.time_end = (typeof time_high =='undefined') ? parseFloat(0) : parseFloat(time_low) + 0.1;

			if(typeof chart != typeof undefined){
				chart.xAxis[0].setExtremes(time_low, time_low);
				for(var i in chart.series)
					loadDataset(chart.series[i]);
			} else {
				$("#loader2").fadeOut();
			}

		} else {

			this.time_start = parseFloat(time_low);
			this.time_end = parseFloat(time_high);

			if(chart) {
				chart.xAxis[0].setExtremes(time_low, time_high, true);
				for(var i in chart.series)
					loadDataset(chart.series[i]);
			} else {
				$("#loader2").fadeOut();
			}
		}

//		this.time_start = (this.time_start == 0) ? 0 : parseFloat(this.time_start).toFixed(1);
	//	this.time_end = (this.time_end == 0) ? 0 : parseFloat(this.time_end).toFixed(1);


	}


	/**
	 * Show or hide the title (name) of the graph
	 */
	this.showTitle = function( show, redraw )
	{
		if (chart)
		{
			redraw = (redraw === undefined) ? true : redraw;
			chart.setTitle( { text: this.name }, null, redraw );
		}
	}


	/**
	 * Load the graph from the server using the unique ID
	 * @param id {string} graph unique ID
	 * @param onComplete {Function} [optional] call when finished with arguments: success {Boolean}
	 */
	 this.extendedOptions = {
				chart: {
						style: {
							 fontFamily: 'Dosis',
						},
						spacingBottom: 20,
						spacingTop: 1,
						spacingLeft: 0,
						spacingRight: 10,
						marginBottom: 95,
						marginTop: 15,
						marginLeft: 55,
						marginRight: 10,
						backgroundColor:"#e8ffdf"
				},
				tooltip: {
					formatter: function(){
						var s = '<b>'+ this.x.toFixed(2) +'</b><br>';

						s += '<span style="color:'+ this.point.series.color +'">\u25CF</span> ';

						s += this.point.series.name + " " + this.y;

						return s;

					}
				},
				title: {
					style: {
					 color: '#434348',
					 fontSize: '25px',
					},
				},
				legend: {
					layout: "horizontal",
					align: "left",
					itemDistance: 4,
					maxHeight: 57,
					margin: 0,
					padding: 7,
					itemMarginBottom: 2,
					floating: false,
					y: 10,
					itemHiddenStyle: '#4f7b3f',
					 navigation: {
		                activeColor: '#3E576F',
		                animation: true,
		                arrowSize: 10,
		                itemFontSize: 10,
		                inactiveColor: '#CCC',
		                style: {
		                    fontWeight: 'bold',
		                    color: '#333',
		                    fontSize: '10px'
		                }
           			 },
					itemStyle: {
						 fontSize: 13
					  }
				},
				yAxis: {
					  gridLineColor: '#4f7b3f',
					  labels: {
					  	useHTML: true,
					  	formatter: function(){
					  		if(this.value == 0){
					  			return "<b style='color:#000;'>" + this.value + "</b>";
					  		}else{
					  			return this.value;
					  		}
					  	},
						 style: {
							 fontSize: '14px',
							color: '#434348'
						 }
					  },
					  lineColor: '#4f7b3f',
					  minorGridLineColor: '#4f7b3f',
					  tickColor: '#4f7b3f',
					  minRange:1
				 },
				xAxis: {
					  gridLineColor: '#4f7b3f',
					  labels: {
					  	useHTML: true,
					  	formatter: function(){
					  		if(this.value == 0){
					  			return "<b style='color:#000;'>" + this.value + "</b>";
					  		}else{
					  			return this.value;
					  		}
					  	},
						 style: {
							fontSize: '14px',
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
							fontSize:'15px'
						 }
					  },
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
				background: '#E4EDE1'
				 }
			   }
			};



	this.load = function(category, timemin, timemax, onComplete )
	{
		Popups.syncOpen( "Loading Graph..." );
				graph.clear(); // reset the old graph
				// graph.setType( 'line');
				graph.time_start = parseFloat(timemin);
				graph.time_end = parseFloat(timemax);

				for(var i in category)
				{
					//var color = (i < data.colors.length) ? data.colors[i] : null;
					var color = null;
					graph.addDataset( category[i], null, color );
				}

				//graph.modified = false;
				Popups.info( "Graph loaded successfully", 'good', 2 );
				onComplete && onComplete(true);
				Popups.syncClose();
	}


	/**
	 * Save the graph to the server using the unique ID (or get one assigned)
	 * @param {Function} onSaveComplete Called when save completes (not called if save() returns FALSE)
	 * @return {boolean} TRUE on success and FALSE if there's nothing to save.
	 */
	this.save = function( onSaveComplete )
	{
		if (chart == null || chart.series.length == 0)
			return false;

		Popups.syncOpen( "Saving Graph..." );

		var vars = [], colors = [];
		for (var i in chart.series)
		{
			vars.push( chart.series[i].name );
			colors.push( chart.series[i].color );
		}

		var args = {
			'op': 'graph-save',
			'title': graph.name,
			'type': chartType,
			'variables': vars.join(','),
			'colors': colors.join(','),
			'time_start': graph.time_start,
			'time_end': graph.time_end,
			'session': sessionKey
		};

		//console.log(args);

		if (this.id)
			args.id = this.id;

		$.ajax( {
			url: "../ajax.php",
			data: args,
			success: function( data, status, xhr )
			{
				if ( 'id' in data )
				{
					graph.id = data.id; // assign a unique ID
					graph.modified = false;
					Popups.info( "Graph Saved Successfully!", 'good' );
				}
			},
			error: function()
			{
				Popups.info( "Failed to save graph! Check your network connection.", 'bad' );
			},
			complete: function()
			{
				Popups.syncClose();
				onSaveComplete && onSaveComplete();
			}
		});

		return true;
	}


	/**
	 * Pass graph variables to iframe's parent.
	 */
	this.dataToQuery = function() {

		graph.name = $('#graph-name').val();

		var vars = [], colors = [];

		if( chart ) {
			for (var i in chart.series)
			{
				vars.push( chart.series[i].name );
				//colors.push( chart.series[i].color );
			}
		}
			var args;
			if( $('.ui-block-c a').hasClass('ui-btn-active') ){

				// if bar graph is selected
				args = {
					'variables': vars,
					'time_start': graph.time_start,
					'time_end': graph.time_start,
					'name': graph.name
				};

			}else{
					args = {
					'variables': vars,
					//'colors': colors.join(','),
					'time_start': graph.time_start,
					'time_end': graph.time_end,
					'name': graph.name
				};
			}

			$(".graph-" + $('.iframe-id').val() + ' .graph-data', parent.document.body).val(JSON.stringify(args));
			$("#law-add-graph", parent.document.body).empty().append("<input type='hidden' id='graph-data' value = '" + JSON.stringify(args) + "''>");

	};


	// ------------ private methods -------------

	/**
	 * Setup initial chart options and create the chart
	 */
	function setup( series )
	{
		//if graph loads from front page, then change its height
		if(getURLParameter('page') == 'dashboard'){
			graph.extendedOptions.chart.height = 400;
			graph.extendedOptions.legend.maxHeight = 45;
			graph.extendedOptions.chart.marginLeft = 65;


			if(graph.time_start == graph.time_end){
				graph.extendedOptions.legend.maxHeight = 56;
			}
		}
		var options = {
			title: { text: '' },
			chart: { type: chartType, renderTo: element[0], marginBottom: 80 },
			colors: chartColors,
			plotOptions: {
				spline: { marker: { enabled: false } },
				line: { marker: { enabled: false } },
				column: { borderWidth: 0, pointPadding: 0, groupPadding: 0.05 },
				 series: {
	                cursor: 'pointer',
	                point: {
	                    events: {
	                        click: function () {
	                        	this.series.hide();
	                        }
	                    }
	                }
            	}
			},
			series: [ series ],
			yAxis: { title: { text: null } },
			xAxis: { min: graph.time_start, max: graph.time_end,
				title: { text: "Days", align: "high" } },
			credits: { enabled: false }
		};

		options = $.extend(true, options, graph.extendedOptions );

		return new Highcharts.Chart(options);
	}

	var lastStartTime = undefined;
	var lastEndTime = undefined;
	var datasetCache = {};

	/**
	 * Load the data from the server for the given series
	 * @param series {Series} Highcharts series instance.
	 * @param onLoaded {Function} Callback for when the data has loaded (called on success only)
	 */
	function loadDataset( series, onLoaded, redraw )
	{
		var loadCallback = function( data )
        {
            for(var i in data){
                data[i] = Object.keys(data[i]).map(function(k) { return data[i][k] });
            }

            var options = { data: data, lineWidth : 4 };

            // A 1-dimensional array needs start and interval values
            if ( data.length > 0 && !(data[0] instanceof Array) )
            {
                options.pointStart = graph.time_start;

                options.pointInterval = (graph.time_end - graph.time_start)/data.length;

                graphicStep = options.pointInterval;
            }

            series.update( options, redraw );

            onLoaded && onLoaded( series );


            var index = this.loadingFadeOutIn;
            if( index > 0 ) {
                this.loadingFadeOutIn = index-1;
            } else  {
                $("#loader2").fadeOut();
                this.loadingFadeOutIn = 0;
            }
        };

		if (graph.time_start === lastStartTime && graph.time_end === lastEndTime)
		{
			var cachedXhr = datasetCache[series.name];
			if (cachedXhr)
			{
				cachedXhr.done(loadCallback);
				return;
			}
		}
		else
		{
			// if date range was change then reset data set cache
			lastStartTime = graph.time_start;
			lastEndTime = graph.time_end;
			datasetCache = {};
		}

		redraw = (redraw === undefined) ? true : redraw;

		var seriesJson = series.name

		if(!$.isArray(series.name)){
			var pairs = seriesJson.split('/');

		}else{
			var pairs = seriesJson;
		}
		seriesJson = JSON.stringify(pairs);


		var xhrCall = $.ajax( {
			url: urlGameServer + "/datasets/get?time_start="+graph.time_start+"&time_end="+graph.time_end,
			type : "post",
			data: seriesJson,
            contentType: "application/json"
		} );
		datasetCache[series.name] = xhrCall;
		xhrCall.done(loadCallback);
		xhrCall.fail(function () {
			// remove cached request on failure
			delete datasetCache[series.name];
        });
	}
};


function getURLParameter(name) {
	return decodeURIComponent((new RegExp('[?|&]' + name + '=' + '([^&;]+?)(&|#|;|$)').exec(location.search)||[,""])[1].replace(/\+/g, '%20'))||null
}
