import { NgModule } from '@angular/core';
import { EventoComponent } from './EventoComponent';
import { EventoService } from './EventoService';
import { FormsModule } from '@angular/forms';
import { CommonModule } from '@angular/common';

@NgModule({    
    imports: [ FormsModule, CommonModule],
    declarations: [EventoComponent],
    bootstrap: [EventoComponent],
    providers: [EventoService]
})
export class EventoModule { }  