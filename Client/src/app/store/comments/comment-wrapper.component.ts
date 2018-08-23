import { Component, OnInit, Input, Injectable } from '@angular/core';
import { Article } from '../../models/article.model';
import { ActivatedRoute } from '@angular/router';
import { RepositoryService, supportedClass } from '../../services/repository.service';
import { NestedTreeControl } from '@angular/cdk/tree';
import { MatTreeNestedDataSource } from '@angular/material/tree';
import { BehaviorSubject } from 'rxjs';

export class FileNode {
    children!: FileNode[];
    filename!: string;
    type: any;
}

 

@Component({
  selector: 'godsend-comment-wrapper',
    templateUrl: './comment-wrapper.component.html',
})
export class CommentWrapperComponent implements OnInit {
    @Input()
    id = "";
    @Input()
    clas!: supportedClass;
    comments: any = {};
    TREE_DATA: string = "{}";
    dataChange = new BehaviorSubject<FileNode[]>([]);

    get data(): FileNode[] { return this.dataChange.value; }
    nestedTreeControl: NestedTreeControl<FileNode>;
    nestedDataSource: MatTreeNestedDataSource<FileNode>;

    constructor(private route: ActivatedRoute, private repo: RepositoryService) {
        this.nestedTreeControl = new NestedTreeControl<FileNode>(this._getChildren);
        this.nestedDataSource = new MatTreeNestedDataSource();

        this.dataChange.subscribe(data => this.nestedDataSource.data = data);
    }

    hasNestedChild = (_: number, nodeData: FileNode) => !nodeData.type;

    private _getChildren = (node: FileNode) => node.children;

    initialize() {
        // Parse the string to json object.
        const dataObject = JSON.parse(this.TREE_DATA);

        // Build the tree nodes from Json object. The result is a list of `FileNode` with nested
        //     file node as children.
        const data = this.buildFileTree(dataObject, 0);
        console.log(data);
        // Notify the change.
        this.dataChange.next(data);
    }

    /**
     * Build the file structure tree. The `value` is the Json object, or a sub-tree of a Json object.
     * The return value is the list of `FileNode`.
     */
    buildFileTree(obj: any, level: number): FileNode[] {
        return Object.keys(obj).reduce<FileNode[]>((accumulator, key) => {
            const value = obj[key];
            const node = new FileNode();
            node.filename = key;

            if (value != null) {
                if (typeof value === 'object') {
                    node.children = this.buildFileTree(value, level + 1);
                } else {
                    node.type = value;
                }
            }

            return accumulator.concat(node);
        }, []);
    }
    

    ngOnInit() {
        console.clear();
        console.log(this.id);
        this.repo.getEntityComments<any>(this.clas, this.id, a => {
            this.comments = this.repo.comments;
            this.TREE_DATA = JSON.stringify(this.comments);
            this.initialize();
        });
        
  }

}
