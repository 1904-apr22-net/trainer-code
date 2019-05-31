import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RouterModule, Routes } from '@angular/router';
import { AboutComponent } from './about/about.component';
import { CardComponent } from './card/card.component';
import { PretendGuard } from './pretend.guard';

const routes: Routes = [
  { path: 'about', component: AboutComponent, canActivate: [PretendGuard] },
  { path: '', component: CardComponent }
];

@NgModule({
  declarations: [],
  imports: [
    CommonModule,
    RouterModule.forRoot(routes)
  ]
})
export class AppRoutingModule { }
