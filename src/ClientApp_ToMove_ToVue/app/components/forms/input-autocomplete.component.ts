import { Component, OnInit } from '@angular/core';
import { Ng, Models, Components } from './../../root';
import { InputComponent } from './input-component';
@Component({
    selector: 'input-autocomplete',
    templateUrl: './input-autocomplete.component.html',
    styleUrls: ['./input-autocomplete.component.css']
})
export class InputAutocompleteComponent //extends InputComponent
    implements Ng.DoCheck // Ng.OnChanges,
{

    @Ng.Input() model;
    @Ng.Input() selectedText = '';
    @Ng.Input() suggestions: Models.IListItem[] = [];
    @Ng.Output() change = new Ng.EventEmitter<{text:string}>();
    @Ng.Output() done = new Ng.EventEmitter < Models.IListItem>();
    hasChanged: boolean = false;
    inFocus: boolean = false;
    acceptTextInput: boolean = false;
    filteredSuggestions: Models.IListItem[] = [];
    showList: boolean = false;
    selectedItem: Models.IListItem;
    selectedValue;
    hash;
    debounce: number = 500;
    timerKey;
    //TODO: select click/change not firing

    constructor() {
        //super();
    }
    ngDoCheck() {

        var hash = '';
        for (var i = 0, l = this.suggestions.length; i < l; i++) {
            hash = `${hash}.${this.suggestions[i].id}`;
        }

        if (this.hash == hash)
            return;
        this.hash = hash;
        console.log('ngDoCheck');
        this.hasChanged = false;
        this.handleNewText();
        this.hasChanged = true;
    }

    suggestionsChanged(newValue, oldValue) {
        if (newValue != null) {
            this.filteredSuggestions = this.suggestions.filter(this.contains);
        }
    }
    blur() {

        this.acceptTextInput = false;
        this.showList = false;

        if (this.filteredSuggestions.length > 0) {
            this.selectedValue = this.filteredSuggestions[0].id;
            this.selectedText = this.filteredSuggestions[0].name;
            this.selectedItem = this.filteredSuggestions[0];
        }
        this.bubbleDone();

    }
    focus() {
        this.showList = true;
        this.acceptTextInput = true;
    }
    selectMouseOver = () => {
        this.inFocus = true;
    }
    selectMouseOut = () => {
        this.inFocus = false;
    }
    selectChange($event) {
        
        console.log('select change');
        const selected = $event.target.selectedOptions;
        const index = $event.target.selectedIndex;
        if (selected.length != 0) {
            this.inFocus = false;
            this.showList = false;

            this.selectedItem = this.filteredSuggestions[index];
            this.selectedText = this.selectedItem.name;
        }
        this.bubbleDone();
    }

    keyPressed($event: Event) {
        if (!this.acceptTextInput)
            return false;

        this.handleNewText();
        return true;
    }
    handleNewText = () => {
        if (this.timerKey != null)
            return;
        this.filteredSuggestions = this.suggestions.filter(this.contains);
        this.timerKey = setTimeout(this.bubbleChange, this.debounce);
    }
    contains = (value) => {
        return this.selectedText == null || this.selectedText == '' || value.name.toLowerCase().includes(this.selectedText.toLowerCase());
    }

    bubbleChange = () => {
        if (!this.hasChanged)
            return;

        this.timerKey = null;
        if (typeof this.selectedText == "string") {
            console.log('bubbleChange=>' + this.selectedText);
            this.change.emit({ text: this.selectedText });
        }

    }
    bubbleDone = () => {
        this.inFocus = false;
        this.showList = false;
        if (this.selectedItem == null && this.filteredSuggestions && this.filteredSuggestions.length > 0)
            this.selectedItem = this.filteredSuggestions[0];
        if (this.selectedItem != null) {
            //console.log('bubbleDone=>' + this.selectedItem.id);
            this.done.emit(this.selectedItem);
        }
    }
    handleFormat() {
        this.handleNewText();
    }
}