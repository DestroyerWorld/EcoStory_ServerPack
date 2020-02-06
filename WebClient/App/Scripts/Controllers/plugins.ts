/// <reference path="../app.ts" />
/// <reference path="../Services/resources.ts" />

'use strict';

module EcoServerConfig {

    class Graph {
        public name: string[];
        public data: number[][];
        public labels: string[];
        public options = {
            animation: false,
        };
    }


    export class PluginsController {
        public plugins;

        public selectedPlugin;

        public tabs = {
            display: { enable: false, active: false },
            config: { enable: false, active: false },
            graphs: { enable: false, active: false }
        };

        public graphs: Graph[] = [];

        constructor(private EcoServerResource: EcoServerResource) {
            this.plugins = EcoServerResource.Plugins().query();
        }

        PluginSelect(plugin): void {
            this.selectedPlugin = plugin;

            this.tabs.config.enable = this.selectedPlugin.editableObject;
            this.tabs.display.enable = this.selectedPlugin.displayText;
            this.tabs.graphs.enable = this.selectedPlugin.graphs;

            if (this.tabs.config.enable) {
                this.tabs.config.active = true;
            } else if (this.tabs.display.enable) {
                this.tabs.display.active = true;
            } else if (this.tabs.graphs.enable) {
                this.tabs.graphs.active = true;
            }


            // organse chart data

            this.graphs = [];

            if (this.selectedPlugin.graphs) {
                angular.forEach(this.selectedPlugin.graphs, (value, key) => {
                    var graph = new Graph();
                    graph.name = ["series"];
                    graph.data = [value.series];
                    graph.labels = [];
                    for (var i = 0; i < value.series.length; ++i) {
                        graph.labels.push(i.toString());
                    }

                    this.graphs.push(graph);
                });
            }
        }
    }
}

angular.module('EcoServerConfig')
    .controller('PluginsController', EcoServerConfig.PluginsController);
