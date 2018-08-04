import { Component, Input, Output, EventEmitter } from '@angular/core';
import { stepComponent } from '../step/step.component';

@Component({
    selector: 'aInsurance',
    templateUrl: './aInsurance.component.html',
    styleUrls: ['./aInsurance.component.css']
})
export class aInsuranceComponent {
    @Input() insurance: any;
    @Output() fieldValueChanged = new EventEmitter<any>();
    @Output() stepChanged = new EventEmitter<any>();

    fieldValueChange(field: any) {
        this.fieldValueChanged.emit(field);
    }
    stepChange(insurance: any) {
        this.stepChanged.emit(insurance);
    }
}
