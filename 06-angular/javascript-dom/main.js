'use strict';

// this function is now registered
// to be run when the 'window' object gets the 'load'
// event.
window.onload = function () {
    let header = document.getElementById('header');

    console.log(header.innerHTML);

    header.innerHTML = 'Hello <em>manipulated</em> DOM';

    console.log('this runs second');
};

console.log('this runs first (body doesn\'t exist yet)');
