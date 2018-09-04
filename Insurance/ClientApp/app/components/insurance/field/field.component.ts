import { Component, Input, Output, EventEmitter, ViewChild, ElementRef } from '@angular/core';
import * as moment from 'jalali-moment';
import { DatePickerDirective, DpDatePickerModule, CalendarValue } from 'ng2-jalali-date-picker';
import { Console } from '@angular/core/src/console';
import { NgForm } from "@angular/forms";
import { insuranceService } from '../../../services/insurance.service';
import { authService } from '../../../services/auth.service';

@Component({
    selector: 'field',
    templateUrl: './field.component.html',
    styleUrls: ['./field.component.css']
})
export class fieldComponent {
    @Input() insurance: any;
    @Input() step: any;
    @Input() fieldSet: any;
    @Input() field: any;
    // options: any;

    _insuranceService: insuranceService;
    _authService: authService;

    @Output() valueChanged = new EventEmitter<any>();

    constructor(insuranceService: insuranceService, authService: authService) {
        this.initYears();
        this._insuranceService = insuranceService;
        this._authService = authService;
    }

    ngOnInit() {
        if (this.field && this.field.type == "image")
            this.showImage();

        //console.log("oninit");
        //console.log(this.field.dataValues);

        if (this.field && this.field.type == "comboBox" && !this.field.dataValues && !this.field.fatherid)
            this._insuranceService.fetchDataValues(this.field);

        else if (this.field && this.field.type == "paymentType" && !this.field.dataValues)
            this._insuranceService.fetchPaymentTypes(this.field);

        else {
            this.replacePatterns();
        }

        this._insuranceService.resetChild(this.step, this.field);

    
        // this.options = [{ id: 1, title: 'mohammad' }, { id: 2, text: 'hamid' }];
    }


    @ViewChild('imageCanvas') imageCanvas: ElementRef;
    valueChange(event: any) {
        if (this.field.type == 'image' || this.field.type == 'inputImage') {
            let fi = event.target;
            if (fi.files && fi.files[0]) {
                this.processImage(fi.files[0]);
            }
        }
        else if (this.field.type == 'number' && isNaN(parseInt(event.data))) {
            this.field.value = this.field.value.replace(event.data, "");
            //console.log(event);
        }
        else if (this.field.type == 'comboBox' || this.field.type == 'year')
            this.field.value = event.value;
        //console.log("field " + this.field.name + " change to -> " + this.field.value);

        this._insuranceService.resetChild(this.step, this.field);
        this._insuranceService.refreshFields(this.insurance);
        this._insuranceService.ValidateAllRequired(this.step);

        if (this.step.number == 1)
            this._insuranceService.calcPrice(this.insurance);

        this.valueChanged.emit(this.field);
    }
    //-----------------------------------------------------------------------------
    processImage(file: any) {
        //is file an image
        var filename = file.name;
        var index = filename.lastIndexOf(".");
        var strsubstring = filename.substring(index, filename.length);
        strsubstring = strsubstring.toLowerCase();
        if (!(strsubstring == '.png' || strsubstring == '.jpeg' || strsubstring == '.jpg')) {
            alert("لطفا فقط تصویر انتخاب کنید.");
            this.field.value = false;
            return;
        }

        if (file < 200000) {
            this._insuranceService.addFile(this.insurance.name, this.field.name, file);
            this.field.value = true;
            return;
        }
        //compress image
        let cnv = document.createElement('canvas');
        var reader = new FileReader();

        let thisPointer = this;

        reader.onload = (event: any) => {
            let img = new Image();
            img.onload = function () {
                var MAX_WIDTH = 800;
                var MAX_HEIGHT = 600;
                var width = img.width;
                var height = img.height;

                if (width > height) {
                    if (width > MAX_WIDTH) {
                        height *= MAX_WIDTH / width;
                        width = MAX_WIDTH;
                    }
                } else {
                    if (height > MAX_HEIGHT) {
                        width *= MAX_HEIGHT / height;
                        height = MAX_HEIGHT;
                    }
                }

                cnv.width = width;
                cnv.height = height;
                var ctx = cnv.getContext("2d");

                if (ctx)
                    ctx.drawImage(img, 0, 0, width, height);

                var dataurl = cnv.toDataURL("image/jpeg",0.9);
                var blobBin = atob(dataurl.split(',')[1]);
                var array = [];
                for (var i = 0; i < blobBin.length; i++) {
                    array.push(blobBin.charCodeAt(i));
                }

                //get compressed file
                var createdFile = new Blob([new Uint8Array(array)], { type: 'image/jpeg' });
                //set compressed file into files array
                thisPointer._insuranceService.addFile(thisPointer.insurance.name, thisPointer.field.name, createdFile);
                //show image
                if (thisPointer.field.type == 'image') {
                    thisPointer.showImage();
                }

            }
            img.src = event.target.result;
        }

        reader.readAsDataURL(file);



        this.field.value = true;
    }
    //-----------------------------------------------------------------------------
    imageFile = "/common/image/uploadfile.jpg";
    showImage() {
        let file = this._insuranceService.getFile(this.insurance.name, this.field.name);
        if (!file)
            return;
        var reader = new FileReader();
        reader.onload = (event: any) => {
            this.imageFile = event.target.result;
        }

        reader.readAsDataURL(file);
    }
    //-----------------------------------------------------------------------------
    datePickerConfig = {
        format: 'YYYY/MM/DD'
    }
    //-----------------------------------------------------------------------------
    years: option[] = [];
    initYears() {
        let jalaliYear: any;
        let gregorianYear: any;
        jalaliYear = moment().locale('fa').format('YYYY')
        gregorianYear = moment().locale('en').format('YYYY')
        for (var i = 0; i <= 25; i++) {
            let year = new option();
            year.id = jalaliYear;
            year.text = jalaliYear + " - " + gregorianYear;
            this.years.push(year);
            jalaliYear--;
            gregorianYear--;
        }
    }
    //-----------------------------------------------------------------------------
    @ViewChild('fileInput') fileInput: ElementRef;
    public openFileDialog(): void {
        let event = new MouseEvent('click', { bubbles: false });
        this.fileInput.nativeElement.dispatchEvent(event);
    }
    //-----------------------------------------------------------------------------
    replacePatterns() {
        while (true) {
            let Patterns = /\[.*\]/i.exec(this.field.value);
            if (Patterns == null) {
                break;
            }
            let fakeValue = Patterns[0].replace(/^\[+|\]+$/g, '');
            let actualValue = "";
            if (fakeValue.startsWith("user.")) {
                fakeValue = fakeValue.replace("user.", "");
                
                    actualValue = this._authService.user[fakeValue];
            }
            else {
                actualValue = this._insuranceService.textByName(this.insurance.name, fakeValue);
            }
            if (actualValue == null)
                actualValue = "";
          
            this.field.value = this.field.value.replace(Patterns, actualValue)
        }

    }
}
class option {
    id: number;
    text: string;
}
