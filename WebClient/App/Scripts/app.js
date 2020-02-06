/// <reference path="../../typings/angularjs/angular.d.ts" />
/// <reference path="../../typings/angularjs/angular-route.d.ts" />
'use strict';
angular
    .module('EcoServerConfig', [
    'ngAnimate',
    'ngCookies',
    'ngMessages',
    'ngResource',
    'ngRoute',
    'ngSanitize',
    'ngTouch',
    'ui.bootstrap',
    'angular-json-editor',
    'chart.js'
])
    .config(function ($routeProvider) {
    $routeProvider
        .when('/', {
        templateUrl: 'Views/main.html',
        controller: 'MainController',
        controllerAs: 'vm'
    })
        .when('/plugins', {
        templateUrl: 'Views/plugins.html',
        controller: 'PluginsController',
        controllerAs: 'vm'
    })
        .otherwise({
        redirectTo: '/'
    });
})
    .config(function (JSONEditorProvider) {
    JSONEditorProvider.configure({
        //plugins: {
        //    sceditor: {
        //        style: 'sce/development/jquery.sceditor.default.css'
        //    }
        //},
        defaults: {
            options: {
                iconlib: 'bootstrap3',
                theme: 'bootstrap3',
                ajax: false,
                disable_edit_json: true,
                disable_properties: true,
                disable_collapse: true,
            }
        }
    });
});
