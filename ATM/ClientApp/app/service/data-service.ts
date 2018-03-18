import { Injectable, Inject } from '@angular/core';
import { Http, RequestOptions } from '@angular/http';
import { Observable } from 'rxjs/Observable';
import 'rxjs/add/operator/map';
import 'rxjs/add/operator/catch';
@Injectable()
export class DataService {
    baseUrl: string;
    constructor(private http: Http, @Inject('BASE_URL') baseUrl: string) {
        this.baseUrl = baseUrl + 'api/SampleData/';
    }
    //api call to authenticate user
    public Authenticate(param: any): Observable<any> {
        return this.http
            .get(this.baseUrl + 'Authenticate', { headers: param })
            .map(result => {
                return result;
            })
            .catch((error: any) => this.handleError(error));
    }
    //api call the fetch account balance
    public getBalance(param: any): Observable<any> {
        return this.http.get(this.baseUrl + 'Balance', { headers: param })
            .map(result => {
                return result;
            })
            .catch((error: any) => this.handleError(error));
    }
    //api call the withdraw amount
    public withdraw(param: any): Observable<any> {
        return this.http.put(this.baseUrl + 'Withdraw', param)
            .map(result => {
                return result;
            })
            .catch((error: any) => this.handleError(error));
    }
    //display error if api fails due to exception
    private handleError(error: Response): Observable<any> {
        alert('Something went wrong, please contact administrator');
        return Observable.throw(error);
    }
}