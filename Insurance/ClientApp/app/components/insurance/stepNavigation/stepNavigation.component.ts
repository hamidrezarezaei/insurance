import { Component, Input, Output, EventEmitter } from '@angular/core';

@Component({
    selector: 'stepNavigation',
    templateUrl: './stepNavigation.component.html',
    styleUrls: ['./stepNavigation.component.css']
})
export class stepNavigationComponent {
    @Input() insurance: any;
    @Output() stepChanged = new EventEmitter<any>();


    stepChange(number: number) {
        //this.currentInsurance = insurance;
        this.insurance.currentStep = number;
        this.stepChanged.emit(this.insurance);

    }
}
