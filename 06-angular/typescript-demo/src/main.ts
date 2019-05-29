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
                        debugger;
                        deckId = res.deck_id;
                        console.log(deckId);
                    })
                    .catch(console.log);
            });
        });
    }
}

Main.Main();
