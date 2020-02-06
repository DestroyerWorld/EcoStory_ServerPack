var Popups = new function()
{
	var timeoutInfo = 0; // timer set when waiting while an info popup displays
	var popupOpen; // set if there's already a popup open


	this.syncOpen = function( text )
	{
		var popup = $('#popupSync');
		popup.find('#label').text( text );
		//window.parent.$('body').css('overflow','hidden'); 
		//window.parent.$('body').addClass('stop-scrolling')
		//$('body').addClass('stop-scrolling')
		popup.popup("open");
	}


	this.syncClose = function()
	{
		var popup = $('#popupSync');
		popup.popup("close");
	}


	/**
	 * Open a popup prompt dialog popup with the given text:Object.
	 * @param text {Object} Fields with text to replace: header, body, subtext, btn0, btn1
	 * @param callback {Function} [optional] callback that is passed the clicked button name.
	 */
	this.prompt = function( text, callback )
	{
		if (popupOpen)
		{	// defer until the other popup closes
			popupOpen.one( "popupafterclose", function() { popupOpen = null; Popups.prompt( text, callback ); } );
			popupOpen.popup("close");
			return;
		}
		var popup = popupOpen = $("#popupPrompt");
		for( var id in text )
			$("#"+id, popup).text( text[id] ).filter( (text[id] == null) ? ":visible" : ":hidden" ).toggle();
		popup.one( "popupafterclose", function() { popupOpen = null } );
		popup.popup("open", { transition: 'pop', positionTo: 'window' } );
		$('#btn0,#btn1', popup).on( 'click.popup', function()
		{
			$('#btn0,#btn1', popup).off('click.popup'); // remove these click handlers
			callback && callback( $(this).attr('id') );
		} );
	}


	this.info = function( text, type, secToShow )
	{
		if (popupOpen)
		{	// defer until the other popup closes
			timeoutInfo && clearTimeout(timeoutInfo);
			timeoutInfo = 0;
			popupOpen.one( "popupafterclose", function() { popupOpen = null; Popups.info( text, type, secToShow ); } );
			popupOpen.popup("close");
			return;
		}
		var popup = popupOpen = $("#popupInfo");
		setTimeout( function()
		{
			secToShow = secToShow ? secToShow : 4; // default to 4 seconds
			type = type ? type : 'neutral';
			$("span", popup).text( text );
			popup.removeClass("neutral good bad").addClass(type);
			popup.one( "popupafterclose", function() { popupOpen = null } );
			popup.one( "popupafteropen", function() {
				$('#popupInfo-screen').addClass("ui-screen-hidden");
				$(document).off("focusin"); // Remove the stupid event that blocks inputs from being clicked on while popup is open.
			} );
			popup.popup("open");
			timeoutInfo = setTimeout( function() { popup.popup("close"); timeoutInfo = 0; }, secToShow * 1000 );
		}, 30 );
	}

};