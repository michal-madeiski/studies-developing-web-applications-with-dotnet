import { Injectable, inject, signal } from '@angular/core';
import { ArticleModel } from '../models/article.model';
import { CategoryModel } from '../models/category.model';
import { HttpClient, HttpParams } from '@angular/common/http';

interface ShopStorageState {
  articles: ArticleModel[];
  lastLoadedId: number;
  hasMore: boolean;
}
@Injectable({
  providedIn: 'root',
})
export class ShopService {
  private apiArticleUrl = "http://localhost:5095/api/articleapi";
  private apiCategoryUrl = "http://localhost:5095/api/categoryapi";
  private readonly storageKey = 'shop_articles_storage';
  private readonly articleListSize = 3;

  private http = inject(HttpClient);

  articles = signal<ArticleModel[]>([]);
  public hasMoreArticles = signal<boolean>(true);
  private lastLoadedArticleId = 0;
  categories = signal<CategoryModel[]>([]);


  getArticleById(id: number) {
    return this.http.get<ArticleModel>(`${this.apiArticleUrl}/${id}`);
  }

  addArticle(name: string, price: number, categoryId: number) {
    const payload = {
      name: name,
      price: price,
      categoryId: categoryId
    };
    return this.http.post(this.apiArticleUrl, payload);
  }

  updateArticle(id: number, name: string, price: number, categoryId: number) {
    const payload = {
      name: name,
      price: price,
      categoryId: categoryId
    };
    return this.http.put(`${this.apiArticleUrl}/${id}`, payload);
  }

  deleteArticle(id: number) {
    return this.http.delete<void>(`${this.apiArticleUrl}/${id}`);
  }

  loadShop() {
    this.http.get<CategoryModel[]>(this.apiCategoryUrl).subscribe((data) => {
      this.categories.set(data);
      this.init();
    });
  }

  loadCategories() {
    this.http.get<CategoryModel[]>(this.apiCategoryUrl).subscribe((data) => {
      this.categories.set(data);
    });
  }

  getCategoryById(id: number) {
    return this.http.get<CategoryModel>(`${this.apiCategoryUrl}/${id}`);
  }

  getCategoryByIdCache(id: number) {
    return this.categories().find(c => c.id === id);
  }

  addCategory(category: CategoryModel) {
    return this.http.post<CategoryModel>(this.apiCategoryUrl, category);
  }

  updateCategory(category: CategoryModel) {
    return this.http.put<CategoryModel>(`${this.apiCategoryUrl}/${category.id}`, category);
  }

  deleteCategory(id: number) {
    return this.http.delete<void>(`${this.apiCategoryUrl}/${id}`);
  }

  constructor() {
    this.restoreState();
  }

  init() {
    if (this.articles().length === 0) {
      this.loadMoreArticles();
    }
  }

  loadMoreArticles() {
    if (!this.hasMoreArticles()) return;

    const params = new HttpParams()
      .set('lastId', this.lastLoadedArticleId)
      .set('size', this.articleListSize);

    this.http.get<any[]>(`${this.apiArticleUrl}/GetNext`, { params })
      .subscribe({
        next: (response) => {
          if (!response || response.length === 0) {
            this.hasMoreArticles.set(false);
            this.saveState();
            return;
          }

          const newArticles: ArticleModel[] = response.map(a => ({
            id: a.id,
            name: a.name,
            price: a.price,
            imageUrl: a.imageUrl,
            categoryId: a.categoryId
          }));

          this.articles.update(current => [...current, ...newArticles]);
          this.lastLoadedArticleId = newArticles[newArticles.length - 1].id;
          this.saveState();
        },
        error: (err) => console.error('Error while loading articles:', err)
      });
  }

  private saveState() {
    const state: ShopStorageState = {
      articles: this.articles(),
      lastLoadedId: this.lastLoadedArticleId,
      hasMore: this.hasMoreArticles()
    };
    localStorage.setItem(this.storageKey, JSON.stringify(state));
  }

  private restoreState() {
    const json = localStorage.getItem(this.storageKey);
    if (json) {
      try {
        const state = JSON.parse(json) as ShopStorageState;
        this.articles.set(state.articles);
        this.lastLoadedArticleId = state.lastLoadedId;
        this.hasMoreArticles.set(state.hasMore);
      } catch (e) {
        console.warn('Error while loading from storage, cleaning...', e);
        localStorage.removeItem(this.storageKey);
      }
    }
  }

  refreshAll() {
    this.articles.set([]);
    this.lastLoadedArticleId = 0;
    this.hasMoreArticles.set(true);
    localStorage.removeItem(this.storageKey);
    this.loadMoreArticles();
  }
}