import { ACTION_TRIGGERS } from '../actions/constants';
import { TriggersService } from '../services/triggers.service';

const triggersService = new TriggersService();

const initState = {
    chart: triggersService.init(),
    triggersService: triggersService
};

export function triggers(state = initState, action: any) {
    switch (action.type) {
        case ACTION_TRIGGERS.addTrigger: {
            let chart = Object.assign({}, state.chart);

            triggersService.addTrigger(chart, action.payload.node, action.payload.triggerType);

            return Object.assign({}, state, {
                chart: chart
            });
        }

        default:
            return state;
    }
}