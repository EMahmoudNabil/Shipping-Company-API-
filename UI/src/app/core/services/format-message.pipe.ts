import { Pipe, PipeTransform } from '@angular/core';
import { DomSanitizer, SafeHtml } from '@angular/platform-browser';

@Pipe({
  name: 'formatMessage'
})
export class FormatMessagePipe implements PipeTransform {
  constructor(private sanitizer: DomSanitizer) {}

  transform(value: string): SafeHtml {
    if (!value) return '';
    
    // تحويل الأسطر الجديدة إلى <br>
    let formatted = value.replace(/\n/g, '<br>');
    
    // تمييز أرقام التتبع
    formatted = formatted.replace(
      /(\b[A-Z0-9]{8,}\b)/g, 
      '<span class="tracking-number">$1</span>'
    );
    
    return this.sanitizer.bypassSecurityTrustHtml(formatted);
  }
}