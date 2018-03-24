import { ValidationResponse } from './validation-response';
import { ValidationRequest } from './validation-request';
import { Validator } from './validator';
import { Models } from './../../root'
import * as sugar from 'sugar';

export class DateValidator extends Validator {

    validate = () => {
        
        if (this.request.metadata == null
            || this.request.metadata.date == null)
            return { isValid: true, message: '' };
        
        this.validation = this.request.metadata.date;

        var message = 'invalid date';

        var invalidResponse = { message: message, isValid: false };

        
        if (this.value == null || this.value == undefined) {
            return { isValid: true, message: '' };//This is not a required check
        }
        var date = new Date(sugar.Date.create(this.value));
        if (date == null) {
            return invalidResponse;
        }
        //if (this.request.metadata.displayFormat == "time")
        //    this.value = date.toLocaleTimeString();
        //else
        //    this.value = date.toLocaleDateString();
        return { isValid:true, message:''};
        //else if (typeof this.value == 'string') {
        //    if (this.value.trim() == '')
        //        return;//This is not a required check
        //}

        //if (!this.isDate(this.value))
        //    this.response = invalidResponse;

    }
    isDate = (value): boolean => {
        var d = new Date(value);
        if (Object.prototype.toString.call(d) === "[object Date]") {
            return !isNaN(d.getTime());
        }
        return false;               
    }
}