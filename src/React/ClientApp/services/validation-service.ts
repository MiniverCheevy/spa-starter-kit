import { Models } from './../root'
import * as Validation from './validation';

export class ValidationServicePrototype
{
    private validators: Validation.Validator[] = [
        
        new Validation.DecimalValidator(),
        new Validation.IntValidator(),
        new Validation.DateValidator(),
        new Validation.RequiredValidator(),
        new Validation.StringLengthValidator()
    ];
    public validate(request: Validation.ValidationRequest): Validation.ValidationResponse
    {

        const result = new Validation.ValidationResponse();
        for (let validator of this.validators) {

            if (request.metadata.isHidden || request.metadata.isReadOnly || request.metadata.control == null)
                return { isValid:true, message:'' };
            const validatorResult = validator.isValid(request);
            
            if (!validatorResult.isValid) {
                //console.log(request.metadata.propertyName + '=>' + JSON.stringify(validatorResult));
                return validatorResult;
            }
                
        }
        return result;
    }
}
export const ValidationService = new ValidationServicePrototype();

    
