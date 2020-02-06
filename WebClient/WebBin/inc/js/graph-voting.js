//VOTING GRAPH DATA
function requestData(json) {
	
	var chart2 = $('#container-graph').highcharts();
	
	//Data to chart
	chart2.series[0].setData(json);

	//Update names on Tooltip
	/*chart2.tooltip.options.formatter = function() {
		 if (this.point.name == "Yes") {
			return '<div class="tooltip"><b>'+this.point.name+' votes:</b> '+this.point.y+'<br/><b>Supported by:</b> '+yesnames+'</div>'
		 } else return '<div class="tooltip"><b>'+this.point.name+' votes:</b> '+this.point.y+'<br/><b>Supported by:</b> '+nonames+'</div>';
	}*/
		
	//error resets padding value on update, so reresetting each update
	//chart2.dataLabels.padding = 10;
}

//VOTING GRAPH
$(function () {

Highcharts.setOptions({
	global: {
		useUTC: false
	}
});

//Global styling
options = {

	chart: {
		style: {
			fontFamily: 'Dosis',
		},
		spacingBottom: 0,
		spacingTop: 0,
		spacingLeft: 0,
		spacingRight: 0,
		marginBottom: 0,
		marginTop: 0,
		marginLeft: 0,
		marginRight: 0,
		width: 400,
		height: 340,
		backgroundColor:"#e8ffdf"
	},
	legend: {
		enabled : false,
		layout: 'vertical',
		align: 'right',
		verticalAlign: 'middle',
		itemMarginBottom:20,
		itemHiddenStyle: '#ffffff',
		itemStyle: {
			 fontSize: 15
		  }
	},
	colors: ['#96ED79', '#54CC80', '#90ed7d', '#f7a35c', '#8085e9', '#f15c80', '#e4d354', '#8085e8', '#8d4653', '#91e8e1'],
	title: {
		enabled : false,
		style: {
		 color: '#ffffff',
		 fontSize: '30px'
		},
	},
	xAxis: {
		enabled : false,
	  gridLineColor: '#ffffff',
	  labels: {
		 style: {
			  fontSize: '15px',
			color: '#ffffff'
		 }
	  },
	  lineColor: '#ffffff',
	  minorGridLineColor: '#ffffff',
	  tickColor: '#ffffff',
	  title: {
		 style: {
			color: '#ffffff',
			fontSize:'20px'
		 }
	  }
	},
	yAxis: {
		enabled : false,
		gridLineColor: '#ffffff',
	  labels: {
		 style: {
			 fontSize: '15px',
			color: '#ffffff'
		 }
	  },
	  lineColor: '#ffffff',
	  minorGridLineColor: '#ffffff',
	  tickColor: '#ffffff',
	  min: 0,
	  title: {
		  enabled: false,
			align: 'middle',
		 style: {
			color: '#ffffff',
			fontSize:'20px'
		 }
	  }
	},
	credits: {
	  enabled:false
   },
   labels: {
	  style: {
		 color: '#ffffff'
	  }
   },
   plotOptions: {
		column: {
			 borderWidth: 0,
		},
		bar: {
			 borderWidth: 0,
		},
		pie: {
			 borderWidth: 0,
			 shadow: {
					color: '#386427 ',
					width: 7,
					opacity: 0.15,
					offsetY: 1,
					offsetX: 1
				}
		}
   },
   navigation: {
		enabled : false,
	  buttonOptions: {
		 symbolStroke: '#ffffff',
		 theme: {
			fill: '#71AB5C'
		 }
	  },
	  menuStyle: {
	border: '1px solid #477D32',
	background: '#E4EDE1'
	 }
   }


};

//Voting graph styling
chart2options = {

	chart: {
		renderTo: 'container-graph',
		defaultSeriesType: 'pie',
		spacingBottom: 0,
		spacingTop: 0,
		spacingLeft: 0,
		spacingRight: 0,

		marginBottom: 00,
		marginTop: 0,
		marginLeft: 00,
		marginRight: 00,
		style: {
			fontFamily: 'Dosis',
		},
		backgroundColor: null,
		events:
		{
			load: requestData
		}
	},
	title: {
		text: ''
	},
	tooltip: {
		enabled : false,
		useHTML: true,
		borderWidth: 3,
		borderRadius: 4,
		backgroundColor: 'rgba(219, 255, 207, 0.6)',
		style: { fontSize: '20px' }
	},
	  plotOptions: {
			pie: {
				dataLabels: {
					enabled: true,
					//format: '{point.name}: {point.percentage:.1f} %',
					color: '#365729',
					fill: '#365729',
					connectorWidth:0,
					//borderColor: '#4A8235',
					//borderWidth: 3,
					//borderRadius: 4,
					distance:-50,
					//backgroundColor: '#5BA041',
					//padding:10,
					style: { fontSize: '18px',
					textShadow: 'none',
					textTransform: 'uppercase'
					},
					shadow: {
						color: '#386427 ',
						width: 1,
						opacity: 0.25,
						offsetY: 2,
						offsetX: 2
					},
					formatter:  function () {
						if (this.percentage == 0 ) {
							
							/*this.series.options.dataLabels.y = -160;
							this.series.options.dataLabels.x = -250;
							*/
							}
							return this.point.name+':  '+ Math.round(this.percentage) +'%';
					}
				},
			}
		},
		series: [{
					name: 'Votes',
					data: []
				}]
};

// Create the chart
chart2options = jQuery.extend(true, {}, options, chart2options);
chart2 = new Highcharts.Chart(chart2options);

});