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

    get dProps() {
        let res: any[] = [];
        if (this.products) this.products.forEach(x => {
            if (x.decimalProps) x.decimalProps.forEach(y => {
                if (res.filter(z => z.property.name == y.property.name).length==0) res.push(y)
            })
        });
        return res;
    }

    get iProps() {
        let res: any[] = [];
        if (this.products) this.products.forEach(x => {
            if (x.intProps) x.intProps.forEach(y => {
                if (res.filter(z => z.property.name == y.property.name).length == 0) res.push(y)
            })
        });
        return res;
    }

    get sProps() {
        let res: any[] = [];
        if (this.products) this.products.forEach(x => {
            if (x.stringProps) x.stringProps.forEach(y => {
                if (res.filter(z => z.property.name == y.property.name).length == 0) res.push(y)
            })
        });
        return res;
    }

    getIntValue(prod: Product, propName: string) {
        if (prod.intProps != undefined && prod.intProps.filter(x => x.property.name == propName)[0] != undefined)
            return prod.intProps.filter(x => x.property.name == propName)[0].value;
        else return '';
    }

    getStringValue(prod: Product, propName: string) {
        if (prod.stringProps != undefined && prod.stringProps.filter(x => x.property.name == propName)[0] != undefined)
            return prod.stringProps.filter(x => x.property.name == propName)[0].value;
        else return '';
    }

    getDecimalValue(prod: Product, propName: string) {
        if (prod.decimalProps != undefined && prod.decimalProps.filter(x => x.property.name == propName)[0] != undefined)
            return prod.decimalProps.filter(x => x.property.name == propName)[0].value;
        else return '';
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