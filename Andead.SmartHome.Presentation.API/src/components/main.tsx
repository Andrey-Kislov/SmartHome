import React, { Component } from 'react';
import './main.css';
import FlowChart from './flow-chart';
import LogEvents from './log-events';

export default class Main extends Component {
    logHub(dateTime: string, message: string) {
        console.log(`${dateTime}: ${message}`);
    }

    deviceStateHub(dateTime: string, deviceId: number, attributeId: number, value: string) {
        console.log(`${dateTime}: DeviceId=${deviceId}, AttributeId=${attributeId}, Value='${value}'`);
    }

    render() {
        return (
            <div className="App">
                <FlowChart />
                <LogEvents hubUrl="/log" onNewEvent={this.logHub} />
                <LogEvents hubUrl="/states" onNewEvent={this.deviceStateHub} />
            </div >
        );
    }
}
