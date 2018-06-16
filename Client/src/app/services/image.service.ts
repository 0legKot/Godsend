import { Injectable } from "@angular/core";
import { DataService } from "./data.service";
import { Observable } from "rxjs";
import { HttpClient } from "@angular/common/http";
import { map } from "rxjs/internal/operators/map";


@Injectable({
    providedIn: 'root'
})
export class ImageService {
    constructor(private data: DataService) { }

    getImages(id: string, fn: (_: string[]) => any): void {
        console.log('id is'+id);
        this.data.sendRequest<string[]>('get', 'api/image/getImages/' + id)
            .subscribe(response => fn(response));
    }
}
