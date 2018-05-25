import { Component, OnInit, OnDestroy } from '@angular/core';
import { EventoModel } from './EventoModel';
import { EventoService } from './EventoService';

@Component({
    selector: 'evento',
    moduleId: module.id,    
    templateUrl: './evento.html'    
})
export class EventoComponent implements OnInit {
    evento: EventoModel

    constructor(private eventoService: EventoService) { }

    private loadEventos() {
        this.evento = new EventoModel();
        this.eventoService.getEventos(1104).subscribe(data => {this.evento = data});
    }

    ngOnInit(){
        this.loadEventos();
    }
} 