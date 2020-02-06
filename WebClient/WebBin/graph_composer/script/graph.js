/** @type {string} URL to the game server */
var urlGameServer = APPIQUERYURL;

var DatasetGraph = function( selector )
{
	// ------ public members ------
	/** @type {string} unique ID of the current graph (null if none loaded) */
	this.id = null;

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
			var placeholder = { name: name, data: [], id: name, type: chartType };
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
		this.time_start = parseFloat(time_low);
		this.time_end = parseFloat(time_high);
		if (chart)
		{
			chart.xAxis[0].setExtremes(time_low, time_high, true);
			for(var i in chart.series)
				loadDataset(chart.series[i]);
		}
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
						backgroundColor: null,
						spacingBottom: 20,
					spacingTop: 10,
					spacingLeft: 0,
					spacingRight: 10,
					marginBottom: 110,
					marginTop: 50,
					marginLeft: 65,
					marginRight: 30
				},
				title: {
					style: {
					 color: '#434348',
					 fontSize: '25px'
					},
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
					  tickColor: '#4f7b3f',
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
				background: '#E4EDE1'
				 }
			   }
			};

	this.load = function(category, timemin, timemax, onComplete )
	{
		Popups.syncOpen( "Loading Graph..." );
			
				graph.clear(); // reset the old graph
				//graph.name = 'test';
				//graph.setType( 'line');
				graph.time_start = parseFloat(timemin);
				graph.time_end = parseFloat(timemax);
				
				for(var i in category)
				{
					//var color = (i < data.colors.length) ? data.colors[i] : null;
					var color = null;
					graph.addDataset( category[i][0], null, color );
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

		// var vars = [], colors = [];
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
		var vars = [], colors = [];
		
		for (var i in chart.series)
			{
				vars.push( chart.series[i].name );
				//colors.push( chart.series[i].color );
			}
			
			var args;

			if( $('.ui-block-c a').hasClass('ui-btn-active') ){

				/* if bar graph is selected */

				args = {
					'variables': vars.join(','),
					'single_time': graph.time_start,
				};	

			}else{				
					args = {
					'variables': vars.join(','),
					//'colors': colors.join(','),
					'time_start': graph.time_start,
					'time_end': graph.time_end,
				};	
			}


			/* appending a graph value when a user edits a graph */

			/* getting a current value if it exists */

			var value = $('#law-add-graph #graph-data', parent.document.body).val();
			var current = value ? JSON.parse(value) : null;

			if(current != null){
				current = JSON.parse($('#law-add-graph #graph-data', parent.document.body).val());				
				var x = args.variables.split(',');

				current = current.variables;				

				/* adding a graph data to $('#graph-data') if it isn't already there */

				if(typeof current == "string" && $.inArray(current,x) == -1 ){

					/* if user adds first graph we're including its data to $('#graph-data') */
					x.push(current);					

				}else if(typeof current == "object"){

					/* if user adds more graphs we're including array data to $('#graph-data') */

					for(var i = 0; i < current.length; i++){
						if( $.inArray(current[i], x) == -1 ){
							x.push(current[i]);							
						}
					}
				}

				/* including final data */
				args.variables = x;

				$('#law-add-graph #graph-data', parent.document.body).val(JSON.stringify(args));	
			}
				
			
			$('#law-add-graph #graph-data', parent.document.body).val(JSON.stringify(args));	

			// $("#law-add-graph", parent.document.body).empty().append("<input type='hidden' id='graph-data' value = '" + JSON.stringify(args) + "''>"); 


	};


	// ------------ private methods -------------

	/**
	 * Setup initial chart options and create the chart
	 */
	function setup( series )
	{
		var options = {
			title: { text: null },
			chart: { type: chartType, renderTo: element[0], marginBottom: 80 },
			colors: chartColors,
			plotOptions: {
				spline: { marker: { enabled: false } },
				line: { marker: { enabled: false } },
				column: { borderWidth: 0, pointPadding: 0, groupPadding: 0.05 }
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


	/**
	 * Load the data from the server for the given series
	 * @param series {Series} Highcharts series instance.
	 * @param onLoaded {Function} Callback for when the data has loaded (called on success only)
	 */
	function loadDataset( series, onLoaded, redraw )
	{
		redraw = (redraw === undefined) ? true : redraw;
		var seriesJson = series.name
		var pairs = seriesJson.split('/');
		seriesJson = JSON.stringify(pairs);

		$.ajax( {
			url: urlGameServer + "/datasets/get?time_start="+graph.time_start+"&time_end="+graph.time_end,
			type : "post",
			data: seriesJson,
			contentType: "application/json",
			success: function( data, status, xhr )
			{	
				console.log(data)
				for(i in data){
					data[i] = Object.keys(data[i]).map(function(k) { return data[i][k] });
				}
				var options = { data: data, lineWidth : 4};
				// A 1-dimensional array needs start and interval values
				if ( data.length > 0 && !(data[0] instanceof Array) )
				{
					options.pointStart = graph.time_start;
					options.pointInterval = (graph.time_end - graph.time_start) / data.length;

					options.pointInterval = parseFloat(options.pointInterval.toFixed(2));
				}

				series.update(options,redraw);

				onLoaded && onLoaded( series );
			}
		} );
	}
};
