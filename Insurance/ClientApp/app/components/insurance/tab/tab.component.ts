import { Component, Input, Output, EventEmitter } from '@angular/core';
import { insuranceComponent } from '../insurance.component';

@Component({
    selector: 'tab',
    templateUrl: './tab.component.html',
    styleUrls: ['./tab.component.css']
})
export class tabComponent {
    @Input() insurances: any;
    @Input() currentInsurance: any;
    @Output() tabChanged = new EventEmitter<any>();


    tabChange(insurance: any) {
        //this.currentInsurance = insurance;
        this.tabChanged.emit(insurance);
    }


}
