function getDiscussionGraphData(id, category, timemin, timemax) {
		$( function ()
		{
			// Create the graph class
			var graph = new DatasetGraph('#'+id);
			
			graph.extendedOptions = { 
				chart: { 
						style: {
							 fontFamily: 'Dosis',
						},
						backgroundColor: null,
						height: 290,
						spacingBottom: 0,
						spacingTop: 1,
						spacingLeft: 0,
						spacingRight: 10,
						marginBottom: 95,
						marginTop: 15,
						marginLeft: 45,
						marginRight: 10,
						backgroundColor:"#e8ffdf"
				},
				title: {
					style: {
					 color: '#434348',
					 fontSize: '25px'
					},
				},
				legend: {
					layout: 'horizontal',
					align: 'center',
					marginTop: 10,
					maxHeight: 45,
					navigation: {
						animation: true
					},
					itemHiddenStyle: '#4f7b3f',
					itemStyle: {
						 fontSize: 14				 
					  }
				},
				yAxis: {
					  gridLineColor: '#4f7b3f',
					  labels: {
						 style: {
							 fontSize: '14px',
							color: '#434348'
						 }
					  },
					  lineColor: '#4f7b3f',
					  minorGridLineColor: '#4f7b3f',
					  tickColor: '#4f7b3f',	 
				 },
				xAxis: {
					  gridLineColor: '#4f7b3f',
					  labels: {
						 style: {
							fontSize: '14px',
							color: '#434348'
						 }
					  },
					  lineColor: '#4f7b3f',
					  minorGridLineColor: '#4f7b3f',
					  tickColor: '#4f7b3f',
					  title: {
						  offset: 25,
						 style: {
							color: '#434348',
							fontSize:'15px'
						 }
					  }
				},
				navigation: {
				  buttonOptions: {
					 symbolStroke: '#434348',
					 theme: {
						fill: '#DBFFCF'
					 }
				  },
				  menuStyle: {
				border: '1px solid #477D32',
				background: '#E4EDE1'
				 }
			   }
			};
			if(timemin == timemax){
				graph.setType('column');
			}else{
				graph.setType('line');
			}
			graph.load(category, timemin, timemax, function()
			{
				graph.showTitle(true);
			} );
		} );
		
	};