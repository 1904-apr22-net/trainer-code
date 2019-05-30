import { Injectable } from '@angular/core';
import { Card } from './models/card';
import { HttpClient } from '@angular/common/http';
import { DrawCardResponse } from './models/draw-card-response';

// we can also provide the service in modules or in components
// and that will scope the service to the lifetime of that thing.
@Injectable({
  providedIn: 'root' // makes this a singleton service.
})
export class DeckOfCardsService {
  private static newDeckUrl = 'https://deckofcardsapi.com/api/deck/new/';

  private static getDrawCardUrl(deckId: string = 'new', count: number = 1): string {
    return `https://deckofcardsapi.com/api/deck/${deckId}/draw/?count=${count}`;
  }

  // createDeck(): Promise<CreateDeckResponse> {
  //   return fetch(CardService.newDeckUrl)
  //     .then(res => res.json());
  // }

  // HttpClient itself uses something called Observable,
  // from a library named rxJs.
  // Observables can have many "results" arrive, and many subscribers

  // we can convert Observables into the simpler Promise using toPromise.
  // Promises only have either success/failure arrive once,
  // and have only one "subscriber".

  drawCard(): Promise<Card> {
    return this.http.get<DrawCardResponse>(DeckOfCardsService.getDrawCardUrl())
      .toPromise()
      .then(res => res.cards[0]);
    // httpclient's "get", etc generic methods
    // auto-deserialize JSON into whatever the generic type parameter is.
    // (in this case, DrawCardResponse.)
  }

  // syntactic sugar to auto-bind this ctor parameter
  // to a new property on the class.
  constructor(private http: HttpClient)
  { }
  // (like the code below would do)

  // httpClient: HttpClient;

  // constructor(httpClient: HttpClient) {
  //   this.httpClient = httpClient;
  // }
}
