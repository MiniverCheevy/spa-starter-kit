import { Ng, Components, Services } from "./../../root"
import * as  FileDrop  from 'filedrop';
let filedropjs: any = (<any>window).fd;

@Ng.Injectable()
export class FileUploadService {
    elementId: string;
    url: string;
    callback;
    uploadType;
    canceller;
    token;

    constructor(private messenger: Services.MessengerService) {
    }


    configure(elementId, url, callback, uploadType)
    {
        this.elementId = elementId;
        this.url = url;
        this.callback = callback;
        this.uploadType = uploadType;
        this.tryConfig();
    }
    tryConfig=()=>
    {
        
        if (this.token != null)
            clearTimeout(this.token);

        filedropjs = (<any>window).fd;
        if (filedropjs == null) {
            this.token = setTimeout(this.tryConfig, 250);
        }
        else
        {
            this.doConfigure();
        }

    }
    doConfigure = () => {
        //var options = { iframe: { url: url } };
        var zone = new filedropjs.FileDrop(this.elementId);//, options);
        var validate = this.validateFile;
        var elementId = this.elementId;
        var url = this.url;
        var callback = this.callback;
        var uploadType = this.uploadType;
        var messenger = this.messenger;
        zone.event('send', function (files) {
            files.each(function (file) {
                file.event('sendXHR', () => {
                    this.messenger.incrementHttpRequestCounter();
                   
                });
                file.event('done', function (xhr) {
                    //console.log('Done uploading ' + this.name);
                    messenger.decrementHttpRequestCounter();
                    if (callback != null) {
                        callback(JSON.parse(xhr.response));
                    }
                });

                //file.event('progress', function (sent, total) {
                //    var p = document.querySelector('#progressText');
                //    p.textContent = 'Uploaded ' + Math.round(sent / total * 100) + '%...';
                //    zone.el.appendChild(p);
                //})

                validate(file, uploadType, url);
            });
        });

        zone.event('iframeDone', function (xhr) {
            //console.log('Done uploading via <iframe>');
        });

        
}
    validateFile = (file, uploadType, url) => {
        switch (uploadType) {
            case Components.UploadType.Document:
                this.validateDocument(file, url);
                break;
            case Components.UploadType.Image:
                this.validateImage(file, url);
                break;
            case Components.UploadType.Csv:
                this.validateCsv(file, url);
                break;
            case Components.UploadType.Any:
                file.sendTo(url);
                break;
        }
    }
    validateDocument = (file, url) => {
        if (file && file.type) {
            const type = file.type;
            if (type.indexOf('pdf') > -1 || type.indexOf('image') > -1 || type.indexOf('msword') > -1 || type.indexOf('excel') > -1 || type.indexOf('openxmlformats') > -1 || type.indexOf('officedocument') > -1) {
                file.sendTo(url);
            } else {
                this.messenger.showResponseMessage({ isOk: false, message: 'Only image files, pdf files and microsoft office files are allowed.' });
            }
        }

    }
    validateImage = (file, url) => {
        if (file && file.type) {
            const type = file.type;
            if (type.indexOf('image') > -1) {
                file.sendTo(url);
            } else {
                this.messenger.showResponseMessage({ isOk: false, message: 'Only image files are allowed.' });
            }
        }
    }
    validateCsv = (file, url) => {
        if (file && file.type) {
            const type = file.type;
            if (type.indexOf('excel') > -1 ||
                type.indexOf('openxmlformats') > -1 ||
                type.indexOf('officedocument') > -1 ||
                type.indexOf('csv')) {
                file.sendTo(url);
                return;
            } else {
                this.messenger.showResponseMessage({ isOk: false, message: 'Only csv files are allowed.' });

            }
        }
    }

}

