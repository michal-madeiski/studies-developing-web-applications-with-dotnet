import { Component } from '@angular/core';

@Component({
  selector: 'app-header',
  standalone: true,
  template: `
    <header class="header">
      <div class="logo">
        <h1>Shop</h1>
      </div>
    </header>
  `,
  styles: `
    .header {
      background: linear-gradient(135deg, #6a11cb 0%, #2575fc 100%);
      color: white;
      padding: 1.5rem 2rem;
      box-shadow: 0 4px 12px rgba(0, 0, 0, 0.15);
      display: flex;
      justify-content: center;
      align-items: center;
      margin-bottom: 25px;
    }
    .logo {
      display: flex;
      align-items: center;
      gap: 15px;
    }
    h1 {
      margin: 0;
      font-family: 'Segoe UI', Roboto, sans-serif;
      font-weight: 700;
      letter-spacing: 1px;
      text-transform: uppercase;
    }
  `
})
export class HeaderComponent {}