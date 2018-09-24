import { Component, AfterViewInit, Input, DoCheck, ViewChild, ElementRef } from '@angular/core';
import { NG_VALUE_ACCESSOR } from '@angular/forms';
import { CustomControlValueAccessor } from '../shared/custom-control-value-accessor';

@Component({
    selector: 'godsend-richtext',
    styleUrls: [
        './richtext.component.css'
    ],
    templateUrl: './richtext.component.html',
    providers: [
        { provide: NG_VALUE_ACCESSOR, useExisting: RichtextComponent, multi: true }
    ],
})

export class RichtextComponent extends CustomControlValueAccessor<string> implements AfterViewInit, DoCheck {

    // = Model
    // this.model
    // = Editing mode
    @Input()
    edit = false;

    @Input()
    maxLength?: number;

    curLength = 0;
    divClass: allowedDivClass = 'ok';
    editor: any;

    @ViewChild('editbox')
    el!: ElementRef;

    // Next is pell's source code
    actions: any = {
        bold: {
            icon: '<b>B</b>',
            title: 'Bold',
            result: () => this.exec('bold')
        },
        italic: {
            icon: '<i>I</i>',
            title: 'Italic',
            result: () => this.exec('italic')
        },
        underline: {
            icon: '<u>U</u>',
            title: 'Underline',
            result: () => this.exec('underline')
        },
        strikethrough: {
            icon: '<strike>S</strike>',
            title: 'Strike-through',
            result: () => this.exec('strikeThrough')
        },
        heading1: {
            icon: '<b>H<sub>1</sub></b>',
            title: 'Heading 1',
            result: () => this.exec('formatBlock', '<H1>')
        },
        heading2: {
            icon: '<b>H<sub>2</sub></b>',
            title: 'Heading 2',
            result: () => this.exec('formatBlock', '<H2>')
        },
        paragraph: {
            icon: '<i class= "fa fa-paragraph"></i>',
            title: 'Paragraph',
            result: () => this.exec('formatBlock', '<P>')
        },
        quote: {
            icon: '&#8220;&#8221;',
            title: 'Quote',
            result: () => this.exec('formatBlock', '<BLOCKQUOTE>')
        },
        olist: {
            icon: '<i class="fa fa-list-ol"></i>',
            title: 'Ordered List',
            result: () => this.exec('insertOrderedList')
        },
        ulist: {
            icon: '<i class="fa fa-list-ul"></i>',
            title: 'Unordered List',
            result: () => this.exec('insertUnorderedList')
        },
        code: {
            icon: '&lt;/&gt;',
            title: 'Code',
            result: () => this.exec('formatBlock', '<PRE>')
        },
        line: {
            icon: '&#8213;',
            title: 'Horizontal Line',
            result: () => this.exec('insertHorizontalRule')
        },
        link: {
            icon: '&#128279;',
            title: 'Link',
            result: () => {
                const url = window.prompt('Enter the link URL');
                if (url) { this.exec('createLink', url); }
            }
        },
        image: {
            icon: '&#128247;',
            title: 'Image',
            result: () => {
                const url = window.prompt('Enter the image URL');
                if (url) { this.exec('insertImage', url); }
            }
        },
        left: {
            icon: '<i class= "fa fa-align-left"></i>',
            title: 'Align left',
            result: () => this.exec('justifyLeft')
        },
        center: {
            icon: '<i class= "fa fa-align-center"></i>',
            title: 'Align center',
            result: () => this.exec('justifyCenter')
        },
        right: {
            icon: '<i class= "fa fa-align-right"></i>',
            title: 'Align right',
            result: () => this.exec('justifyRight')
        },
        justify: {
            icon: '<i class= "fa fa-align-justify"></i>',
            title: 'Justify',
            result: () => this.exec('justifyFull')
        },

    };

    classes = {
        actionbar: 'pell-actionbar',
        button: 'pell-button',
        content: 'pell-content'
    };
    // == On view initialization
    ngAfterViewInit(): void {

        this.editor = this.init({
            // <HTMLElement>, required
            element: this.el.nativeElement,

            // <Function>, required
            // Use the output html, triggered by element's `oninput` event
            onChange: (html: string, text: string) => {
                this.changeValue(html);
                this.curLength = text.length;
            },

            // <Array[string | Object]>, string if overwriting, object if customizing/creating
            // action.name<string> (only required if overwriting)
            // action.icon<string> (optional if overwriting, required if custom action)
            // action.title<string> (optional)
            // action.result<Function> (required)
            // Specify the actions you specifically want (in order)
            actions: [
                'bold',
                'italic',
                'underline',
                'strikethrough',
                'left',
                'center',
                'right',
                'justify',
                'heading1',
                'heading2',
                'paragraph',
                'quote',
                'olist',
                'ulist',
                'link',

            ],

            // classes<Array[string]> (optional)
            // Choose your custom class names
            classes: {
                actionbar: 'actionbar',
                button: 'button',
                content: 'content'
            }
        });
    }


    ngDoCheck(): void {
        // Initially sets editor text to model, in Edge innerHTML is <br> by default
        if (this.editor && this.editor.content && (!this.editor.content.innerHTML || this.editor.content.innerHTML === '<br>')) {
            this.editor.content.innerHTML = this.value;
            this.curLength = this.editor.content.textContent.length;
        }

        this.divClass = !this.maxLength || this.curLength <= this.maxLength * 0.8 ? 'ok' :
            this.curLength <= this.maxLength ? 'warning' : 'error';
    }

    exec = (command: string, value: any = null) => {
        document.execCommand(command, false, value);
    }

    preventTab = (event: any) => {
        if (event.which === 9) {
            event.preventDefault();
        }
    }

    init = (settings: any) => {
        const self = this;
        settings.actions = settings.actions
            ? settings.actions.map((action: any) => {
                if (typeof action === 'string') {
                    return this.actions[action];
                } else if (this.actions[action.name]) {
                    return { ...this.actions[action.name], ...action };
                }
                return action;
            })
            : Object.keys(this.actions).map(action => this.actions[action]);

        settings.classes = { ...this.classes, ...settings.classes };

        const actionbar = document.createElement('div');
        actionbar.className = settings.classes.actionbar;
        settings.element.appendChild(actionbar);

        settings.element.content = document.createElement('div');
        settings.element.content.contentEditable = true;
        settings.element.content.className = settings.classes.content;
        settings.element.content.oninput = (event: any) => settings.onChange(event.target.innerHTML, event.target.textContent);
        settings.element.content.onkeydown = this.preventTab;

        // Paste everything as plain text
        settings.element.content.addEventListener('paste', function (e: any) {
            e.preventDefault();
            const text = e.clipboardData.getData('text/plain');
            self.exec('insertHTML', text);
        });

        settings.element.appendChild(settings.element.content);

        settings.actions.forEach((action: any) => {
            const button = document.createElement('button');
            button.className = settings.classes.button;
            button.innerHTML = action.icon;
            button.title = action.title;
            button.onclick = action.result;
            actionbar.appendChild(button);
        });

        if (settings.styleWithCSS) { this.exec('styleWithCSS'); }

        return settings.element;
    }


}

type allowedDivClass = 'ok' | 'warning' | 'error';

