import { Component, OnInit, ViewChild } from '@angular/core';
import { Router } from '@angular/router';

import { RepositoryService } from '../../services/repository.service';
import {
    Product, ProductInfo, Category, FilterInfoView, DecimalPropertyInfo,
    StringPropertyInfo, IntPropertyInfo, allowedOrderBy, orderBy
} from '../../models/product.model';
import { searchType } from '../search/search.service';
import { SearchInlineComponent } from '../search/search-inline.component';
import { ImageService } from '../../services/image.service';
import { CategoryService } from '../../services/category.service';
import { guidZero } from '../../models/cart.model';
import { retry } from 'rxjs/operators';

@Component({
    selector: 'godsend-products',
    templateUrl: './products.component.html',
    styleUrls: ['./products.component.css']
})
export class ProductsComponent implements OnInit {
    // private selectedId: string;
    // page: number = 1;
    // rpp: number = 10;
    type = searchType.product;
    images: { [id: string]: string; } = {};
    // searchProducts?: ProductInfo[];
    templateText = 'Waiting for data...';

    comparsionSet: string[] = new Array < string>();
    categories?: Category[];
    filter: FilterInfoView = new FilterInfoView();
    orderBy: allowedOrderBy[] = orderBy;

    @ViewChild(SearchInlineComponent)
    searchInline?: SearchInlineComponent;

    imagg: any = {};

    get pagesCount(): number {
        return Math.ceil(this.repo.productsCount / this.repo.productFilter.quantity);
    }

    onPageChanged(page: number) {
        this.repo.productFilter.page = page;
        this.getProducts();
    }

    getProducts() {
        this.repo.getByFilter(res => {
            this.imageService.getPreviewImages(res.map(pi => pi.id), (smth: any) => this.imagg = smth);
        });
    }

    get isFilteredByCategory(): boolean {
        return Boolean(this.repo.productFilter.categoryId);
    }

    toggleComparsion(id: string) {
        if (!this.isFilteredByCategory) return;
        if (this.comparsionSet.indexOf(id) == -1)
            this.comparsionSet.push(id);
        else this.comparsionSet = this.comparsionSet.filter(x => x != id);
    }

    get products(): ProductInfo[] {
        // return this.searchProducts || this.repo.products;
        return this.repo.products;
    }

    getImage(pi: ProductInfo): string {
        return this.images[pi.id];
    }

    createProduct(descr: string, name: string) {
        // TODO create interface with only relevant info
        const prod = new Product('', new ProductInfo('', descr, 0, name), guidZero);
        this.repo.createOrEditEntity('product', prod, 0, 0, pi => this.router.navigateByUrl('products/' + pi.id));
    }

    deleteProduct(id: string) {
        this.repo.deleteEntity('product', id, 0, 0);
    }

    // onFound(products: ProductInfo[]) {
    //    this.templateText = 'Not found';
    //    this.searchProducts = products;
    // }

    constructor(private repo: RepositoryService,
        private imageService: ImageService,
        private catService: CategoryService,
        private router: Router) {
    }

    ngOnInit() {
        this.getProducts();
    }

    getCategories(): void {
        this.categories = this.catService.cats ? this.catService.cats.map(cws => cws.cat) : [];
        console.log(this.categories);
    }

    getSubcategories(category: Category): void {
        this.categories = this.catService.getSubcategories(category);
        this.getCategoryProps(category);
    }

    getByCategory(category: Category): void {
        this.repo.productFilter.categoryId = category.id;
        this.getProducts();
    }

    getByFilter(): void {
        if (this.filter) {
            if (this.filter.stringProps) {
                this.repo.productFilter.stringProps = this.filter.stringProps
                    .filter(prop => prop.part !== '' && prop.part != null)
                    .map(prop => new StringPropertyInfo(prop.propId, prop.part));
            }
            if (this.filter.intProps) {
                this.repo.productFilter.intProps = this.filter.intProps
                    .filter(prop => prop.left != null && prop.right != null)
                    .map(prop => new IntPropertyInfo(prop.propId, prop.left, prop.right));
            }
            if (this.filter.decimalProps) {
                this.repo.productFilter.decimalProps = this.filter.decimalProps
                    .filter(prop => prop.left != null && prop.right != null)
                    .map(prop => new DecimalPropertyInfo(prop.propId, prop.left, prop.right));
            }

            this.repo.productFilter.orderBy = this.filter.orderBy;

            this.repo.productFilter.sortAscending = this.filter.sortAscending;

            this.getProducts();
        }
    }

    getCategoryProps(category: Category): void {
        this.catService.getCategoryProps(category.id, filter => { this.filter = filter; console.log(filter); });
    }

    setCurrentCategory(category: Category): void {
        this.repo.productFilter.categoryId = category.id;
        this.getProducts();
        this.getCategoryProps(category);
    }
}
