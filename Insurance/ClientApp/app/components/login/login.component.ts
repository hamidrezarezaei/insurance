import { Component, Input, Output, EventEmitter } from '@angular/core';
import { authService } from '../../services/auth.service';

//import { fieldSetComponent } from '../fieldSet/fieldSet.component';

@Component({
    selector: 'login',
    templateUrl: './login.component.html',
    styleUrls: ['./login.component.css']
})
export class loginComponent {
    _authService: authService;
    @Output() previousStepClicked = new EventEmitter();

    constructor(authService: authService) {
        this._authService = authService;
    }

    currentTab = 1;
    changeTab(tab: number) {
        this.currentTab = tab;
        this._authService.user.clientMessage = "";
    }
    previousStepClick() {
        this.previousStepClicked.emit();
    }
}
