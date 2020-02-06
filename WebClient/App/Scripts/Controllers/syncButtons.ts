/// <reference path="../app.ts" />
/// <reference path="../Services/resources.ts" />

'use strict';

module EcoServerConfig {
    export class SyncButtonsController {
        public plugins;

        constructor(private EcoServerResource: EcoServerResource, private $scope: any) {
            this.plugins = EcoServerResource.Plugins().query();
        }
        
        public onSubmit() {
            console.log('onSubmit data in sync controller', this.$scope.editor.getValue());
        };

        public onAction2() {
            console.log('onAction2');
        };;
    }
}

angular.module('EcoServerConfig')
    .controller('SyncButtonsController', EcoServerConfig.SyncButtonsController);
