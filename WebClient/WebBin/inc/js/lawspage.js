var LASTINTERVALID = null; // global variable which will help counter to work right

function reload_page()
	{
		if (sessionStorage.getItem('pluginBugFixedByReloading') !== 'fixed') {
			sessionStorage.setItem('pluginBugFixedByReloading', 'fixed');
			location.reload();
		}
	}

function lawspage(lawID,curentLaw,Law,action='load'){
	curentLaw = curentLaw || null;

	function filter_function(people){

		return people['id'] == lawID;
	}

	function array_filter(arr, func) {

		var retObj = {},
		k;

		func = func || function(v) {
			return v;
		};

		// Fix: Issue #73
		if (Object.prototype.toString.call(arr) === '[object Array]') {
			retObj = [];
		}

		for (k in arr) {
			if (func(arr[k])) {
				retObj[k] = arr[k];
			}
		}

		return retObj;
	}

	function array_values(input) {

		var tmp_arr = [],
		key = '';

		if (input && typeof input === 'object' && input.change_key_case) { // Duck-type check for our own array()-created PHPJS_Array
			return input.values();
		}

		for (key in input) {
			tmp_arr[tmp_arr.length] = input[key];
		}

		return tmp_arr;
	}

		var json;

		$.get(APPIQUERYURL+'/api/v1/laws', law_load);
	function law_load(lawsData){
		if( window.variables == undefined ) {
			setTimeout(function(){
				law_load(lawsData);
			}, 200);
			return;
		}
		var this_law = array_filter(lawsData, filter_function);
		$("a#"+lawID).parent().addClass("menu-highlight");
		// $(".menu-laws").addClass("menu-highlight");

		var data = lawsData;
				for(var i=0; i<data.length; i++)
				{
					if(data[i].Guid == lawID)
					{
						checkedData = data[i];
						break;
					}
				}
				if(typeof checkedData == undefined || typeof checkedData == "undefined" ){
					$("html, body").animate({ scrollTop: 0 }, "slow");
					$('#status').empty().html('This law does not exist').fadeIn();
					$('.law-wrapper').hide();
					return false;
				}else{
					if(curentLaw) {
						curentLaw.inEffect = checkedData.InEffect;
					}

					$('.law-wrapper').show();
					$('#laws-landing-page').hide();
				}



				var currentLawID = checkedData['Guid'];
			    graphData = (typeof checkedData.Graph != 'undefined') ? checkedData.Graph : null;


				$('#laws-page-wrapper').css('min-height', '712px');



					//Update Law Page
					if( checkedData['ProposingUser'] != undefined ) {
						$('#law-author').text(checkedData['ProposingUser']);
					} else {
						$('#law-author').hide();
						$('#law-author-label').hide();
					}
					$('#law-title').text(checkedData['Title']);

					var ratified = Math.abs(parseInt(checkedData['VoteEndsInSeconds']));

					var days = parseInt(ratified/86400);
					var hours = Math.floor((ratified - (days * 86400))/3600);

					var ratifedDay;
					var ratifedHour;
					var ratifedEnding = " <span class='localize' translate-key='154'>ago</span>";
					if(days == 1 ){
						ratifedDay = "1 <span class='localize' translate-key='38'>day</span> ";
					}
					else if (days > 1 ){
						ratifedDay = days + " <span class='localize' translate-key='155'>days</span> ";
					}else{
						ratifedDay = ""; // otherwise we'll get the 'undefinied' word in front-end
					}

					if(hours == '' ){
						ratifedHour = "";
					}
					if(hours == 1 ){
						ratifedHour = "1 <span class='localize' translate-key='143'>hr</span>";
					}
					else if (hours > 1 ){
						ratifedHour = hours + " <span class='localize' translate-key='156'>hrs</span>";
					}
					else if ( (hours < 1 ) && (days < 1 ) ){
						ratifedHour = "<span class='localize' translate-key='158'>in the last hour</span>";
						ratifedEnding = "";
					}

					$('#timeOfVote').html(ratifedDay+ratifedHour+ratifedEnding);

					var description = checkedData['Description'];

					/* checking if "description" is a valid JSON string  */
					$('#law-reasoning-description-graph p').text(description);

					//Set law's graph data
					if (graphData != null && action=='vote') {}
						else if(graphData != null && action=='load'){
							setTimeout(function(){
								// var iframe = document.getElementById('iframe')
								document.getElementById('iframe').contentWindow.getGraphData(graphData.Keys, graphData.TimeMin, graphData.TimeMax);
								// iframe.contentWindow.getGraphData(graphData.Keys, graphData.TimeMin, graphData.TimeMax);
								$('#iframe').fadeIn();
							}, 1000);
					}else{
						$('#iframe').parent().hide();
						$('#iframe').fadeOut();
					}

					var lawText = checkedData['LawText'].replace(/\n/g, "<br />")
					lawText = lawText.replace(/(<section><section>I)/g,"<section>I")
					$('#law-text').empty().html(lawText);

					if(checkedData.Map != null && checkedData.Map != "") {
						var map = {};
				            map.layerSelected = checkedData.Map.WorldLayer;
				            map.frame = checkedData.Map.Frame;
				            map.timeStartSeconds = checkedData.Map.TimeStartSeconds;
				            map.timeEndSeconds = checkedData.Map.TimeEndSeconds;
				            map.playSpeed = checkedData.Map.PlaySpeed;
				            map.currentTimeSeconds = checkedData.Map.CurrentTimeSeconds;
				            map.flat = checkedData.Map.Flat;
				            map.pause = checkedData.Map.Pause;
				            map.camPos = checkedData.Map.CameraPosition;

						if( checkedData['LawText'].indexOf("districtEditor") != -1 && $(".district-controls-wrapper").length == 0 ) {

							var fieldId = $("#law-text").find("districteditor").attr("fieldid");
							map.layerSelected = "ProposedDistricts";

							variables.getLawDistrict( currentLawID, fieldId, function(obj){

								variables.LoadProposedDistricts(obj.data);

								lawspage.propDist = obj.data;
								lawspage.currDist = obj.currentDistricts;

								$(".district-controls-wrapper").show();
								for(var i in obj.data.DistrictMetadata) {
									var d = obj.data.DistrictMetadata[i];
									var rgb = "rgb("+d.R+","+d.G+","+d.B+")";
									if( d.ID == 0 ) continue;
									$(".district-wrap").before('<div class="district-item proposed-districts" title="'+d.Name+'" data-id="'+d.ID+'" data-color="'+JSON.stringify(d)+'" style="background:'+rgb+'">'+d.Name+'</div>');
								}
								for(var i in obj.currentDistricts.DistrictMetadata) {
									var d = obj.currentDistricts.DistrictMetadata[i];
									var rgb = "rgb("+d.R+","+d.G+","+d.B+")";
									if( d.ID == 0 ) continue;
									$(".district-wrap").before('<div class="district-item current-districts" title="'+d.Name+'" data-id="'+d.ID+'" data-color="'+JSON.stringify(d)+'" style="background:'+rgb+';display:none;">'+d.Name+'</div>');
								}


							$('#law-reasoning-description-map-viewer').css('margin-right', '300px');
							});

						}

						if($("#law-reasoning-description-map-viewer").length == 0) {
							var viewer = $('<div class="view-map" id="law-reasoning-description-map-viewer"><p id="map-position-text"></p><p id="map-info-text">.</p><div class="district-controls-wrapper"><div class="district-title toggle_prposed_district">Proposed Districts</div><input type="hidden" class="district-wrap"></div></div>');
							$("#law-reasoning-description-graph-viewer").after(viewer);

							var view = viewer[0];

							// view.settings = JSON.parse(checkedData.Map);
							view.settings = map;
							view.OnPositionUpdated = function(pos) {
								view.children[0].innerText = pos.x + ", " + pos.y;
							};
							view.OnInfoUpdated = function(text) {
								view.children[1].innerText = text;
							}

							//Load District
							if(checkedData.District != null && checkedData.District != "")
								ECO.MINIMAP.SetZone(checkedData.District);

						}
						if( checkedData['InEffect'] == true && checkedData.Map.WorldLayer == "ProposedDistricts" ) {
							$(".view-map").hide();
						}
					}

					$('#law-currentid').empty().append("<input type='hidden' id='currentID' value = '" + lawID + "''>");

					if(checkedData['VotesYes'] == 0 && checkedData['VotesNo'] == 0){
						$('.counter').hide();
						$('#container-graph').hide();
						$('#poll').prepend( $('<p>No votes have been submitted</p>') );
						$('#poll').css({paddingLeft : "0px"});
					} else {
						$('.counter').show();
						$('#container-graph').show();
						$('#poll>p').remove();
					}

					var yes = localStorage.getItem('yes');
					var no = localStorage.getItem('no');
					if(typeof yes == 'undefined' || yes == ''){
						yes = 'yes'
					}
					if(typeof no == 'undefined' || no == ''){
						no = 'no'
					}
					//Update Voting Graph
					var votes = [[yes, checkedData['VotesYes']], [no, checkedData['VotesNo']]];
					requestData(votes);

					//Load comments
					// var law = new Law(checkedData['Guid']);
					if(curentLaw) {
						curentLaw.GetComments(currentLawID);
					}

					//Load countdown
					var t = Math.floor(checkedData['VoteEndsInSeconds'] );
					counter(t);


					/* check if law is active */
					var isActive = $("#active-laws a#" + getURLParameter("lawid")).length;
					$("#law-type").val(isActive);
					if(isActive){
						$(".section-wrapper .current-results p").html("Final Results")

					}


					// Hide Voting and Show Stats if Vote is Done
					if (checkedData['InEffect'] == true) {
						$('.law-discussion-textarea, .disc-btns').remove();
						$('.vote-wrapper').hide();
						$('.vote-subwrapper-2').addClass('vote-subwrapper-2-passed');
						$('#container-graph').addClass('container-1-passed');
						$('.law-vote-passed').addClass('law-vote-passed-pass');
						$('#law-reasoning-title p').text('Law In Effect');
						$('#law-discussion-wrapper').removeClass('section-alternating-color-2');
						$('#law-discussion-wrapper').addClass('section-alternating-color-1');
						$("#poll").hide();

					}
					// Show Voting and Stats if Vote is Not Done
					else if (checkedData['InEffect'] == false) {
						$('.vote-wrapper').show();
						$('.vote-subwrapper-2').removeClass('vote-subwrapper-2-passed');
						$('#container-graph').removeClass('container-1-passed');
						$('.law-vote-passed').removeClass('law-vote-passed-pass');
						$('#law-discussion-wrapper').show();
					}

					// Display user's current vote
					var vote = null;


					//Cancel Law
					 var user = localStorage.getItem('username');
					 var auth = localStorage.getItem('authtoken');

					if( $.inArray(user, checkedData.VotedYes) != -1){
						$("#vote-no").prop('disabled', false);
						$(".law-yourvote-no").hide();
						$("#vote-yes").prop('disabled', true);
						$(".law-yourvote-yes").fadeIn();
					}else if( $.inArray(user, checkedData.VotedNo) != -1 ){
						$("#vote-yes").prop('disabled', false);
						$(".law-yourvote-yes").hide();
						$("#vote-no").prop('disabled', true);
						$(".law-yourvote-no").fadeIn();
					}

						$('.vote-wrapper #vote-list .vote-list-item').remove();
						var content = $("<div id='fancy-vote-list'> <center> <h2> Vote List  </h2> </center> </div>");
						/*$(document).on('click', '#list-of-voters > a', function(){
							$.fancybox({
								'content' : content,
								'width' : 600,
								'height' : 600,
								'autoScale': false,
			                    'autoDimensions': false,
								'type' : 'iframe',
								openEffect: 'none',
					        	closeEffect: 'none'
							});
						});*/
						var votedNo = $("<div id='fancy-voted-no'> <h3> Voted No </h3> </div>");
						var votedYes = $("<div id='fancy-voted-yes'><h3> Voted Yes </h3> </div>");

						for(i=0; i<checkedData.VotedNo.length; i++)
						{
							votedNo.append('<p class="vote-list-item">'+checkedData.VotedNo[i]+'</p>');
						}
						for(i=0; i<checkedData.VotedYes.length; i++)
						{
							votedYes.append('<p class="vote-list-item">'+checkedData.VotedYes[i]+'</p>');
						}

						content.append(votedNo);
						content.append(votedYes);

						/*for(i=0; i<checkedData.VotedNo.length; i++)
						{
							$('.vote-wrapper #vote-list #voted-no').append('<p class="vote-list-item">'+checkedData.VotedNo[i]+',</p>');
						}

						for(i=0; i<checkedData.VotedYes.length; i++)
						{
							$('.vote-wrapper #vote-list #voted-yes').append('<p class="vote-list-item">'+checkedData.VotedYes[i]+',</p>');
						}*/

					//Conflicting laws
					$('.law-conflict').empty().removeClass( "law-vote-passed-pass" );
					if(checkedData['ExistingLawGuid'] && !checkedData['InEffect']){
						$('.law-conflict').addClass( "law-vote-passed-pass" ); //make visible
						$('.law-conflict').append("If passed, this law will replace <a href='?authtoken=" + auth + "&lawid="+checkedData['ExistingLawGuid']+"'> "+ checkedData['ExistingLawTitle'] +" </a>");
					}

					var button = $('.cancel-law');
					if (button.length==0) {			
						if(checkedData.ProposingUser == user && !checkedData.InEffect){
							$('.section-law-info .cancel-law').remove();
							$('.section-law-info').append("<button class='cancel-law global-button global-button-small'><span class='localize' translate-key='128' >Delete Proposal</span></button>");
						}
					}

					if( checkedData.VotesYes + checkedData.VotesNo > 1 ) {
						var button = $('.section-law-info .cancel-law.global-button.global-button-small');
						$.ajax({
							url : APPIQUERYURL + "/isadmin/"+user,
							type : 'GET',
							success: function(response){
								if( !response ) {
									$('.section-law-info .cancel-law.global-button.global-button-small').hide();
								}
							}
						})
					}

					if(isActive){
						$(".button-vote").prop("disabled", true);
					}

					//Cancel Query
					$('#page-wrapper').on('click', '.cancel-law', function(){
						$.ajax({
							type: 'POST',
							url: APPIQUERYURL+"/api/v1/laws/cancellaw?authtoken="+ auth +"&guid=" + currentLawID,
							success: function(r){
								location.href = location.pathname;
							},
							error: function(err){
								variables.showError(err);
							}
						})
					});
				}

}

	/* counter - a function to create a countdown  */

	function counter(t)
	{
		if(LASTINTERVALID)
			clearInterval(LASTINTERVALID);

		// t is the exact number of seconds which will be given by a query.

		if(t < 0 && $("#law-type").val() != "0"){
			return false;
		}

		/* converting t into hours and minutes */
		if(t < 0)
			t = -parseInt(t);

		var hours = parseInt(t / 3600);

		var minutes = parseInt( ((t -  hours*3600) / 60) );

		var seconds =  parseInt( ( ( ( t - hours*3600 ) - minutes*60 ) ) );

		function counterDisplay () {
			var displayhours = " " + hours + " ";
			var displayminutes = " " + minutes + " ";
			var displayseconds = " " + ("00" + seconds).slice (-2) + " "; // add leading 0 when under 10 -> 09, 08, 07..

			if(hours <= 0){
				displayhours = '';
			}
			if(minutes <= 0) {
				displayminutes = '';
			}

			$('.hours').empty().append(displayhours);
			$('.minutes').empty().append(displayminutes);
			$('.seconds').empty().append(displayseconds);
			$('.counter').addClass('law-vote-passed-pass');

		} counterDisplay ()


		/* update counter every second */

		var interval = setInterval(function(){

			seconds = seconds - 1;

			/* refresh the page when the countdown ends */

			if(hours <= 0 && minutes <= 0 && seconds <= 0){
				$('.counter').empty().fadeOut();
				clearInterval(interval);
				LASTINTERVALID = null;
				reload_page();
			}else{

				/* updating minutes */

				if(seconds <= 0){
					if(minutes >= 0){
						minutes = minutes - 1;
						seconds = 59;
					}
					else if(hours >= 0){
						hours = hours - 1;
						minutes = 59;
						seconds = 59
					}else{
						reload_page();
					}

				}

				counterDisplay ()
			}

		}, 1000);

		LASTINTERVALID = interval;
	}
