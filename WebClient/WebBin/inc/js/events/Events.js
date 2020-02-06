(function(){
	var Events = function(){
		this.Events = new Array();
	}

	// Events.html functions start
	Events.prototype.showEvents = function(time){
		var data = new Object();

		if(time){
			// data.startDay = Math.floor(time[0] / 86400);
			// data.endDay = Math.floor(time[1] / 86400);
			data.startDay = time[0];
			data.endDay = time[1];
		}
		// data.testing = true;

		Promise.resolve({
			data : {
				url : APPIQUERYURL + "/api/v1/analysis/discussions/suggest",
				data : data,
				type : 'GET'
			},
			this : this
		}).then(this.PromiseAjax)
		.then(function(obj){
			return new Promise(function(resolve,reject){
				obj.this.Events = obj.data;

				if(Object.keys(obj.data).length){
					variables.Events = true;
				}
				variables.citizenActivity();

				return resolve(obj);
			})
		})
		.then(this.eventsContent)
		.then(this.SaveDiscussionEvent)
		.catch(function(err){
			variables.showError(err);
		});
	}

	Events.prototype.eventsContent = function(obj){

		return new Promise(function(resolve,reject){
			if(Object.keys(obj.data).length){

				var data = obj.data,
				_this = obj.this;
				for(var i in data){

					_this.appendTitle(data[i].EventTitle,i);

					if(data[i].Discussion){
						_this.appendClassDiscution([data[i].Discussion.Guid, data[i].Discussion.Title], i)
					}

					if(data[i].EventData){
						_this.appendChatSnippets(data[i].EventData.ChatLogSnippets, i);

						if(data[i].EventData.Maps.length){
							_this.appendMap(data[i].EventData.Maps, i);
						}

						if(data[i].EventData.Graphs.length){
							_this.appendGraph(data[i].EventData.Graphs[0], i);
						}
					}
				}

				variables.Events = true;
				variables.citizenActivity();

				return resolve(obj);

			}else{
				return reject({messages:'No Events.'});
			}
		});
	}

	Events.prototype.appendTitle = function(title, id){
		var html = '<div class="row"> <h3 class="events-group-'+id+'">'+title+'<div class="class-discution-'+id+' class-discussion"></div></h3> <div class="col-md-12 event-wrapper-2"> <div class="map-'+id+'"></div> <div class="graph-'+id+'"></div><div class="chat-snippets-'+id+'"></div></div> </div> </div>';
		$('.events-table').append(html);
	}

	Events.prototype.appendChatSnippets = function(data, id){
		var place = $('.chat-snippets-'+id);
		for(var k in data){
			for(var i in data[k]){

				var text = this.StringFiltr(data[k][i].Text);

				var html = '<p><span class="curr-event-chat-tag">' + data[k][i].Tag + '</span> <span class="curr-event-chat-user">' + data[k][i].Username + ':</span> <span class="curr-event-chat-text">' + text +'</span></p>';

					if(data[k][i].Graph){
						html+= '<div class="graph-'+id+'-'+k+'-'+i+'"></div>';
					}

					if(data[k][i].Map){
						var settings = JSON.stringify(data[k][i].Map)
						html+= '<div class="map-'+id+'-'+k+'-'+i+'"></div>';
					}

				place.append(html);
			}
		}
		this.appendChatSnippetsMapsAndGraphs(data, id);
	}

	Events.prototype.StringFiltr = function(text){
		var text_new = '';
		x(text);
		function x(t){

			var c = t.search("<");
			if(c >= 0){
				text_new += t.substring(0, c);
				var v = t.substring(c, t.length);
				var b = v.substring(0, 3);
				var b1 = v.substring(0, 4);

				if(b == '<b>'){

					text_new+= b;
					v = v.substring(3, v.length);

				}else if(b1 == '</b>'){

					text_new+= b1;
					v = v.substring(4, v.length);

				}else if(v.search("><") >= 0){

					var l = v.search("><");
					v = v.substring(l+1, v.length);

				}else{

					text_new+= v;

				}

				x(v);
			}else{

				text_new+= t;
				return true;
			}
		}

		return text_new;
	}

	Events.prototype.appendChatSnippetsMapsAndGraphs = function(data, id){
		var _this = this;

		for(var k in data){
			for(var i in data[k]){

				if(data[k][i].Graph){
					_this.appendGraph(data[k][i].Graph, id+'-'+k+'-'+i);
				}

				if(data[k][i].Map){
					_this.appendMap([data[k][i].Map], id+'-'+k+'-'+i);
				}

			}
		}
	}

	Events.prototype.appendClassDiscution = function(className, id){
		var place = $('.class-discution-' + id);
		place.append('<a href="#" data-id="'+className[0]+'" class="discution_save" ><span class="localize" translate-key="37">Discussion</span></a>');
	}

	Events.prototype.appendMap = function(obj, id){
		var view = null,
			viewer = null,
			place = $('.map-'+id);

		for(var i in obj){
			view = null;
			viewer = null;

			viewer = $('<div style="width:300px; height:300px; margin: 10px auto;" class="view-map" id="map-'+id+'"></div>');
			place.append(viewer);

			view = viewer[0];
			view.settings = obj[i];
			if(view && ECO.MINIMAP.initialized) {
				ECO.MINIMAP.addView(view);
			}
		}
	}

	Events.prototype.appendGraph = function(obj, id){
		var place = $('.graph-'+id);
			place.append("<div class='discussion-graph' id='discussion-graph-" + id + "'> </div>");
			obj.Keys[0] = [obj.Keys[0].join("/")];
			getDiscussionGraphData('discussion-graph-' + id, obj.Keys, obj.TimeMin, obj.TimeMax);

	}
	// Events functions end

	// helper functions start
	Events.prototype.PromiseAjax = function(obj){
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

	Events.prototype.timeFilter = function(TeacherObj){
		var _this = TeacherObj;


		return function(time){
			$('.events-table').html('');

			_this.showEvents(time);
		}
	}

	Events.prototype.joinChatSnippets = function(chats){
		for(i in chats){
			chats[i] = '<span style="color:#46815d;">#' + chats[i].Tag + ' ' + chats[i].Sender + ':</span> ' + chats[i].Text;
		}
		return chats;
	}
	// helper functions end

	Events.prototype.saveDiscussion = function(data,Guid){

		var save_data = new Object();
		for(i in data){
			if(data[i].Discussion.Guid == Guid){
				save_data = data[i].Discussion;
				// save_data.Activities = {};
			}
		}
		Promise.resolve({
			data : {
				url : APPIQUERYURL + "/api/v1/analysis/discussions/save",
				data : JSON.stringify(save_data),
				type : 'POST'
			},
			this : this
		}).then(this.PromiseAjax)
		.then(function(obj){
			return new Promise(function(resolve,reject){
				window.location.href = '/discussion.html?classId=' + Guid;
			})
		})
		.catch(function(err){
			variables.showError(err);
		});
	}

	Events.prototype.SaveDiscussionEvent = function(obj){
		return new Promise(function(resolve,reject){
			$(document).on('click', '.discution_save', function(){
				var Guid = $(this).attr('data-id'),
					data = obj.data,
					_this = obj.this;
				_this.saveDiscussion(data,Guid);

			});
		})
	}

		Events.prototype.eventBackground = function(){
			checkForChanges();
			function checkForChanges(){
				var	offset = $(".events-table").offset(),
				element1 = $(".events-table"),
				element2 = $('.curr-event-background');

				if (element1.css('height') != element2.css('height')){
					element2.css('width',$(".curr-event").width()+'px');
					element2.css('top', offset.top-90+'px');
					element2.css('left', offset.left-270+'px');
					element2.css('height',element1.height()+'px');
				}
					setTimeout(checkForChanges, 500);
			}
		}

	// initialization start
	Events.prototype.bindEvent = function(){
		this.showEvents();
	}

	// create unique Teacher object
	var events = new Events();
	eventsFilter = events.timeFilter(events);
	events.bindEvent();
	events.eventBackground();
	// initialization end
})()