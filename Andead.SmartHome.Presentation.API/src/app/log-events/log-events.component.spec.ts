import { NO_ERRORS_SCHEMA } from '@angular/core';
import { ComponentFixture, TestBed } from '@angular/core/testing';
import { LogEventsComponent } from './log-events.component';

describe('LogEventsComponent', () => {
    let component: LogEventsComponent;
    let fixture: ComponentFixture<LogEventsComponent>;

    beforeEach(async () => {
        await TestBed.configureTestingModule({
            declarations: [LogEventsComponent]
        })
        .compileComponents();
    });

    beforeEach(() => {
        fixture = TestBed.createComponent(LogEventsComponent);
        component = fixture.componentInstance;
        fixture.detectChanges();
    });

    it('should create', () => {
        expect(component).toBeTruthy();
    });
});
