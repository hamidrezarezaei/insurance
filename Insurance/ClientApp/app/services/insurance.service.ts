import { Injectable, Inject } from '@angular/core';
import { Http, Headers, RequestOptions } from '@angular/http';
import * as moment from 'jalali-moment';
import { forEach } from '@angular/router/src/utils/collection';


@Injectable()
export class insuranceService {
    public insurances: any;
    public currentInsurance: any;
    public isWaiting = false;
    baseUrl: string;

    constructor(private http: Http, @Inject('BASE_URL') baseUrl: string) {
        this.baseUrl = baseUrl;
        //console.log('insuranceserviceconstructor');
        this.fetchInsurances();
    }

    //-----------------------------------------------------------------------------
    files: fileClass[] = [];
    // اگر فایل بود آن را آپدیت کن در غیر اینصورت فایل را اضافه می کند
    addFile(insuranceName: string, fieldName: string, file: any) {
        if (this.isFileExist(insuranceName, fieldName)) {
            this.updateFile(insuranceName, fieldName, file);
        }
        else {
            this.pushFile(insuranceName, fieldName, file);
        }
        //console.log(this.files);

    }
    //فایل را به آرایه اضافه می کند
    pushFile(insuranceName: string, fieldName: string, file: any) {
        let f = new fileClass();
        f.insuranceName = insuranceName;
        f.fieldName = fieldName;
        f.file = file;
        this.files.push(f);
    }
    //فایل موجود در آرایه را آپدیت می کند
    updateFile(insuranceName: string, fieldName: string, file: any) {
        for (let i = 0; i < this.files.length; i++) {
            let f = this.files[i];
            if (f.insuranceName == insuranceName && f.fieldName == fieldName) {
                f.file = file;
            }
        }
    }
    getFile(insuranceName: string, fieldName: string) {
        for (let i = 0; i < this.files.length; i++) {
            let f = this.files[i];
            if (f.insuranceName == insuranceName && f.fieldName == fieldName) {
                return f.file;
            }
        }
        return null
    }
    isFileExist(insuranceName: string, fieldName: string) {
        for (let i = 0; i < this.files.length; i++) {
            let f = this.files[i];
            if (f.insuranceName == insuranceName && f.fieldName == fieldName) {
                return true;
            }
        }
        return false;
    }
    fileCount(insuranceName: string) {
        let c = 0;
        for (let i = 0; i < this.files.length; i++) {
            let f = this.files[i];
            if (f.insuranceName == insuranceName) {
                c++;
            }
        }
        return c;
    }
    //-----------------------------------------------------------------------------
    insuranceChange(insurance: any) {
        //اگر استپ اول از بیمه ای که میخواهیم نیست آن را لود می کنیم
        insurance.currentStep = 1;
        this.currentInsurance = insurance;
        //console.log('hrr1');
        //console.log(insurance.steps);
        if (insurance.steps.length < 1) {
            console.log('hrr2');
            this.fetchStep(insurance, 1);
        }
    }
    //-----------------------------------------------------------------------------
    fetchInsurances() {
        //console.log('fetchInsurances');
        this.isWaiting = true;
        this.http.get(this.baseUrl + 'api/Insurance/GetInsurances').subscribe(result => {
            this.insurances = result.json();
            for (var i = 0; i < this.insurances.length; i++) {
                if (this.insurances[i].isDefault) {
                    this.currentInsurance = this.insurances[i];
                }
            }
            //this.currentInsurance = this.insurances[0];
            this.fetchStep(this.currentInsurance, 1);
            this.isWaiting = false;
            //console.log(this.insurances)
        }, error => console.error(error));
    }
    //-----------------------------------------------------------------------------
    stepChange(insurance: any) {
        //اگر به آخرین مرحله بوده که باید بریم سراغ ثبت سفارش
        if (insurance.currentStep > insurance.stepCount) {
            this.addOrder(insurance);
            return;
        }
        //اگر استپی که میخواهیم نیست آن را لود می کنیم
        for (let i = 0; i < insurance.steps.length; i++) {
            let step = insurance.steps[i];
            if (step.number == insurance.currentStep)
                return;
        }
        this.fetchStep(insurance, insurance.currentStep);
    }
    //-----------------------------------------------------------------------------
    fetchStep(insurance: any, stepNumber: number) {
        //console.log('fetchStep');
        let params = "insuranceId=" + insurance.id + "&stepNumber=" + stepNumber;
        this.isWaiting = true;
        this.http.get(this.baseUrl + 'api/Insurance/GetStep?' + params).subscribe(result => {
            let step = result.json();
            //اگر استپ دارد باید استپ خوانده شده به آنها اضافه شود و در غیر اینصورت آرایه ایجاد شود
            if (!insurance.steps)
                insurance.steps = new Array(step);
            else
                insurance.steps.push(step);
            //console.log(step)
            this.isWaiting = false;
        }, error => console.error(error));
    }
    //-----------------------------------------------------------------------------
    //for show or hide
    refreshFields(insurance: any) {
        for (var i = 0; i < insurance.steps.length; i++) {
            let step = insurance.steps[i];
            for (var k = 0; k < step.fieldSets.length; k++) {
                let fieldSet = step.fieldSets[k];
                for (let j = 0; j < fieldSet.fields.length; j++) {
                    let field = fieldSet.fields[j];
                    if (field.showIf && field.showIf != "") {
                        field.isShowField = this.execJavascript(field.showIf);
                        console.log(field.isShowField);
                    }
                    if (field.type == "calc") {
                        let res = this.execJavascript(field.formula);
                        field.value = Math.ceil(res);
                    }
                    else if (field.type == "price") {
                        field.value = insurance.price;
                    }

                }
            }
        }
    }
    //-----------------------------------------------------------------------------
    updatePriceField(insurance: any) {
        for (var i = 0; i < insurance.steps.length; i++) {
            let step = insurance.steps[i];
            for (var k = 0; k < step.fieldSets.length; k++) {
                let fieldSet = step.fieldSets[k];
                for (let j = 0; j < fieldSet.fields.length; j++) {
                    let field = fieldSet.fields[j];
                    if (field.type == "price") {
                        field.value = insurance.price;
                    }
                }
            }
        }
    }
    //-----------------------------------------------------------------------------
    resetChild(step: any, field: any) {
        for (var k = 0; k < step.fieldSets.length; k++) {
            let fieldSet = step.fieldSets[k];
            for (let j = 0; j < fieldSet.fields.length; j++) {
                let tempField = fieldSet.fields[j];
                if (tempField.fatherid && tempField.fatherid == field.id) {
                    this.fetchChildDataValues(tempField, field.value);
                    tempField.value = null;
                }
            }
        }
    }
    //-----------------------------------------------------------------------------
    fetchDataValues(field: any) {
        field.isShowLoading = true;
        //console.log(field);
        let params = "dataTypeId=" + field.dataTypeid;
        this.http.get(this.baseUrl + 'api/Insurance/GetDataValues?' + params).subscribe(result => {
            let dataValues = result.json();
            field.dataValues = dataValues;
            field.isShowLoading = false;
            //console.log(dataType)
        }, error => console.error(error));
    }
    //-----------------------------------------------------------------------------
    fetchChildDataValues(field: any, fatherValue: number) {
        //console.log('fetchChildDataValues >fathervalue = ' + fatherValue);
        field.isShowLoading = true;
        field.dataValues = null;
        let params = "dataTypeId=" + field.dataTypeid + "&fatherId=" + fatherValue;
        this.http.get(this.baseUrl + 'api/Insurance/GetChildDataValues?' + params).subscribe(result => {
            let dataValues = result.json();
            if (dataValues.length == 1) {
                field.value = dataValues[0].id;
            }
            field.dataValues = dataValues;

            field.isShowLoading = false;
        }, error => console.error(error));
    }
    //-----------------------------------------------------------------------------
    fetchPaymentTypes(field: any) {
        field.isShowLoading = true;
        //console.log(field);
        //let params = "dataTypeId=" + field.dataTypeid;
        this.http.get(this.baseUrl + 'api/Order/GetPaymentTypes').subscribe(result => {
            let dataValues = result.json();
            field.dataValues = dataValues;
            field.isShowLoading = false;
            //console.log(dataType)
        }, error => console.error(error));

    }
    //-----------------------------------------------------------------------------
    ValidateAllRequired(step: any) {
        //console.log('ValidateAllRequired');

        for (var k = 0; k < step.fieldSets.length; k++) {
            let fieldSet = step.fieldSets[k];
            for (let j = 0; j < fieldSet.fields.length; j++) {
                var field = fieldSet.fields[j];
                if (field.type == "label" || field.type == "html")
                    continue;
                if (field.type == "acceptCheckBox" && !field.value) {
                    step.isValidAllRequired = false;
                    return false;
                }
                if (field.isRequire && !field.value) {
                    step.isValidAllRequired = false;
                    return false;
                }
            }
        }
        //console.log('ValidateAllRequired return true');

        step.isValidAllRequired = true;
        return true;
    }
    //-----------------------------------------------------------------------------
    calcPrice_server(insurance: any) {

        if (this.ValidateAllRequired(insurance.steps[0])) {
            //console.log("calcPrice");

            const headers = new Headers({ 'Content-type': 'application/json' });
            this.http.post(this.baseUrl + "api/insurance/CalcPrice/", insurance.steps[0], new RequestOptions({ headers: headers }))
                .subscribe(result => {
                    let res = +result.text();
                    if (res == -1)
                        console.log("خطا در محاسبه");
                    else {
                        insurance.price = Math.ceil(res);
                    }
                });
        }
        else {
            insurance.price = null;
        }
    }
    //-----------------------------------------------------------------------------
    execJavascript(code: string) {
        let f = new Function("field", code);
        let res = f(this);
        return res;
    }
    //-----------------------------------------------------------------------------
    calcPrice(insurance: any) {
        //console.log('calcPrice');
        if (insurance.steps[0].isValidAllRequired) {
            //پارامتر کل کلاس است برای اینکه موقع نوشتن فرمول قابل فهم تر باشد نام فیلد گذاشتیم
            // let f = new Function("field", insurance.formula);
            //let res = f(this);
            let res = this.execJavascript(insurance.formula);
            insurance.price = Math.ceil(res);
            this.updatePriceField(insurance);
        }
        else {
            insurance.price = null;
        }
    }
    //-----------------------------------------------------------------------------
    yearCount(insuranceName: string, fieldName: string) {
        let x = this.value(insuranceName, fieldName);
        let jalaliYear = moment().locale('fa').format('YYYY');
        return +jalaliYear - x;
    }
    //-----------------------------------------------------------------------------
    attribute(insuranceName: string, fieldName: string, attributeName: string) {
        //console.log('attribute');
        let attribute = this.findAttribute(insuranceName, fieldName, attributeName);
        if (attribute)
            return parseFloat(attribute.value);
        //return parseInt(attribute.value);
        return null;
    }
    //-----------------------------------------------------------------------------
    findAttribute(insuranceName: string, fieldName: string, attributeName: string) {
        let dataValue = this.findSelectedDataValue(insuranceName, fieldName);
        if (!dataValue)
            return null;
        //console.log(dataValue);

        for (var i = 0; i < dataValue.categories.length; i++) {
            let cat = dataValue.categories[i].category;
            for (var j = 0; j < cat.attributes.length; j++) {
                let attr = cat.attributes[j];
                if (attr.name == attributeName)
                    return attr;
            }
        }
        return null;
    }
    //-----------------------------------------------------------------------------
    findSelectedDataValue(insuranceName: string, fieldName: string) {
        let field = this.findField(insuranceName, fieldName);
        for (var i = 0; i < field.dataValues.length; i++) {
            let dv = field.dataValues[i];
            if (dv.id == field.value)
                return dv;
        }
        return null;
    }
    //-----------------------------------------------------------------------------
    delayDays(insuranceName: string, fieldName: string) {

        let field = this.findField(insuranceName, fieldName);
        if (!field)
            return null;

        let val = moment(field.value).toDate();
        let now = moment().toDate();

        var timeDiff = val.getTime() - now.getTime();

        var diffDays = Math.ceil(timeDiff / (1000 * 3600 * 24));
        if (diffDays > -1)
            return 0;
        return Math.abs(diffDays);
    }
    //-----------------------------------------------------------------------------
    textByName(insuranceName: string, fieldName: string) {
        let field = this.findField(insuranceName, fieldName);
        return this.textById(insuranceName, field.id)
    }
    //-----------------------------------------------------------------------------
    textById(insuranceName: string, fieldId: number) {

        let field = this.findFieldById(insuranceName, fieldId);
        switch (field.type) {
            case "textBox":
            case "textArea":
                return field.value;
            case "comboBox":
            case "radio":
            case "radioButton":
                return this.findSelectedDataValue(insuranceName, field.name).text;
            case "checkBox":
                return field.value ? "بله" : "خیر";
            case "year":
                return field.value;
            case "date":
                let MomentDate = moment(field.value);
                return MomentDate.locale('fa').format("YYYY/M/D")

        }
        return 'www';
    }
    //-----------------------------------------------------------------------------
    value(insuranceName: string, fieldName: string) {
        let field = this.findField(insuranceName, fieldName);
        if (field) {
            if (field.type == "date") {
                let MomentDate = moment(field.value);
                return MomentDate.locale('fa').format("YYYY/M/D");
            }
            if (field.type == "number") {
                return field.value.replace(/,/g, "");
            }

            return field.value;


        }
        return null;
    }
    //-----------------------------------------------------------------------------
    findField(insuranceName: string, fieldName: string) {
        let insurance = this.findInsurance(insuranceName);
        for (let i = 0; i < insurance.steps.length; i++) {
            let step = insurance.steps[i];
            for (var k = 0; k < step.fieldSets.length; k++) {
                let fieldSet = step.fieldSets[k];
                for (let j = 0; j < fieldSet.fields.length; j++) {
                    if (fieldSet.fields[j].name == fieldName) {
                        return fieldSet.fields[j];
                    }
                }
            }
        }
        return null;
    }
    //-----------------------------------------------------------------------------
    findFieldById(insuranceName: string, fieldId: number) {
        let insurance = this.findInsurance(insuranceName);
        for (let i = 0; i < insurance.steps.length; i++) {
            let step = insurance.steps[i];
            for (var k = 0; k < step.fieldSets.length; k++) {
                let fieldSet = step.fieldSets[k];
                for (let j = 0; j < fieldSet.fields.length; j++) {
                    if (fieldSet.fields[j].id == fieldId) {
                        return fieldSet.fields[j];
                    }
                }
            }
        }
        return null;
    }
    //-----------------------------------------------------------------------------
    findInsurance(insuranceName: string) {
        for (let i = 0; i < this.insurances.length; i++) {
            if (this.insurances[i].name == insuranceName) {
                return this.insurances[i];
            }
        }
        return null;
    }
    //-----------------------------------------------------------------------------
    minifyInsurance(insurance: any) {
        var resInsurance = {
            id: insurance.id,
            orderIndex: insurance.orderIndex,
            price: insurance.price,
            steps: Array()
        };

        for (let i = 0; i < insurance.steps.length; i++) {

            let step = insurance.steps[i];
            let resStep = {
                orderIndex: step.orderIndex,
                fieldSets: Array()
            };
            for (var k = 0; k < step.fieldSets.length; k++) {
                let fieldSet = step.fieldSets[k];
                let resFieldSet = {
                    orderIndex: fieldSet.orderIndex,
                    fields: Array()
                }
                for (let j = 0; j < fieldSet.fields.length; j++) {
                    let field = fieldSet.fields[j];
                    if (field.type == "label" || field.type == "html")
                        continue;
                    let resField = {
                        name: field.name,
                        title: field.title,
                        value: field.value,
                        type: field.type,
                        orderIndex: field.orderIndex
                    };
                    resFieldSet.fields.push(resField);
                    //field.dataValues = null;
                }
                resStep.fieldSets.push(resFieldSet);
            }
            resInsurance.steps.push(resStep);
        }
        return resInsurance;
    }
    //-----------------------------------------------------------------------------
    addOrder(insurance: any) {
        //console.log("addOrder start");
        const headers = new Headers({ 'Content-type': 'application/json' });
        this.isWaiting = true;
        let minInsurance = this.minifyInsurance(insurance);

        this.http.post(this.baseUrl + "api/Order/AddOrder/", minInsurance, new RequestOptions({ headers: headers }))
            .subscribe(result => {
                let orderId = +result.text();
                if (orderId == -1) {
                    console.log("خطا در ثبت سفارش");
                }
                else if (this.fileCount(insurance.name) > 0) {
                    this.uploadOrderFiles(orderId, insurance.name);
                }
                else {
                    window.location.href = '/profile/Payment/Index/' + orderId;
                }
            });
    }

    uploadOrderFiles(orderId: number, insuranceName: string) {
        let formData: FormData = new FormData();

        //console.log(insuranceName);
        this.isWaiting = true;

        for (let i = 0; i < this.files.length; i++) {
            if (this.files[i].insuranceName == insuranceName)
                formData.append(orderId + "@" + this.files[i].fieldName, this.files[i].file);
        }

        this.http.post(this.baseUrl + "api/Order/uploadOrderFiles/", formData)
            .subscribe(result => {
                let orderId = +result.text();
                if (orderId == -1) {
                    this.isWaiting = false;
                    console.log("خطا در آپلود فایل");
                }
                else {
                    console.log("سفارش با موفقیت ثبت شد کد پرداخت");
                    window.location.href = '/profile/Payment/Index/' + orderId;
                }
            });
    }
}

class fileClass {
    insuranceName: string;
    fieldName: string;
    file: any;
}