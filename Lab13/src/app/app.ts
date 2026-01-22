import { Component, signal } from '@angular/core';
import { RouterOutlet } from '@angular/router';
import { HeaderComponent } from './components/header';
import { ArticleList } from './article-list/article-list';
import { FooterComponent } from './components/footer';

@Component({
  selector: 'app-root',
  imports: [HeaderComponent, ArticleList, FooterComponent],
  templateUrl: './app.html',
  styleUrl: './app.scss'
})
export class App {
  protected readonly title = signal('Lab13');
}
