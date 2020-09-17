import { Component, Input, OnInit, Output, EventEmitter } from '@angular/core';
import * as signalR from '@microsoft/signalr';

@Component({
    selector: 'log-events',
    templateUrl: './log-events.component.html',
    styleUrls: ['./log-events.component.scss']
})
export class LogEventsComponent implements OnInit {

    @Input() hubUrl: string;
    @Output() onNewEvent: EventEmitter<any[]> = new EventEmitter<any[]>();

    constructor() { }

    ngOnInit(): void {
        let connection = new signalR.HubConnectionBuilder()
            .withUrl(this.hubUrl)
            .build();

        connection.on('newEvent', (...args: any[]) => {
            this.onNewEvent.emit([...args]);
        });

        connection.start()
            .then(() => console.log(`Connected to hub`))
            .catch(() => console.log(`Can't connect to hub!`));
    }
}
