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
import { GameService } from './_services/game.service';
import { StagelogService } from './_services/stagelog.service';
import { LeaderboardService } from './_services/leaderboard.service';
import { HttpClientModule } from '@angular/common/http';

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
      HttpClientModule,
      AppRoutingModule
   ],
   providers: [
      GameService,
      StagelogService,
      LeaderboardService,
   ],
   bootstrap: [
      AppComponent
   ]
})
export class AppModule { }
