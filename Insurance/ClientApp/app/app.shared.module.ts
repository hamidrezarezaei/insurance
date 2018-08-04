import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { HttpModule } from '@angular/http';
import { RouterModule } from '@angular/router';
import { DpDatePickerModule } from 'ng2-jalali-date-picker';
import { NgSelect2Module } from 'ng-select2';

import { frenchDecimalPipe } from "./pipe/frenchDecimal.pipe";

import { AppComponent } from './components/app/app.component';

import { insuranceComponent } from './components/insurance/insurance.component';
import { tabComponent } from './components/insurance/tab/tab.component';
import { stepNavigationComponent } from './components/insurance/stepNavigation/stepNavigation.component';
import { aInsuranceComponent } from './components/insurance/aInsurance/aInsurance.component';
import { stepComponent } from './components/insurance/step/step.component';
import { fieldSetComponent } from './components/insurance/fieldSet/fieldSet.component';
import { fieldComponent } from './components/insurance/field/field.component';

import { loginComponent } from './components/login/login.component';
import { loginFormComponent } from './components/login/loginForm/loginForm.component';
import { registerFormComponent } from './components/login/registerForm/registerForm.component';

@NgModule({
    declarations: [
        frenchDecimalPipe,
        AppComponent,
        insuranceComponent,
        tabComponent,
        stepNavigationComponent,
        aInsuranceComponent,
        stepComponent,
        fieldSetComponent,
        fieldComponent,

        loginComponent,
        loginFormComponent,
        registerFormComponent,
    ],
    imports: [
        DpDatePickerModule,
        NgSelect2Module,
        CommonModule,
        HttpModule,
        FormsModule,
        RouterModule.forRoot([
            { path: '', redirectTo: 'insurance', pathMatch: 'full' },
            { path: '**', redirectTo: 'home' }
        ])
    ]
})
export class AppModuleShared {
}
