import { Component, OnInit } from "@angular/core";
import { ActivatedRoute, Router } from "@angular/router";
import { RepositoryService, entityClass } from "../../services/repository.service";
import { ImageService } from "../../services/image.service";
import { StorageService } from "../../services/storage.service";
import { CategoryService } from "../../services/category.service";
import { Product } from "../../models/product.model";

@Component({
    selector: 'godsend-product-comparison',
    templateUrl: 'products-comparison.component.html'

})
export class ProductsComparisonComponent implements OnInit {
    products?: Product[];
    
    readonly clas: entityClass = 'product';

    images: string[] = [];
    
    get authenticated() {
        return this.storage.authenticated;
    }

    get cats() {
        return this.catService.cats;
    }
    constructor(
        private route: ActivatedRoute,
        private router: Router,
        private repo: RepositoryService,
        private imageService: ImageService,
        private storage: StorageService,
        private catService: CategoryService
    ) { }
    ngOnInit() {
        console.log('ids:'+ this.route.snapshot.params.ids.split(','));
        this.repo.getProductsForComparement('product', this.route.snapshot.params.ids.split(','), p => {
            this.products = p;
        });
        //this.imageService.getImages(this.route.snapshot.params.id, images => { this.images = images; });
    }
}