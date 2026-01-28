import { Component, computed, inject, OnInit, signal} from '@angular/core';
import { ShopService } from '../../services/shop.service';
import { ActivatedRoute, Router } from '@angular/router';
import { ArticleManager } from '../article-manager/article-manager';
import { ArticleModel } from '../../models/article.model';

@Component({
  selector: 'app-article-full',
  imports: [],
  templateUrl: './article-full.html',
  styleUrl: './article-full.scss',
})
export class ArticleFull implements OnInit{
  public shopService=inject(ShopService);
  public router=inject(Router);
  private articleManager = inject(ArticleManager);
  route=inject(ActivatedRoute);
  article = this.articleManager.currentArticle

  uploadApiUrl = 'http://localhost:5095/upload/';
  imgPath = computed(() => {
    if (!this.article()?.imageUrl) {
        return '/assets/no_img.png'; 
    }

    return `${this.uploadApiUrl}${this.article()?.imageUrl}`;
  });

  categoryModel = computed(() => {
    const categories = this.shopService.categories();
    return categories.find(c => c.id === this.article()!.categoryId);
    //return this.shopService.getCategoryByIdCache(this.article()!.categoryId);
  })

  onModify() {
    this.articleManager.onArticleModifyStart();
  }

  onDelete() {
    this.articleManager.onArticleDelete();
  }

  ngOnInit(): void {
    this.route.paramMap.subscribe((params) => {
      let id = Number(params.get('id'));
      this.shopService.getArticleById(id).subscribe((article) => {
        this.article.set(article)
        if(article==undefined)
          this.router.navigate([`/article`]);
      });
    });
  }
}
