var PlayersControls = function(){
	var timeStart = '';
	var timeEnd = '';

	function sliderIniti(data){
		var values = [ 0, 1000 ];

		timeStart = data[ 0 ] * 86400;
		timeEnd = data[ 1 ] * 86400;

		$( "#players-slider-range" ).slider({
			range: true,
			min: values[0],
			max: values[1],
			values: values,
			stop: function( event, ui ) {
				timeStart = (ui.values[ 0 ] / values[1]) * data[1] * 86400;
				timeEnd = (ui.values[ 1 ] / values[1]) * data[1] * 86400;
				updateText();
				playersFilter([(ui.values[ 0 ] / values[1]) * data[1],(ui.values[ 1 ] / values[1]) * data[1]]);
			},
			slide: function( event, ui ){
				timeStart = (ui.values[ 0 ] / values[1]) * data[1] * 86400;
				timeEnd = (ui.values[ 1 ] / values[1]) * data[1] * 86400;
				updateText();
			},
			create: function( event, ui ){
				updateText();
			}
		});
	}

	var secondsToString = function(seconds, br) {
		var date = new Date(null);
		date.setSeconds(seconds);

		try {
			date = date.toISOString().substr(11,5);
		} catch(e){
			date = '';
		}

		if (br == 'br') {
			return Math.floor(seconds / 86400) + "<br>" + date;
		} else {
			return Math.floor(seconds / 86400) + ", " + date;
		}
	}

	var updateText = function() {
		$(".range-controls-time-user-activ-span-1").html(secondsToString(timeStart, 'br'));
		$(".range-controls-time-user-activ-span-2").html(secondsToString(timeEnd, 'br'));
	}

	var getTime = function(){
		sliderIniti(variables.timerange);
	}
	this.bindEvents = function(){
		getTime();
	}
}

var playersControls = new PlayersControls();