import { Component } from 'react';
import * as signalR from '@microsoft/signalr';

interface HubProps {
    hubUrl: string;
    onNewEvent: Function;
}

export default class LogEvents extends Component<HubProps> {
    componentDidMount() {
        let connection = new signalR.HubConnectionBuilder().withUrl(this.props.hubUrl).build();

        //connection.on('newEvent', (dateTime: string, message: string) => {
        //    console.log(`[${dateTime}]: ${message}`);
        //});

        connection.on('newEvent', (...args: any[]) => this.props.onNewEvent(...args));

        connection.start()
            .then(() => console.log(`Connected to hub`))
            .catch(() => console.log(`Can't connect to hub!`));
    }

    render() {
        return null;
    }
}