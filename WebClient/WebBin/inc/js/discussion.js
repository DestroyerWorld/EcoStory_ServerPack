 ;(function(){
	var Discussion = function(){
		this.Discussion = new Array();
		this.classId = '';
		this.iframe_count = 0;
		this.load_iframe_count = 0;
	}

	// Discussion.html functions start
	Discussion.prototype.showDiscussion = function(){
		var data = new Object();
		var classId = this.classId;

		Promise.resolve({
			data : {
				url : APPIQUERYURL + "/api/v1/analysis/discussions/" + classId,
				data : data,
				type : 'GET'
			},
			this : this
		}).then(this.PromiseAjax)
		.then(function(obj){
			return new Promise(function(resolve,reject){
				if(obj.data){
					obj.this.Discussion = obj.data;
				}
				return resolve(obj);
			})
		})
		.then(this.discussionContent)
		.catch(function(err){
			variables.showError(err);
		});
	}

	Discussion.prototype.saveDiscussion = function(){
		this.chageGraphandMap();

		var data = new Object();
			data.Guid = this.Discussion.Guid;
			data.Title = this.Discussion.Title;
			data.Description = this.Discussion.Description;
			data.Activities =  new Array();
		var k = 0;
		for(var i in this.Discussion.Activities){
			if(Object.keys(this.Discussion.Activities[i]).length){
				data.Activities[k] = this.Discussion.Activities[i];
				k++;
			}
		}

		Promise.resolve({
			data : {
				url : APPIQUERYURL + "/api/v1/analysis/discussions/save",
				data : JSON.stringify(data),
				type : 'POST'
			},
			this : this
		}).then(this.PromiseAjax)
		.then(function(obj){
			return new Promise(function(resolve,reject){
				if(obj.data){
					obj.this.Discussion = obj.data[0].Discussion;
				}
				$("#loader").fadeOut();

				return resolve(obj);
			})
		})
		.catch(function(err){
			variables.showError(err);
		});
	}

	Discussion.prototype.discussionContent = function(obj){
		return new Promise(function(resolve,reject){
			var data = obj.this.Discussion,
				_this = obj.this;

			_this.appendTitle(data.Title, data.Description);

			var activities = data.Activities;
			for(i in activities){
				_this.appendContent(activities[i],i);
				_this.appendChatSnippets(activities[i].Data.ChatLogSnippets, i);

				if(activities[i].Data.Maps.length){
					for(j in activities[i].Data.Maps){
						_this.appendMap(activities[i].Data.Maps[j], i,j);
					}
				}

				if(activities[i].Data.Graphs.length){
					for(j in activities[i].Data.Graphs){
						activities[i].Data.Graphs[j].TimeMax = Math.floor(activities[i].Data.Graphs[j].TimeMax*10)/10
						_this.appendGraph(activities[i].Data.Graphs[j],i,j);
					}
				}

				_this.addGraphandMapEvent(i);
			}

			_this.enableEdit();
			if(_this.iframe_count == 0){
				$("#loader").fadeOut();
			}
		});
	}

	Discussion.prototype.appendTitle = function(title, description){
		var html = '<div class="section-wrapper">';
				html +=	'<div class="discussion-wrapper">';
					html +=	'<div class="global-title-small">';
						html +=	'<a href="#" class="edit" data-type="text" data-activities="false" data-name="title">';
							html +=	title;
							html +=	' <i class="fa fa-pencil" style="display:none" aria-hidden="true"></i>';
						html +=	'</a>';
					html +=	'</div>';
					html +=	'<p class="discussion-description-wrapper">';
						html +=	'<a href="#" class="edit" data-type="textarea" data-activities="false" data-name="description">';
							html += description;
							html += ' <i class="fa fa-pencil" style="display:none" aria-hidden="true"></i>';
						html +=	'</a>';
					html +=	'</p>';
			// 	html +=	'</div>';
			// html +=	'</div>';
		$('.discussion-content').append(html);
	}

	Discussion.prototype.appendContent = function(data, id){
		var htmlTitle = '<div class="activit" data-id="'+id+'">',
			select = '<select data-pk="' + id + '" class="discussion-format" style="display: inline;"><option value="Data" selected>Data</option><option value="Discussion">Discussion</option><option value="Causes And Effects">Causes And Effects</option><option value="Reflection">Reflection</option></select>',
			description = '';
			if(data.Format == 'Data'){
				select = '<select data-pk="' + id + '" class="discussion-format" style="display: inline;"><option value="Data" selected>Data</option><option value="Discussion">Discussion</option><option value="Causes And Effects">Causes And Effects</option><option value="Reflection">Reflection</option></select>';
				description = '<p class="specific-description">Study the following data.</p>';
			}else if(data.Format == 'Discussion'){
				select = '<select data-pk="' + id + '" class="discussion-format" style="display: inline;"><option value="Data">Data</option><option value="Discussion" selected>Discussion</option><option value="Causes And Effects">Causes And Effects</option><option value="Reflection">Reflection</option></select>';

			}else if(data.Format == 'Reflection'){
				select = '<select data-pk="' + id + '" class="discussion-format" style="display: inline;"><option value="Data">Data</option><option value="Discussion">Discussion</option><option value="Causes And Effects">Causes And Effects</option><option value="Reflection" selected>Reflection</option></select>';

			}else if(data.Format == 'Causes And Effects'){
				select = '<select data-pk="' + id + '" class="discussion-format" style="display: inline;"><option value="Data">Data</option><option value="Discussion">Discussion</option><option value="Causes And Effects" selected>Causes And Effects</option><option value="Reflection">Reflection</option></select>';
				description = '<p class="specific-description">Draw arrows from causes to their effects. (Note that some effects may have multiple causes, and some causes may have multiple effects.)</p>';
			}
			// select = '<a href="#" class="format" data-type="select" data-value="' + data.Format + '" data-pk="' + id + '" buttons="no">' + data.Format + '</a>';
			htmlTitle+= '<div id="section-title"><p style="padding: 0;position:relative">'+select+'<i class="fa fa-fw fa-caret-down" style="position: absolute;top: 6px;right: 0px"></i></p></div><div class="global-title-small"><a href="#" class="edit" data-type="text" data-activities="true" data-name="title" data-pk="'+id+'">'+data.Title+' <i class="fa fa-pencil" style="display:none" aria-hidden="true"></i></a></div><p class="discussion-description-wrapper"><a href="#" class="edit" data-type="textarea" data-name="description" data-pk="'+id+'" data-activities="true">'+data.Description+' <i class="fa fa-pencil" style="display:none" aria-hidden="true"></i></a></p>'+description;


		var graphs = data.Data.Graphs;

		var html = '<div class="section-wrapper">';
				html += '<button class="eco-button pull-right remove_activity" data-id="'+id+'">Remove Activity</button>';
				html += htmlTitle;
					html += '<div class="discussion-chat-wrapper"><div class="chat-snippets-'+id+'"></div></div>';

					html += '<div class="discussion-map-wrapper">';
						html += '<div class="map-'+id+'">';
						html += '</div>';
						html += '<button class="eco-button addMap-button addMap-'+id+'" data-id="'+id+'"> ADD MAP </button>';
					html += '</div>';

					html += '<div class="discussion-graph-wrapper">'
						html += '<div class="graph-'+id+'">'
						html += '</div>';
						html += '<button class="eco-button addGraph-button addGraph-'+id+'" data-id="'+id+'"> Add Graph</button>';

					html += '</div>';
				html += '</div>';
			html += '</div>';
		$('.discussion-content').append(html);
	}

	Discussion.prototype.appendChatSnippets = function(obj, id){
		var place = $('.chat-snippets-'+id);

		for(i in obj){
			obj[i] = this.joinChatSnippets(obj[i]);
			place.append('<p>'+obj[i]+'</p>');
		}
	}

	Discussion.prototype.appendMap = function(obj, id, i){
		this.iframe_count++;
		var _this = this;
		var place = $('.map-'+id);
		var html = '<div class="discussion-map map-'+id+'-'+i+'" data-id="'+id+'" data-j="'+i+'">';
			html += '<input type="hidden" class="map-data" value="">';
			html += '<div></div>';
			// html+= "<iframe class='comment-map'  id='map_iframe-"+id+"-"+i+"' src='map_iframe.html?id="+id+'-'+i+"&settings="+JSON.stringify(obj)+"' style='width: 420px;height:540px;margin-bottom: 15px;'></iframe>"
			html += '<button class="eco-button discussion-remove-map remove-map-'+id+'-'+i+'" data-id="'+id+'-'+i+'" style="margin:0;display:block">Remove Map</button>';
		html += '</div>';
		place.append(html);

		$(".map-"+id+"-"+i+" div").load( '../map.html',function(e){
			_this.loadIframe(false, true);
			var data = _this.Discussion.Activities;
			var i1 = $(this).parent().data('id');
			var i2 = $(this).parent().data('j');
			var view = $(".map-"+id+"-"+i+" .view-map")[0];
			var pause = true;

			if(Object.keys(data[i1].Data.Maps[i2]).length){
				view.settings = data[i1].Data.Maps[i2];
				view.settings.pause = false;
				var layerSelected = data[i1].Data.Maps[i2].layerSelected;
				$(".map-"+id+"-"+i+" .map-layer-select").attr('data-val', layerSelected);
			}else{

				// _this.Discussion.Activities[i1].Data.Maps[i2] = {};
				pause = true;
				view.mapdata = $(".map-"+id+"-"+i+" .map-data")[0];
				ECO.MINIMAP.addView(view);
			}
			new MapControls().bindEvents(".map-"+id+"-"+i, view, pause);
			ECO.MINIMAP.fillLayerSelects(".map-"+id+"-"+i);

		});

		_this.removeMapEvent(id,i);

		if(!obj){
			_this.Discussion.Activities[id].Data.Maps[i] = {};
		}

	}

	Discussion.prototype.removeMap = function(id,i){
		this.Discussion.Activities[id].Data.Maps.splice(i, 1);
		$('.map-'+id+'-'+i).remove();
	}

	Discussion.prototype.appendGraph = function(obj,id,i){
		this.iframe_count++;
		var _this = this;
		var place = $('.graph-'+id);
		var html = '<div class=" discussionform-graph graph-'+id+'-'+i+'">';
				html+= '<input type="hidden" class="graph-data" value="">';
				html+= "<iframe class='comment-graph' id='graph_composer-"+id+"-"+i+"' src='graph_composer/index.html?id="+id+'-'+i+"&settings="+JSON.stringify(obj)+"' style='margin-top: 0px;margin-bottom: 15px;'></iframe>";
				html+= '<button class="eco-button discussion-remove-graph remove-graph-'+id+'-'+i+'" data-id="'+id+'-'+i+'" style="margin:0">Remove Graph</button>';
			html+= '</div>';

		place.append(html);
		_this.loadIframe("#graph_composer-"+id+"-"+i, false);

		$("#graph_composer-"+id+"-"+i).fadeIn();
		_this.removeGraphEvent(id,i);

		if(!obj){
			_this.Discussion.Activities[id].Data.Graphs[i] = {};
		}
	}

	Discussion.prototype.removeGraph = function(id,i){
		this.Discussion.Activities[id].Data.Graphs.splice(i, 1);
		$('.graph-'+id+'-'+i).remove();
	}

	Discussion.prototype.chageGraphandMap = function(){
		var _this = this;
		var data = _this.Discussion;

		for(i in _this.Discussion.Activities){
			if(Object.keys(_this.Discussion.Activities[i]).length){
				if(_this.Discussion.Activities[i].Data.Graphs.length){
					for(j in _this.Discussion.Activities[i].Data.Graphs){
						var currentGraphs = $(".graph-" +i+'-'+j+' .graph-data').val();
						if(currentGraphs){
							currentGraphs = JSON.parse(currentGraphs);
							var Keys = [];
							for(k in currentGraphs.variables){
								Keys.push(currentGraphs.variables[k].split('/'));
							}
							data.Activities[i].Data.Graphs[j].Keys = Keys;
							data.Activities[i].Data.Graphs[j].TimeMin = currentGraphs.time_start;
							data.Activities[i].Data.Graphs[j].TimeMax = currentGraphs.time_end;
						}
					}
				}
				if(_this.Discussion.Activities[i].Data.Maps.length){
					for(j in _this.Discussion.Activities[i].Data.Maps){
						_this.Discussion.Activities[i].Data.Maps[0].pause = true;
						var currentMaps = $('.map-'+i+'-'+j+' .map-data').val();
						if(currentMaps){
							currentMaps = JSON.parse(currentMaps);
							data.Activities[i].Data.Maps[j].camPos = currentMaps.camPos;
							data.Activities[i].Data.Maps[j].currentTime = currentMaps.currentTime;
							data.Activities[i].Data.Maps[j].flat = currentMaps.flat;
							data.Activities[i].Data.Maps[j].frame = currentMaps.frameNum;
							data.Activities[i].Data.Maps[j].layerSelected = currentMaps.layerSelected;
							data.Activities[i].Data.Maps[j].pause = true;
							data.Activities[i].Data.Maps[j].playSpeed = currentMaps.playSpeed;
							data.Activities[i].Data.Maps[j].timeEnd = currentMaps.timeEnd;
							data.Activities[i].Data.Maps[j].timeStart = currentMaps.timeStart;

						}
					}
				}
			}
		}
	}


	// helper functions start
	Discussion.prototype.PromiseAjax = function(obj){
		return new Promise(function(resolve,reject){
			$.ajax({
				url: obj.data.url,
				data: obj.data.data,
				type: obj.data.type,
				contentType: "application/json",
				// dataType: 'json',
				success: function(data){
					obj.data = data
					return resolve(obj);
				},
				error : function(err){
					return reject(err);
				}
			});
		});
	}

	Discussion.prototype.timeFilter = function(TeacherObj){
		var _this = TeacherObj;

		return function(time){
			$('.events-table').html('');

			_this.showEvents(time);
		}
	}

	Discussion.prototype.joinChatSnippets = function(chats){
		for(i in chats){
			chats[i] = '<span style="color:#46815d;">#' + chats[i].Tag + ' ' + chats[i].Sender + ':</span> ' + chats[i].Text;
			/*
			chats[i] = '<span class="curr-event-chat-tag">#' + chats[i].Tag + '</span> <span class="curr-event-chat-user">' + chats[i].Sender + ':</span> <span class="curr-event-chat-tag">' + chats[i].Text +'<span class="curr-event-chat-tag">;
			*/
		}
		return chats;
	}

	Discussion.prototype.enableEdit = function(){
		var _this = this;

		var editable = $('.edit').editable({
			type: 'text',
			title: 'Enter data',
			success: function(response, newValue) {

				var activities = $(this).data('activities'),
					name = $(this).data('name');

				if(activities == true){
					var id = $(this).data('pk');

					if(name == 'title'){
						if (newValue.length >= 50) {
							return false;
						}else{
							_this.Discussion.Activities[id].Title = newValue;
						}
					}else if(name == 'description'){
						_this.Discussion.Activities[id].Description = newValue;
					}
				}else{
					if(name == 'title'){
						if (newValue.length >= 50) {
							return false;
						}else{
							_this.Discussion.Title = newValue;
						}
					}else if(name == 'description'){
						_this.Discussion.Description = newValue;
					}
				}
			}
		});

		editable.on('shown', function(e, editable) {
			var textarea = $('.editable-container textarea')[0];
			var heightLimit = 1000;
			if (textarea) {
				textarea.style.height = $(this).height()+10+ "px";
				textarea.oninput = function() {
				  textarea.style.height = ""; /* Reset the height*/
				  textarea.style.height = Math.min(textarea.scrollHeight, heightLimit) + "px";
				};
				var name = $(this).data('name');
			}

			editable.input.$input.keypress(function (e) {
				if(name == 'title'){
					if ($(this).val().length >= 50) {
						return false;
					}
				}
			});
		});


		$(document).on('change', '.discussion-format', function(){
			var id = $(this).data('pk');
			var format = $(this).val();
				_this.Discussion.Activities[id].Format = format;
				$('.activit[data-id="'+id+'"] .specific-description').remove();

				if(format == 'Data'){
					$('.activit[data-id="'+id+'"] .discussion-description-wrapper').after('<p class="specific-description">Study the following data.</p>');
				}else if(format == 'Causes And Effects'){
					$('.activit[data-id="'+id+'"] .discussion-description-wrapper').after('<p class="specific-description">Draw arrows from causes to their effects. (Note that some effects may have multiple causes, and some causes may have multiple effects.)</p>');
				}

		});

        $(document).on('click', '.remove_activity', function(){
		$(this).parent().remove();
		var id = $(this).data('id');
		_this.Discussion.Activities[id] = {};

			$('.section-wrapper').removeClass( "section-alternating-color-1" );
			$('.section-wrapper').removeClass( "section-alternating-color-2" );
			$('.section-wrapper:odd').addClass( "section-alternating-color-1" );
			$('.section-wrapper:even').addClass( "section-alternating-color-2" );
		});

		$( ".edit" )
		.mouseover(function() {
			$( this ).find( "i" ).show();
		})
		.mouseout(function() {
			$( this ).find( "i" ).hide();
		});
	}

	Discussion.prototype.printDiscussionForm = function(){
		this.chageGraphandMap();
		var iframe = document.createElement('iframe');
		$(iframe).addClass('print_iframe');
		$('.print_iframe').remove();

		var _this = this,
			data = _this.Discussion;
		localStorage.map_initialized = 0;

		var maps_count = 0;
		iframe.onload = function() {
			var doc = iframe.contentDocument ? iframe.contentDocument : iframe.contentWindow.document;
				console.warn('data.Title',data.Title)
				var html = '<table style="margin-top:50px; width: 100%;font-family: Dosis;">'
					+'<tr style="display: block; margin:10px">'
						+'<td style= margin-top:70px;  padding-left:10px;padding-right:10px;">Name: ______________________________________________</td>'
						+'<td style="padding-left:10px;padding-right:10px;">Date: _______________________</td>'
					+'</tr>'
					+'<tr style="display: block;margin: 30px 10px 0;">'
						+'<td style="width: 98%;padding-left:10px;padding-right:10px;display: inherit;font-weight:700;font-size:35px;white-space: pre-wrap;" colspan="3">'+data.Title+'</td>'
					+'</tr>'
					+'<tr style="display: block; margin:10px">'
						+'<td style="width: 98%;padding-left:10px;padding-right:10px;font-weight:700;font-size:25px;white-space: pre-wrap;" colspan="3">'+data.Description+'</td>'
					+'</tr>';

			var dataActivities = data.Activities,
				Graphs = '',
				Maps = '';
			for(i in dataActivities){
				Graphs = '';

				if(Object.keys(dataActivities[i]).length){
					if(dataActivities[i].Data.Graphs.length){
						for(j in dataActivities[i].Data.Graphs){

							var TimeMin = dataActivities[i].Data.Graphs[j].TimeMin;
							var TimeMax = dataActivities[i].Data.Graphs[j].TimeMax;
							var Keys = JSON.stringify(dataActivities[i].Data.Graphs[j].Keys);
							Graphs+='<td style="margin-top:55px; display: inline-block;padding:0 10px 20px;" colspan="3">'
											+ "<iframe class='comment-graph' id='graph_composer-"+i+"-"+j+"' src='graph_composer/print_iframe.html?Keys="+Keys+"&TimeMin="+TimeMin+"&&TimeMax="+TimeMax+"' style='width: 400px;height: 290px;border: 1px solid black;'></iframe>"
										+'</td>'
						}
					}
					

					Maps = '';
					if(dataActivities[i].Data.Maps.length){
						for(j in dataActivities[i].Data.Maps){
							var TimeMin = dataActivities[i].Data.Maps[j].TimeStart;
							var TimeMax = dataActivities[i].Data.Maps[j].TimeEnd;
							var settings = JSON.stringify(dataActivities[i].Data.Maps[j]);
							maps_count++;
							Maps+='<td style=" margin-top:55px;display: inline-block;padding:0 10px 20px;" colspan="3">'
									+ "<div style='display: block;background:black;position: absolute;padding: 5px;'>"+dataActivities[i].Data.Maps[j].layerSelected+"</div>"
									+ "<iframe class='comment-map'  id='map_iframe-"+i+"-"+j+"' src='print_map_iframe.html?settings="+settings+"' style='width: 400px;height: 290px;border: 1px solid black;'></iframe>"
								+'</td>'
						}
					}

					var html_Data = '';
					var html_Reflection = '';

					if(dataActivities[i].Format === 'Data'){

						html_Data = '<tr style="display: block; margin:10px">'
										+'<td style="width: 98%;padding-left:10px;padding-right:10px;" colspan="3">Study the following data.</td>'
									+'</tr>';
					} else if( dataActivities[i].Format === 'Causes And Effects' ){
						html_Data = '<tr style="display: block; margin:10px">'
										+'<td style="width: 98%;padding-left:10px;padding-right:10px;" colspan="3">Draw arrows from causes to their effects. (Note that some effects may have multiple causes, and some causes may have multiple effects.)</td>'
									+'</tr>';
					} else if( dataActivities[i].Format === 'Reflection' ){
						html_Reflection += '<tr style="display: block; margin:10px"><td style="width: 98%;padding-left:10px;padding-right:10px;" colspan="3">___________________________________________________________________________________________________________________________</td></tr>'
											+'<tr style="display: block; margin:10px"><td style="width: 98%;padding-left:10px;padding-right:10px;" colspan="3">___________________________________________________________________________________________________________________________</td></tr>'
											+'<tr style="display: block; margin:10px"><td style="width: 98%;padding-left:10px;padding-right:10px;" colspan="3">___________________________________________________________________________________________________________________________</td></tr>'
											+'<tr style="display: block; margin:10px"><td style="width: 98%;padding-left:10px;padding-right:10px;" colspan="3">___________________________________________________________________________________________________________________________</td></tr>'
					}
					html += '<tr style="display: block; margin:80px 10px 0">'
							+ '<td style="width: 98%;padding-left:10px;padding-right:10px;display: inherit;font-size:25px;font-weight:700;white-space: pre-wrap;" colspan="3">'+ dataActivities[i].Title.replace(/\n/g, "<br />") + '</td>'
						+ '</tr>'
						+ '<tr style="display: block; margin:10px">'
							+ '<td style="width: 98%;padding-left:10px;padding-right:10px;white-space: pre-wrap;" colspan="3">'+dataActivities[i].Description+'</td>'
						+ '</tr>'
						+ html_Data
						+ html_Reflection
						+'<tr style="display: block; margin:10px;">'
						+ Maps
						+ Graphs
						+ '</tr>';
					if( dataActivities[i].Data.ChatLogSnippets.length ){
						html += '<tr style="display: block; margin:10px">';
							for( j in dataActivities[i].Data.ChatLogSnippets ){
								html += '<td style="width: 98%;padding-left:10px;padding-right:10px;" colspan="3">' + dataActivities[i].Data.ChatLogSnippets[j] + '</td>';
							}
						html += '</tr>';
					}
				}
			}

			html += '</table>';

			//Remove site url from print doc
			html += '<style type="text/css" media="print">@page {margin-top: 5px;margin-bottom: 5px;}</style>';
			var x = doc.getElementsByTagName('body')[0];
			
			x.innerHTML = html;
		}
		var Interval = setInterval(function(){
			if(localStorage.map_initialized == maps_count){
				iframe.contentWindow.focus();
				iframe.contentWindow.print();
				iframe.contentWindow.onfocus = function(){
					$('.print_iframe').remove();
	
				}
				clearInterval(Interval);
				$("#loader").fadeOut();

				//testing
				 //$('.print_iframe').addClass('testing-iframe');
				 //$('#loader').hide();

			}
		}, 1500);
		// iframe.style.display = 'block';
		document.getElementsByTagName('body')[0].appendChild(iframe);

	}
	// helper functions end

	Discussion.prototype.addGraphandMapEvent = function(id){
		var _this = this;
		$(document).on('click', '.addGraph-'+id, function(){
			var i = _this.Discussion.Activities[id].Data.Graphs.length;
			_this.appendGraph(false, id,i);
		});

		$(document).on('click', '.addMap-'+id, function(){
			var i = _this.Discussion.Activities[id].Data.Maps.length;
			_this.appendMap(false, id,i);
		});
	}

	Discussion.prototype.removeGraphEvent = function(id,i){
		var _this = this;
		$(document).on('click', '.remove-graph-'+id+'-'+i, function(){
			_this.removeGraph(id,i);
		});
	}

	Discussion.prototype.removeMapEvent = function(id,i){
		var _this = this;

		$(document).on('click', '.remove-map-'+id+'-'+i, function(){
			_this.removeMap(id,i);
		});
	}

	Discussion.prototype.bindEvent = function(){
		var _this = this;

		$(document).on('click', '.save', function(){
			$("#loader").fadeIn();
			_this.saveDiscussion();
		})

		$(document).on('click', '.print', function(){
			$("#loader").fadeIn();
			_this.printDiscussionForm();
		})
	}

	Discussion.prototype.loadIframe = function(iframe_graph, map){
		var _this = this;

		if(iframe_graph){
			$(iframe_graph).on('load', function(){

				_this.load_iframe_count++;
				if(_this.load_iframe_count == _this.iframe_count){
					$("#loader").fadeOut();
				}
			});
		}

		if(map){
			_this.load_iframe_count++;

			if(_this.load_iframe_count == _this.iframe_count){
				$("#loader").fadeOut();
			}
		}
	}

	Discussion.prototype.getParameterByName = function(name) {
	    this.classId = decodeURIComponent((new RegExp('[?|&]' + name + '=' + '([^&;]+?)(&|#|;|$)').exec(location.search)||[,""])[1].replace(/\+/g, '%20'))||null;
	}

	// create unique Teacher object
	// $(window).load(function(){
		// var discussion = new Discussion();
		window.discussion = new Discussion();
		discussion.getParameterByName('classId');
		discussion.showDiscussion();
		discussion.bindEvent();
	// })
	// initialization end
})()