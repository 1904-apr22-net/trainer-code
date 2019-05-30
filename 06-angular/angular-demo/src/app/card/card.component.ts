import { Component, OnInit } from '@angular/core';
import { Card } from '../models/card';
import { DeckOfCardsService } from '../deck-of-cards.service';

@Component({
  selector: 'app-card',
  templateUrl: './card.component.html',
  styleUrls: ['./card.component.css']
})
export class CardComponent implements OnInit {

  card: Card;

  constructor(private cardSvc: DeckOfCardsService) { }

  // this is a "lifecycle hook" for the component
  // we do initialization stuff for the component in here.
  ngOnInit() {
    this.drawNewCard();
  }

  drawNewCard() {
    this.cardSvc.drawCard().then(card => this.card = card);
  }

}
