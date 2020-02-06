;(function(){
	var Players = function(){
		this.Players = new Array();
	}

	// Players functions start
	Players.prototype.showPlayers = function(time){
		var data = new Object();

		if(time){
			data.startDay = time[0];
			data.endDay = time[1];
		}

		Promise.resolve({
			data : {
				url : APPIQUERYURL + "/api/v1/analysis/playstyles",
				data : data,
				type : 'GET'
			},
			this : this,
		}).then(this.PromiseAjax)
		.then(function(obj){
			return new Promise(function(resolve,reject){
				obj.data = obj.this.groupData(obj.data);
				obj.this.Players = obj.data;
				return resolve(obj);
			})
		})
		.then(this.appendPlayers)
		.catch(function(err){
			variables.showError(err);
		});
	}

	Players.prototype.appendPlayers = function(obj){
		return new Promise(function(resolve,reject){
			var _this = obj.this;
			if(Object.keys(obj.data).length){
				$('.players-table').html('');
				var table = $('.players-table'),
					data = obj.data;

				var data = {
					this : _this,
					players : data,
					table : table
				}

				_this.playersHtml(data,false)

				variables.Playstyles = true;
				variables.citizenActivity();

				return resolve({messages:'Election is shown.'});
			}else{
				return reject({messages:'No Election.'});
			}
		});
	}

	Players.prototype.playersHtml = function(data,time){
		var _this = data.this,
			table = data.table,
			summary = data.this.joinContributingStats($.extend(true, {}, data.players)),
			players = data.players;
		for(i in players){
			_this.appendHTML({group:i, players : players, count:players[i].length, table:table, summary:summary});
		}
	}
	// Players functions end


	// helper functions start
	Players.prototype.groupData = function(players,time){
		var data = new Array();

		for(i in players){
			if(data[players[i].Playstyle]){
				data[players[i].Playstyle].push(players[i]);
			}else{
				data[players[i].Playstyle] = new Array();
				data[players[i].Playstyle].push(players[i]);
			}
		}

		return data;
	}

	Players.prototype.joinContributingStats = function(players,time){
		for(i in players){
			players[i] = players[i].map(function(k){
				return k.ContributingStats.map(function(e){
					return e.Summary;
				}).join(', ');
			});
		}
		return players;
	}


	Players.prototype.PromiseAjax = function(obj){
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

	Players.prototype.timeFilter = function(PlayersObj){
		var _this = PlayersObj;

		return function(time){
			_this.showPlayers(time);
		}
	}

	Players.prototype.appendHTML = function(data){
		var summary = '';

		for(i in data.players[data.group]){
			if(data.summary[data.group][i]){
				summary += '<p>'+data.players[data.group][i].Username+' '+data.summary[data.group][i]+'</p>';
			} else {
				summary += '<p>'+data.players[data.group][i].Username+' did nothing</p>';
			}
		}

		data.table.append('<div class="players-group">'+data.count+' players focused on '+data.group+'</div> <div class="players-content"> '+summary+' </div>');
	}
	// helper functions end

	// initialization start
	Players.prototype.bindEvent = function(){
		this.showPlayers();
	}

	// create unique Teacher object
	var players = new Players();

	$(window).load(function(){
		playersFilter = players.timeFilter(players);
		players.bindEvent();
	})
	// initialization end
})()