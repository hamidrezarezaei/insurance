import { Injectable, Inject } from '@angular/core';
import { Http, Headers, RequestOptions } from '@angular/http';

@Injectable()
export class authService {
    baseUrl: string;
    public user: any;
    public isWaiting = false;

    constructor(private http: Http, @Inject('BASE_URL') baseUrl: string) {
        this.baseUrl = baseUrl;
        this.fetchUser();
    }
    //-----------------------------------------------------------------------------
    fetchUser() {
        this.isWaiting = true;
        this.http.get(this.baseUrl + 'api/User/GetUser').subscribe(result => {
            this.user = result.json();
            console.log(this.user)
            this.isWaiting = false;
        }, error => console.error(error));
    }
    //-----------------------------------------------------------------------------
    login(actualUserName: string, passWord: string) {
        this.user.actualUserName = actualUserName;
        this.user.passWord = passWord;
        console.log(this.user);

        this.isWaiting = true;
        let formData: FormData = new FormData();
        const headers = new Headers({ 'Content-type': 'application/json' });

        this.http.post(this.baseUrl + "api/User/Login/", this.user, new RequestOptions({ headers: headers }))
            .subscribe(result => {
                this.user = result.json();
                console.log(this.user)
                this.isWaiting = false;
            });
    }
    //-----------------------------------------------------------------------------
    Register(firstName: string,
        lastName: string,
        phoneNumber: string,
        nationalCode: string,
        email: string,
        actualUserName: string,
        passWord: string,
        passWord2: string) {
        if (passWord != passWord2) {
            this.user.clientMessage = "تفاوت در نام کاربری و تکرار آن.";
            return;
        }
        this.user.firstName = firstName;
        this.user.lastName = lastName;
        this.user.phoneNumber = phoneNumber;
        this.user.nationalCode = nationalCode;
        this.user.email = email;
        this.user.actualUserName = actualUserName;
        this.user.passWord = passWord;

        console.log(this.user);
        let formData: FormData = new FormData();
        const headers = new Headers({ 'Content-type': 'application/json' });

        this.isWaiting = true;

        this.http.post(this.baseUrl + "api/User/Register/", this.user, new RequestOptions({ headers: headers }))
            .subscribe(result => {
                this.user = result.json();
                console.log(this.user)
                this.isWaiting = false;
            });
    }
}