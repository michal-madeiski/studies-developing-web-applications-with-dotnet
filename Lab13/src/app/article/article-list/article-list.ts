import { Component, input, output, inject } from '@angular/core';
import { Article } from '../article/article';
import { ArticleModel } from '../../models/article.model';
import { ShopService } from '../../services/shop.service';

@Component({
  selector: 'app-article-list',
  imports: [Article],
  templateUrl: './article-list.html',
  styleUrl: './article-list.scss',
})
export class ArticleList {
  public shopService = inject(ShopService);
  articles = input.required<ArticleModel[]>(); 
  selectedId = input<number | undefined>(undefined);
  select = output<number>();
  add = output<void>();
}