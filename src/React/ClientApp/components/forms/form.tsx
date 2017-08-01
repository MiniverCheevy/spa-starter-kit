import * as React from 'react';
import { Models, Services } from './../../root';
import { observable, observer } from './../../mx';
import * as Validation from './../../services/validation';



export class Form {
    @observable renderCount = observable(0);
    parent: Form;
    childForms: Form[] = [];
    properties: Models.UIMetadata[] = [];
    isValid = true;
    isDirty = false;
    callback: (form: Form) => void
    @observable model;
    
    constructor(public metadata, model) {
        this.model = observable(model);
        this.properties = Services.FormsService.getProperties(this.metadata);
        this.renderCount.set(this.renderCount.get() + 1);
        
    }
    updateModel=(model)=>
    {
        Object.assign(this.model, model);
        this.renderCount.set(this.renderCount.get() + 1);
    }
    setCallback(callback: (form: Form) => void)
    {
        this.callback = callback;
    }
    addChildForm(form: Form) {
        form.parent = this;
        this.childForms.push(form);
    }
    setDirty=()=>
    {
        this.isDirty = true;
        if (this.parent != null)
            this.parent.setDirty();
    }
    onChange=(event)=> {
        this.setDirty();
        this.updateProperty(event.target.name, event.target.value);
        console.log('form=>'+ event.target.name+'='+ event.target.value);
        if (this.callback != null)
            this.callback(this);
    }
    updateProperty(key, value) {
        this.model[key] = value
    }
    getValue(key: string)
    {
        return this.model[key];
    }
    getMetadata(key: string)
    {
        return this.metadata[key];
    }
    getRenderer()
    {
        return <span className="hidden">{this.renderCount}</span>;
    }
}
