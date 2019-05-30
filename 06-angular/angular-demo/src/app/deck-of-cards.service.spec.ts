import { TestBed } from '@angular/core/testing';

import { DeckOfCardsService } from './deck-of-cards.service';
import { HttpClient } from '@angular/common/http';

describe('DeckOfCardsService', () => {
  let httpSpy = jasmine.createSpyObj('HttpClient', ['get']);


  beforeEach(() => TestBed.configureTestingModule({
    providers: [
      { provide: HttpClient, useValue: httpSpy }
    ]
  }));

  it('should be created', () => {
    const service: DeckOfCardsService = TestBed.get(DeckOfCardsService);
    expect(service).toBeTruthy();
  });
});
