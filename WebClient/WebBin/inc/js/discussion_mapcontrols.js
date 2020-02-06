var MapControls = function(){
	var mainMap = 'd';
	var mapSelector = '';
	var parent_map = '';


	function sliderIniti(parent_map,selector,mainMap){
		var values = [ 0, 1000 ]
		$( selector ).slider({
			range: true,
			min: values[0],
			max: values[1],
			values: values,
			slide: function( event, ui ) {
				// start range
				mainMap.settings.timeStart = (ui.values[ 0 ] / values[1]) * ECO.MINIMAP.serverTime;
				updateText(parent_map, mainMap);

				// end reang
				mainMap.settings.timeEnd = (ui.values[ 1 ] / values[1]) * ECO.MINIMAP.serverTime;
				updateText(parent_map, mainMap);
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
			return "Day " + Math.ceil(seconds / 86400) + "<br>" + date;
		} else {
			return "Day " + Math.ceil(seconds / 86400) + ", " + date;
		}
	}

	var updateText = function(parent_map, mainMap) {
		$(parent_map+" .range-controls-time-1").html(secondsToString(mainMap.settings.timeStart, 'br'));
		$(parent_map+" .range-controls-time-2").html(secondsToString(mainMap.settings.timeEnd, 'br'));
		$(parent_map+" .speed-button").html(mainMap.settings.playSpeed + "<br>days/sec");
	}

	this.bindEvents = function(parent_map, mainMap, pause){
		parent_map = parent_map;
		mainMap = mainMap;

		positionText = $(parent_map+' .map-position-text');
		infoText = $(parent_map+' .map-info-text');


		$(window).load(function() {
			//$('.ui-slider-handle:last').css({'margin-left': '0'})
			//$('.ui-slider-handle:first').css({'margin-left': '-34px'})
		});

		sliderIniti(parent_map, parent_map+" .slider-range", mainMap);

		$(parent_map+" .speed-button").click(function() {
			$(parent_map+" .map-play-speed").fadeToggle();
		});

		$(parent_map+" .map-play-speed").on('input', function () {
			mainMap.settings.playSpeed = (this.value / this.max).toFixed(2);
			updateText();
		});

		$(parent_map+" .map-layer-select").on('input', function () {
			ECO.MINIMAP.setLayer(this.value, mainMap);
		});

		//change
		$(parent_map+" .map-toggle-2d").click(function () {

			if($(this).data('type') == 2){
				$(this).val('3D');
				$(this).data('type',3);
			}else if($(this).data('type') == 3){
				$(this).val('2D');
				$(this).data('type',2);
			}
			mainMap.toggle3D();
		});

		//change
		$(parent_map+" .map-toggle-play").click(function () {
			var newState = ! $(this).data('type');
			$(this).data('type', newState);
			$(this).html(newState?'<i class="fa fa-play" aria-hidden="true"></i>':'<i class="fa fa-pause" aria-hidden="true"></i>');
			mainMap.settings.pause = newState;
		});

		mainMap.OnTimeUpdated = function(seconds){
			if(pause){
				$(parent_map+" .map-toggle-play").trigger('click');
				pause = false;
			}
			updateText(parent_map, mainMap);
			$(parent_map+" .map-progress-bar").val(seconds/ECO.MINIMAP.serverTime);
			$(parent_map+" .progress-day").html(secondsToString(mainMap.settings.currentTime));
		};
		if(infoText) {
			mainMap.OnInfoUpdated = function(text) {
				$(parent_map+' .map-info-text').text(text);
			}
		}

		mainMap.OnPositionUpdated = function(pos) {
			$(parent_map+' .map-position-text').text(pos.x + ", " + pos.y);
		};

		if(mainMap.settings.flat){
			$(parent_map+" .map-toggle-2d").val('3D');
			$(parent_map+" .map-toggle-2d").data('type',3);
		}

	}
}

