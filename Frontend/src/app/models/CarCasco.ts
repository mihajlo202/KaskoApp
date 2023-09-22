export class CarCasco {
    id:number;
    carMake: string;
    carModel: string;
    priceFirst: number;
    priceSecond: number;
    priceThird: number;

    constructor(id: number = 0, carMake: string = "", carModel: string = "", priceFirst: number = 0, 
    priceSecond: number = 0, priceThird: number = 0) {
        this.id = id;
        this.carMake = carMake;
        this.carModel = carModel;
        this.priceFirst = priceFirst;
        this.priceSecond = priceSecond;
        this.priceThird = priceThird;
    }
}