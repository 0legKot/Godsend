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

    getImage(id: string, fn: (_: string) => any): void {
        this.data.sendRequest<string>('get', 'api/image/GetPreviewImage/' + id)
            .subscribe(response => fn(response));
    }

    getImages(id: string, fn: (_: string[]) => any): void {
        this.data.sendRequest<string[]>('get', 'api/image/getImages/' + id)
            .subscribe(response => fn(response));
    }
}
