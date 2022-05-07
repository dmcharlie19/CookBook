import { Component, OnInit } from '@angular/core';
import { ErrorService } from '../services/errorService';

@Component({
  selector: 'app-error-display',
  templateUrl: './error-display.component.html',
  styleUrls: ['./error-display.component.css']
})
export class ErrorDisplayComponent implements OnInit {

  error: String = "";
  constructor(private ei: ErrorService) { }

  ngOnInit(): void {
    console.log("ngOnInit");

    this.ei.onErrorOcured.subscribe(e => {
      console.log(e);
      this.error = e;
    });
  }

  public setError(error: String) {
    console.log("setError");
    this.error = error;
  }

}
