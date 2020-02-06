// Get URL Parameters - Relative protocol, domain, port
var url = window.location.href
var arr = url.split("/");
var result = arr[0] + "//" + arr[2];
var APPIQUERYURL = result;
var ACCOUNTAPPIQUERYURL = "https://ecoauth.strangeloopgames.com/";

function getURLParameter(name) {
	return decodeURIComponent((new RegExp('[?|&]' + name + '=' + '([^&;]+?)(&|#|;|$)').exec(location.search)||[,""])[1].replace(/\+/g, '%20'))||null
}