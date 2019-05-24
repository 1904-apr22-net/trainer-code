'use strict';

// javascript casts types pretty freely one into another
// for example, any value can be in an if-condition
// and it will be converted to boolean.

// there's truthy and falsy values in JS...
// all values are truthy except for the short list of falsy values.

// 0, -0
// NaN
// ""
// null
// undefined
// false

// if (object.property) { // if != null

// }

// we have two operators in javascript for equals.
// == (!=)
// === (!==)

// double equals is loose equality
// tries to compare value without comparing type
// (converts types in surprising ways)

console.log(1 == "1");
console.log([] == 0);
console.log(1 === "1");

// triple equals is what we should use,
// compares type and value.

// new in ES6, template literals
// aka string interpolation
function printComparison(a, b) {
    console.log(`${a} == ${b}:
    ${a == b}`);
}

printComparison(1, "1");
