import { Component, Inject, OnInit } from '@angular/core';
import { MatDialogRef, MAT_DIALOG_DATA } from '@angular/material/dialog';

@Component({
  selector: 'app-delete-recipe-dialog',
  templateUrl: './delete-recipe-dialog.component.html',
  styleUrls: ['./delete-recipe-dialog.component.css']
})
export class DeleteRecipeDialogComponent implements OnInit {

  constructor(@Inject(MAT_DIALOG_DATA) public recipeTitle: string,
    public dialogRef: MatDialogRef<DeleteRecipeDialogComponent>) { }

  ngOnInit(): void {
  }

  onCancel() {
    this.dialogRef.close({ event: 'cancel' });
  }

  onDelete(): void {
    this.dialogRef.close({ event: 'delete' });
  }
}

