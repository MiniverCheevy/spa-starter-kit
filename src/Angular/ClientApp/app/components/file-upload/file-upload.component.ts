import { Component, AfterContentInit  } from '@angular/core';
import { Services, Ng, Models } from './../../root';
import { UploadType } from './UploadType';
import { FileUploadService } from './file-upload-service';

@Component({
    selector: 'file-upload',
    templateUrl: './file-upload.component.html',
    styleUrls: ['./file-upload.component.css']
})

@Ng.Injectable()
export class FileUploadComponent implements AfterContentInit  {
    elementId = "file-drop-container";
    
    @Ng.Input() url: string;
    @Ng.Input() uploadtype: UploadType;
    @Ng.Output() done = new Ng.EventEmitter();

    constructor(private fileUploader: FileUploadService, private messenger: Services.MessengerService) { }

    ngAfterContentInit () {
        this.fileUploader.configure(this.elementId, this.url, this.uploadDone, this.uploadtype);
    }


    uploadDone = (response: Models.IResponse) => {
        this.messenger.numberOfPendingHttpRequest = 0;
        this.done.emit({});
        this.messenger.showResponseMessage(response);
    }
}