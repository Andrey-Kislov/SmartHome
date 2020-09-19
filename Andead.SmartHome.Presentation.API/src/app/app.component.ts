import { Component } from '@angular/core';

@Component({
    selector: 'app-root',
    templateUrl: './app.component.html',
    styleUrls: ['./app.component.scss']
})
export class AppComponent {
    title = 'smart-home';
    logHubUrl = '/log';

    logHub(logEvent: any[]): void {
        console.log(`${logEvent[0]}: ${logEvent[1]}`);
    }
}
