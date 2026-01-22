import { Component, input, output, computed } from '@angular/core';
import { ArticleModel } from '../models/article.model';

@Component({
  selector: 'app-article-full',
  imports: [],
  templateUrl: './article-full.html',
  styleUrl: './article-full.scss',
})
export class ArticleFull {
  article=input.required<ArticleModel>();
  modifyPress = output();
  deletePress = output();

  imgPath = computed(() => 'assets/article_img/'+this.article().img);

  onModify() {
    this.modifyPress.emit();
  }

  onDelete() {
    this.deletePress.emit();
  }
}
