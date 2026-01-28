import { Component, inject, signal, OnInit } from '@angular/core';
import { ShopService } from '../../services/shop.service';
import { CategoryForm } from '../category-form/category-form';
import { CategoryModel } from '../../models/category.model';
import { Router } from '@angular/router';
import { RouterModule } from '@angular/router';
import { CategoryList } from '../category-list/category-list';

@Component({
  selector: 'app-category-manager',
  imports: [CategoryForm, RouterModule, CategoryList],
  templateUrl: './category-manager.html',
  styleUrl: './category-manager.scss',
})
export class CategoryManager implements OnInit {
  public router = inject(Router)
  public shopService = inject(ShopService);
  public currentId?: number;
  public currentCategory = signal<CategoryModel | undefined>(undefined);
  isCategoryModifyOpen = false;
  isCategoryAddOpen = false;

  ngOnInit() {
    this.shopService.loadCategories();
  }

  onSelect(id: number) {
    this.currentId = id;
    this.currentCategory.set(this.shopService.categories().find((category) => category.id == id));
    this.router.navigate([`/category/${id}/show`])
  }

  onCategoryModifyStart() {
    this.isCategoryModifyOpen = true;
  }

  onCategoryModifyCancel() {
    this.isCategoryModifyOpen = false;
  }

  onCategoryModifySave(modifyData: { name: string }) {
    if (this.currentId) {
      const updatedCategory: CategoryModel = {
        id: this.currentId,
        name: modifyData.name,
      };

      this.shopService.updateCategory(updatedCategory).subscribe({
        next: () => {
          this.shopService.categories.update(cats => cats.map(c => c.id === updatedCategory.id ? { ...updatedCategory } : c));
          this.shopService.loadCategories();
          this.shopService.refreshAll();
          this.currentCategory.set(updatedCategory);
          this.isCategoryModifyOpen = false;
        },
        error: (err) => {
          console.error('Error while updating:', err);
        }
      });
    }
  }

  onCategoryDelete() {
    if (this.currentId) {
      this.shopService.deleteCategory(this.currentId).subscribe({
        next: () => {
          this.shopService.loadCategories();
          this.shopService.refreshAll();
          this.currentId = undefined;
          this.currentCategory.set(undefined);
          this.isCategoryModifyOpen = false;
          this.router.navigate(['/category']);
        },
        error: (err) => {
          console.error('Error while deleting:', err);
        }
      });
    }
  }

  onCategoryAddStart() {
    this.isCategoryAddOpen = true;
  }

  onCategoryAddCancel() {
    this.isCategoryAddOpen = false;
  }

  onCategoryAddSave(addData: { name: string }) {
    const newCategory: CategoryModel = {
      id: 0,
      name: addData.name,
    };

    this.shopService.addCategory(newCategory).subscribe({
      next: () => {
        this.shopService.loadCategories();
        this.shopService.refreshAll();
        this.isCategoryAddOpen = false;
      },
      error: (err) => {
        console.error('Erorr while adding:', err);
      }
    });
  }
}