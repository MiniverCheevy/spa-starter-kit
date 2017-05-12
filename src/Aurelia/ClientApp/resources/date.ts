import * as moment from 'moment';

export class DateValueConverter {
    
    toView(value) {
        if (value == null)
            return '';

        return moment(value).format('M/D/YYYY');
    }
}