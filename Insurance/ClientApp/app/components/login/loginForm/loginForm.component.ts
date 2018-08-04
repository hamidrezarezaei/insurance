import { Component, Input, Output, EventEmitter } from '@angular/core';
import { authService } from '../../../services/auth.service';
import { NgForm } from "@angular/forms";
//import { fieldSetComponent } from '../fieldSet/fieldSet.component';

@Component({
    selector: 'loginForm',
    templateUrl: './loginForm.component.html',
    styleUrls: ['./loginForm.component.css']
})
export class loginFormComponent {
    _authService: authService;

    @Output() previousStepClicked = new EventEmitter();

    constructor(authService: authService) {
        this._authService = authService;
    }

    Login(form: NgForm): void {
        //console.log(form.value.passWord);
        this._authService.login(form.value.actualUserName, form.value.passWord);
    }
    previousStepClick() {
        this.previousStepClicked.emit();
    }
}
