import * as React from "react";
import { Component } from "react";
import { connect } from "react-redux";
import { INode } from "@mrblenny/react-flow-chart";
import Button from "devextreme-react/button";

import { addTrigger } from "../actions/triggers";
import { TriggersService, TriggerType }  from "../services/triggers.service";

interface IFlowNodeActions {
    addAndTrigger: any,
    addOrTrigger: any,
    node: INode,
    triggersService: TriggersService
}

class _FlowNodeActions extends Component<IFlowNodeActions> {
    render() {
        const nodeOutputCount = this.props.triggersService.getNodeOutputCount(this.props.node);

        return (
            <div>
                {nodeOutputCount === 0 ?
                    <Button text="AND" onClick={() => this.props.addAndTrigger(this.props.node)} />
                    :
                    <Button text="OR" onClick={() => this.props.addOrTrigger(this.props.node)} />
                }
            </div>
        );
    }
}

const mapStateToProps = (state: any) => {
    return {
        triggersService: state.triggers.triggersService
    };
};

const mapDispatchToProps = (dispatch: any) => {
    return {
        addAndTrigger: (node: INode) => dispatch(addTrigger(node, TriggerType.And)),
        addOrTrigger: (node: INode) => dispatch(addTrigger(node, TriggerType.Or)),
    };
};

const FlowNodeActions = connect(mapStateToProps, mapDispatchToProps)(_FlowNodeActions);
export default FlowNodeActions;