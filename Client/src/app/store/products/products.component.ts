import { Component, OnInit, ViewChild } from '@angular/core';
import { ActivatedRoute, ParamMap } from '@angular/router';

import { RepositoryService } from '../../services/repository.service';
import { Product, ProductInfo } from '../../models/product.model';
import { searchType } from '../search/search.service';
import { SearchInlineComponent } from '../search/search-inline.component';
import { ImageService } from '../../services/image.service';
import { forEach } from '@angular/router/src/utils/collection';

@Component({
    selector: 'godsend-products',
    templateUrl: './products.component.html',
    styleUrls: ['./products.component.css']
})
export class ProductsComponent implements OnInit {
    // private selectedId: string;
    type = searchType.product;
    images: { [id: string]: string; } = {};
    searchProducts?: ProductInfo[];
    templateText = 'Waiting for data...';

    @ViewChild(SearchInlineComponent)
    searchInline!: SearchInlineComponent;

    imagg: any = {};

    get products(): ProductInfo[] | {} {
        return this.searchProducts || this.repo.products;
    }

    getImage(pi: ProductInfo): string {
        return this.images[pi.id];
    }

    createProduct(descr: string, name: string) {
        // TODO create interface with only relevant info
        const prod = new Product('', new ProductInfo('', descr, name, 0, 0));
        this.repo.createOrEditEntity('product', prod, () => this.searchInline.doSearch());
    }

    deleteProduct(id: string) {
        this.repo.deleteEntity('product', id, () => this.searchInline.doSearch());
    }

    onFound(products: ProductInfo[]) {
        this.templateText = 'Not found';
        this.searchProducts = products;
    }

    constructor(private repo: RepositoryService, private imageService: ImageService) {
    }

    ngOnInit() {
        this.repo.getEntities<ProductInfo>('product', res => {
            this.imageService.getPreviewImages(res.map(pi => pi.id), (smth: any) => { this.imagg = smth });
            /*for (let p of res) {
                this.imageService.getImage(p.id, image => { this.images[p.id] = image; });
            }*/
        });
       
    }

}
