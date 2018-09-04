import { Component, OnInit, ViewChild, ElementRef, SimpleChange } from '@angular/core';
import { NG_VALUE_ACCESSOR } from '@angular/forms';
import { Input } from '@angular/core';
import { CustomControlValueAccessor } from '../shared/custom-control-value-accessor';
import { Image } from '../../models/image.model';
import { RepositoryService } from '../../services/repository.service';
import { ImageService } from '../../services/image.service';

@Component({
    selector: 'godsend-gallery',
    templateUrl: './gallery.component.html',
    providers: [
        { provide: NG_VALUE_ACCESSOR, useExisting: GalleryComponent, multi: true }
    ],
})
export class GalleryComponent extends CustomControlValueAccessor<Image[]> implements OnInit {
    @Input()
    edit = false;

    fullView = false;

    curIndex = 0;

    images?: string[];

    prevModel?: Image[] = undefined;

    @ViewChild('fileInput') fileInput!: ElementRef;

    constructor(private repo: RepositoryService, private imageService: ImageService) {
        super();
    }

    ngOnInit(): void {

    }

    /**
     * kostyl'
     */
    ngAfterContentChecked() {
        if (this.value != null && this.value != this.prevModel) {
            this.prevModel = this.value;
            this.refreshImages();
        }
    }

    refreshImages() {
        console.log('refresh images');

        if (this.value == null) {
            return;
        }

        this.imageService.getImages(this.value.map(image => image.id), response => {
            this.images = response;
        });
    }


    remove(index: number) {
        if (this.value) {
            if (this.value.length === 1) {
                this.changeValue([]);
            } else {
                this.changeValue(this.value.filter((_, i) => i != index));
            }
        }
    }

    enterFullView(index: number) {
        this.curIndex = index;
        this.fullView = true;
    }

    exitFullView() {
        this.fullView = false;
    }

    // next image in full view mode
    incrementIndex() {
        if (this.value) {
            this.curIndex = (this.curIndex < this.value.length - 1) ? this.curIndex + 1 : 0;
        }
    }

    // previous image in full view mode
    decrementIndex() {
        if (this.value) {
            this.curIndex = (this.curIndex > 0) ? this.curIndex - 1 : this.value.length - 1;
        }
    }

    uploadFiles() {
        const inputEl: HTMLInputElement = this.fileInput.nativeElement;

        if (this.value == null || inputEl.files == null) {
            return;
        }

        const fileCount: number = inputEl.files.length;

        if (fileCount + this.value.length > 5) {
            alert("Too many images");
            return;
        }

        const formData = new FormData();
        if (fileCount > 0) {
            for (let i = 0; i < fileCount; i++) {
                const typeScriptTheBest = inputEl.files.item(i);
                if (typeScriptTheBest != null) {
                    formData.append('file[]', typeScriptTheBest);
                }
            }
            let itemsProcessed = 0;
            const tmp = this.value;
            this.repo.uploadImages(formData, res => {
                res.forEach((image, index, array) => {
                    tmp.push(image);
                    itemsProcessed++;
                    if (itemsProcessed === array.length) {
                        this.changeValue(tmp);
                        console.log("finished upload");
                        console.dir(this.value);
                    }
                });
            });
        }
    }
}
