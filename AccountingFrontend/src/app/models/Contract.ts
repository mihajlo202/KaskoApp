export class Contract {
    id:number;
    fullName: string;
    registration: string;
    address: string;
    carMake: string = "";
    carModel: string = "";
    year: number = new Date().getFullYear();
    tief: boolean = false;
    tiefText: string = "";
    weather: boolean = false;
    weatherText: string = "";
    crash: boolean = false;
    crashText: string = "";
    mounth: number = 1;
    createdOn: Date = new Date();
    activated: boolean = false;

    constructor(id: number = 0, fullName: string = "", registration: string = "", address: string = "") {
        this.id = id;
        this.fullName = fullName;
        this.registration = registration;
        this.address = address;
    }
}