import { CategoryModel } from "./category.model";

export interface ArticleModel {
  id: number;
  name: string;
  price: number;
  category: CategoryModel;
  img: string;
}