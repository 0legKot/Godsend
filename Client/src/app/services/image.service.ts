import { Injectable } from '@angular/core';
import { DataService } from './data.service';

@Injectable({
    providedIn: 'root'
})
export class ImageService {
    constructor(private data: DataService) { }

    getImage(id: string, fn: (_: string) => any): void {
        this.data.sendRequest<string>('get', 'api/image/GetPreviewImage/' + id)
            .subscribe(response => fn(response));
    }

    getPreviewImages(ids: string[], fn: any): void {
        this.data.sendRequest<any>('post', 'api/image/getPreviewImages', ids)
            .subscribe(response => fn(response));
    }

    getImages(ids: string[], fn: (_: string[]) => any): void {
        this.data.sendRequest<string[]>('post', 'api/image/getImages/', ids)
            .subscribe(response => fn(response));
    }
}
