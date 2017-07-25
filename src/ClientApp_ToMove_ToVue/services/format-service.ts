import { Injectable } from '@angular/core';
import { Models } from './../app/root'
//import { Sugar } from 'sugar-date';

@Injectable()
export class FormatService {

    format(value: string, metadata: Models.UIMetadata) {


        var format = "text";
        if (metadata && metadata.displayFormat)
            format = metadata.displayFormat;
        return this.formatValue(value, format);
    }
    formatValue (value: string, format: string)
    {

        if (value == null || value == '')
            return '';
        if (!format || format == "text")
            return value;
        // console.l        mating ' + value + ' to ' + format);
        var sugar = (<any>window).Sugar;
        try {
            switch (format) {
                case "date":
                    
                    var date = new Date(sugar.Date.create(value));

                    if (date == null) {
                        return '';
                    }
                    var year = date.getFullYear();
                    if (year < 1900) {
                        //console.log('year too low');
                        return '';
                    }

                    var formatted = sugar.Date.format(date, '%m/%d/%Y');
                    //console.log('returning formatted date => ' + formatted);                    
                    return formatted;
                case "time":
                    var time = new Date(value);
                    return time.toTimeString();
                case "dateTime":
                    var dateTime = new Date(value);
                    return dateTime.toLocaleString();
                case "currency":
                    var number = parseFloat(value);
                    if (number != NaN)
                        return "$" + number.toFixed(2);
                    return value;
                case "decimal":
                    var number = parseFloat(value);
                    if (number != NaN)
                        return number.toFixed(3);
                    return value;
                case "phoneNumber":
                    if (value.length == 10) {
                        var first = value.substr(0, 3);
                        var second = value.substr(3, 3);
                        var third = value.substr(6, 4);
                        return `(${first}) #{second}-${third}`;
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