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

// event handler/listener same thing.

// domobject.onload = function
// domobject.addEventListener(function);

// often we want code to run when the DOM is ready
// for that, better than load event on window,
// we can use DOMContentLoaded event on the document.

document.addEventListener('DOMContentLoaded', () => {
    let button1 = document.getElementById('button1');

    button1.addEventListener('click', () => {
        let newPara = document.createElement('p');
        newPara.innerHTML = 'new paragraph';
        document.body.appendChild(newPara);
    });

    let link = document.querySelector('a');
    link.addEventListener('click', event => {
        // disable browser's default
        // behavior on this event on this element
        event.preventDefault();

        // this is how to navigate to a new page
        window.location.href = 'https://microsoft.com';

        button1.innerHTML = 'clicked';
    });

    let table = document.querySelector('table');

    let firstTd = table.tBodies[0].rows[0].cells[0];

    firstTd.addEventListener('dblclick', printEventDetails);

    table.addEventListener('dblclick', printEventDetails);

    window.addEventListener('dblclick', printEventDetails, true);
});

function printEventDetails(event) {
    // prevents the event from continuing
    // to bubble/capture to another element.
    event.stopPropagation();

    // even stops other event handlers
    // on this same element.
    event.stopImmediatePropagation();


    console.log(`event.type: ${event.type}`);
    console.log(`event.target: ${event.target}`);
    console.log(`event.currentTarget: ${event.currentTarget}`);
    console.log(`this: ${this}`);
    console.log('----------------');
}
