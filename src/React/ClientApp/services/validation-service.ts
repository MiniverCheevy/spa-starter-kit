import { Models } from './../root'
import * as Validation from './validation';

export class ValidationServicePrototype
{
    private validators: Validation.Validator[] = [
        new Validation.RequiredValidator(),
        new Validation.DecimalValidator(),
        new Validation.IntValidator(),
        new Validation.DateValidator()
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
export const ValidationService = new ValidationServicePrototype();

    
