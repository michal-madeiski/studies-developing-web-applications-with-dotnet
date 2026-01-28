import { Component, OnInit, input, output, signal, inject } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { CategoryModel } from '../../models/category.model';
import { ShopService } from '../../services/shop.service';

@Component({
  selector: 'app-category-form',
  imports: [FormsModule],
  templateUrl: './category-form.html',
  styleUrl: './category-form.scss',
})
export class CategoryForm implements OnInit {
  public shopService = inject(ShopService);
  category=input.required<CategoryModel>();
  isAdd=input.required<boolean>();
  cancel=output<void>();
  save=output<{name:string}>();
  enteredName=signal("");

  ngOnInit(): void {
    this.enteredName.set(this.category().name);
  }

  onClickInitials() {
    this.enteredName.set(this.category().name);
  }

  onCancel() {
    this.cancel.emit();
  }

  onSubmit() {
    this.save.emit({
      name:this.enteredName(),
    })
  }
}
