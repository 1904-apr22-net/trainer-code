import CreateDeckResponse from "./create-deck-response";
import { DrawCardResponse } from "./draw-card-response";

export default class CardService {
    private static newDeckUrl = 'https://deckofcardsapi.com/api/deck/new/';

    private static getDrawCardUrl(deckId: string, count: number): string {
        return `https://deckofcardsapi.com/api/deck/${deckId}/draw/?count=${count}`;
    }

    createDeck(): Promise<CreateDeckResponse> {
        return fetch(CardService.newDeckUrl)
            .then(res => res.json());
    }

    drawCard(deckId: string, count: number = 1): Promise<DrawCardResponse> {
        return fetch(CardService.getDrawCardUrl(deckId, count))
            .then(res => res.json());
    }
};
