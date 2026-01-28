import { Component, OnInit, input, output, signal, computed, inject, effect, untracked } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { ArticleModel } from '../../models/article.model';
import { CategoryModel } from '../../models/category.model';
import { ShopService } from '../../services/shop.service';

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
  save=output<{name:string, categoryId:number, price:number}>();
  enteredName=signal("");
  enteredCategory=signal<number>(this.shopService.categories()[0].id);
  enteredPrice=signal(0);

  ngOnInit(): void {
    this.enteredName.set(this.article().name);
    this.enteredCategory.set(this.article().categoryId);
    this.enteredPrice.set(this.article().price);
  }

  onClickInitials() {
    this.enteredName.set(this.article().name);
    this.enteredPrice.set(this.article().price);
    this.enteredCategory.set(this.article().categoryId);
  }

  onCancel() {
    this.cancel.emit();
  }

  onSubmit() {
    this.save.emit({
      name:this.enteredName(),
      categoryId:this.enteredCategory(),
      price:this.enteredPrice()
    })
  }

  uploadApiUrl = 'http://localhost:5095/upload/';
  imgPath = computed(() => {    
    if (!this.article().imageUrl || this.article().imageUrl === '') {
        return '/assets/no_img.png'; 
    }

    return `${this.uploadApiUrl}${this.article().imageUrl}`;
  });
}
