import { Component, Inject } from '@angular/core';
import { MAT_DIALOG_DATA, MatDialogModule, MatDialogRef } from '@angular/material/dialog';

@Component({
  selector: 'app-delete-confirm-dialog',
  imports: [MatDialogModule],
  template: `
    <h2 mat-dialog-title class="text-danger">تأكيد الحذف</h2>
    <mat-dialog-content>
      <p>{{ data.message || 'هل أنت متأكد من رغبتك في حذف هذا العنصر؟' }}</p>
      <p class="text-muted" *ngIf="data.itemId">رقم العنصر: {{ data.itemId }}</p>
    </mat-dialog-content>
    <mat-dialog-actions align="end">
      <button mat-button mat-dialog-close class="ml-2">إلغاء</button>
      <button mat-raised-button color="warn" [mat-dialog-close]="true">حذف</button>
    </mat-dialog-actions>
  `,
  styles: [`
    .text-danger { color: #dc3545; }
    .mat-dialog-actions { justify-content: flex-end; padding: 16px 24px; }
    .text-muted { color: #6c757d; font-size: 0.9em; }
    mat-dialog-content { padding: 16px 24px; }
  `]
})
export class DeleteConfirmDialogComponent {
  constructor(
    public dialogRef: MatDialogRef<DeleteConfirmDialogComponent>,
    @Inject(MAT_DIALOG_DATA) public data: any
  ) {}
}