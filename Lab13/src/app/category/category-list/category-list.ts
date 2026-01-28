import { Component, input, output } from '@angular/core';
import { Category } from '../category/category';
import { CategoryModel } from '../../models/category.model';

@Component({
  selector: 'app-category-list',
  imports: [Category],
  templateUrl: './category-list.html',
  styleUrl: './category-list.scss',
})
export class CategoryList {
  categories = input.required<CategoryModel[]>(); 
  selectedId = input<number | undefined>(undefined);
  select = output<number>();
  add = output<void>();
}