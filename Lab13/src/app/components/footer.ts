import { Component } from '@angular/core';

@Component({
  selector: 'app-footer',
  standalone: true,
  template: `
    <footer class="footer">
      <div class="content">
        <p>Lab13</p>
      </div>
    </footer>
  `,
  styles: `
    .footer {
      background: linear-gradient(135deg, #232526 0%, #414345 100%);
      color: #ecf0f1;
      padding: 2rem;
      text-align: center;
      margin-top: 3rem;
    }
    .content {
      display: flex;
      flex-direction: column;
      gap: 10px;
      align-items: center;
    }
    p { 
      margin: 0; 
      font-size: 1.2rem; 
      font-weight: 500;
      font-family: 'Segoe UI', sans-serif;
    }
  `
})
export class FooterComponent {}