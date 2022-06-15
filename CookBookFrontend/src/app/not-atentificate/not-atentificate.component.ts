import { Component, OnInit } from '@angular/core';
import { MatDialogRef } from '@angular/material/dialog';;

@Component({
  selector: 'app-not-atentificate',
  templateUrl: './not-atentificate.component.html',
  styleUrls: ['./not-atentificate.component.css']
})
export class NotAtentificateComponent implements OnInit {


  constructor(public dialogRef: MatDialogRef<NotAtentificateComponent>) {
    console.log(dialogRef.disableClose);

  }

  ngOnInit(): void {
  }

  onButtonClick(): void {
    this.dialogRef.close();
  }
}
