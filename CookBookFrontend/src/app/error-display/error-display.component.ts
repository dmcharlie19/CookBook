import { Component, OnInit } from '@angular/core';
import { ErrorService } from '../services/errorService';

@Component({
  selector: 'app-error-display',
  templateUrl: './error-display.component.html',
  styleUrls: ['./error-display.component.css']
})
export class ErrorDisplayComponent implements OnInit {

  error: string = "";
  constructor(private _errorService: ErrorService) { }

  ngOnInit(): void {
    this._errorService.onErrorOcured.subscribe(e => {
      console.log(`ErrorDisplayComponent: ${e}`);
      this.error = e;
    });

    this._errorService.onErrorClear.subscribe(() => this.error = "")
  }
}

