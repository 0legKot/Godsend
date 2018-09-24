import { Component, ViewChild, ElementRef, Output, EventEmitter } from '@angular/core';
import { Input } from '@angular/core';
import { Image } from '../../models/image.model';
import { RepositoryService } from '../../services/repository.service';
import { ImageService } from '../../services/image.service';

@Component({
    selector: 'godsend-gallery',
    templateUrl: './gallery.component.html',
})
export class GalleryComponent {
    @Input()
    edit = false;

    @Input()
    value!: Image[];

    @Output()
    readonly valueChange = new EventEmitter<Image[]>();

    fullView = false;

    curIndex = 0;

    @ViewChild('fileInput') fileInput!: ElementRef;

    constructor(private repo: RepositoryService, private imageService: ImageService) {
    }

    getPath(index: number): string {
        return this.imageService.getImagePath(this.value[index].id);
    }

    changeValue(newValue: Image[]) {
        this.value = newValue;
        this.valueChange.emit(newValue);
    }

    remove(index: number) {
        if (this.value) {
            if (this.value.length === 1) {
                this.changeValue([]);
            } else {
                this.changeValue(this.value.filter((_, i) => i !== index));
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
            alert('Too many images');
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
                        console.log('finished upload');
                        console.dir(this.value);
                    }
                });
            });
        }
    }
}
