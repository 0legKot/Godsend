import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { RepositoryService, entityClass } from '../../services/repository.service';
import { ImageService } from '../../services/image.service';
import { StorageService } from '../../services/storage.service';
import { CategoryService } from '../../services/category.service';
import { Product, EAV } from '../../models/product.model';

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
        const res: any[] = [];
        if (this.products) { this.products.forEach(x => {
            if (x.decimalProps) {
                x.decimalProps.forEach(y => {
                if (res.filter(z => z.property.name === y.property.name).length === 0) {
                    res.push(y); }
                });
            }
        });
        }
        return res;
    }

    get iProps() {
        const res: any[] = [];
        if (this.products) { this.products.forEach(x => {
            if (x.intProps) x.intProps.forEach(y => {
                if (res.filter(z => z.property.name == y.property.name).length == 0) res.push(y)
            })
        });
        }
        return res;
    }

    get sProps() {
        let res: any[] = [];
        if(this.products) this.products.forEach(x => {
            if(x.stringProps) x.stringProps.forEach(y => {
                if (res.filter(z => z.property.name == y.property.name).length == 0) res.push(y)
            })
        });
        return res;
    }

    getValue(props: EAV<any>[], propName: string) {
        if (props !== undefined && props.filter(x => x.property.name == propName)[0] != undefined)
            return props.filter(x => x.property.name == propName)[0].value;
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
        console.log('ids:' + this.route.snapshot.params.ids.split(','));
        this.repo.getProductsForComparement('product', this.route.snapshot.params.ids.split(','), p => {
            this.products = p;
        });
        // this.imageService.getImages(this.route.snapshot.params.id, images => { this.images = images; });
    }
}
