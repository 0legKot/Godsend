import { Injectable } from '@angular/core';
import { DataService } from './data.service';
import { CatsWithSubs, Category } from '../models/product.model';

@Injectable({
    providedIn: 'root'
})
export class CategoryService {
    public cats?: CatsWithSubs[];
    private flatCats?: CatsWithSubs[];

    constructor(private data: DataService) {
        this.getCategories();
    }

    public getCategories(): void {
        if (this.cats === undefined) {
            this.data.sendRequest<CatsWithSubs[]>('get', 'api/product/getAllCategories')
                .subscribe(cats => {
                    this.cats = cats;
                    this.flatCats = this.flatten(cats);

                    console.dir(this.cats);
                    console.dir(this.flatCats);

                });
        }        
    }

    getSubcategories(cat: Category): Category[] {
        if (this.flatCats) {
            const tmp = this.flatCats.find(c => c.cat == cat);

            if (tmp) return tmp.subs.map(c => c.cat);
        }
        return [];
    }

    private flatten(cats: CatsWithSubs[]): CatsWithSubs[] {
        const queue = cats.map(c => c);
        const result: CatsWithSubs[] = [];

        while (queue.length > 0) {
            const cur = queue.shift();
            if (cur && cur.subs) {
                queue.push(...cur.subs);
            }
            if (cur) result.push(cur);
        }

        return result;
    }
}
