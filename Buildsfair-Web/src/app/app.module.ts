import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { NavComponent } from './nav/nav.component';
import { HomeComponent } from './home/home.component';
import { GameComponent } from './game/game.component';
import { LeaderboardComponent } from './leaderboard/leaderboard.component';
import { DebugComponent } from './debug/debug.component';
import { TestComponent } from './test/test.component';

@NgModule({
   declarations: [
      AppComponent,
      NavComponent,
      HomeComponent,
      GameComponent,
      LeaderboardComponent,
      DebugComponent,
      TestComponent
   ],
   imports: [
      BrowserModule,
      AppRoutingModule
   ],
   providers: [],
   bootstrap: [
      AppComponent
   ]
})
export class AppModule { }
