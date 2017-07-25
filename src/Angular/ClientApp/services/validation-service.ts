import { Injectable } from '@angular/core';
import { Models } from './../app/root'
import * as Validation from './validation';

@Injectable()
export class ValidationService
{
    private validators: Validation.Validator[] = [
        new Validation.RequiredValidator(),
        new Validation.DecimalValidator(),
        new Validation.IntValidator()
    ];
    public validate(request: Validation.ValidationRequest): Validation.ValidationResponse
    {

        var result = new Validation.ValidationResponse();
        for (let validator of this.validators)
        {

            var result = validator.isValid(request);
            
            if (!result.isValid)
                return result;
        }
        return result;
    }
}
export const ValidationServiceStatic = new ValidationService();

    
