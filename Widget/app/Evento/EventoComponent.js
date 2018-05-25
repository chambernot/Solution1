"use strict";
var __decorate = (this && this.__decorate) || function (decorators, target, key, desc) {
    var c = arguments.length, r = c < 3 ? target : desc === null ? desc = Object.getOwnPropertyDescriptor(target, key) : desc, d;
    if (typeof Reflect === "object" && typeof Reflect.decorate === "function") r = Reflect.decorate(decorators, target, key, desc);
    else for (var i = decorators.length - 1; i >= 0; i--) if (d = decorators[i]) r = (c < 3 ? d(r) : c > 3 ? d(target, key, r) : d(target, key)) || r;
    return c > 3 && r && Object.defineProperty(target, key, r), r;
};
var __metadata = (this && this.__metadata) || function (k, v) {
    if (typeof Reflect === "object" && typeof Reflect.metadata === "function") return Reflect.metadata(k, v);
};
Object.defineProperty(exports, "__esModule", { value: true });
var core_1 = require("@angular/core");
var EventoModel_1 = require("./EventoModel");
var EventoService_1 = require("./EventoService");
var EventoComponent = (function () {
    function EventoComponent(eventoService) {
        this.eventoService = eventoService;
    }
    EventoComponent.prototype.loadEventos = function () {
        var _this = this;
        this.evento = new EventoModel_1.EventoModel();
        this.eventoService.getEventos(1104).subscribe(function (data) { _this.evento = data; });
    };
    EventoComponent.prototype.ngOnInit = function () {
        this.loadEventos();
    };
    return EventoComponent;
}());
EventoComponent = __decorate([
    core_1.Component({
        selector: 'evento',
        moduleId: module.id,
        templateUrl: './evento.html'
    }),
    __metadata("design:paramtypes", [EventoService_1.EventoService])
], EventoComponent);
exports.EventoComponent = EventoComponent;
//# sourceMappingURL=EventoComponent.js.map