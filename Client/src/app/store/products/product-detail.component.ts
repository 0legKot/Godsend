// import { switchMap } from 'rxjs/operators';
import { Component, OnInit } from '@angular/core';
import { Router, ActivatedRoute } from '@angular/router';
import { Observable } from 'rxjs';

import { Product, ProductWithSuppliers, SupplierAndPrice, Category, Property, EAV, propertyType } from '../../models/product.model';
import { RepositoryService, entityClass } from '../../services/repository.service';
import { CartService } from '../../services/cart.service';
import { OrderPartDiscreteSend, guidZero, OrderPartDiscreteView } from '../../models/cart.model';
import { ImageService } from '../../services/image.service';
import { StorageService } from '../../services/storage.service';
import { LinkRatingEntity } from '../../models/rating.model';
import { CategoryService } from '../../services/category.service';
import { searchType, AllSearchResult } from '../search/search.service';
import { Supplier, SupplierInfo } from '../../models/supplier.model';

@Component({
    selector: 'godsend-product-detail',
    templateUrl: 'product-detail.component.html',
    styleUrls: ['./product-detail.component.css']
})
export class ProductDetailComponent implements OnInit {
    data?: ProductWithSuppliers;

    selectedSupplier?: SupplierAndPrice;

    quantity = 1;
    edit = false;
    searchTypeSupplier = searchType.supplier;
    foundSuppliers?: SupplierInfo[];
    supplierToAdd?: SupplierInfo;
    priceToAdd?: number;

    readonly clas: entityClass = 'product';

    images: string[] = [];

    backup: ProductBackup = {
        name: '',
        description: '',
    };

    get price(): string {
        return this.selectedSupplier ? (this.selectedSupplier.price * this.quantity).toFixed(2) : '';
    }

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
        private cart: CartService,
        private imageService: ImageService,
        private storage: StorageService,
        private catService: CategoryService
    ) { }

    gotoProducts(product?: Product) {
        const productId = this.route.snapshot.params.id;
        this.router.navigate(['/products', { id: productId}]);
    }

    deleteProduct() {
        if (this.data) {
            this.repo.deleteEntity('product', this.data.product.info.id, 1, 10);
            this.gotoProducts();
        }
    }
    buy() {
        // Todo make button disabled if no data?

        if (this.data == null || this.selectedSupplier == null) {
            console.log('ERROR: no data');
            return;
        }

        const op: OrderPartDiscreteView = {
            quantity: this.quantity,
            product: this.data.product,
            supplier: this.selectedSupplier.supplier,
            price: this.selectedSupplier.price
        };
        this.cart.addToCart(op);
    }

    editMode() {
        if (this.data == null) {
            console.log('no data');
        } else {
            this.backup = {
                name: this.data.product.info.name,
                description: this.data.product.info.description,
                cat: this.data.product.jsonCategory,
                decimalProps: this.data.product.decimalProps,
                intProps: this.data.product.intProps,
                stringProps: this.data.product.stringProps
            };
            this.edit = true;
        }
    }

    save() {
        if (this.data) {
            this.repo.createOrEditEntity('product', Product.EnsureType(this.data.product), 1, 10);
        }

        this.edit = false;
    }

    discard() {
        if (this.data) {
            this.data.product.info.name = this.backup.name;
            this.data.product.info.description = this.backup.description;
            this.data.product.jsonCategory = this.backup.cat;
            this.data.product.stringProps = this.backup.stringProps;
            this.data.product.intProps = this.backup.intProps;
            this.data.product.decimalProps = this.backup.decimalProps;
        }

        this.edit = false;
    }

    ngOnInit() {
        this.repo.getEntity<ProductWithSuppliers>('product', this.route.snapshot.params.id, p => {
            this.data = p;
            this.selectedSupplier = p.suppliers[0];
        });
        this.imageService.getImages(this.route.snapshot.params.id, images => { this.images = images; });
    }

    changeCategory(newCat: Category) {
        if (this.data) {
            this.data.product.jsonCategory = newCat;
            this.refreshProperties(newCat.id);
        }
    }

    refreshProperties(catId: string) {
        this.catService.getCategoryProps(catId, filter => {
            if (this.data) {
                if (filter.decimalProps) {
                    this.data.product.decimalProps = filter.decimalProps.map(dp =>
                        new EAV<number>(this.data!.product.id,
                            { id: dp.propId, name: dp.name, type: propertyType.indexOf('decimal') },
                            0));
                } else {
                    this.data.product.decimalProps = [];
                }
                if (filter.stringProps) {
                    this.data.product.stringProps = filter.stringProps.map(sp =>
                        new EAV<string>(this.data!.product.id,
                            { id: sp.propId, name: sp.name, type: propertyType.indexOf('string') },
                            ''));
                } else {
                    this.data.product.stringProps = [];
                }
                if (filter.intProps) {
                    this.data.product.intProps = filter.intProps.map(ip =>
                        new EAV<number>(this.data!.product.id,
                            { id: ip.propId, name: ip.name, type: propertyType.indexOf('int') },
                            0));
                } else {
                    this.data.product.intProps = [];
                }
            }
        });
    }

    refreshFoundSuppliers(newData: AllSearchResult) {
        this.foundSuppliers = newData.suppliersInfo;
    }

    addSupplier() {
        // do something
        if (this.data && this.supplierToAdd && this.priceToAdd) {
            this.data.suppliers.push(new SupplierAndPrice(new Supplier(this.supplierToAdd, []), this.priceToAdd));
            this.supplierToAdd = undefined;
            this.priceToAdd = 0;
        }
    }

    /*get product(): Product | {} {
        return this.service.product;
    }*/
}

interface ProductBackup {
    name: string;
    description: string;
    cat?: Category;
    decimalProps?: EAV<number>[];
    stringProps?: EAV<string>[];
    intProps?: EAV<number>[];
}
