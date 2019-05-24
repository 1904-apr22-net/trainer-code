'use strict';

// object literal syntax
let obj = {
    name: "Bill",
    age: 20
};

obj.sayName = function () {
    console.log(this.name);
}

console.log(obj);

console.log(obj.age); // preferred
console.log(obj["age"]);

obj.id = 4;

obj.sayName();
// the way "this" works (except with arrow functions)
// is, in a given function, it refers to
// the thing to the left of the dot,
// THIS TIME the function was called.

let func = obj.sayName;
// func(); // error - "this" is undefined.

// objects can inherit from objects in C#.
// "prototypal inheritance"

let obj2 = {
    id: 2,
    name: "Nick"
};

obj2.__proto__ = obj;

console.log(obj2.age);
obj.age = 10;
console.log(obj2.age);
console.log(obj2);
console.log(obj2.id);

obj2.age = 30;

console.log(obj.age);

function Person(name, age) {
    this.name = name;
    this.age = age;

    this.sayName = function () {
        console.log(this.name);
    }
}

// we call constructors like this
let bill = new Person("Bill", 25);
console.log(bill);

function Student(name, age, school) {
    // kinda like deriving from a parent class
    this.__proto__ = new Person(name, age);

    this.school = school;

    this.goToSchool = function () { };
}

let timmy = new Student("Timmy", 12, "UTA");

console.log(timmy);
timmy.sayName();
timmy.name = "Tim";
timmy.sayName();

// classes in ES6

class Person2 {
    constructor(name, age) {
        this.name = name;
        this.age = age;
    }

    // method syntax in ES6...
    // works on objects literals too.
    sayName() {
        console.log(this.name);
    }
}

class Student2 extends Person2 {
    constructor(name, age, school) {
        super(name, age); // calls parent class ctor
        //  (like base in c#)
        this.school = school;
    }

    goToSchool() { }
}

let billy = new Student2("Billy", 11, "UTA");
console.log(billy);

// classes are really just syntactic sugar in JS.

// arrow functions and "this"
bill.sayName();

function changeBill() {
    // change bill to say his name
    // with an arrow function instead of a regular one

    bill.sayName = () => {
        // in an arrow function, instead "this" being
        // bound WHEN CALLED,
        // it's bound whent the function is written.
        console.log(this.name);
    }
}
let obj3 = {
    name: "Nick",
    changeBill: changeBill
};
obj3.changeBill();
bill.sayName();


// in practice: we dont' use arrow functions
// for functions that should be "attached" to an object.
// we do use them when we pass functions as parameters to customize function behavior

// new in ES5:
// - strict mode
//     it turned a bunch of silent errors
//     into thrown errors
//     turns some sloppy syntax into errors
//     prevents assigning to undeclared variables.

/*
new in ES6:
    let & const (block scope)
    arrow functions
    method syntax
    default parameters
    string interpolation (`${}`)
    classes with class inheritance
    symbol type
    a lot of built-in functions on built-in objects
        like array, string
    Set and Map built-in objects
    Promises (kind of like Task in C#)
    for-of loop (like foreach in C#)
    es6 modules
    getters/setters (like C# properties)
    internationalization features
    spread, destructuring operators
*/
