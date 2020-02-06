//JQuery style, 
(function(global,factory){
		factory( global );
}(typeof window !== "undefined" ? window : this, function( window, noGlobal ) {
	El = function() {
		return new El.init();
	},

	init = El.init = function() {
		alert(11111)
		return firstLoadElectionPage();
	}

	// get url params 
	function getURLParameter(name) {
		return decodeURIComponent((new RegExp('[?|&]' + name + '=' + '([^&;]+?)(&|#|;|$)').exec(location.search)||[,""])[1].replace(/\+/g, '%20'))||null
	}

	function firstLoadElectionPage(){

	}

	return El;
}));