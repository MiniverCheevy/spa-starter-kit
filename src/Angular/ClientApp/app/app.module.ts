import { NgModule, ErrorHandler, Injector } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { FormsModule } from '@angular/forms';
import { HttpModule, Http } from '@angular/http';
import { RouterModule } from '@angular/router';
import * as Services from './../services';
import * as Api from './../api.generated';
import { LayoutComponent } from './layout/layout.component';
import { NavmenuComponent } from './layout/navmenu.component';
import { HomeComponent } from './home/home.component';
import { AppComponent } from './app.component';
import { ToolbarIconLinkComponent } from './components/toolbar-icon-link.component';
import { InputFieldComponent } from './components/forms/input-field.component';
import { SwitchComponent } from './components/forms/switch.component';
import { SorterComponent } from './components/grid/sorter.component';
import { PagerComponent } from './components/grid/pager.component';
import { GridComponent } from './components/grid/grid.component';
import { InputSpanComponent } from './components/forms/input-span.component';
import { PushButtonComponent } from './components/buttons/push-button.component';
import { InputDropdownComponent } from './components/forms/input-dropdown.component';
import { FileUploadComponent } from './components/file-upload/file-upload.component';
import { FileUploadService } from './components/file-upload/file-upload-service';
import { InputAutocompleteComponent } from './components/forms/input-autocomplete.component';
import { InputRadioButtonListComponent } from './components/forms/input-radio-button-list.component';
import { PlusMinusButtonComponent } from './components/buttons/plus-minus-button.component';
import { ConfirmDialogComponent } from './components/dialog/confirm-dialog.component';

import { ScratchComponent } from './scratch/scratch.component';

export class GlobalErrorHandler implements ErrorHandler {
    handleError(ex) {
        var error: any = {};
        var exc: any = ex;
        if (ex._nativeError != null)
            exc = ex._nativeError;

        error.errorMsg = exc.message;
        error.ErrorObject = exc.originalStack;
        AjaxServiceStatic.logError(exc, null, exc.originalStack);
        console.log(ex);
        console.error(exc.originalStack);
    };
}

@NgModule({
    bootstrap: [AppComponent],
    declarations: [
        NavmenuComponent,
        HomeComponent,
        LayoutComponent,
        NavmenuComponent,
        ToolbarIconLinkComponent,
        AppComponent,
        InputFieldComponent,
        SwitchComponent,
        ScratchComponent,
        SorterComponent,
        PagerComponent,
        GridComponent,
        PushButtonComponent,
        InputSpanComponent,
        InputDropdownComponent,
        FileUploadComponent,        
        InputAutocompleteComponent,
        InputRadioButtonListComponent,
        PlusMinusButtonComponent,
        ConfirmDialogComponent
    ],    
    providers: [
        { provide: 'ORIGIN_URL', useValue: location.origin },
        { provide: ErrorHandler, useClass: GlobalErrorHandler },

        Services.MessengerService,
        Services.CurrentUserService,
        Services.AjaxService,
        Services.FormatService,
        Services.ListService,
        FileUploadService,
        Api.providers
        
    ],
    exports: [InputFieldComponent],
    imports: [
        BrowserModule,
        FormsModule,
        HttpModule,
        RouterModule.forRoot([
            
            { path: 'home', component: HomeComponent },
            { path: 'scratch', component: ScratchComponent },
            { path: '', redirectTo: 'home', pathMatch: 'full' },
            { path: '**', redirectTo: 'home' }
        ], { useHash:true })
    ]
})
export class AppModule {
    constructor(http: Http) {
        AjaxServiceStatic = new Services.AjaxService(Services.MessengerServiceStatic, Services.CurrentUserServiceStatic, http);
    }
}

export let AjaxServiceStatic: Services.AjaxService;
