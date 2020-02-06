;(function(){
	var Gameaction = function(){
		this.gameactions = new Array();
		this.title = '';
		this.description = '';
		this.iconClass = '';
	}


	Gameaction.prototype.getActions = function(){
		var _this = this;
		var classId = this.classId;

		var data = [
			{
				"title" :	"Components of Air",
				"description" :	"Trigger a disscussion on strategies to build a habitable atmosphere.",
				"icon-class" :	"comment",
			},
			{
				"title" :	"Building Materials",
				"description" :	"Send additional building materials to the Martian surface.",
				"icon-class" :	"pencil",
			},
			{
				"title" :	"Habitat Next Steps",
				"description" :	"Share a hint with players about their next physiological need.",
				"icon-class" :	"record",
			},
		];

		this.gameactions = data;
	}

	Gameaction.prototype.showActions = function(){
		var _this = this;

		$(".gameactions-content #section-title").append("<p> " + this.classId + " </p>");

		var html = '';
		for (var i in this.gameactions) {
				var gameaction = this.gameactions[i];

				html += '<div class="gameaction-title">'+gameaction['title']+'</div>';
			html += '<div class="gameaction">';
				html += '<div class="gameaction-description">'+gameaction['description']+'</div>';

				html += '<div class="row submit">';
					html += '<div class="col-lg-12">';
						html += '<div class="eco-button submit-action">Submit Action</div>';
					html += '</div>';
				html += '</div>';
			html += '</div>';
		}

		$(".gameactions-content").append(html);
	}

	Gameaction.prototype.bindEvent = function(){
		var _this = this;

		$(document).on("click",".submit-action",function(){
			$('.success').fadeOut();
			$('.success').css({"background":"#39BB66"}).empty().html("Action was submitted").fadeIn();
			$('html,body').animate({scrollTop: '0'}, "slow");
		})
	}

	Gameaction.prototype.getParameterByName = function(name) {
	    this.classId = decodeURIComponent((new RegExp('[?|&]' + name + '=' + '([^&;]+?)(&|#|;|$)').exec(location.search)||[,""])[1].replace(/\+/g, '%20'))||null;
	}

	// create unique Teacher object
	// $(window).load(function(){
		window.gameactions = new Gameaction();
		gameactions.getParameterByName('classId');
		gameactions.getActions();
		gameactions.showActions();
		gameactions.bindEvent();
	// })
	// initialization end
})()