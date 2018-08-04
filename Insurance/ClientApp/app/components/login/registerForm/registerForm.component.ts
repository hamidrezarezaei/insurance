import { Component, Input, Output, EventEmitter } from '@angular/core';
//import { fieldSetComponent } from '../fieldSet/fieldSet.component';
import {  authService } from '../../../services/auth.service';
import { NgForm } from "@angular/forms";

@Component({
    selector: 'registerForm',
    templateUrl: './registerForm.component.html',
    styleUrls: ['./registerForm.component.css']
})
export class registerFormComponent {
    _authService: authService
    @Output() previousStepClicked = new EventEmitter();

    constructor(authService: authService) {
        this._authService = authService;
    }

    Register(form: NgForm): void {
        this._authService.Register(form.value.firstName, form.value.lastName,
            form.value.phoneNumber, form.value.nationalCode, form.value.email, 
            form.value.actualUserName, form.value.passWord, form.value.passWord2);
    }
    previousStepClick() {
        this.previousStepClicked.emit();
    }
}
