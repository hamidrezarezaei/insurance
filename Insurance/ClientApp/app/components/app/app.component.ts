import { Component } from '@angular/core';
import { authService } from '../../services/auth.service';
import { insuranceService } from '../../services/insurance.service';


@Component({
    selector: 'app',
    templateUrl: './app.component.html',
    styleUrls: ['./app.component.css'],
    providers: [authService, insuranceService]

})
export class AppComponent {
    _authService: authService;
    _insuranceService: insuranceService;

    constructor(authService: authService, insuranceService: insuranceService) {
        this._authService = authService;
        this._insuranceService = insuranceService;
    }
}
