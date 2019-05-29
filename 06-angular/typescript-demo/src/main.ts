import CardService from "./card-service";

class Main {
    constructor() { }

    static Main() {
        document.addEventListener('DOMContentLoaded', () => {
            let newDeckButton = <HTMLButtonElement>document.getElementById('newDeckButton');
            let drawCardButton = <HTMLButtonElement>document.getElementById('drawCardButton');
            let cardContainer = <HTMLDivElement>document.getElementById('cardContainer');

            let cardService = new CardService();
            let deckId: string;

            newDeckButton.addEventListener('click', () => {
                cardService.createDeck()
                    .then(res => {
                        drawCardButton.disabled = false;
                        cardContainer.innerHTML = '';
                        deckId = res.deck_id;
                        console.log(deckId);
                    })
                    .catch(console.log);
            });

            drawCardButton.addEventListener('click', () => {
                cardService.drawCard(deckId)
                    .then(res => {
                        let image = <HTMLImageElement>document.createElement('img');
                        image.src = res.cards[0].image;
                        cardContainer.appendChild(image);
                    })
                    .catch(console.log);
            });
        });
    }
}

Main.Main();
