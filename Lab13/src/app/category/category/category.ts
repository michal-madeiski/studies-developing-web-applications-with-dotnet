import { Component, input, output } from '@angular/core';
import { CategoryModel } from '../../models/category.model';

@Component({
  selector: 'app-category',
  imports: [],
  templateUrl: './category.html',
  styleUrl: './category.scss',
})
export class Category {
  category = input.required<CategoryModel>();
  selectedId = output<number>();
  isSelected = input.required<boolean>();

  onSelectedCategory() {
    this.selectedId.emit(this.category().id);
  }

}
