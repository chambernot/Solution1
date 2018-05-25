import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { EventoModule } from './Evento/EventoModule';
import { EventoComponent } from './Evento/EventoComponent';
import { AppComponent } from './app.component';
import { HttpModule } from '@angular/http';
import { FormsModule } from '@angular/forms';

import './rxjs-extensions';

@NgModule({    
    imports: [
        BrowserModule,
        FormsModule,
        HttpModule,
        EventoModule
    ],
    declarations: [AppComponent],
    bootstrap: [AppComponent, EventoComponent]
})
export class AppModule { }  