/// <reference path="../app.ts" />
/// <reference path="../Services/resources.ts" />
'use strict';
var EcoServerConfig;
(function (EcoServerConfig) {
    var Graph = (function () {
        function Graph() {
            this.options = {
                animation: false,
            };
        }
        return Graph;
    })();
    var PluginsController = (function () {
        function PluginsController(EcoServerResource) {
            this.EcoServerResource = EcoServerResource;
            this.tabs = {
                display: { enable: false, active: false },
                config: { enable: false, active: false },
                graphs: { enable: false, active: false }
            };
            this.graphs = [];
            this.plugins = EcoServerResource.Plugins().query();
        }
        PluginsController.prototype.PluginSelect = function (plugin) {
            var _this = this;
            this.selectedPlugin = plugin;
            this.tabs.config.enable = this.selectedPlugin.editableObject;
            this.tabs.display.enable = this.selectedPlugin.displayText;
            this.tabs.graphs.enable = this.selectedPlugin.graphs;
            if (this.tabs.config.enable) {
                this.tabs.config.active = true;
            }
            else if (this.tabs.display.enable) {
                this.tabs.display.active = true;
            }
            else if (this.tabs.graphs.enable) {
                this.tabs.graphs.active = true;
            }
            // organse chart data
            this.graphs = [];
            if (this.selectedPlugin.graphs) {
                angular.forEach(this.selectedPlugin.graphs, function (value, key) {
                    var graph = new Graph();
                    graph.name = ["series"];
                    graph.data = [value.series];
                    graph.labels = [];
                    for (var i = 0; i < value.series.length; ++i) {
                        graph.labels.push(i.toString());
                    }
                    _this.graphs.push(graph);
                });
            }
        };
        return PluginsController;
    })();
    EcoServerConfig.PluginsController = PluginsController;
})(EcoServerConfig || (EcoServerConfig = {}));
angular.module('EcoServerConfig')
    .controller('PluginsController', EcoServerConfig.PluginsController);
