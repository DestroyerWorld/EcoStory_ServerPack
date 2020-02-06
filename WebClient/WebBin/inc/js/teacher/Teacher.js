;(function(){
	var Teacher = function(){
		this.lawActivity = new Array();
		this.electionActivity = new Array();
		this.currentElections = new Array();
		this.previousElections = new Array();
		this.Lows = new Array();
	}

	// Election functions start
	Teacher.prototype.showElection = function(){
		Promise.resolve({
			data : {
				url : APPIQUERYURL + "/api/v1/elections/activity",
				data : new Array(),
				type : 'GET'
			},
			this : this,
			electionType : 'current'
		}).then(this.PromiseAjax)
		.then(function(obj){
			obj.this.electionActivity = obj.data;
			obj.this.showCurrentElection(obj);
			obj.this.showPreviousElection(obj);

		}).catch(function(err){
			variables.showError(err);
		});
	}

	Teacher.prototype.appendElection = function(obj){
		var _this = this;
		return new Promise(function(resolve,reject){
			var _this = obj.this;
			if(obj.activity.length){
				var table = $('.teacher-row'),
					activity = obj.activity,
					data = obj.data,
					electionType = obj.electionType,
					summary = '';

				var data = {
					this : _this,
					activity : activity,
					election : data,
					electionType : electionType,
					table : table
				}
				_this.electionHtml(data,false)

				variables.activityElections = true;
				variables.citizenActivity();

				return resolve({messages:'Election is shown.'});
			}else{
				return reject({messages:'No Election.'});
			}
		});
	}

	Teacher.prototype.electionHtml = function(data,time){
		var _this = data.this,
			activity = data.activity,
			table = data.table,
			electionType = data.electionType,
			election = data.election;

		for(i in activity){
			if(electionType == 'current' && election && election.Guid == activity[i].Election.Guid){
				summary = _this.joinEvents(activity[i].Events,time);
				if(summary){
					_this.appendHTML({summary:summary, result: 'Projected Results: '+election.Results.Result, table:table, name:'Election proposed by '+activity[i].Election.Proposer,link:APPIQUERYURL+'/elections.html?election=current'});
				}
			}else if(electionType == 'previous'){
				for(k in election){
					if(election[k].Guid == activity[i].Election.Guid){
						summary = _this.joinEvents(activity[i].Events,time);
						if(summary){
							_this.appendHTML({summary:summary, result:election[k].Results.Result, table:table, name:'Election proposed by '+activity[i].Election.Proposer,link:APPIQUERYURL+'/elections.html?election='+election[k].Guid});
						}
					}
				}
			}
		}
	}


	Teacher.prototype.showCurrentElection = function(obj){
		var x = Promise.resolve({
			data : {
				url : APPIQUERYURL + "/api/v1/elections/current",
				data : new Array(),
				type : 'GET'
			},
			this : obj.this,
			electionType : 'current',
			activity : obj.data
		});

		if(variables.currentElections.length == 0){
			x.then(this.PromiseAjax)
			.then(function(obj){
				return new Promise(function(resolve,reject){
					obj.this.currentElections = obj.data;
					return resolve(obj);
				})
			})
		}else{
			x.then(function(obj){
				return new Promise(function(resolve,reject){
					obj.data = variables.currentElections;
					obj.this.currentElections = variables.currentElections;
					return resolve(obj);
				})
			})
		}

		x.then(this.appendElection)
		.catch(function(err){
		});
	}

	Teacher.prototype.showPreviousElection = function(obj){
		var x = Promise.resolve({
			data : {
				url : APPIQUERYURL + "/api/v1/elections/previous",
				data : new Array(),
				type : 'GET'
			},
			this : obj.this,
			electionType : 'previous',
			activity : obj.data
		})
		if(variables.previousElections.length == 0){
			x.then(this.PromiseAjax)
			.then(function(obj){
				return new Promise(function(resolve,reject){
					obj.this.previousElections = obj.data;
					return resolve(obj);
				})
			})
		}else{
			x.then(function(obj){
				return new Promise(function(resolve,reject){
					obj.data = variables.previousElections;
					obj.this.previousElections = variables.previousElections;
					return resolve(obj);
				})
			})
		}

		x.then(this.appendElection)
		.catch(function(err){
			variables.showError(err);
		});
	}
	// Election functions end

	// Low functions start
	Teacher.prototype.showLow = function(){
		Promise.resolve({
			data : {
				url : APPIQUERYURL + "/api/v1/laws/activity",
				data : new Array(),
				type : 'GET'
			},
			this : this,
		}).then(this.PromiseAjax)
		.then(function(obj){
			obj.this.lawActivity = obj.data;
			obj.this.getlow(obj);
		})
		.catch(function(err){
			variables.showError(err);
		});
	}

	Teacher.prototype.getlow = function(obj){
		var x = Promise.resolve({
			data : {
				url : APPIQUERYURL + "/api/v1/laws",
				data : new Array(),
				type : 'GET'
			},
			this : obj.this,
			activity : obj.data
		})
		if(variables.Laws.length == 0){
			x.then(function(obj){
				return new Promise(function(resolve,reject){
					obj.this.Lows = obj.data;
					return resolve(obj);
				})
			})
		}else{
			x.then(function(obj){
				return new Promise(function(resolve,reject){
					obj.data = variables.Laws;
					obj.this.Lows = variables.Laws;
					return resolve(obj);
				})
			})
		}

		x.then(this.appendLow)
		.catch(function(err){
			variables.showError(err);
		});
	}

	Teacher.prototype.appendLow = function(obj){
		var _this = this;
		return new Promise(function(resolve,reject){
			var _this = obj.this;
			if(obj.activity.length){
				var table = $('.teacher-row'),
					activity = obj.activity,
					data = obj.data,
					electionType = obj.electionType,
					summary = '',
					lawType = '',
					result = '';

				var data = {
					this : _this,
					activity : activity,
					lows : data,
					table : table
				}

				_this.LowHtml(data,false);
				return resolve({messages:'Lows is shown.'});
			}else{
				return reject({messages:'No Low.'});
			}
		});
	}

	Teacher.prototype.LowHtml = function(data,time){
		var _this = data.this,
			activity = data.activity,
			table = data.table,
			lows = data.lows;

			for(i in activity){
				for(k in lows){
					if(lows[k].Guid == activity[i].Law.Guid){
						summary = _this.joinEvents(activity[i].Events,time);
						if(summary){
							switch(lows[k].State){
								case 'passed' :
									lawType = 'Passed with ';
									break;
								case 'failed' :
									lawType = 'Failed with ';
									break;
								default :
									lawType = ' ';
									break;
							}
							result = lawType + lows[k].VotesYes + ' Yes : ' + lows[k].VotesNo + ' No';
							_this.appendHTML({summary:summary, result:result, table:table, name:lows[k].Title,link:APPIQUERYURL+'/laws.html?lawid='+lows[k].Guid});
						}
					}
				}
			}
	}
	// Low functions end

	// helper functions start
	Teacher.prototype.joinEvents = function(events,time){
		return events.filter(function(elem){
			if(time){
				if( elem.EventTime > time[0] && elem.EventTime < time[1] ){
					return true;
				}
				return false;
			}else{
				return true;
			}
		}).map(function(elem){
			var players = elem.Players.join(', ');
			return (players) ? '<span title="'+elem.Players.join(', ')+'">'+elem.Summary+'</span>' : elem.Summary;
		}).join(", ");
	}

	Teacher.prototype.PromiseAjax = function(obj){
		return new Promise(function(resolve,reject){
			$.ajax({
				url: obj.data.url,
				data: obj.data.data,
				type: obj.data.type,
				dataType: 'json',
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

	Teacher.prototype.timeFilter = function(TeacherObj){
		var _this = TeacherObj;

		return function(time){
			$('.teacher-row').html('');

			var lowData = {
				this : _this,
				activity : _this.lawActivity,
				lows : _this.Lows,
				table : $('.teacher-row')
			};
			_this.LowHtml(lowData,time);

			var currentElectionData = {
				this : _this,
				activity : _this.electionActivity,
				election : _this.currentElections,
				electionType : 'current',
				table : $('.teacher-row')
			};
			_this.electionHtml(currentElectionData,time);

			var previousElectionData = {
				this : _this,
				activity : _this.electionActivity,
				election : _this.previousElections,
				electionType : 'previous',
				table : $('.teacher-row')
			};
			_this.electionHtml(previousElectionData,time);
		}


	}

	Teacher.prototype.appendHTML = function(data){
		data.table.append('<div class="data-row"> <div> <p><a href='+data.link+'>'+data.name+'</a></p> </div> <div class="resut-col"> <p>'+data.result+'</p> </div> <div> <p class="summary">'+data.summary+'</p> </div> </div>');
		$('.activity-noactivity').parent().remove();
	}
	// helper functions end

	// initialization start
	Teacher.prototype.bindEvent = function(){
		this.showElection();
		this.showLow();
	}

	// create unique Teacher object
	var teacher = new Teacher();
	$(window).load(function(){
		teacherFilter = teacher.timeFilter(teacher);
		teacher.bindEvent();
	})
	// initialization end
})()