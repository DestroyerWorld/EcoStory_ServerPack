/// <reference path="../app.ts" />

'use strict';

module EcoServerConfig {
  export interface IMainScope extends ng.IScope {
    awesomeThings: any[];
  }

  export class MainController {

    constructor (private $scope: IMainScope) {
      $scope.awesomeThings = [
        'HTML5 Boilerplate',
        'AngularJS',
        'Karma'
      ];
    }
  }
}

angular.module('EcoServerConfig')
    .controller('MainController', EcoServerConfig.MainController);
