import { Models } from './../root'
import * as sugar from 'sugar';

class FormatServicePrototype {

    format(value: string, metadata: Models.UIMetadata) {
        var format = "text";
        if (metadata && metadata.displayFormat)
            format = metadata.displayFormat;
        //TODO: add do not format to UI attribute
        if (metadata && metadata.propertyName && ( metadata.propertyName.endsWith('ID') || metadata.propertyName.endsWith('Id')))
            return value;
        return this.formatValue(value, format);
    }
    formatForDisplay(value: string, metadata: Models.UIMetadata) {
        //TODO: add , to #s, $ to currency
        var format = "text";
        if (metadata && metadata.displayFormat)
            format = metadata.displayFormat;
        //TODO: add do not format to UI attribute
        if (metadata && metadata.propertyName && (metadata.propertyName.endsWith('ID') || metadata.propertyName.endsWith('Id')))
            return value;
        return this.formatValue(value, format);
    }
    //TODO: add parseValue to deformat, possibly rename to toView and toModel 
    private formatValue (value: any, format: string)
    {

        if (value == null || value === '')
            return '';
        if (!format || format == "text")
            return value;
        
        try {
            
            switch (format) {
                case "bool":
                    if (value == true || value == "true")
                        return "Yes";
                    else if (value == null || value == '')
                        return '';
                    else
                        return 'No';
                case "date":
                    
                    var date = new Date(sugar.Date.create(value));

                    if (date == null) {
                        return value;
                    }
                    var year = date.getFullYear();
                    if (year < 1900) {
                        return '';
                    }

                    var formatted = sugar.Date.format(date, '%m/%d/%Y');              
                    return formatted;
                case "time":               
                    var time = sugar.Date.create(value, { fromUTC: true });
                    var formatted = sugar.Date.format(time, '%r');
                    return formatted;
                case "dateTime":
                    var dateTime = new Date(value);
                    return dateTime.toLocaleString();
                case "int":
                    var number = parseInt(value);
                    if (number != NaN)
                        return sugar.Number.format(number, 0);
                    return value;
                case "currency":
                    var number = parseFloat(value);
                    if (number != NaN)
                        return sugar.Number.format( number,2);
                    return value;
                case "decimal":
                    var number = parseFloat(value);
                    if (number != NaN)
                        return sugar.Number.format(number, 2);
                    return value;
                case "phoneNumber":
                    if (value.length == 10) {
                        var first = value.substr(0, 3);
                        var second = value.substr(3, 3);
                        var third = value.substr(6, 4);
                        return `(${first}) ${second}-${third}`;
                    }
                    else {
                        return value;
                    }
            }
            return value;
        }
        catch (e) {
            console.log(e);
            return value;
        }
    }
}
export const  FormatService = new FormatServicePrototype();