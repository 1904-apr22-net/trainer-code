'use strict';

document.addEventListener('DOMContentLoaded', () => {
    let button = document.getElementById('jokeButton');
    let fetchButton = document.getElementById('jokeFetch');
    let header = document.getElementById('jokeText');

    button.addEventListener('click', () => {
        let xhr = new XMLHttpRequest();

        xhr.addEventListener('readystatechange', () => {
            // when readyState is 4, the response is ready
            if (xhr.readyState === 4) {
                if (xhr.status >= 200 && xhr.status < 300) {
                    console.log(xhr.responseText);
                    // deserialize JSON
                    let jokeObj = JSON.parse(xhr.responseText);
                    console.log(jokeObj);

                    let joke = jokeObj.value.joke;

                    // like innerHTML, but without parsing the HTML.
                    header.textContent = joke;
                }
            }
        });

        xhr.open('GET', 'http://api.icndb.com/jokes/random?escape=javascript');

        xhr.send();
    });

    fetchButton.addEventListener('click', () => {
        // defaults to 'get' with no body
        fetch('http://api.icndb.com/j/random?escape=javascript')
            .then(resource => resource.json(), console.log)
            .then(obj => {
                header.textContent = obj.value.joke;
            })
            .catch(console.log);
    });
});
