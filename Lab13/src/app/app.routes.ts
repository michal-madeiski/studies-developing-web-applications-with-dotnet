import { Routes } from '@angular/router';
import { ArticleManager } from './article/article-manager/article-manager';
import { CategoryManager } from './category/category-manager/category-manager';
import { ArticleFull } from './article/article-full/article-full';
import { ArticleFullPlaceholder } from './article/article-full-placeholder/article-full-placeholder';
import { CategoryFull } from './category/category-full/category-full';
import { CategoryFullPlaceholder } from './category/category-full-placeholder/category-full-placeholder';

export const routes: Routes = [
    {path: 'article', component: ArticleManager, children: [
        {path: '', component: ArticleFullPlaceholder, pathMatch: 'full'},
        {path: ':id/show', component: ArticleFull},
    ]},
    {path: 'category', component: CategoryManager, children: [
        {path: '', component: CategoryFullPlaceholder, pathMatch: 'full'},
        {path: ':id/show', component: CategoryFull},
    ]},
    {path: '', redirectTo: 'article', pathMatch: 'full'},
    {path: '**', redirectTo: 'article'},
];
