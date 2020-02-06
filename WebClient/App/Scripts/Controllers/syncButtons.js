/// <reference path="../app.ts" />
/// <reference path="../Services/resources.ts" />
'use strict';
var EcoServerConfig;
(function (EcoServerConfig) {
    var SyncButtonsController = (function () {
        function SyncButtonsController(EcoServerResource, $scope) {
            this.EcoServerResource = EcoServerResource;
            this.$scope = $scope;
            this.plugins = EcoServerResource.Plugins().query();
        }
        SyncButtonsController.prototype.onSubmit = function () {
            console.log('onSubmit data in sync controller', this.$scope.editor.getValue());
        };
        ;
        SyncButtonsController.prototype.onAction2 = function () {
            console.log('onAction2');
        };
        ;
        ;
        return SyncButtonsController;
    })();
    EcoServerConfig.SyncButtonsController = SyncButtonsController;
})(EcoServerConfig || (EcoServerConfig = {}));
angular.module('EcoServerConfig')
    .controller('SyncButtonsController', EcoServerConfig.SyncButtonsController);
