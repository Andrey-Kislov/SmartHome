import { Component } from 'react';
import * as signalR from '@microsoft/signalr';

export default class LogEvents extends Component {
    componentDidMount() {
        let connection = new signalR.HubConnectionBuilder().withUrl('/log').build();

        connection.on('newEvent', (dateTime: string, message: string) => {
            console.log(`[${dateTime}]: ${message}`);
        });

        connection.start()
            .then(() => console.log(`Connected to hub`))
            .catch(() => console.log(`Can't connect to hub!`));
    }

    render() {
        return null;
    }
}