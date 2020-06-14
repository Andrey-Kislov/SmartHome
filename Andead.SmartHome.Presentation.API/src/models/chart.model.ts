import ChartNode from "./chart-node.model";

export default class Chart {
    constructor() {
        this.nodes = {
            node1: new ChartNode("node1", "Step 1", 100, 100)
        };
    }

    offset: any = {
        x: 0,
        y: 0
    };

    scale: number = 1;

    nodes: any = {};
    links: any = {};
    selected: any = {};
    hovered: any = {};
}