;(function(){
	var ViewDistricts = function(){
		this.laws = []
	}

	ViewDistricts.prototype.loadDistricts = function(){
		var _this = this;
		var data = new Object();

		Promise.resolve({
			data : {
				url : APPIQUERYURL + "/api/v1/laws/districts",
				data : {},
				type : 'GET'
			},
			this : this
		}).then(this.PromiseAjax)
		.then(function(obj){
			return new Promise(function(resolve,reject){
				if( obj.data.DistrictMap != undefined ) {
					$(".map-part").show();

					$("#dstricts-map").append('<div class="map-dist"><input type="hidden" class="map-data" value=""><div><div class="view-map"><p id="map-position-text"></p><p id="map-info-text"></p><div type="text" class="progress-day" name="progress-day">Day 0, 00:00</div><input type="button" data-type="2" value="2D" class="map-toggle-2d"></div></div></div>');

					var view = $(".map-dist .view-map")[0];
					view.mapdata = $(".map-dist .map-data")[0];
					view.settings = {
						layerSelected: "ProposedDistricts"
					}
					ECO.MINIMAP.addView(view);

					resolve(obj);
				} else {
					$(".there-no-districts").show();
				}
			})
		}).then(function(obj){
			return new Promise(function(resolve,reject){
				variables.LoadProposedDistricts(obj.data)

				for(var i in obj.data.DistrictMetadata) {
					var d = obj.data.DistrictMetadata[i];

					var rgb = "rgb("+d.R+","+d.G+","+d.B+")";
					if( d.ID == 0 ) continue;
					$(".district-controls-wrapper").append('<div class="district-item proposed-districts" title="'+d.Name+'" data-id="'+d.ID+'" data-color="'+JSON.stringify(d)+'" style="background:'+rgb+'">'+d.Name+'</div>');
				}

				resolve(obj);
			})
		})
		.catch(function(err){
			variables.showError(err);
		});
	}

	ViewDistricts.prototype.PromiseAjax = function(obj){
		return new Promise(function(resolve,reject){
			$.ajax({
				url: obj.data.url,
				data: obj.data.data,
				type: obj.data.type,
				contentType: "application/json",
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

	ViewDistricts.prototype.bindEvent = function(){
		var _this = this;
	}

	ViewDistricts.prototype.init = function(){
		var _this = this;
		$(document).ready(function(){
			_this.bindEvent();
			setTimeout(function(){_this.loadDistricts()},1000)
		})
	}

	window.viewdistricts = new ViewDistricts();
	viewdistricts.init();

})()