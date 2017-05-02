
//lifted with love from angularjs 1.0
//https://raw.githubusercontent.com/angular/angular.js/ddb4ef13a9793b93280e6b5ab2e0593af1c04743/src/Angular.js
//better handling of arrays (more model binder friendly) than $.params

export const EncoderService = new EncoderServicePrototype();

class EncoderServicePrototype {
    private isNumber = (value) => {
        try {
            var num = parseFloat(value);
            return !isNaN(num) && isFinite(num);
        }
        catch (e) {
            return false;
        }
    }
    private isDate = (value) => {
        return toString.call(value) === '[object Date]';
    }
    private isObject = (value) => {
        // http://jsperf.com/isobject4
        return value !== null && typeof value === 'object';
    }

    private serializeValue = (v) => {
        if (this.isObject(v)) {
            return this.isDate(v) ? v.toISOString() : v;
        }
        return v;
    }

    private encodeUriQuery = (val, pctEncodeSpaces) => {
        return encodeURIComponent(val).
            replace(/%40/gi, '@').
            replace(/%3A/gi, ':').
            replace(/%24/g, '$').
            replace(/%2C/gi, ',').
            replace(/%3B/gi, ';').
            replace(/%20/g, (pctEncodeSpaces ? '%20' : '+'));
    }
    public test() {
        var obj = { obj: { a: 'a', b: 'b', c: [1, 2, 3], d: [{ z: 'z' }, { y: 'y' }] } };
        console.log(this.serializeParams(obj));

    }
    public serializeParams = (params, prefix?) => {
        if (!params) return '';
        var parts = [];

        for (var key in params) {
            var value = params[key];
            if (value != null) {
                var isNumber = this.isNumber(key);
                var name = '';
                if (isNumber)
                    name = prefix;
                else
                    name = prefix == null ? key : prefix + "." + key;
                if (this.isObject(value)) {
                    parts.push(this.serializeParams(value, name));
                }
                else if (Array.isArray(value)) {
                    Array.prototype.forEach.call(<any>value, (v) => {
                        if (this.isObject(value)) {
                            parts.push(this.serializeParams(value, name));
                        }
                        else {
                            parts.push(this.encodeUriQuery(name, null) + '=' + this.encodeUriQuery(this.serializeValue(v), null));
                        }
                    });
                }
                else {
                    parts.push(this.encodeUriQuery(name, null) + '=' + this.encodeUriQuery(this.serializeValue(value), null));
                }
            }
        }
        return parts.join('&');
    }
}