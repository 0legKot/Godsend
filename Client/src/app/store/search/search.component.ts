import { Component, OnInit } from '@angular/core';
import { SearchService, searchType, AllSearchResult } from './search.service';
import { SearchBaseComponent } from './search.base.component';
import { ImageService } from '../../services/image.service';

@Component({
    selector: 'godsend-search',
    templateUrl: './search.component.html',
    styleUrls: [
        '../suppliers/suppliers.component.css',
        '../products/products.component.css'
    ]
})
export class SearchComponent extends SearchBaseComponent implements OnInit {
    searchResult?: AllSearchResult;

    /**
     * images as a dictionary where key is id and value is base64-encoded image
     * */
    images: { [id: string]: string } = {};

    constructor(private ss: SearchService, private imageService: ImageService) { super(); }

    ngOnInit() {
        super.ngOnInit();
    }

    doSearch(term: string) {
        this.ss.findByType(searchType.all, term, res => {
            console.dir(res);
            this.searchResult = res;

            const ids = res.productsInfo
                    .filter(p => p.preview != null)
                    .map(p => p.preview!.id)
                .concat(res.suppliersInfo
                    .filter(s => s.preview != null)
                    .map(s => s.preview!.id));

            if (ids.length > 0) {
                this.imageService.getPreviewImages(ids, images => this.images = images);
            }
        });
    }

}
