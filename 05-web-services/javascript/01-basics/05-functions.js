'use strict';

// callback functions

// example of operation without callback
// library function
function addSlowly(a, b) {
    // we usually do callbacks with things
    // that take some time... hence "slowly"
    let result = a + b;
    return result;
}
// using it without callback
let result = addSlowly(3, 4);
console.log(result);


// same thing with callback pattern
// library function with callback
function multiplySlowly(a, b, callback) {
    let result = a * b;
    callback(result);
}
// use it with callback
multiplySlowly(3, 4, result => {
    console.log(result);
});

// closure
function newCounter() {
    let c = 0;
    return () => c++;
}

// function newCounter () {
//     let count = 0;
//     return function () {
//         return count++;
//     }
// }

console.log('--------');

// debugger;

let c = 5;

let counter1 = newCounter();
console.log(counter1()); // 0
console.log(counter1()); // 1
console.log(counter1()); // 2
console.log('--------');
let counter2 = newCounter();
console.log(counter2()); // 0
console.log(counter2()); // 1
console.log(counter1()); // 3

// functions "close over" any variables
// that they reference, keeping them alive
// when, being out of scope, they would
// normally be cleaned up.

// this behavior is called closure, and
// JS functions have it.

// sometimes we just call the functions themselves
// "closures"

// IIFE
// immediately invoked function expression
(function () {
    let x;

    console.log("run immediately");
})();

// it's disorganized to pollute the global
// scope with your variables.

// IIFE provides a non-global scope
// for some behavior/variables that we
// want to run right away.

(function () {
    let x;

    console.log("run immediately");
})();

let myLibrary = (function () {
    // like "private" stuff
    let privateData = 0;

    function privateFunction() {
        privateData++;
        return privateData;
    }

    return {
        // like "public" stuff
        libraryFunction(x) {
            return x + privateFunction();
        }
    };
})();

console.log(myLibrary.libraryFunction(1));
console.log(myLibrary.libraryFunction(2));
