;(function(){
var Law = function(id){

	/* object fields */

	/* @type {string} introduces the unique id of the current LAW  */
	this.currentLawID = id;


	/* @type {string} introduces the username if it exists  */
	this.username = localStorage.getItem("username");

	/* @type {string} introduces the authtoken if it exists  */
	this.authtoken = localStorage.getItem("authtoken");

	/* @type {bool} introduces the Law if it in Effect  */
	this.inEffect = false;

	/* @type {object} all data */
	this.allComments = [];

	/* @type {object} all data */
	this.allLaws = [];

	var law = this;

	/* METHODS */

	/**
	* get all Laws
	*/
	this.getAllLaw = function(callback,update){
		update = update || false;
		var url = APPIQUERYURL + '/api/v1/laws?authtoken=' + law.authtoken,
			_this = this;

		if(Object.keys(_this.allLaws).length && !update){
			callback(_this.allLaws);
		}else{
			$.ajax({
				url : url,
				type : "get",
				async: false,
				success : function(data) {
					// saving election data, because not to do ajax second time
					_this.allLaws = data;
					callback(data);
				}
			})
		}
	}

	/**
	* get all comments
	*/
	this.getAllComments = function(callback,update){
		var url = APPIQUERYURL + '/api/v1/laws/getcomments?lawguid=' + law.currentLawID + '&authtoken=' + law.authtoken,
			_this = this;
		if(Object.keys(_this.allComments).length && !update){
			callback(_this.allComments);
		}else{
			$.ajax({
				url : url,
				type : "get",
				async: false,
				success : function(data) {
					// saving comments data, because not to do ajax second time
					_this.allComments = data;
					callback(data);
				}
			})
		}
	}

	/**
	* Opens a law page depending on the @currentLawID
	*/
	this.loadLawPage = function(){

		var lawID = law.currentLawID;
	    var data = {
	        "id": lawID
	    };
	    data = $.param(data);

	    //Hide previous votes
	    $(".law-yourvote-no").hide();

	    $(".law-yourvote-yes").hide();

	    $("#vote-no").prop('disabled', false);

	    $("#vote-yes").prop('disabled', false);

		//Hide instructions
		$('#laws-page-instructions-wrapper').hide();

	    //Get Data For Law via ID
	    // console.log(this.currentLawID)
	    // $("a#"+this.currentLawID).parent().addClass("menu-highlight");
	    lawspage(this.currentLawID,this,Law);
	}

	/**
	* Removing comment
	*/
	this.removeComment = function(comment){

		comment = JSON.stringify(comment);

		var data = { Username: law.username, Text: comment };

		var url = `${APPIQUERYURL}/api/v1/laws/removecomment?authtoken=${law.authtoken}&lawguid=${law.currentLawID}`;

		$.ajax({
			url: url,
			type: 'POST',
			data: data,
			success: function(response){
				law.GetComments(true);
			}
		})
	}

	/**
	* Gets the comments depending on the @currentLawID
	*/
	this.GetComments = function(type){
			type = type || false;
	        this.getAllComments(getComments,type);
	        function getComments(data){
            $('#law-discussion-wrapper-2').empty(); // clear as user opens new law
            //if there're no comments hide the comment section
            if(!data.length){
				if(law.inEffect){
					$('#law-discussion-wrapper').hide();
					$('.law-wrapper').addClass('no-discussion');
				}
				return false;
            }

            if(ECO.MINIMAP.initialized)
            	ECO.MINIMAP.removeDeadViews();

            for (var i = 0; i < data.length; i++) {
            	var isEdited;

            	var comment = data[i].Text;
            	// hack for fixing old format comments
            	if (comment[0] === '"')
            		comment = comment.substring(1, comment.length - 1).replace(/<br>/g, "\n");
                //getting days, hours and minutes from timestamp value of query
                var timestamp = data[i].Timestamp;
				var commentTime = "<span class='localize' translate-key='146'>Posted on</span>  " + ConvertSecondsComments(timestamp);

                //editing and removing comments
                var editComment = "";
                var removeComment = "";

                if(law.username == data[i].Username){
					removeComment = "<a id='removeComment' href='javascript:void(0)'><i class='fa fa-times'></i> remove </a>";
                }

                comment = comment.trim();

				text = $("<div class='law-discussion-wrapper'></div>");
				var section = $("<div class='comment-section1'> </div>");
				section.append("<div class='law-discussion-user'>" + data[i].Username + "<span class='law-discussion-time'>" + commentTime +((data[i].IsEdited) ? "&nbsp;<i>(edited)</i>" : "")+"</span></div>");
				section.append($("<div class='law-discussion-comment'></div>").text(comment));
				text.append(section);
				 // $("<div class='law-discussion-user'>" + data[i].Username + "<span class='law-discussion-time'>" + commentTime + "</span></div><div class='law-discussion-comment'>" + comment + "</div>");

				 var graphs = "";

				 if (data[i].Graph && data[i].Graph.Keys != '') {

					graphs = {'categories': data[i].Graph.Keys, 'time_start': data[i].Graph.TimeMin, 'time_end': data[i].Graph.TimeMax };

					graphs = JSON.stringify(graphs);

                    section.after("<div class='discussion-graph' id='discussion-graph-" + i + "'></div>");
                }


                var view = null;
				if(data[i].Map != null && data[i].Map != "") {
					// var viewer = $('<div class="view-map" id=map-'+i+'"><p id="map-position-text"></p><p id="map-info-text">.</p></div>');
					var viewer = $('<div class="view-map" id=map-'+i+'"></div>');
					section.after(viewer);

					view = viewer[0];
					view.settings = data[i].Map;

					//Start to displaying time of display maps
					//test = '<div class="testtime">'+view.settings.currentTime+'</div>';
					//text.append(test);

					// view.OnPositionUpdated = function(pos) {
					// 	view.children[0].innerText = pos.x + ", " + pos.y;
					// };
					// view.OnInfoUpdated = function(text) {
					// 	view.children[1].innerText = text;
					// }
				}

				editComment = "<a data-graph='"+ graphs +"' id='editComment' href='javascript:void(0)'><i class='fa fa-edit'></i> edit </a>";

                text.append(removeComment);
                text.append(editComment);

                $('#law-discussion-wrapper-2').prepend(text);

                if (data[i].Graph && data[i].Graph.Keys != '') {

                    getDiscussionGraphData('discussion-graph-' + i, data[i].Graph.Keys, data[i].Graph.TimeMin, data[i].Graph.TimeMax);

                }

				if(view && ECO.MINIMAP.initialized) {
					ECO.MINIMAP.addView(view);
				}
            }
        }
        // })
	}

	/**
	* Adding a new comment
	*/
	this.addComment = function(comment, isEdited){
		if(!comment){
			comment = escapeHtml($('#comment').val())
			comment = JSON.stringify(comment);
		}
		var data = {
			'Guid': law.currentLawID,
			'Username': law.username,
			'Text': comment,
		};

		if(isEdited){
			data.IsEdited = true;
		}

		if(this.username == null || this.authtoken == null){
			variables.showError("To add comment you need to access from the game server");
			return false;
		}

		if(this.inEffect){
            variables.showError("You can't add comment for Proposed Law.")
			return false;
		}

		if( $('#law-add-graph #graph-data').length && $('#law-add-graph #graph-data').val() != '' &&  $('#law-add-graph #graph-data').val() != null /* && $('.fancy-add-graph').is(":hidden") */ ){

			var lawAddGraph = JSON.parse($('#law-add-graph #graph-data').val());

			 data.Graph  = {
				Keys: lawAddGraph.variables,
				TimeMin: lawAddGraph.time_start,
				TimeMax: lawAddGraph.time_end,
				Title: lawAddGraph.title
			};
		}

		if( $("#main-map").css('display') != 'none' &&  $('#map-data').length && $('#map-data').val() != '' &&  $('#map-data').val() != null /* && $('.fancy-add-graph').is(":hidden") */ ){
			data.Map  = JSON.parse($('#map-data').val());
		}

		if ( ($('#comment').val() != null && $('#comment').val() != "") || comment != null) {
			var url = APPIQUERYURL + '/api/v1/laws/addcomment?authtoken=' + law.authtoken + '&lawguid=' + law.currentLawID;
			$.post(url, data, function(r) {

				// location.reload();
				location.href = '';

			}).fail(function(error) {
				if(error){
					variables.showError(error);
				}
			});
		}
	}

	/**
	* Adding a comment
	*/
	// text = escapeHtml(text);
function escapeHtml(text) {
  return text
      .replace(/&/g, "&amp;")
      .replace(/</g, "&lt;")
      .replace(/>/g, "&gt;")
      .replace(/"/g, "&quot;")
      .replace(/'/g, "&#039;")
      .replace(/\n/g,"<br>");
}
	this.pushComment = function(){

		//if comment is empty show error
		if ($('#comment').val() == "") {

			$("html, body").animate({
				scrollTop: 0
			}, "slow");

			$('#status').empty().html('Your comment is empty.').fadeIn();

			return false;
		}

		if(this.inEffect){
            variables.showError("You can't add comment for Proposed Law.")
			return false;
		}

		//check if current law wasn't deleted and add comment
		this.getAllLaw(pushComment);
		function pushComment(data){
			$('.remove-graph').fadeOut();
			$('.remove-map').fadeOut();

			$('.fancy-add-graph').attr('data-action', 'add');
			$('.fancy-add-map').attr('data-action', 'add');


			var cycleLength = data.length;
			for (var i = 0; i < cycleLength; i++) {

				//if current law wasn't deleted then add a comment

				if (data[i].Guid == law.currentLawID) {

					law.addComment();

					break;
				}
			}
		}
	}

	/**
	* Sending a vote
	*/
	this.sendVote = function(vote){

		//Check if yes or no vote
		var currentLawID = law.currentLawID;

	    var url = APPIQUERYURL + "/api/v1/laws/" + currentLawID + "/vote/" + ((vote == 0) ? 'true' : 'false') + "?authtoken=" + law.authtoken;

	    //Prep data to send
	    var lawID = currentLawID;

	    //Get Data For Law Based on ID
	    if (law.username == '' || law.username == 'none' || law.username == null) {
	        $("html, body").animate({
	            scrollTop: 0
	        }, "slow");

	        $('#status').empty().html('To vote you must access this law from the game server.').fadeIn();

	        return false;
	    }

	    $.ajax({
	        type: "POST",
	        dataType: "json",
	        url: url,
	        //data: data,
	        success: function(data) {

	            if (!data.hasOwnProperty('reason')) {

	                //Update Voting Graph
	                var votes = [
	                    ['yes', data['votesYes']],
	                    ['no', data['votesNo']]
	                ];

	                requestData(votes);

	                //Update law page
	                //onUpdateLawsDisplay;

	                if (vote == 0) {

	                    //when a user voted YES:
	                    //Disable vote button that user already voted

	                    $("#vote-no").prop('disabled', false);

	                    $(".law-yourvote-no").hide();

	                    $("#vote-yes").prop('disabled', true);

	                    $(".law-yourvote-yes").fadeIn();

	                } else if (vote == 1) {

	                    //when a user voted NO:
	                    //Disable vote button that user already voted

	                    $("#vote-yes").prop('disabled', false);

	                    $(".law-yourvote-yes").hide();

	                    $("#vote-no").prop('disabled', true);

	                    $(".law-yourvote-no").fadeIn();
	                }
	            }
	            lawspage(lawID,null,Law,action='vote');
	        },
	        error: function(a, b, c) {

	            var error = JSON.parse(a);

	            $("html, body").animate({
	                scrollTop: 0
	            }, "slow");

	            $('#status').empty().html(error).fadeIn();
	        }
	    })
	}

	/**
	* Updating vote results
	*/
	this.updateVoteResult = function(){

		this.getAllLaw(updateVoteResult,true);
		function updateVoteResult(data){

    		/* getting current law data */

    		var current_guid = null;
    		var cycleLength = data.length;
			for (var i = 0; i < cycleLength; i++) {

				//if current law wasn't deleted then add a comment
				if (data[i].guid == law.currentLawID) {

					current_guid = data[i];

					break;
				}
			}

			if(current_guid != null){

				/* updating graph */

				var votes = [
                    ['yes', current_guid['votesYes']],
                    ['no', current_guid['votesNo']]
                ];

                requestData(votes);

                /* updating vote list  */
                $('#fancy-vote-list p').remove();

                $('.vote-wrapper #vote-list .vote-list-item').remove();

                for(var i = 0; i < current_guid['votesNo']; i++)
                {
                	$('.vote-wrapper #vote-list #voted-no, #fancy-vote-list #fancy-voted-no').append("<p class='vote-list-item'>" + current_guid.votedNo[i] + "</p>");
                }
                for(var i = 0; i < current_guid['votesYes']; i++)
                {
                	$('.vote-wrapper #vote-list #voted-yes, #fancy-vote-list #fancy-voted-yes').append("<p class='vote-list-item'>" + current_guid.votedYes[i] + "</p>");
                }
			}
		}
	}

 }

 function bindEvents(){

 	//have laws and elections link pass url parameters
	var ignore = (getURLParameter('ignoretownhall') == "true" ? "&ignoretownhall=true" : "");
	var username = localStorage.getItem("username");
	var authtoken = localStorage.getItem('authtoken');
	var laws;
	var elections;
	var law;

	//handling law click event from dropdown menu
	$(document).on('click', '#proposed-laws ul a, #active-laws ul a', function(){
		var guid = $(this).attr('id');
		law = new Law(guid);
		law.loadLawPage();
		// var law = new Law(guid);
		// lawspage(guid);
	})

	//On Vote button click
	$(document).on('click', '.button-vote', function(){
		if($(this).attr('id') == 'vote-yes'){
			law.sendVote(0);
			 // location.reload(); //temporary til graph live-update is fixed
		}else{
			law.sendVote(1);
			 // location.reload(); //temporary til graph live-update is fixed
		}
	})

	//remove Comments
	$(document).on("click", "#removeComment", function(){

		var content = $(this).parent().find('.law-discussion-comment').html();

		law.removeComment(content);

	})

	$(document).on("click", ".toggle_prposed_district", function(){
		if( $(this).html() == "Active Districts" ) {
			$(this).html("Proposed Districts");
			$(".proposed-districts").show();
			$(".current-districts").hide();
			//variables.LoadProposedDistricts(lawspage.propDist);
			variables.setLayer('ProposedDistricts');
		} else {
			$(this).html("Active Districts");
			$(".proposed-districts").hide();
			$(".current-districts").show();
			variables.setLayer('Districts');

		}
	})

	//edit Comments
	var commenttext;
	$(document).on("click", "#editComment", function(){

		$('.comment-graph').remove();

		$('#graph-data').val('');

		commenttext = $(this).parent().find('.law-discussion-comment').html();

		$('#comment').val(commenttext);

		window.scrollTo(0,document.body.scrollHeight);

		var graphs = $(this).attr('data-graph');

		if(graphs){

			$('#graph-data').val(graphs);

			$('.disc-btns').hide();

			$('.edit-comment-buttons').show();

			// $('.election-comment').css('margin-right', '10px');

			$('.comment-graph').remove();

			$('.edit-comment-buttons #removeGraph').fadeIn();

			var composer = $("<iframe class='comment-graph' id='graph_composer' src='graph_composer/index.html'> </iframe>");

			$("#comment").before(composer);

			$('.comment-graph').fadeIn();

		}else{

			$('.disc-btns, #removeGraph').hide();
			$('.edit-comment-buttons').show();
		}

		// removing graph
		$(document).on('click', '.edit-comment-buttons #cancel-edit', function(){
			$('.comment-graph').remove();
			$('#comment').val('');
			$('#graph-data').val('');
			$('.edit-comment-buttons').hide();
			$('.disc-btns').show();
		})
	})

	//submit
	$(document).on('click', '.edit-comment-buttons #submit', function(){

		law.removeComment(commenttext);

		var comment = $('#comment').val();

		setTimeout(function(){

			law.addComment(comment, true);

		}, 1000);

		$('#comment').val('');
		$('.comment-graph').remove();
		$('.edit-comment-buttons').hide();
		$('.disc-btns').show();
	})

	//adding comments
	$(document).on('click', '.fancy-add-graph', function(){

		var composer = $("<iframe	 class='comment-graph' id='graph_composer' src='graph_composer/index.html'> </iframe>");

		if($('iframe.comment-graph').length > 0){
			$('.iframe.comment-graph').remove();
		}
		$(".law-discussion-textarea, #law-add-reasoning").before(composer);
		$(this).hide();

		composer.fadeIn();
		$('.remove-graph').css({'display': 'inline-block', 'margin-right': '17px'}).fadeIn();

		setTimeout(function() {
				var elem = $("#graph_composer");
				elem = elem.offset()
				$('html, body').animate({scrollTop: elem.top - 200 });
		},300)
	});

/*
 	$('#container-graph').before("<div id='list-of-voters' style='float: left;'> <a href='#'> List Of Voters </a> </div>");*/

	/* default chart type is line*/
	localStorage.setItem('graph-type','line');

	$('.remove-graph.global-button').css('display','none');


	//if lawid is a url parameter then open that page
	// $(window).load(function() {
		if (getURLParameter('lawid') != null) {

			law = new Law(getURLParameter('lawid'));
			law.loadLawPage();

		}
	// });

 	/* Maybe Depreciated
	$(document).on("click", '#laws-wrapper ul a', function() {

		var lawid = $(this).attr('id');

		law = new Law(lawid);

		law.loadLawPage();

	});
	*/


    /* hiding 'add-graph' button when the graph is selected */
    $(document).on('click','.fancy-save', function(){

    	$('.fancy-add-graph').attr('data-save','yes');

		if ( $('#graph-data').val()) {

				$('.remove-graph.global-button').show();

				$('.fancy-add-graph').html('Edit graph');

				$('.fancy-add-graph').attr('data-action', 'edit');

				savebutton = 1;
		}
    });

     /* when comment sent "edit graph" button becomes "add graph" */
    $(document).on('click', '#pushComment', function(){

		if($('#map-toggle-2d').val() == '3D'){
			$('#map-toggle-2d').trigger("click");
		}
		$('.fancy-add-graph').html('Add graph');

		law.pushComment();

    })

    /* Removing graph */
    $(document).on('click', '.remove-graph', function(){
		$(this).hide();

    	// $('.fancy-add-graph').html('Add Graph');

    	$('.fancy-add-graph').attr('data-action', 'add');

    	$('#graph-data').val('');

    	$('#graph_composer').remove();

		$('.fancy-add-graph').show();
    })

    /* updating vote list and vote */
    setInterval(function(){

    	if(typeof law != 'undefined'){
			law.updateVoteResult();
    	}
    }, 60000);
 }
 $(document).ready(function() {
 	bindEvents();
 });
})()

 function getURLParameter(name) {
	  	return decodeURIComponent((new RegExp('[?|&]' + name + '=' + '([^&;]+?)(&|#|;|$)').exec(location.search)||[,""])[1].replace(/\+/g, '%20'))||null
}

