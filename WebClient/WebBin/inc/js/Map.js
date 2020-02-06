!(function(){
	var Map = function(){
		this.timeBar = function(isSingle){
			var selector = $(".ui-slider-handle[aria-labelledby=end-label], .graph-range-day-right, .ui-slider-input#end");
			var start, end;

			$(".ui-slider-handle[aria-labelledby=start-label]").change();

			if(isSingle){
				$('#graph-time #end').slider('disable');
				selector.hide();

				// //bar graph
				start = 0;
				end = 10000;

				$('#graph-time #start').attr({'min': start, 'max': end});
			 	$('#graph-time #end').attr({'min': start, 'max': end});

			 	$(".ui-slider-handle").css("pointer-events", "auto")
			 	$(".ui-slider-track").css("background-color","#6cb351");
			}else{
				$('#graph-time #end').slider('enable');

				selector.show();

				 start = 0;
				 end = 10000;
				 $('#graph-time #start').attr({'min': 0, 'max': end});
				 $('#graph-time #end').attr({'min': 0, 'max': end});

			 	$(".ui-slider-track").css("background-color","transparent");
			}
		}

		this.bindEvents = function(map){
			$(document).on('click','#line,#column',function(){
				map.timeBar($(this).data('type'));
			})
		}
	}

	var map = new Map();

	$(document).ready(function() {
		map.bindEvents(map);
	});
})()