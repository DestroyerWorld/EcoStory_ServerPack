(function(){
	var Election = function(){

	/* @type string - introducses username value in the url */
	this.username = localStorage.getItem("username");

	/* @type string - introduces the authtoken value in the url */
	this.authtoken = localStorage.getItem("authtoken");

	/* @type bool - shows if there's an election running */
	this.runningElection = false

	/* @type string - url parameter for disabling Town Hall blocking mode */
	this.ignoretownhall = getURLParameter('ignoretownhall');

	/* @type object - settings for elections pages */
	this.settings = {
						availableCommentPages : ['current']
					};

	/* @type object or array - election data from ajax request */
	this.electionData = Array();

	/* @type object or array - election data from ajax request */
	this.currentElectionData = {};

	/* @type object or array - vote data from ajax request */
	this.voteData = Array();

	/* @type object or array - info data from ajax request */
	this.infoData = Array();

	this.election = this;

	/* METHODS */

	/**
	* showing Error messages
	*/
	this.showError = function(message, type){
		if(typeof type == "undefined"){
            $('.error').css('background-color', '#D4784B');
			variables.showError(message);
		}else if(type == "success"){
			$('.error').empty().html(message).css('background-color', '#61AF61').fadeIn();
			$('html,body').animate({
				scrollTop: '0'
			}, "slow");
		}
	}

	/**
	* get all election by type(previous,current)
	*/
	this.getElection = function(type, callback, update){
		type = type || 'current';
		update = update || false;
		var url = APPIQUERYURL + "/api/v1/elections/" + type,
			_this = this;
		if(Object.keys(_this.electionData).length && !update){
			callback(_this.electionData);
		}else{
			$.ajax({
				url : url,
				type : "get",
				async: false,
				success : function(data) {
					// saving election data, because not to do ajax second time
					_this.electionData = data;
					if (data.Comments) {
                        if (data.Comments.length==0) {
                            $('#graph_composer').remove()
                        }
                    }

					callback(data);
				}
			})
		}
	}

	/**
	* get current election
	*/
	this.getCurrentElection = function(callback,update){
		update = update || false;
		var url = APPIQUERYURL + "/api/v1/elections/current",
			_this = this;

		if(Object.keys(_this.currentElectionData).length && !update){
			callback(_this.currentElectionData);
		}else{
			$.ajax({
				url : url,
				type : "get",
				async: false,
				success : function(data) {
					// saving election data, because not to do ajax second time
					_this.currentElectionData = data;
					callback(data);
				}
			})
		}
	}

	/**
	* get all vote by guid
	*/
	this.getVote = function(guid, callback, update){
		update = update || false;
		var url = APPIQUERYURL + "/api/v1/elections/votes" + ((typeof guid != typeof undefined) ? "?guid=" + guid : "");
			_this = this;

		if(Object.keys(_this.voteData).length && !update){
			callback(_this.voteData);
		}else{
			$.ajax({
				url : url,
				type : "get",
				async: false,
				success : function(data) {
					// saving vote data, because not to do ajax second time
					_this.voteData = data;
					callback(data);
				}
			})
		}
	}

	/**
	* get info
	*/
	this.getInfo = function(callback, update){
		var url = APPIQUERYURL + "/info",
			_this = this;

		if(Object.keys(_this.infoData).length && !update){
			callback(_this.infoData);
		}else{
			$.ajax({
				url : url,
				type : "get",
				async: false,
				success : function(data) {
					// saving info data, because not to do ajax second time
					_this.infoData = data;
					callback(data);
				}
			})
		}
	}

	/**
	* get all comments
	*/
	this.getAllComments = function(guid,update){
		update = update || false;
		if(currentPage != "elections"){
			return false;
		}
		var type = (typeof guid != typeof undefined) ? "previous" : "current";

		this.getElection(type,updateComment,update);

        if(ECO.MINIMAP.initialized)
        	ECO.MINIMAP.removeDeadViews();

		function updateComment(data){
			if(typeof guid != typeof undefined){
				for(var i = 0; i < data.length; i++)
				{
					if(data[i].Guid == guid){
						data = data[i];
						break;
					}
				}
			}
			if( (!data.Comments.length) && (typeof guid != typeof undefined) ){
				$(".election-discussion").hide();
			}else{
				$(".election-discussion").show();
			}

			$('.election-discussion .discussion-user').empty();
			if(data instanceof Array){
				data = data[0];
			}
			var cycleLength = data.Comments.length;
			for(var i = 0; i < cycleLength; i++)
			{
				var comment;

				var isEdited;
				data.Comments[i].Text = data.Comments[i].Text
				.replace(/\\{1,2}n/g, "<br>").replace(/\\"/g, '&quot').replace(/^["]|"$|\\/g, "");

				//checking if return text is valid json text
				if (/^[\],:{}\s]*$/.test(data.Comments[i].Text.replace(/\\["\\\/bfnrtu]/g, '@').
				replace(/"[^"\\\n\r]*"|true|false|null|-?\d+(?:\.\d*)?(?:[eE][+\-]?\d+)?/g, ']').
				replace(/(?:^|:|,)(?:\s*\[)+/g, ''))) {

					comment = JSON.parse(data.Comments[i].Text);

					// Editing feature to do
				//isEdited = JSON.parse(data.Comments[i].IsEdited);

				}else{

				  comment = data.Comments[i].Text;

				  // Editing feature to do
				//isEdited = data.Comments[i].IsEdited;

				}
				while(comment.indexOf("\n") >= 0){

					comment = comment.replace("\n", "<br>");

				}
				//getting days, hours and minutes from timestamp value of query
				var timestamp = data.Comments[i].Timestamp;

				var commentTime = "Posted on  " + ConvertSecondsComments(timestamp);

				text = $("<div class='law-discussion-wrapper'></div>");

				var section1 = $("<div class='comment-section1'> </div>");

				//checking if a user can remove comment
				var removeComment= "";

				var editComment = "";

				var hasgraph = (data.Comments[i].Graph && data.Comments[i].Graph.Keys != '') ? true : false;

				//including removeGraph and editgraph buttons
				if(data.Comments[i].Username == election.username){
					removeComment = "<a data-graph='"+ hasgraph +"' id='removeComment' href='javascript:void(0)'><i class='fa fa-times'></i> remove </a>";

					var graphs = "";

					if(hasgraph){
						graphs = {'categories': data.Comments[i].Graph.Keys, 'time_start': data.Comments[i].Graph.TimeMin, 'time_end': data.Comments[i].Graph.TimeMax };

						graphs = JSON.stringify(graphs);
					}

					editComment = "<a data-graph='"+ graphs +"' id='editComment' href='javascript:void(0)'><i class='fa fa-edit'></i> edit </a>";

				}

				section1.append("<div class='law-discussion-user'>" + data.Comments[i].Username + "<span class='law-discussion-time'>" + commentTime + ( isEdited ? "<i style='font-size:13x;'>&nbsp;(edited)</i>" : "" ) + "</span></div><div class='law-discussion-comment'>" + comment + "</div>");
				text.append(section1);

				var view = null;

				if(data.Comments[i].Map != null && data.Comments[i].Map != "") {
					var viewer = $('</div><div class="view-map" id=map-'+data.Guid+'"></div>');
					section1.after(viewer);

					view = viewer[0];

					view.settings = data.Comments[i].Map;
				}

				if (data.Comments[i].Graph && data.Comments[i].Graph.Keys != '') {

					text .append("<div class='discussion-graph' id='discussion-graph-" + i + "'></div>");
				}

				//Editing and removing comments on hold
				//text.append(removeComment);
				//text.append(editComment);
				$('.election-discussion .discussion-user').prepend(text);

				if (data.Comments[i].Graph && data.Comments[i].Graph.Keys != '') {
					getDiscussionGraphData('discussion-graph-' + i, data.Comments[i].Graph.Keys, data.Comments[i].Graph.TimeMin, data.Comments[i].Graph.TimeMax);
				}

				if(view && ECO.MINIMAP.initialized) {
					ECO.MINIMAP.addView(view);
				}
			}

			$(".current-rankings").hide();
		}
	}

	this.removeComment = function(comment, hasgraph){

		comment = JSON.stringify(comment);

		var data = { Username: election.username, Text: comment };


		var url = APPIQUERYURL + "/api/v1/elections/removecomment?authtoken=" + election.authtoken;
		$.ajax({
			url: url,
			type: 'POST',
			data: data,
			success: function(response){
				election.getAllComments(undefined,true);
			}
		})
	}

	/**
	* adding a comment
	*/
	this.addComment = function(comment, isEdited){

		if(typeof comment == typeof undefined){
			comment = $('.election-discussion #comment').val();
		}

		if(election.username == null || election.authtoken == null){
			election.showError("To add comment you need to access from the game server");
			return false;
		}

		if(comment == ""){
			election.showError("The comment cannot be empty");
			return false;
		}

		if(this.settings['availableCommentPages'].indexOf( getURLParameter("election") ) < 0){
			election.showError("You can't add comment for Previous Election.");
			return false;
		}

		$('.error').empty().fadeOut();

		if(comment){

			comment = JSON.stringify(comment);
			
			comment = comment.replace(/\\n/, "<br>");
			var url = APPIQUERYURL + "/api/v1/elections/addComment?authtoken="+election.authtoken;

			var data = { 'Username': election.username, 'Text': comment, 'IsEdited': isEdited };

			if($('#graph-data').val().length > 0){

				var graph = JSON.parse($('#graph-data').val());

				graph.keys = graph.variables;

				graph.timeMin = graph.time_start;

				graph.timeMax = graph.time_end;

				delete graph.variables;

				delete graph.time_start;

				delete graph.time_end;

				data.Graph = graph;
			}

			if( $("#main-map").css('display') != 'none' && $('#map-data').length && $('#map-data').val() != '' &&  $('#map-data').val() != null /* && $('.fancy-add-graph').is(":hidden") */ ){
				data.Map  = JSON.parse($('#map-data').val());
			}

			$.ajax({
				type: 'POST',
				url: url,
				data: data,
				success: function(){
					location.reload();
					// election.getAllComments(undefined,true);

					// $('#graph-data').val('');

					// $('#addGraph').attr('data-action', 'add').html('Add Graph').fadeIn();

					// $('#removeGraph').fadeOut();
					// $('.remove-map').fadeOut();
					// $('#comment').val('');

					// $('.comment-graph').remove();
					// $('#graph_composer').remove();


					// $("#map-control-wrapper").hide();
					// $('.fancy-add-map').show();
				},
				error: function(error){
						this.showError(error);
				}
			})

		}
	}

	this.isElectionRunning = function(callback){
		$.get(APPIQUERYURL + "/api/v1/elections/iselectionrunning", function(data, status){

			var contin = true;
			if(status == "success"){
				if(data == true){
					if(callback){
						callback();
					}
					$(".current-election a").html("Current Election");
				}else{
					if(callback){
						//election.showError("No Election Running");
						contin = false;
					}
					$(".current-election").html("No Current Election").removeClass('current-election').addClass('menu-no-link').removeAttr("href");
				}
			}
			if(!contin)
				return false;
			//is town hall construted?
			$.get(APPIQUERYURL + "/api/v1/elections/townhallconstructed", function(data,status){
				if(election.ignoretownhall == "true"){
					data = true;
				}
				if(status == "success" && data == false && currentPage == "elections"){
					election.showError("You must build a town hall in order to hold an election", "success");
				}
			})
		})
	}

	/**
	*
	* Displays all speeches in the election
	* @param int  x - how many speeches to show
	*
	*/
	this.displayAllSpeeches = function(){

		var count = $('.vote-item').find('p.candidate-name .candidate-speech').length;

		var characters = 300; //show "see more" link when the speech is have more than # characters

		var item = null;

		// var isVoted = $(".candidate-name#" + election.username).length;
		// if(isVoted){
		// 	$("a#add-self").html('Update Speech').attr('data-action', 'update');
		// 	$('#election-propose-button').html('Update Speech').attr('data-action', 'update');

		// 	$('#remove-self').css('display', 'inline-block');
		// }
		if(count){

			$('.vote-item p.candidate-name .candidate-speech').each(function(){
				if( $(this).html().length >= characters ){

					item = $(this);

					//include "see more" link only once
					if(item.find('span').length != 0){
						return true;
					}

					// dividing speech into two parts
					var text = item.html();

					var p1 = text.substring(0, characters);

					var p2 = text.substring((characters), text.length);

					var user = item.parent().attr("data-id").replace(" ", "_");

					item.html('');

					// adding "see more" link
					item.append("<span data-user="+ user + " class='part1'>" + p1 + "</span>");

					item.append("<span class='read_more'>... <a> <i class='fa fa-fw fa-caret-down'></i>See More</a> </span>");

					item.append("<span style='display:none;' data-user=" + user +" class='part2'>" + p2 + "</span>");

					// when a user clicks on "see more"
					$(document).on('click', '.read_more', function(){

						/* making visible whole speech and including "see_less" link */
						var current = $(this).parents("p.candidate-name").attr('data-id').replace(" ", "_");

						$(this).remove();

						// $(this).parent().find(".see_less").remove();

						$('.results-page').fadeOut();

						$('.vote-page').css('width','100%');

						$('.candidate-speech p#' + current).find('.see_less').remove();

						$('span.part2[data-user='+current+']').fadeIn();

						$('span.part2[data-user='+current+']').after("<span class='see_less'> <a> <i class='fa fa-fw fa-caret-up'></i>See Less </a> </span>");


					})

					/* when a user clicks on "see less" */
					$(document).on('click', '.see_less', function(){

						/* Making only a part of speech visible and including "see_more" link */
						var current = $(this).parents("p.candidate-name").attr('data-id').replace(" ", "_");

						$(this).remove();

						$(this).parent().find(".read_more").remove();

						var count = $('.vote-item p .see_less').length;

						if(count == 0){

							$('.results-page').fadeIn();

							$('.vote-page').css('width','55%');

						}


						$(this).remove();

						$('span.part2[data-user='+current+']').fadeOut();

						$('.candidate-speech p#' + current).find('.read_more').remove();

						$('span.part1[data-user='+current+']').after("<span class='read_more'> ... <a><i class='fa fa-fw fa-caret-down'></i> See More </a> </span>").fadeIn();
					})
				}
			})
		}
	}
	/**
	* Updating current election
	*/
	this.updateCurrentElection = function(){

		$('#election-wrapper, .candidates-page').fadeOut();

		setTimeout(function(){

			election.getAllComments();

			election.showCandidates();

			$('.page:not(.results-page), #add-self, .law-discussion-textarea, .law-discussion-textarea + div, .info, .info + small, .vote-item+button ').fadeIn();

			$('#election-wrapper').fadeIn(); //?

			$('.vote-item p').each(function(){
				if($(this).id == election.username && typeof $(this).id != typeof undefined){
					$('#remove-self').fadeIn();
					return  false;
				}
			})
		}, 500)

	}

	/**
	* voting
	*/
	this.vote = function(){

		if(election.username == null || election.authtoken == null)
		{
			election.showError("To vote you must be logged in.")
			return false;
		}

		var RankedVotes = [];

		var data = [];

		var valid = true;

		var values = [];

		$('.vote select').each(function(){

			var x = $(this).find("option:selected");

			var user = $(this).parent().data('user');

			var value = x.val();
			if(value && value != 0){

				RankedVotes.push(user);

				data[value] = user;

				values.push(parseInt(value));

			}
		})
		var sending = [];
		data.forEach(function(item,ind){
			sending.push(item);
		})

		if(RankedVotes.length == 0){
			election.showError("No candidates were ranked.");
			return false;
		}

		$('.error').empty().fadeOut();

		var url = APPIQUERYURL + "/api/v1/elections/vote?authtoken=" + election.authtoken;

		var cycleLength = values.length;
		//if value is undefined then remove it from array
		for(var i = 0; i < cycleLength; i++)
		{
			if(typeof values[i] == typeof undefined ){
				values.splice(i, 1);
			}
		}

		//sort values to check if any vote's missing
		values.sort(function(a,b){
			return a-b;
		});

		//check if some number is missing
		var message = "The selection can't skip any number";
		var max = values[values.length-1];

		var s = sum(max);

		var arrSum = values.reduce(function(a,b){
			return a+b;
		})

		if(s != arrSum){
			valid = false;
		}

		if(!valid){
			election.showError(message);
			return false;
		}

		// console.log(sending); return false;
		RankedVotes = JSON.stringify(sending);

		//make an ajax request to send vote to the server
		var settings = {
			  "async": false,
			  "crossDomain": true,
			  "url": url,
			  "method": "POST",
			  "headers": {
			    "content-type": "application/json",
			    "cache-control": "no-cache"
			  },
			  "processData": false,
			  "data": "{\n    \"Voter\":\""+election.username+"\",\n    \"RankedVotes\":\ "+RankedVotes+"\n}"
			}

			// load the Election page when the ajax request is done
			$.ajax(settings).done(function (response) {
				location.reload();
			}).fail(function(err) {
				variables.showError(err);
			});
	}

	function updateInfo(data, guid){
		var isPrevious = $('.previous-election.active-election').length;

		// updating election info
				if(data instanceof Array){
					data = data[0];
				}
				if( data.Candidates.length != 0 ){

					var prevEnded = ConvertSeconds(data.TimeEndAgo);

					var info = "";

					if(isPrevious != 0 || typeof guid != typeof undefined){
						info += "<li><span class='localize' translate-key='153'>Ended</span>  " + prevEnded +" <span class='localize' translate-key='154'>ago</span></li>";
						info += "<li>" ;
						info += (data.Results.Winner != "") ? "<span class='localize' translate-key='159'>The winner was</span> " + data.Results.Winner + ".</li>" : "There was no winner.</li>";
					}

					if(data.Results.StatusMessage){
						info += "<li>"+data.Results.StatusMessage+"</li>";
					}

				}

				var names = [];

				data.Candidates.forEach(function(item,index){
					names.push(item.CandidateName);
				})
				$('#candidate-names').val(JSON.stringify(names));

				$('.current-rankings table tr:first-of-type td').html("<span class='localize' translate-key='134'>All Votes</span>");
				votersCount = data.TotalVotes;

				$('.candidate-speech p').remove();

				$('.vote .vote-item').remove();

				var res = (isPrevious > 0 || typeof guid != typeof undefined) ? "Final Results" : "Current Results";

				$('.section-title-results span').empty().append(res);

				info += "<li>" + data.TotalVotes + (data.TotalVotes==1 ? " <span class='localize' translate-key='148'>vote</span>": " <span class='localize' translate-key='149'>votes</span>")+" <span class='localize' translate-key='132'>total</span>. </li>";

				var winnerInfo

			/*	if(isPrevious == 0 && typeof guid == typeof undefined){
						info += (data.Results.Winner == "") ? "<li>Winner will become the world leader.</li>" : "<li>Winner will replace current leader: <span class='text-highlight'> " + data.Results.Winner + "</span>.</li>";
					}*/
				/* if(isPrevious == 0 && typeof guid == typeof undefined && data.Results.Result != 'Election ends in a tie. No winner.'){
					var leaderReplace = (data.Results.Winner == "") ? " and will become the world leader." : " and will replace " + data.Results.Winner + " as world leader.";
				} else { var leaderReplace = ''; }*/
				var leaderReplace = '.';


				if(isPrevious == 0 && typeof guid == typeof undefined) {
					info += "<li><b><span class='localize' translate-key='147'>Projected result:</span></b> "+ data.Results.Result + leaderReplace + "</li>";
				}

				$('.election-info').html(info);




				if(isPrevious == 0 && typeof guid == typeof undefined) {
					var info2 = "<li><span class='localize' translate-key='129'>Rank the candidates from best to worst.</span></li>";
				}

				$('.canidates-info').append(info2);
	}
	/**
	*  show candidates and their speeches
	*/
	this.showCandidates = function(guid){
		if(currentPage != "elections"){
			//return false;
		}

		var url = null;

		var isPrevious = $('.previous-election.active-election').length;
		//$('#elections-page-instructions-wrapper').hide();

		//disable selects when the previous page is loaded
		var active = "";

		if( isPrevious == 0 && typeof guid == typeof undefined){
			type = 'current'
		}else{
			type = 'previous';
			active = "disabled";
		}

		var rank = 0,
			candidates_count = 0,
			candidates_votes = [],
			votersCount = 0,
			_this = this;

		this.getElection(type,showCandidates);

		function showCandidates(data){
			if(typeof guid != typeof undefined || isPrevious > 0){
				//new ES6 syntax for searching item in array
				if(data.find(x => x.Guid == guid)){
					data = data.find(x => x.Guid == guid);
				}
			}

			if(guid && !isPrevious && Array.isArray(data)){
				if(!data.find(x => x.Guid == guid)){
					election.showError("Election does not exist");
					$('#election-wrapper').hide();
					$('#elections-page-instructions-wrapper').show();
					return false;
				}
			}
			election.runningElection = data;

			updateInfo(data, guid);

			//votes per round table
			var results = data.Results;

			var rounds = 0;
			var results = results.CandidateRounds;
			window.results = results;

			results = results.sort(function(a,b){
				a = a.VotesPerRound[a.VotesPerRound.length - 1];
				b = b.VotesPerRound[b.VotesPerRound.length - 1];
				return b-a;
			})

			// return false;
			var candidates_ordered_arr = [];
			results.forEach(function(item, index){
				if(item.VotesPerRound.length > rounds){
					rounds = item.VotesPerRound.length;
				}

				data.Candidates.forEach(function(elem,i){
					if(elem.CandidateName == item.Candidate){
						candidates_ordered_arr.push(elem);
					}
				})
			})
			data.Candidates	= candidates_ordered_arr;

			/*/styling table
			if(rounds <= 3){
				$('.votes-per-round table').css('width', "74%");
			}else if(rounds <= 5){
				$('.votes-per-round table').css('width', "85%");
			}else{
				$('.votes-per-round table').css('width', "100%");
			}*/

			//if no one has voted yet
			if(rounds == 0){
				$(".no-votes").show();
				$(".votes-per-round table, .current-rankings, .view-all-votes").css("display", "none");
			}else{
				$(".votes-per-round table").show();
				$(".no-votes").hide();
			}


			$('.votes-per-round table tr:not(.title-heading)').remove();

			$('.votes-per-round table tr:first-of-type td').attr('colspan', rounds + 1 );

			var row = $("<tr class='table-top'> <td class=' table-top-left'> <span class='localize' translate-key='25'>Candidates</span> </td> </tr>");

			for(var i = 0; i < rounds; i++ )
			{
				row.append("<td><span class='localize' translate-key='131'>Round</span>  "+(i+1)+"</td>");
			}
			$('.votes-per-round table').append(row);
			var candidates = data.Candidates;

			if(candidates.length == 0){
				return false;
			}

			candidates_count = candidates.length;

			$('.candidates-page .candidate-name').empty();
			var cycleLength = candidates.length;
			for(var i = 0; i < cycleLength; i++)
			{
				if(typeof candidates[i].Speech != typeof undefined){

					//appending candidate names into "votes per round" table
					var r = $("<tr class='table-middle' data-candidate = '"+candidates[i].CandidateName+"'> <td  class='table-left'> "+ candidates[i].CandidateName +" </td> </tr>");

					for(var td = 0; td < rounds; td++){

						r.append("<td class='round-vote' data-round = '"+ (td+1) +"'> </td>");

					}

					$('.votes-per-round table').append(r);

					var votingData = data.Results.CandidateRounds;

					var append = $("<p class='candidate-name' id='" + candidates[i].CandidateName + "'> <span> "+ candidates[i].CandidateName +":  </span> <label>"+decode(candidates[i].Speech)+"</label> </p>");


					var row = $("<div class='vote-item' data-user='"+ candidates[i].CandidateName +"'> </div>");

					var select = $("<select "+active+" > </select>");

					row.append(select);

					row.append("<p data-id='"+candidates[i].CandidateName.replace(" ","-")+"' class='candidate-name' id='"+candidates[i].CandidateName+"'><span class='candidate-name-title'> " + candidates[i].CandidateName + ":</span> <span class='candidate-speech'>"+ decode(candidates[i].Speech) +"</span></p>")

					$('.vote button').before(row);

					if(candidates[i].CandidateName == election.username && typeof guid == typeof undefined){
						$('a#add-self').hide();
						$('#add-self').html('update speech').attr('data-action', 'update');
						$('#election-propose-button').html('Update Speech').attr('data-action', 'update');
						$('#remove-self').css('display', 'inline-block');
					}
				}else{
					rank++;
				}
			}
			election.displayAllSpeeches();

			if( $('#candidate-names').length != 0 ){
				if(typeof guid != typeof undefined)
				{
					setupTable(guid);
				}
				else
				{
					setupTable();
				}
			}
			//updating data in votes per rounds table
			var novotes = 0;

			results.forEach(function(item, index){

				//getting each candidate votes
				var v = item.VotesPerRound;
				if(v.length == 0){
					novotes++;
				}

				$(".votes-per-round table tr[data-candidate='"+item.Candidate+"'] td").each(function(){

					var roundNumber = $(this).attr('data-round');

					if(typeof roundNumber != typeof undefined ){

						var count = (typeof v[roundNumber-1] != "undefined") ? v[roundNumber-1] : "X";

						$(this).html(count);

						//make X's red
						if(count == "X"){
							$(this).addClass('red');
						}

					}
				})
			})
			//making each column's high value bold
			for(var i = 1; i <= rounds; i++)
			{
				var max = 0;

				$(".votes-per-round table td[data-round="+i+"]").each(function(){

					if($(this).html() != "X"){
						var value = parseInt($(this).html())
						if(value > max){
							max = value;
						}
					}
				});

				$(".votes-per-round table td[data-round="+i+"]").each(function(){

					/*if($(this).html() == max){
						$(this).css('font-weight', 'bolder');
					}*/
				});


			}

			if(novotes < results.length){
				$('.results-page').fadeIn();
			}else{
				$('.results-page').fadeOut();
			}

			var total = $("<tr class='table-bottom'> <td> <span class='localize' translate-key='132'>Total</span></td> </tr>");

			for(var i = 0; i < rounds; i++)
			{
				var round = 0;

				$('.votes-per-round tr td').each(function(){
					if(typeof $(this).data('round') != "undefined")
					{
						if( $(this).data('round') == (i+1) && $(this).html() != "X" ){
							round += parseInt($(this).html());
						}
					}
				})

				total.append("<td data-total='"+ (i+1) +"'>"+round+"</td>");
			}

			$('.votes-per-round table').append(total);



			/* sorting the "votes per round" table */
			_this.getVote(guid,tableShow);

			function tableShow(data){
				//if user already voted
				data.forEach(function(item, index){
					if(item.Voter == election.username){
						$('.vote-button').attr('disabled', 'disabled').html('<span class="localize" translate-key="130">Update Vote</span>');
					}
				})
				var cycleLength = data.length;
				for(var i = 0; i < cycleLength; i++){
					var newCycleLength = data[i].RankedVotes.length;
					for(var j = 0; j < newCycleLength; j++)
					{
						$(".current-rankings table tr[data-voter='" + data[i].Voter + "'] td").each(function(){

							var user = $(this).data('user');

							if(typeof user != "undefined"){

								var rank = $.inArray(user, data[i].RankedVotes);

								if(rank == -1){

									$(this).html("");

								}else{
									rank = rank + 1;
									$(this).attr("data-value", rank);
									$(this).html(rank);
								}
							}
						})
					}
				}

				if(getURLParameter("election") != null && getURLParameter("election") != "current" )
				{
					if($("tr[data-voter='"+election.username+"']").length == 0 )
					{
						$(".vote-item select").hide();
					}
				}
			}

			$('.vote-item select option').remove();

			for(var j = 0 ; j <= candidates_count - rank; j++)
			{
				if(j == 0){
					$('.vote-item select').append("<option value='0'> - </option>");
				}else{
					$('.vote-item select').append("<option value='"+j+"'>"+j+"</option>");
				}
			}
		}
	}

	/**
	* remove yourself from the candidates list
	*/
	this.removeSelf = function(){
		var speech = $(".vote-item  p#" + election.username).html();

		var url = APPIQUERYURL + "/api/v1/elections/removecandidate?authtoken=" + election.authtoken + "&speech=" + speech;

		$.post(url, function(status, data){
			$('.current-election').trigger('click');
		})
	}

	/* loads previous elections pages */
	this.loadElectionPage = function(guid){
		var _this = this;

		// setTimeout(function(){

			$('#elections-page-instructions-wrapper').hide();

			if (guid != undefined) { // if not current election
				$(' #add-self, .vote p.info, #remove-self, .election-comment, .election-comment + div, .info + small, .vote-button').hide();
			}else{
				$('#add-self.eco-button,.vote-button').show();
			}
			election.showCandidates(guid);

			election.getAllComments(guid);

			$('#election-wrapper, #wrapper, .vote-page').show();

			$(".current-rankings").hide();

		// }, 500)
	}

	/* show the most recent election */
	this.showRecent = function(){
		var url = APPIQUERYURL + "/api/v1/elections/previous";
		$.get(url, function(data, status){
			if(status == "success"){
				var recent = data.sort(function(a,b){
					return a.TimeEndAgo-b.TimeEndAgo;
				})[0].Guid;

				election.loadElectionPage(recent);

			}
		})

	}

	// call setupTable funcion from election object
	this.setupTable = function(guid){
		setupTable(guid);
	}

}


 function getURLParameter(name) {
	  	return decodeURIComponent((new RegExp('[?|&]' + name + '=' + '([^&;]+?)(&|#|;|$)').exec(location.search)||[,""])[1].replace(/\+/g, '%20'))||null
}

/**
*  Setting up current rankings table
*/
function setupTable(guid){

	election.getVote(guid,setupTable);

	function setupTable(data){
		if( $('#candidate-names').length == 0  || !$('#candidate-names').val()){
			return false;
		}

		/* getting the candidates data to include it on the table */
		var candidates = JSON.parse($('#candidate-names').val());

		/* changing font size of the table depending on the data length */
		var fontSize = "20px";
		var w = "25%";

		if(candidates.length > 10){
			fontSize = "14px";
			w = "50%";
		}else if(candidates.length > 15){
			fontSize = "10px";
			w = "70%";
		}
		else if(candidates.length > 25){
			fontSize = "10px";
			w = "100%";
		}
		//$('.current-rankings table').css({"width": w, "font-size": fontSize });

		/* UPDATING THE TABLE*/

		/* first: removing all rows except heading */
		$('.current-rankings table tr:not(:first-child)').remove();

		var cycleLength = data.length;
		/* appending updated rows based on the candidates data */
		for(var i = 0 ; i <= cycleLength + 1; i++)
		{
			var row = $("<tr> </tr>");
			var newCycleLength = candidates.length;

			for(var j = 0; j <= newCycleLength; j++)
			{
				if(i == 0 && j == 0){

					row.append("<td class='table-key table-top-left'> &nbsp; </td> <td class='table-key' colspan='"+candidates.length+"'> <span class='localize' translate-key='133'>Candidates Ranked</span> </td>")

				}else if(j == 0 && i != 1) {

					row.append("<td> "+ data[i-2].Voter + "</td>");

					row.attr('data-voter', data[i-2].Voter);

					row.attr('class', 'table-middle');

				}else if(j == 0){

					if(i == 1){

						row.append("<td class='table-top table-top-left table-key'><span class='localize' translate-key='135'>Voters</span></td>");

					}
				}else if(i == 1){

					row.append("<td class='table-top'> "+ candidates[j-1] +" </td>");

				}else if(i != 0){

					row.append("<td class='rank' data-user='"+candidates[j-1].trim()+"' data-rank='"+j+"'></td>");

					row.attr('class', 'table-middle');

				}
			}

			$('.current-rankings table').append(row);
		}
		/* making the first row of the table to take all space */
		$('.current-rankings table tr:first-child td').attr('colspan', candidates.length + 1);

		//Sideways Y-axis Title
		//$('.current-rankings .table-left:first').append("<div class='table-key-left'>VOTERS</div>");

		//Alternating colors on tables
		$('.flat-table .table-middle:odd').addClass( "table-alternating-color-1" );
		$('.flat-table .table-middle:even').addClass( "table-alternating-color-2" );


		//updating the selects based on the user's selections
		setTimeout(function(){
			var userSelection = $(".current-rankings tr[data-voter='"+election.username+"']");

			if(!userSelection.length && $('.active-election').length){
				$(".vote-item select").hide();
				return false;
			}
			var obj = [];
			userSelection.find("td.rank").each(function(){
				var item = {};
				item.candidate = $(this).attr("data-user");
				item.vote = $(this).attr("data-value");
				obj.push(item);
			})
			obj.forEach(function(elm){
				$(".vote-item[data-user='"+elm.candidate+"'] select option[value='"+elm.vote+"']").attr("selected", "selected");
			})
		},  1000)
	}
}


// converts seconds into days, hours, minutes
function ConvertSeconds(seconds, mode, x){

	//converting seconds into a number
	seconds = parseFloat(seconds);

	var result = "";

	//getting days
	var days = parseInt(seconds / 86400);

	//getting hours
	var hours = parseInt((seconds - (86400 * days)) / 3600);

	var middle =  (86400 * days) + (3600 * hours);

	//getting minutes
	var minutes = parseInt( (seconds - middle ) / 60);

	//constructing the return text
	if(days != 0){
		result += days + " " + ((days == 1) ? "<span class='localize' translate-key='38'>day</span> " : "<span class='localize' translate-key='155'>days</span> ");
	}

	if(hours != 0){
		result += hours + " " + ((hours == 1) ? "<span class='localize' translate-key='143'>hr</span> " : " <span class='localize' translate-key='156'>hrs</span> ");
	}

	if(minutes != 0){
		result += minutes + " " + ((minutes == 1) ? "<span class='localize' translate-key='144'>min</span> " : "<span class='localize' translate-key='157'>min</span> ");
	}

	if(days == 0 && hours == 0 && minutes == 0){
		result = parseInt(seconds) + " secs";
	}

	//the case when we want to get only day and hour
	if(typeof mode != typeof undefined){

		if(typeof x == typeof undefined){

			return { 'day': days,'hour': hours};

		}else if( x == "day"){

			return { 'day': days };

		}else if( x == "hour"){

			var hour = parseInt(seconds / 3600);

			return { 'hour': hour};

		}

	}

	return result;

}


function decode(string){
	var encodedStr = string.replace(/[\u00A0-\u9999<>\&]/gim, function(i) {
   		return '&#'+i.charCodeAt(0)+';';
	});
	return encodedStr;
}

function sum(x){
	if(x == 1){
		return 1;
	}else{
		return x + sum(x-1);
	}
}

// create unique election object
var election = new Election();

$(document).ready(function(){
	if (typeof bindAllElectionEvents !== 'undefined' && $.isFunction(bindAllElectionEvents))
		bindAllElectionEvents(election);
})

})();