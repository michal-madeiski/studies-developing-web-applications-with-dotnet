import { Component, inject, OnInit } from '@angular/core';
import { ShopService } from '../../services/shop.service';
import { ActivatedRoute, Router } from '@angular/router';
import { CategoryManager } from '../category-manager/category-manager';

@Component({
  selector: 'app-category-full',
  imports: [],
  templateUrl: './category-full.html',
  styleUrl: './category-full.scss',
})
export class CategoryFull implements OnInit{
  public shopService=inject(ShopService);
  public router=inject(Router);
  private categoryManager = inject(CategoryManager);
  route=inject(ActivatedRoute);
  category = this.categoryManager.currentCategory

  onModify() {
    this.categoryManager.onCategoryModifyStart();
  }

  onDelete() {
    this.categoryManager.onCategoryDelete();
  }

  ngOnInit(): void {
    this.route.paramMap.subscribe((params) => {
      let id = Number(params.get('id'));
      this.shopService.getCategoryById(id).subscribe((category) => {
        this.category.set(category)
        if(category==undefined)
          this.router.navigate([`/category`]);
      });
    });
  }
}
