import { NgModule, ErrorHandler } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { FormsModule } from '@angular/forms';
import { HttpModule } from '@angular/http';
import { RouterModule } from '@angular/router';
import { LayoutComponent } from './layout/layout.component';
import { NavmenuComponent } from './layout/navmenu.component';
import { HomeComponent } from './home/home.component';
import { ToolbarIconLinkComponent } from './components/toolbar-icon-link.component';
import { AppComponent } from './app.component';
//import { Services } from './root';
import * as Services from './../services';
import { AjaxServiceStatic } from '../services/ajax-service';
import { InputFieldComponent } from './components/input-field.component';
import { SwitchComponent } from './components/switch.component';
import { ScratchComponent } from './scratch/scratch.component';
import { SorterComponent } from './components/sorter.component';
import { DataTableComponent } from './components/data-table.component';
export class GlobalErrorHandler implements ErrorHandler {
    handleError(ex) {
        var error: any = {};
        var exc: any = ex;
        if (ex._nativeError != null)
            exc = ex._nativeError;

        error.errorMsg = exc.message;
        error.ErrorObject = exc.originalStack;
        AjaxServiceStatic.logError(exc, null, exc.originalStack);
        //(<any>window).jQuery.ajax({
        //    type: "POST",
        //    url: "api/clienterror",
        //    data: error
        //});
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
        DataTableComponent
    ],    
    providers: [
        { provide: 'ORIGIN_URL', useValue: location.origin },
        { provide: ErrorHandler, useClass: GlobalErrorHandler },

        Services.MessengerService,
        Services.CurrentUserService
    ],
    exports: [InputFieldComponent],
    imports: [
        BrowserModule,
        FormsModule,
        HttpModule,
        RouterModule.forRoot([
            { path: '', redirectTo: 'home', pathMatch: 'full' },
            { path: 'home', component: HomeComponent },
            { path: 'scratch', component: ScratchComponent },
            { path: '**', redirectTo: 'home' }
        ], { useHash:true })
    ]
})
export class AppModule {
}
