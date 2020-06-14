import { INode } from "@mrblenny/react-flow-chart";
import Chart from "../models/chart.model";

export enum TriggerType {
    And = 0,
    Or = 1
}

export class TriggersService {
    init(): Chart {
        return new Chart();
    }

    private getRow(chart: Chart, positionY: number): any {
        let result = Object.keys(chart.nodes)
            .filter(node => chart.nodes[node].position.y === positionY)
            .map(node => chart.nodes[node].size.width);

        return result.length === 0 ? [0] : result;
    }

    private getXOffset(chart: Chart, positionY: number): number {
        let xOffset = this.getRow(chart, positionY).reduce((x: number, y: number) => x + y + 20);
        return xOffset === 0 ? 100 : xOffset + 120;
    }

    private getNodeById(chart: Chart, nodeId: string): any {
        let node = Object.keys(chart.nodes).find(node => chart.nodes[node].id === nodeId);

        if (!node)
            return null;

        return chart.nodes[node];
    }

    getNodeOutputCount(node: INode): number {
        return Object.keys(node.ports).filter(port => node.ports[port].type === "output").length;
    }

    addTrigger(chart: Chart, node: INode, triggerType: TriggerType): Chart {
        let nodesCount = Object.keys(chart.nodes).length + 1;
        let linksCount = Object.keys(chart.links).length + 1;

        let parentNode = this.getNodeById(chart, node.id);

        Object.assign(chart.nodes, {
            [`node${nodesCount}`]: {
                id: `node${nodesCount}`,
                type: `Step ${nodesCount}`,
                position: {
                    x: this.getXOffset(chart, parentNode.position.y + 150),
                    y: parentNode.position.y + 150
                },
                ports: {
                    port1: {
                        id: "port1",
                        type: "input"
                    }
                }
            }
        });

        if (triggerType === TriggerType.And) {
            Object.assign(chart.nodes[parentNode.id].ports, {
                port2: {
                    id: "port2",
                    type: "output",
                }
            });
        }

        Object.assign(chart.links, {
            [`link${linksCount}`]: {
                id: `link${linksCount}`,
                from: {
                    nodeId: parentNode.id,
                    portId: "port2"
                },
                to: {
                    nodeId: `node${nodesCount}`,
                    portId: "port1"
                }
            }
        });

        return chart;
    }
}