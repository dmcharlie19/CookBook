import { Component, OnInit } from '@angular/core';
import { ErrorService } from '../services/errorService';

@Component({
  selector: 'app-error-display',
  templateUrl: './error-display.component.html',
  styleUrls: ['./error-display.component.css']
})
export class ErrorDisplayComponent implements OnInit {

  error: string = "";
  constructor(private ei: ErrorService) { }

  ngOnInit(): void {
    console.log("ngOnInit");

    this.ei.onErrorOcured.subscribe(e => {
      console.log(e);
      this.error = e;
    });

    this.ei.onErrorClear.subscribe(x => this.error = "")
  }

  public setError(error: string) {
    console.log("setError");
    this.error = error;
  }

}
