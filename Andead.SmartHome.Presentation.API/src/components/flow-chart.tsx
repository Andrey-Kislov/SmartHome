import React, { Component } from "react";
import { FlowChartWithState } from "@mrblenny/react-flow-chart";

export default class FlowChart extends Component {
    private chartSimple: any = {
        offset: {
            x: 0,
            y: 0
        },
        nodes: {
            node1: {
                id: "node1",
                type: "First step",
                position: {
                    x: 450,
                    y: 100
                },
                ports: {
                    port1: {
                        id: "port1",
                        type: "output",
                        properties: {
                            value: "yes"
                        }
                    }
                }
            },
            node2: {
                id: "node2",
                type: "Second step",
                position: {
                    x: 300,
                    y: 300
                },
                ports: {
                    port1: {
                        id: "port1",
                        type: "input"
                    },
                    port2: {
                        id: "port2",
                        type: "output",
                        properties: {
                            value: "yes"
                        }
                    }
                }
            },
            node3: {
                id: "node3",
                type: "Third step",
                position: {
                    x: 600,
                    y: 300
                },
                ports: {
                    port1: {
                        id: "port1",
                        type: "input"
                    },
                    port2: {
                        id: "port2",
                        type: "output",
                        properties: {
                            value: "yes"
                        }
                    }
                }
            },
            node4: {
                id: "node4",
                type: "Last step",
                position: {
                    x: 450,
                    y: 500
                },
                ports: {
                    port1: {
                        id: "port1",
                        type: "input"
                    },
                    port2: {
                        id: "port2",
                        type: "output",
                        properties: {
                            value: "yes"
                        }
                    }
                }
            }
        },
        links: {
            link1: {
                id: "link1",
                from: {
                    nodeId: "node1",
                    portId: "port1"
                },
                to: {
                    nodeId: "node2",
                    portId: "port1"
                },
            },
            link2: {
                id: "link2",
                from: {
                    nodeId: "node1",
                    portId: "port1"
                },
                to: {
                    nodeId: "node3",
                    portId: "port1"
                },
            },
            link3: {
                id: "link3",
                from: {
                    nodeId: "node2",
                    portId: "port2"
                },
                to: {
                    nodeId: "node4",
                    portId: "port1"
                },
            },
            link4: {
                id: "link4",
                from: {
                    nodeId: "node3",
                    portId: "port2"
                },
                to: {
                    nodeId: "node4",
                    portId: "port1"
                },
            }
        },
        selected: {},
        hovered: {}
    };

    render() {
        return <FlowChartWithState initialValue={this.chartSimple} />
    }
}