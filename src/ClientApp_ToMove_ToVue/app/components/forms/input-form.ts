import { Ng, Models, Services, Components } from './../../root';

export class InputForm {
    private properties: Models.UIMetadata[] = [];

    constructor(private classLevelMetadata) {

    }
    //TODO: isDirty
    public isValid(): boolean {
        var valid = true;
        this.getProperties();
        for (var property of this.properties) {
            var control = <Components.InputComponent>property.control;
            var value = control.internalValue;
            var response = Services.ValidationServiceStatic.validate({ metadata: property, value: value });
            control.showValidationIfNeeded(response);
            console.log(property.jsName + '=>' + response.isValid)
            if (!response.isValid) {
                valid = false;
            }
        }
        return valid;
    }

    getProperties = () => {
        for (var key in this.classLevelMetadata) {
            if (this.classLevelMetadata.hasOwnProperty(key)
                && (!this.classLevelMetadata[key].isHidden ||
                    this.classLevelMetadata[key].isHidden == false)
            ) {
                var property = this.classLevelMetadata[key];
                var control = <Components.InputComponent>property.control;
                if (control != null)
                    this.properties.push(this.classLevelMetadata[key]);
            }
        }
    }



}