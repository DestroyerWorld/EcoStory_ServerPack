/// <reference path="../app.ts" />
'use strict';
var EcoServerConfig;
(function (EcoServerConfig) {
    var MainController = (function () {
        function MainController($scope) {
            this.$scope = $scope;
            $scope.awesomeThings = [
                'HTML5 Boilerplate',
                'AngularJS',
                'Karma'
            ];
        }
        return MainController;
    })();
    EcoServerConfig.MainController = MainController;
})(EcoServerConfig || (EcoServerConfig = {}));
angular.module('EcoServerConfig')
    .controller('MainController', EcoServerConfig.MainController);
