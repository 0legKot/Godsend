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
        // use ID
        this.data.sendRequest<string[]>('get','api/image/getImages')
            .subscribe(response => fn(response));
    }
}
