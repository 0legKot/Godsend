import { Component, OnInit, ViewChild } from '@angular/core';
import { ActivatedRoute, ParamMap } from '@angular/router';

import { RepositoryService } from '../../services/repository.service';
import { Product, ProductInfo, Category, CatsWithSubs, FilterInfo, FilterInfoView, DecimalPropertyInfo, StringPropertyInfo, IntPropertyInfo, allowedOrderBy, orderBy } from '../../models/product.model';
import { searchType } from '../search/search.service';
import { SearchInlineComponent } from '../search/search-inline.component';
import { ImageService } from '../../services/image.service';
import { forEach } from '@angular/router/src/utils/collection';
import { CategoryService } from '../../services/category.service';
import { PagesComponent } from '../pages/pages.component';

@Component({
    selector: 'godsend-products',
    templateUrl: './products.component.html',
    styleUrls: ['./products.component.css']
})
export class ProductsComponent implements OnInit {
    // private selectedId: string;
    page: number = 1;
    rpp: number = 10;
    type = searchType.product;
    images: { [id: string]: string; } = {};
    searchProducts?: ProductInfo[];
    templateText = 'Waiting for data...';

    @ViewChild(SearchInlineComponent)
    searchInline!: SearchInlineComponent;

    imagg: any = {};

    prevPage() {
        this.page--;
        this.repo.getEntities<ProductInfo>('product', this.page, this.rpp, res => {
            this.imageService.getPreviewImages(res.map(pi => pi.id), (smth: any) => this.imagg = smth);
        });
    }
    nextPage() {
        this.page++;
        this.repo.getEntities<ProductInfo>('product', this.page, this.rpp, res => {
        this.imageService.getPreviewImages(res.map(pi => pi.id), (smth: any) => this.imagg = smth);
        });
    }

    get products(): ProductInfo[] | {} {
        return this.searchProducts || this.repo.products;
    }

    getImage(pi: ProductInfo): string {
        return this.images[pi.id];
    }

    createProduct(descr: string, name: string) {
        // TODO create interface with only relevant info
        const prod = new Product('', new ProductInfo('', descr, name, 0, 0));
        this.repo.createOrEditEntity('product', prod, this.page, this.rpp, () => this.searchInline.doSearch());
    }

    deleteProduct(id: string) {
        this.repo.deleteEntity('product', id, this.page, this.rpp, () => this.searchInline.doSearch());
    }

    onFound(products: ProductInfo[]) {
        this.templateText = 'Not found';
        this.searchProducts = products;
    }

    constructor(private repo: RepositoryService, private imageService: ImageService, private cattt: CategoryService) {
    }

    ngOnInit() {
        this.repo.getEntities<ProductInfo>('product', this.page, this.rpp, res => {
            this.imageService.getPreviewImages(res.map(pi => pi.id), (smth: any) => this.imagg = smth);
        });
    }

    categories?: Category[];
    filter?: FilterInfoView;

    getCategories(): void {
        this.categories = this.cattt.cats ? this.cattt.cats.map(cws => cws.cat) : [];
        console.log(this.categories)
    }

    getSubcategories(category: Category): void {
        this.categories = this.cattt.getSubcategories(category);
        this.getCategoryProps(category);
    }

    getByCategory(category: Category): void {
        this.repo.getByCategory(category);
    }

    getByFilter(): void {
        if (this.filter) {
            const trimmedFilter = new FilterInfo();

            if (this.filter.stringProps) {
                trimmedFilter.stringProps = this.filter.stringProps
                    .filter(prop => prop.part !== '' && prop.part != null)
                    .map(prop => new StringPropertyInfo(prop.propId, prop.part));
            }
            if (this.filter.intProps) {
                trimmedFilter.intProps = this.filter.intProps
                    .filter(prop => prop.left != null && prop.right != null)
                    .map(prop => new IntPropertyInfo(prop.propId, prop.left, prop.right));
            }
            if (this.filter.decimalProps) {
                trimmedFilter.decimalProps = this.filter.decimalProps
                    .filter(prop => prop.left != null && prop.right != null)
                    .map(prop => new DecimalPropertyInfo(prop.propId, prop.left, prop.right));
            }

            trimmedFilter.orderBy = this.filter.orderBy;

            console.log('filter');
            console.dir(trimmedFilter);

            this.repo.getByFilter(trimmedFilter);
        }       
    }

    getCategoryProps(category: Category): void {
        this.cattt.getCategoryProps(category, filter => { this.filter = filter; console.log(filter); })
    }

    setCurrentCategory(category: Category): void {
        this.getCategoryProps(category);
    }

    orderBy: allowedOrderBy[] = orderBy;
}
