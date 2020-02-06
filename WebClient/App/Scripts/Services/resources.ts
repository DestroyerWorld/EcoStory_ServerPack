/// <reference path="../../../typings/tsd.d.ts" />

'use strict';

module EcoServerConfig {
    export class EcoServerResource {
        public baseApiUrl;
        public baseSiteUrl;

        static $inject = ["$resource", "$location"];

        constructor(private $resource: ng.resource.IResourceService, private $location: ng.ILocationService) {
            this.baseSiteUrl = $location.protocol() + "://" + $location.host() + ":" + $location.port();
            this.baseApiUrl = this.baseSiteUrl + "/api/";
        }

        public Plugins(): ng.resource.IResourceClass<any> {
            return <ng.resource.IResourceClass<any>>this.$resource(this.baseApiUrl + "v1/plugins");
        }
    }

    angular
        .module('EcoServerConfig')
        .service("EcoServerResource", EcoServerResource);

}