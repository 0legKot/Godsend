import { Component, OnInit, ViewChild } from '@angular/core';
import { Router } from '@angular/router';

import { RepositoryService } from '../../services/repository.service';
import {
    Product, ProductInfo, Category, FilterInfoView, DecimalPropertyInfo,
    StringPropertyInfo, IntPropertyInfo, allowedOrderBy, orderBy
} from '../../models/product.model';
import { searchType } from '../search/search.service';
import { SearchInlineComponent } from '../search/search-inline.component';
import { CategoryService } from '../../services/category.service';
import { guidZero } from '../../models/cart.model';

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
    // searchProducts?: ProductInfo[];
    templateText = 'Waiting for data...';

    comparsionSet: string[] = new Array<string>();
    categories?: Category[];
    filter: FilterInfoView = new FilterInfoView();
    orderBy: allowedOrderBy[] = orderBy;

    productsExperiment?: ProductInfo[];

    @ViewChild(SearchInlineComponent)
    searchInline?: SearchInlineComponent;

    get pagesCount(): number {
        return Math.ceil(this.repo.productsCount / this.repo.productFilter.quantity);
    }

    onPageChanged(page: number) {
        this.repo.productFilter.page = page;
        this.getProducts();
    }

    getProducts() {
        this.repo.getByFilter(res => {
            console.log(res);
        });
    }

    get isFilteredByCategory(): boolean {
        return Boolean(this.repo.productFilter.categoryId);
    }

    get idsForCompare(): string {
        return this.comparsionSet.join(',');
    }

    toggleComparsion(id: string) {
        if (!this.isFilteredByCategory) { return; }
        if (this.comparsionSet.indexOf(id) === -1) {
            this.comparsionSet.push(id);
        } else {
            this.comparsionSet = this.comparsionSet.filter(x => x !== id);
        }
    }

   /*exp get products(): ProductInfo[] {
        // return this.searchProducts || this.repo.products;
        return this.repo.products;
    }*/

    // getImage(pi: ProductInfo): string {
    //    return this.images[pi.id];
    // }

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
        private catService: CategoryService,
        private router: Router) {
    }

    ngOnInit() {
        this.repo.productsExperiment.subscribe((newProducts: ProductInfo[]) => {
            console.log('products changed');
            this.productsExperiment = newProducts;
        });
        this.getProducts();
    }

    getCategories(): void {
        this.categories = this.catService.cats ? this.catService.cats.map(cws => cws.cat) : [];
        console.log(this.categories);
    }

    // getSubcategories(category: Category): void {
    //    this.categories = this.catService.getSubcategories(category);
    //    this.getCategoryProps(category);
    // }

    // getByCategory(category: Category): void {
    //    this.repo.productFilter.categoryId = category.id;
    //    this.getProducts();
    // }

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
        console.log(category);
        this.repo.productFilter.categoryId = category.id;
        this.getProducts();
        this.getCategoryProps(category);
    }
}
