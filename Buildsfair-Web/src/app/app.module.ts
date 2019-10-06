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
import { StageLogService } from './_services/stagelog.service';
import { LeaderboardService } from './_services/leaderboard.service';
import { HttpClientModule } from '@angular/common/http';
import { StageService } from './_services/stage.service';
import { StageObjectService } from './_services/stageobject.service';
import { AlertifyService } from './_services/alertify.service';
import { GameResultService } from './_services/gameresult.service';
import { WebcamModule } from 'ngx-webcam';

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
      AppRoutingModule,
      WebcamModule
   ],
   providers: [
      GameService,
      GameResultService,
      StageLogService,
      LeaderboardService,
      StageService,
      StageObjectService,
      AlertifyService
   ],
   bootstrap: [
      AppComponent
   ]
})
export class AppModule { }
