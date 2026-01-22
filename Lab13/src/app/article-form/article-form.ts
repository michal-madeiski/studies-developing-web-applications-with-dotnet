import { Component, OnInit, input, output, signal, computed, inject } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { ArticleModel } from '../models/article.model';
import { CategoryModel } from '../models/category.model';
import { ShopService } from '../services/shop.service';

@Component({
  selector: 'app-article-form',
  imports: [FormsModule],
  templateUrl: './article-form.html',
  styleUrl: './article-form.scss',
})
export class ArticleForm implements OnInit {
  public shopService = inject(ShopService);
  article=input.required<ArticleModel>();
  isAdd=input.required<boolean>();
  cancel=output<void>();
  save=output<{name:string,category:CategoryModel, price:number}>();
  enteredName=signal("");
  enteredCategory=signal<CategoryModel>(this.shopService.categories[0]);
  enteredPrice=signal(0);

  ngOnInit(): void {
    this.enteredName.set(this.article().name);
    this.enteredCategory.set(this.article().category);
    this.enteredPrice.set(this.article().price);
  }

  onClickInitials() {
    this.enteredName.set(this.article().name);
    this.enteredPrice.set(this.article().price);
    this.enteredCategory.set(this.article().category);
  }

  onCancel() {
    this.cancel.emit();
  }

  onSubmit() {
    this.save.emit({
      name:this.enteredName(),
      category:this.enteredCategory(),
      price:this.enteredPrice()
    })
  }

  imgPath = computed(() => 'assets/article_img/'+this.article().img);
}
