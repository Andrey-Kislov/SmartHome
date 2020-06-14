import * as React from "react";
import { INodeInnerDefaultProps } from "@mrblenny/react-flow-chart";
import styled from "styled-components";

import FlowNodeActions from "./flow-node-actions";

const Outer = styled.div`
    padding: 30px;
`;

const FlowNode = ({ node, config }: INodeInnerDefaultProps) => {
    return <Outer><FlowNodeActions node={node} /></Outer>;
}

export default FlowNode;