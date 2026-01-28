import { Component, computed, input, output, inject } from '@angular/core';
import { ArticleModel } from '../../models/article.model';
import { ShopService } from '../../services/shop.service';

@Component({
  selector: 'app-article',
  imports: [],
  templateUrl: './article.html',
  styleUrl: './article.scss',
})
export class Article {
  private shopService = inject(ShopService)
  article = input.required<ArticleModel>();
  selectedId = output<number>();
  isSelected = input.required<boolean>();

  uploadApiUrl = 'http://localhost:5095/upload/';
  imgPath = computed(() => {
    if (!this.article().imageUrl) {
        return '/assets/no_img.png'; 
    }

    return `${this.uploadApiUrl}${this.article().imageUrl}`;
  });

  categoryModel = computed(() => {
    const currentCategories = this.shopService.categories();
    
    if (currentCategories.length === 0) return null;
    
    return currentCategories.find(c => c.id === this.article().categoryId);
  })

  onSelectedArticle() {
    this.selectedId.emit(this.article().id);
  }

}