// opt in to ES5 fixes
'use strict';

console.log('Hello JS');

// types

var x;

// number
x = 1;
// in JS, number holds whole numbers
// and floats, 64-bit IEEE float
// (like double in C#)
x = 2.5;
x = Infinity;
x = (2 / 0);
x = NaN;
x = "abc" / 4;
x = -0;
x = 7 + NaN;

// boolean
x = true;
x = false;
x = (3 < 4) && (4 <= 4);
x = NaN == NaN;
x = isNaN(NaN);

// string
x = 'a"bc';
x = "a'bc";
x = "\"ab\nc\"";

// null
x = null;
// for historical reasons, typeof null says object.

// undefined
x = undefined;
// var y;
// console.log(y); // undefined

// object
x = {};
x = console;
x = console.log;
// typeof says function, but really it is object type
// functions are objects

// symbol (added in ES6)
x = Symbol("name");
// it's used to make guaranteed-unique keys
// for accessing properties on objects

console.log(x);
console.log(typeof(x));

// functions
// function statement
function printSum(a, b) {
    console.log(a + b);
    // return a + b;
}

console.log(printSum); // a function
printSum(1, 1); // a number
console.log(printSum(1, 1)); // undefined
// the return value of functions that don't
// return (void in C#) is undefined

// function expression
var printProduct = function (a, b) {
    console.log(a * b);
};

printProduct(3, 4);

// added in ES6, "arrow function"
// with expression body
var printDiff = (a, b) => console.log(a - b);
// with block body
var printQuotient = (a, b) => {
    console.log(a / b);
};
// there is a subtle difference between arrow functions
// and other functions, with the value of "this"

// if, switch, while, for

// arrays (objects)
x = Array();
x = [1, 2, 3]; // also an array
x = [true, {}, "abc"];
x = x[0]; // true

// we have for-of loop (like foreach in C#)
// new in ES6
for (var i of [1, 2, 3]) {
    console.log(i);
}

// we've long had foreach as a function on arrays.
[1, 2, 3].forEach(i => {
    debugger; // a breakpoint
    console.log(i);
});

// added in ES6 also... Set and Map objects
// (like C# HashSet and Dictionary)
