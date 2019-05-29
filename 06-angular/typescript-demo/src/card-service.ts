import CreateDeckResponse from "./create-deck-response";

export default class CardService {
    private static newDeckUrl = 'https://deckofcardsapi.com/api/deck/new/';

    createDeck(): Promise<CreateDeckResponse> {
        return fetch(CardService.newDeckUrl)
            .then(res => res.json());
    }
}
