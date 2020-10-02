class Car {
    constructor(brand) {
      this.carname = brand;
    }
  }
  
  mycar = new Car("Ford");
  
  document.getElementById("demo").innerHTML = mycar.carname;