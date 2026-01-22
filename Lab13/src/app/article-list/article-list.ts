import { Component, inject } from '@angular/core';
import { ShopService } from '../services/shop.service';
import { Article } from '../article/article';
import { ArticleFull } from '../article-full/article-full';
import { ArticleModel } from '../models/article.model';
import { ArticleForm } from '../article-form/article-form';
import { CategoryModel } from '../models/category.model';

@Component({
  selector: 'app-article-list',
  imports: [Article, ArticleFull, ArticleForm],
  templateUrl: './article-list.html',
  styleUrl: './article-list.scss',
})
export class ArticleList {
  public shopService = inject(ShopService);
  public currentId?:number;
  public currentArticle?:ArticleModel
  isArticleModifyOpen=false;
  isArticleAddOpen=false;

  onSelect(id: number) {
    this.currentId=id;
    this.currentArticle=this.shopService.articles.find((article)=>article.id==id);
  }

  onArticleModifyStart() {
    this.isArticleModifyOpen=true;
  }

  onArticleModifyCancel() {
    this.isArticleModifyOpen=false;
  }

  onArticleModifySave(modifyDate: {name:string, category:CategoryModel, price:number}) {
    this.isArticleModifyOpen=false;
    this.shopService.articles=this.shopService.articles.map((article)=>article.id==this.currentId?
      {
        ...article,
        name:modifyDate.name,
        category:modifyDate.category,
        price:modifyDate.price
      }:article);
    this.currentArticle=this.shopService.articles.find((article)=>article.id==this.currentId);
  }

  onArticleDelete() {
    if (this.currentId) {
      this.shopService.articles = this.shopService.articles.filter(
        (article) => article.id !== this.currentId
      );

      this.currentId = undefined;
      this.currentArticle = undefined;
      
      this.isArticleModifyOpen = false;
    }
  }

  onArticleAddStart() {
    this.isArticleAddOpen=true;
  }

  onArticleAddCancel() {
    this.isArticleAddOpen=false;
  }

  onArticleAddSave(addData: {name:string, category:CategoryModel, price:number}) {
    const newId = Math.max(...this.shopService.articles.map(a => a.id)) + 1;

    const newArticle: ArticleModel = {
      id:newId,
      name:addData.name,
      category:addData.category,
      price:addData.price,
      img:'no_img.png'
    };

    this.shopService.articles = [...this.shopService.articles, newArticle];
    this.isArticleAddOpen = false;
  }
}