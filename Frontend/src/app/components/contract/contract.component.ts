import {
  HttpClient,
  HttpEventType,
  HttpErrorResponse,
} from '@angular/common/http';
import { environmentVariables } from 'src/app/constants/url-constants';
import { Component } from '@angular/core';
import { Contract } from 'src/app/models/Contract';
import { CarCasco } from 'src/app/models/CarCasco';

@Component({
  selector: 'app-contract',
  templateUrl: './contract.component.html',
  styleUrls: ['./contract.component.css']
})
export class ContractComponent {
  private contractURL = environmentVariables.CONTRACT_URL;
  contracts: Contract[] = [];
  newContract: Contract = new Contract();
  carCasko: CarCasco[] = [];
  carMake: string[] = [];
  carModel: string[] = [];

  constructor(private http: HttpClient) {}

  ngOnInit() {
    this.getCarCasco();
    this.getContracts();
  }

  createContract() {
    let url = this.contractURL + 'CreateContract';
    this.newContract.createdOn = new Date();
    this.http
      .post<boolean>(url, this.newContract)
      .subscribe({
        next: (event) => {
          this.getContracts();
          this.newContract = new Contract();
        },
        error: (err: HttpErrorResponse) => console.log(err),
      });
  };

  getCarCasco() {
    this.carCasko = [];
    
    let url = this.contractURL + 'GetCarCascos';
    return this.http.get<any>(url).subscribe({
      next: (event) => {
        this.carCasko = event;
          this.carMake = this.carCasko
          .map((item) => item.carMake)
          .filter((thing, i, arr) => arr.findIndex(t => t === thing) === i);
          if(this.carMake.length > 0)
          {
            this.newContract.carMake = this.carMake[0];
            this.onCarMakeChange(this.newContract.carMake);
          }
      },
      error: (err: HttpErrorResponse) => console.log(err),
    });
  }

  getContracts() {
    this.contracts = [];
    let url = this.contractURL + 'GetContracts';
    return this.http.get<any>(url).subscribe({
      next: (event) => {
        var pom: Contract[] = event;
        pom.map(item => {
          if(item.tief)
            item.tiefText = "x";
          if(item.weather)
            item.weatherText = "x"
          if(item.crash)
            item.crashText = "x";
        })
        this.contracts = pom;
      },
      error: (err: HttpErrorResponse) => console.log(err),
    });
  }

  onCarMakeChange(value: string){
    console.log("CarMakeChanged")
    console.log(value)
    this.carModel = [];
    this.carModel = this.carCasko
    .filter((item) => item.carMake == value)
    .map((item) => item.carModel);
    if(this.carModel.length > 0)
      this.newContract.carModel = this.carModel[0];


  }

  activateContract(contract: Contract) {
    let url = this.contractURL + 'ActivateContract';
    this.http
      .post<boolean>(url, contract)
      .subscribe({
        next: (event) => {
          this.getContracts();
        },
        error: (err: HttpErrorResponse) => console.log(err),
      });
  }
}

