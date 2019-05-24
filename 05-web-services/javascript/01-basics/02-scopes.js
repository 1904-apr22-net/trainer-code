'use strict';

// ES5 added strict mode
// abc = "abc";
// prevents using undeclared variables

// (without strict mode, undeclared variables
//  became global scope, even from within a function)

// in JavaScript, before ES6, we did not have
// block scope. variables were either global,
// or function-scope.

var x = 4;

function printStuff() {
    var x; // function scope
    // y = 4; // global scope (without strict mode)
    console.log(abc); // not an error, because
    // var declarations are "hoisted" to the top of
    // the function.

    if (3 == 3) {
        var abc = "abc";
        let def = "abc";
    } else {
        var abc = "def";
        let def = "def";
    }
    abc = 5; // function scope
    def = 6; // block scope (reference error)
}
// printStuff();

// ES6 added two new ways to declare a variable -
// let and const.

// var is for function scope (or global),
//    don't use it in new code

// let and const do block scope (or global).

// let will replace var for us
// const prevents changing the value thereafter.

// errors in javascript
try {
    throw 6;
    printStuff();
} catch (error) {
    console.log(error);
}
