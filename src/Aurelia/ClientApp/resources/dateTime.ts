import * as moment from 'moment';

export class DateTimeValueConverter {

    toView(value) {
        if (value == null)
            return '';

        return moment(value).format('M/D/YYYY hh:mm');
    }
}