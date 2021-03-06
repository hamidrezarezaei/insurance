import { Component, Input, Output, EventEmitter } from '@angular/core';
import { insuranceService } from '../../../services/insurance.service';
import { fieldSetComponent } from '../fieldSet/fieldSet.component';
import { authService } from '../../../services/auth.service';

@Component({
    selector: 'step',
    templateUrl: './step.component.html',
    styleUrls: ['./step.component.css']
})
export class stepComponent {
    @Input() step: any;
    @Input() insurance: any;
    @Output() fieldValueChanged = new EventEmitter<any>();
    @Output() stepChanged = new EventEmitter<any>();

    _authService: authService;
    _insuranceService: insuranceService;

    constructor(authService: authService, insuranceService: insuranceService) {
        this._authService = authService;
        this._insuranceService = insuranceService;
    }
    ngOnInit() {
        this._insuranceService.refreshFields(this.insurance);
    }
    nextStepClick() {
        this.insurance.currentStep++;
        this.stepChanged.emit(this.insurance);

    }
    previousStepClick() {
        this.insurance.currentStep--;
        this.stepChanged.emit(this.insurance);
    }
    fieldValueChange(field: any) {


        this.fieldValueChanged.emit(field);
    }

}
