"use strict";
var __decorate = (this && this.__decorate) || function (decorators, target, key, desc) {
    var c = arguments.length, r = c < 3 ? target : desc === null ? desc = Object.getOwnPropertyDescriptor(target, key) : desc, d;
    if (typeof Reflect === "object" && typeof Reflect.decorate === "function") r = Reflect.decorate(decorators, target, key, desc);
    else for (var i = decorators.length - 1; i >= 0; i--) if (d = decorators[i]) r = (c < 3 ? d(r) : c > 3 ? d(target, key, r) : d(target, key)) || r;
    return c > 3 && r && Object.defineProperty(target, key, r), r;
};
Object.defineProperty(exports, "__esModule", { value: true });
var core_1 = require("@angular/core");
var EventoComponent_1 = require("./EventoComponent");
var EventoService_1 = require("./EventoService");
var forms_1 = require("@angular/forms");
var common_1 = require("@angular/common");
var EventoModule = (function () {
    function EventoModule() {
    }
    return EventoModule;
}());
EventoModule = __decorate([
    core_1.NgModule({
        imports: [forms_1.FormsModule, common_1.CommonModule],
        declarations: [EventoComponent_1.EventoComponent],
        bootstrap: [EventoComponent_1.EventoComponent],
        providers: [EventoService_1.EventoService]
    })
], EventoModule);
exports.EventoModule = EventoModule;
//# sourceMappingURL=EventoModule.js.map