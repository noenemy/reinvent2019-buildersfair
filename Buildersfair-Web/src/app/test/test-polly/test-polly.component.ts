import { Component, OnInit } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { TestService } from 'src/app/_services/test.service';
import { AlertifyService } from 'src/app/_services/alertify.service';
import { FormsModule } from '@angular/forms';

@Component({
  selector: 'app-test-polly',
  templateUrl: './test-polly.component.html',
  styleUrls: ['./test-polly.component.css']
})

export class TestPollyComponent implements OnInit {
  texttospeech: any;
  audioPolly = new Audio();

  constructor(private http: HttpClient,
              private testService: TestService,
              private alertify: AlertifyService) { }

  ngOnInit() {
  }

  public speech() {

    const body = {
      text: 'Learn languages with AWS AI/ML'
    };

    this.alertify.message('Now working on it...');

    this.testService.pollyTest(body).subscribe((result: any) => {

      console.log(result);

      this.alertify.success('Polly call success.');
      this.audioPolly.src = result.mediaUri;
      this.audioPolly.load();
      this.audioPolly.play();

    }, error => {
      this.alertify.error('Something wrong. Try again.');
    });
  }
}
