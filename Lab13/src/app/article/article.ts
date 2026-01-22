import { Component, inject, signal, computed, input, output } from '@angular/core';
import { ShopService } from '../services/shop.service';
import { ArticleModel } from '../models/article.model';

@Component({
  selector: 'app-article',
  imports: [],
  templateUrl: './article.html',
  styleUrl: './article.scss',
})
export class Article {
  article = input.required<ArticleModel>();
  selectedId = output<number>();
  isSelected = input.required<boolean>();

  imgPath = computed(() => 'assets/article_img/'+this.article().img);

  onSelectedArticle() {
    this.selectedId.emit(this.article().id);
  }

}