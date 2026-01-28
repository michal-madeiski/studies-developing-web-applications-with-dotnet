import { Component, inject, signal, OnInit } from '@angular/core';
import { ShopService } from '../../services/shop.service';
import { ArticleModel } from '../../models/article.model';
import { ArticleForm } from '../article-form/article-form';
import { CategoryModel } from '../../models/category.model';
import { Router } from '@angular/router';
import { RouterModule } from '@angular/router';
import { ArticleList } from '../article-list/article-list';

@Component({
  selector: 'app-article-manager',
  imports: [ArticleForm, RouterModule, ArticleList],
  templateUrl: './article-manager.html',
  styleUrl: './article-manager.scss',
})
export class ArticleManager implements OnInit {
  public router = inject(Router);
  public shopService = inject(ShopService);
  
  public currentId?: number;
  public currentArticle = signal<ArticleModel | undefined>(undefined);
  
  isArticleModifyOpen = false;
  isArticleAddOpen = false;

  ngOnInit() {
    this.shopService.loadShop();
  }

  onSelect(id: number) {
    this.currentId = id;
    this.currentArticle.set(this.shopService.articles().find((article) => article.id == id));
    this.router.navigate([`/article/${id}/show`]);
  }

  onArticleModifyStart() {
    this.isArticleModifyOpen = true;
  }

  onArticleModifyCancel() {
    this.isArticleModifyOpen = false;
  }

  onArticleModifySave(modifyData: { name: string, categoryId: number, price: number }) {
    const currentArticleValue = this.currentArticle();

    if (!currentArticleValue || !this.currentId) return;

    const updatedArticle: ArticleModel = {
      ...currentArticleValue,
      name: modifyData.name,
      categoryId: modifyData.categoryId,
      price: modifyData.price
    };

    this.shopService.updateArticle(this.currentId, modifyData.name, modifyData.price, modifyData.categoryId).subscribe({
      next: () => {
        this.currentId = undefined;
        this.currentArticle.set(undefined);
        this.shopService.refreshAll();
        this.isArticleModifyOpen = false;
        this.router.navigate(['/article']);
      },
      error: (err) => {
        console.error('Error while updating:', err);
      }
    });
  }

  onArticleDelete() {
    if (this.currentId) {
      this.shopService.deleteArticle(this.currentId).subscribe({
        next: () => {
          this.shopService.refreshAll();
          this.currentId = undefined;
          this.currentArticle.set(undefined);
          this.isArticleModifyOpen = false;
          this.router.navigate(['/article']);
        },
        error: (err) => {
          console.error('Error while deleting:', err);
        }
      });
    }
  }

  onArticleAddStart() {
    this.isArticleAddOpen = true;
  }

  onArticleAddCancel() {
    this.isArticleAddOpen = false;
  }

  onArticleAddSave(addData: { name: string, categoryId: number, price: number }) {
    this.shopService.addArticle(addData.name, addData.price, addData.categoryId).subscribe({
      next: () => {
        this.shopService.refreshAll();
        this.isArticleAddOpen = false;
      },
      error: (err) => {
        console.error('Error while adding:', err);
      }
    });
  }
}