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
		result += days + " " + ((days == 1) ? "<span class='time-title'>day</span> " : "<span class='time-title localize' translate-key='155'>days</span> ");
	}

	if(hours != 0){
		result += hours + " " + ((hours == 1) ? "<span class='time-title'>hr</span> " : " <span class='time-title localize' translate-key='156'>hrs</span> ");
	}

	if(minutes != 0){
		result += minutes + " " + ((minutes == 1) ? "<span class='time-title'>min</span> " : "<span class='time-title localize' translate-key='157'>mins</span> ");
	}

	if(days == 0 && hours == 0 && minutes == 0){
		result = parseInt(seconds) + " seconds";
	}

	//the case when we want to get only day and hour
	if(typeof mode != typeof undefined){

		if(typeof x == typeof undefined){

			return { 'day': days,'hr': hours};

		}else if( x == "day"){

			return { 'day': days };

		}else if( x == "hr"){

			var hour = parseInt(seconds / 3600);

			return { 'hr': hour};

		}

	}

	return result;

}

// converts seconds into days, hours, minutes
function ConvertSecondsComments(seconds, mode, x){

	//converting seconds into a number
	seconds = parseFloat(seconds);

	var result = "";

	//getting days
	var days = parseInt(seconds / 86400);

	//getting hours
	var hours = parseInt((seconds - (86400 * days)) / 3600);
	if(hours <= 9){
		hours = '0'+hours;
	}

	var middle =  (86400 * days) + (3600 * hours);

	//getting minutes
	var minutes = parseInt( (seconds - middle ) / 60);
	if(minutes <= 9){
		minutes = '0'+minutes;
	}

	//constructing the return text
	result = "<span class='localize' translate-key='38'>Day </span> "+days+" , " + hours+":"+minutes;

	return result;

}