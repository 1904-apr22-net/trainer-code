import { Component, OnInit } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Dog } from '../models/dog';

@Component({
  selector: 'app-about',
  templateUrl: './about.component.html',
  styleUrls: ['./about.component.css']
})
export class AboutComponent implements OnInit {
  data: string = 'initial';
  theClass: string = 'grey';
  dogs: Dog[];
  errorMessage: string;

  constructor(private http: HttpClient) { }

  ngOnInit() {
    this.http.get<Dog[]>('http://localhost:51607/api/dogs')
      .toPromise()
      .then(dogs => this.dogs = dogs)
      .catch(err => this.errorMessage = JSON.stringify(err));
  }

}
