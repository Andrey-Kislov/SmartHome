import { Component } from 'react';
import * as signalR from '@microsoft/signalr';

interface IHubProps {
    hubUrl: string;
    onNewEvent: Function;
}

export default class LogEvents extends Component<IHubProps> {
    componentDidMount() {
        let connection = new signalR.HubConnectionBuilder().withUrl(this.props.hubUrl).build();

        connection.on('newEvent', (...args: any[]) => this.props.onNewEvent(...args));

        connection.start()
            .then(() => console.log(`Connected to hub`))
            .catch(() => console.log(`Can't connect to hub!`));
    }

    render() {
        return null;
    }
}