import { Component, Input, Output, EventEmitter } from '@angular/core';
import { fieldComponent } from '../field/field.component';

@Component({
    selector: 'fieldSet',
    templateUrl: './fieldSet.component.html',
    styleUrls: ['./fieldSet.component.css']
})
export class fieldSetComponent {
    @Input() fieldSet: any;
    @Input() insurance: any;
    @Input() step: any;
    @Output() fieldValueChanged = new EventEmitter<any>();

    fieldValueChange(field: any) {
        //console.log('field set:value of ' + field.name + ' change to => ' + field.value);
        this.fieldValueChanged.emit(field);
    }
}
