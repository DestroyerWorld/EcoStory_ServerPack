/// <reference path="../../../typings/tsd.d.ts" />
'use strict';
var EcoServerConfig;
(function (EcoServerConfig) {
    var EcoServerResource = (function () {
        function EcoServerResource($resource, $location) {
            this.$resource = $resource;
            this.$location = $location;
            this.baseSiteUrl = $location.protocol() + "://" + $location.host() + ":" + $location.port();
            this.baseApiUrl = this.baseSiteUrl + "/api/";
        }
        EcoServerResource.prototype.Plugins = function () {
            return this.$resource(this.baseApiUrl + "v1/plugins");
        };
        EcoServerResource.$inject = ["$resource", "$location"];
        return EcoServerResource;
    })();
    EcoServerConfig.EcoServerResource = EcoServerResource;
    angular
        .module('EcoServerConfig')
        .service("EcoServerResource", EcoServerResource);
})(EcoServerConfig || (EcoServerConfig = {}));
