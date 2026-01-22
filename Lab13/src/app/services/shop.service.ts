import { Injectable } from '@angular/core';
import { ArticleModel } from '../models/article.model';
import { CategoryModel } from '../models/category.model';

@Injectable({
  providedIn: 'root',
})
export class ShopService {
    categories:CategoryModel[] = 
  [
    {id: 1, name: "Toys"},
    {id: 2, name: "Tools"}
  ]

  articles:ArticleModel[] =
  [
    {id: 1, name: "Hammer", price: 9.99, category: this.categories[1], img: "hammer.png"},
    {id: 2, name: "Dog", price: 19.99, category: this.categories[0], img: "dog.png"},
    {id: 3, name: "Car", price: 29.99, category: this.categories[0], img: "car.png"}
  ]

  constructor() {

  }
  
}