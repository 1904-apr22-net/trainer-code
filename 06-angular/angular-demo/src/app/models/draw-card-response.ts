import { Card } from './card';

export interface DrawCardResponse {
    success: boolean;
    cards: Card[];
    deck_id: string;
    remaining: number;
};
