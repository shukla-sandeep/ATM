import { Component, Inject } from '@angular/core';
import { Http } from '@angular/http';
import { DataService } from '../../service/data-service';
import 'rxjs/add/operator/catch';
@Component({
    selector: 'fetchdata',
    templateUrl: './fetchdata.component.html'
})
export class FetchDataComponent {
    public cardNumber: string = '';
    public pin: string = '';
    public amount: string = '';
    public state = 'cardDetails';
    public accountType = '';
    public balance = '';

    constructor(private dataService: DataService) { }
    //authenticate user with card details
    enterCardDetails() {
        this.dataService.Authenticate({ cardNumber: this.cardNumber, pin: this.pin }).subscribe(data => {
            console.log(data);
            if (data._body === 'true')
                this.state = 'selectAccount';
        }, () => { });
    }
    //set account type
    onSelectAccount(accountType: string) {
        this.accountType = accountType;
        this.state = 'transaction';
    }
    //balance enquiry
    onBalanceEnquiry() {
        this.dataService.getBalance({ cardNumber: this.cardNumber }).subscribe(data => {
            console.log(data);
            if (data._body) {
                this.balance = data._body;
                this.state = 'balance';
            }
        }, () => { });
    }
    //withdraw money from user account and display balance
    withdrawMoney() {
        this.dataService.withdraw({ cardNumber: this.cardNumber, amount: this.amount }).subscribe(data => {
            console.log(data);
            if (data._body)
                this.balance = data._body;
            else
                alert('Insufficient fund');
            this.state = 'balance';
        }, () => { });
    }
    //validate entered key value. if not numeric then reset
    onKey(evt: any) {
        if (('0123456789').indexOf(evt.key) === -1) {
            evt.srcElement.id == 'cardNumber' ? this.cardNumber = '' : this.amount = '';
        }
    }
}
