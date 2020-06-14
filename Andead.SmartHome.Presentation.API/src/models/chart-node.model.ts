import { INode, IPosition } from "@mrblenny/react-flow-chart";

export default class ChartNode implements INode {
    id: string;
    type: string;
    position: IPosition;
    ports: any = {};

    constructor(id: string, type: string, x: number, y: number) {
        this.id = id;
        this.type = type;
        this.position = {
            x: x,
            y: y
        };
    }
}