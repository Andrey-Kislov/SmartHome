import React, { Component } from "react";
import { FlowChartWithState } from "@mrblenny/react-flow-chart";
import { connect } from "react-redux";

import FlowNode from "../components/flow-node";

interface IFlowChart {
    chart: any;
}

class _FlowChart extends Component<IFlowChart> {
    render() {
        return (
            <div>
                <FlowChartWithState
                    config={{ readonly: true }}
                    initialValue={this.props.chart}
                    Components={{
                        NodeInner: FlowNode
                    }}
                />
            </div>
        );
    }
}

const mapStateToProps = (state: any) => {
    return {
        chart: state.triggers.chart
    };
};

const FlowChart = connect(mapStateToProps)(_FlowChart);
export default FlowChart;