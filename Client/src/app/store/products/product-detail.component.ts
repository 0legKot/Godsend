import { Component, OnInit } from '@angular/core';
import { Router, ActivatedRoute } from '@angular/router';

import { Product, SupplierAndPrice, Category, EAV, propertyType } from '../../models/product.model';
import { RepositoryService, entityClass } from '../../services/repository.service';
import { CartService } from '../../services/cart.service';
import { OrderPartDiscreteView } from '../../models/cart.model';
import { StorageService } from '../../services/storage.service';
import { CategoryService } from '../../services/category.service';
import { searchType, AllSearchResult } from '../search/search.service';
import { SupplierInfo } from '../../models/supplier.model';
import { Image } from '../../models/image.model';
import { AuthenticationService } from '../../services/authentication.service';

@Component({
    selector: 'godsend-product-detail',
    templateUrl: 'product-detail.component.html',
    styleUrls: ['./product-detail.component.css']
})
export class ProductDetailComponent implements OnInit {
    public product?: Product;

    selectedSupplier?: SupplierAndPrice;

    quantity = 1;
    edit = false;
    searchTypeSupplier = searchType.supplier;
    foundSuppliers?: SupplierInfo[];
    supplierToAdd?: SupplierInfo;
    priceToAdd?: number;

    readonly clas: entityClass = 'product';

    // images: string[] = [];

    backup: ProductBackup = {
        name: '',
        description: '',
    };

    get canEdit(): boolean {
        return Boolean(this.auth.roles.find(x => x == 'Administrator' || x == 'Moderator'));
    }

    get canDelete(): boolean {
        return Boolean(this.auth.roles.find(x => x == 'Administrator' || x == 'Moderator'));
    }

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
        public route: ActivatedRoute,
        private router: Router,
        private repo: RepositoryService,
        private cart: CartService,
        private auth: AuthenticationService,
        private storage: StorageService,
        private catService: CategoryService
    ) { }

    deleteProduct() {
        if (this.product) {
            this.repo.deleteEntity('product', this.product.info.id, 1, 10);
            this.router.navigate(['/products']);
        }
    }
    buy() {
        // Todo make button disabled if no data?

        if (this.product == null || this.selectedSupplier == null) {
            console.log('ERROR: no data');
            return;
        }

        const op: OrderPartDiscreteView = {
            quantity: this.quantity,
            productInfo: this.product.info,
            supplierInfo: this.selectedSupplier.supplierInfo,
            price: this.selectedSupplier.price
        };
        this.cart.addToCart(op);
    }

    editMode() {
        if (this.product == null) {
            console.log('no data');
        } else {
            this.backup = {
                name: this.product.info.name,
                description: this.product.info.description,
                cat: this.product.jsonCategory,
                decimalProps: this.product.decimalProps,
                intProps: this.product.intProps,
                stringProps: this.product.stringProps,
                images: this.product.images
            };
            this.edit = true;
        }
    }

    save() {
        if (this.product) {
            this.repo.createOrEditEntity('product', Product.EnsureType(this.product), 1, 10);
        }

        this.edit = false;
    }

    discard() {
        if (this.product) {
            this.product.info.name = this.backup.name;
            this.product.info.description = this.backup.description;
            this.product.jsonCategory = this.backup.cat;
            this.product.stringProps = this.backup.stringProps;
            this.product.intProps = this.backup.intProps;
            this.product.decimalProps = this.backup.decimalProps;
            this.product.images = this.backup.images;
        }

        this.edit = false;
    }

    ngOnInit() {
        const id: string = this.route.snapshot.params.id;
        this.repo.getEntity<Product>('product', id, p => {
            this.product = p;
            this.selectedSupplier = p.suppliersAndPrices ? p.suppliersAndPrices[0] : undefined;
            /*if (this.product.images) {
                this.imageService.getImages(this.product.images.map(i => i.id), images => { this.images = images; });
            }*/
        });
        if (this.repo.viewedProductsIds.find(x => x === id) === undefined) {
            this.repo.viewedProductsIds.push(this.route.snapshot.params.id);
        }
    }

    changeCategory(newCat: Category) {
        if (this.product) {
            this.product.jsonCategory = newCat;
            this.refreshProperties(newCat.id);
        }
    }

    refreshProperties(catId: string) {
        this.catService.getCategoryProps(catId, filter => {
            if (this.product) {
                if (filter.decimalProps) {
                    this.product.decimalProps = filter.decimalProps.map(dp =>
                        new EAV<number>(this.product!.id,
                            { id: dp.propId, name: dp.name, type: propertyType.indexOf('decimal') },
                            0));
                } else {
                    this.product.decimalProps = [];
                }
                if (filter.stringProps) {
                    this.product.stringProps = filter.stringProps.map(sp =>
                        new EAV<string>(this.product!.id,
                            { id: sp.propId, name: sp.name, type: propertyType.indexOf('string') },
                            ''));
                } else {
                    this.product.stringProps = [];
                }
                if (filter.intProps) {
                    this.product.intProps = filter.intProps.map(ip =>
                        new EAV<number>(this.product!.id,
                            { id: ip.propId, name: ip.name, type: propertyType.indexOf('int') },
                            0));
                } else {
                    this.product.intProps = [];
                }
            }
        });
    }

    refreshFoundSuppliers(newData: AllSearchResult) {
        this.foundSuppliers = newData.suppliersInfo;
    }

    addSupplier() {
        // do something
        if (this.product && this.supplierToAdd && this.priceToAdd) {
            if (this.product.suppliersAndPrices == null) {
                this.product.suppliersAndPrices = [];
            }

            this.product.suppliersAndPrices.push(new SupplierAndPrice(this.supplierToAdd, this.priceToAdd));
            this.supplierToAdd = undefined;
            this.priceToAdd = 0;
        }
    }

    removeSupplier(snp: SupplierAndPrice) {
        if (this.product && this.product.suppliersAndPrices) {
            this.product.suppliersAndPrices = this.product.suppliersAndPrices.filter(s => s !== snp);
        }
    }

    setImages(images: Image[]) {
        if (this.product) {
            this.product.images = images;
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
    images?: Image[];
}
