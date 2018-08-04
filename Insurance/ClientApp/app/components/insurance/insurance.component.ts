import { Component, Input, Output, EventEmitter } from '@angular/core';
import { insuranceService } from '../../services/insurance.service';
import { tabComponent } from './tab/tab.component';
import { aInsuranceComponent } from './aInsurance/aInsurance.component';
import { authService } from '../../services/auth.service';

@Component({
    selector: 'insurance',
    templateUrl: './insurance.component.html',
    styleUrls: ['./insurance.component.css'],
    providers: [/*insuranceService*/]

})
export class insuranceComponent {
    @Input() insurances: any;
    @Input() currentInsurance: any;

    _authService: authService;
    _insuranceService: insuranceService;

    constructor(authService: authService,insuranceService: insuranceService) {
        this._authService = authService;
        this._insuranceService = insuranceService;
    }

  
    tabChange(insurance: any) {
        this._insuranceService.insuranceChange(insurance);
        
    }

    stepChange(insurance: any) {
        this._insuranceService.stepChange(insurance);
    }
}
