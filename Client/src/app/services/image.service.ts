import { Injectable, Inject } from '@angular/core';
import { DataService } from './data.service';

@Injectable({
    providedIn: 'root'
})
export class ImageService {
    constructor(private data: DataService, @Inject('BASE_URL') private baseUrl: string) { }

    getImagePath(id: string) {
        return this.baseUrl + 'api/image/getImage/' + id;
    }
}
