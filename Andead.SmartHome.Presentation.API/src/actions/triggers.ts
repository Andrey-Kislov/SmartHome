import { INode } from '@mrblenny/react-flow-chart';
import { ACTION_TRIGGERS } from './constants';
import { TriggerType } from '../services/triggers.service';

export function addTrigger(node: INode, triggerType: TriggerType) {
    return {
        type: ACTION_TRIGGERS.addTrigger,
        payload: { node, triggerType }
    };
}
