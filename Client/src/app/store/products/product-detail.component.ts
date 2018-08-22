// import { switchMap } from 'rxjs/operators';
import { Component, OnInit } from '@angular/core';
import { Router, ActivatedRoute } from '@angular/router';
import { Observable } from 'rxjs';

import { Product, ProductWithSuppliers, SupplierAndPrice } from '../../models/product.model';
import { RepositoryService } from '../../services/repository.service';
import { CartService } from '../../services/cart.service';
import { OrderPartDiscreteSend, guidZero, OrderPartDiscreteView } from '../../models/cart.model';
import { ImageService } from '../../services/image.service';
import { StorageService } from '../../services/storage.service';
import { LinkRatingEntity } from '../../models/rating.model';

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
    showAllRatings = false;
    allRatings?: LinkRatingEntity[];

    images: string[] = [];

    backup = {
        name: '',
        description: ''
    };

    get price(): string {
        return this.selectedSupplier ? (this.selectedSupplier.price * this.quantity).toFixed(2) : '';
    }

    get authenticated() {
        return this.storage.authenticated;
    }

    constructor(
        private route: ActivatedRoute,
        private router: Router,
        private repo: RepositoryService,
        private cart: CartService,
        private imageService: ImageService,
        private storage: StorageService
    ) { }

    gotoProducts(product?: Product) {
        const productId = this.route.snapshot.params.id;
        this.router.navigate(['/products', { id: productId}]);
    }

    deleteProduct() {
        if (this.data) {
            this.repo.deleteEntity('product', this.data.product.info.id,1,10);
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

        }
        else
        this.backup = {
            name: this.data.product.info.name,
            description: this.data.product.info.description
        };

        this.edit = true;
    }

    save() {
        if (this.data) {
            this.repo.createOrEditEntity('product', Product.EnsureType(this.data.product),1,10);
        }

        this.edit = false;
    }

    discard() {
        if (this.data) {
            this.data.product.info.name = this.backup.name;
            this.data.product.info.description = this.backup.description;
        }

        this.edit = false;
    }

    ngOnInit() {
        this.repo.getEntity<ProductWithSuppliers>(this.route.snapshot.params.id, p => {
            this.data = p;
            this.selectedSupplier = p.suppliers[0];
        }, 'product');
        this.imageService.getImages(this.route.snapshot.params.id, images => { this.images = images; });
    }

    saveRating(newRating: number) {
        if (this.data != null) {
            this.repo.saveRating('product', this.data.product.id, newRating, newAvg => {
                if (this.data != null) {
                    this.data.product.info.rating = newAvg;
                }
            });
        }
    }

    getAllRatings() {
        if (this.data != null) {
            this.repo.getAllRatings('product', this.data.product.id, ratings => {
                this.allRatings = ratings;
                this.showAllRatings = true;
            });
        }
    }

    hideRatings() {
        this.showAllRatings = false;
    }
    /*get product(): Product | {} {
        return this.service.product;
    }*/
}
