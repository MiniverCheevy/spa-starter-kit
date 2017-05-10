var __decorate = (this && this.__decorate) || function (decorators, target, key, desc) {
    var c = arguments.length, r = c < 3 ? target : desc === null ? desc = Object.getOwnPropertyDescriptor(target, key) : desc, d;
    if (typeof Reflect === "object" && typeof Reflect.decorate === "function") r = Reflect.decorate(decorators, target, key, desc);
    else for (var i = decorators.length - 1; i >= 0; i--) if (d = decorators[i]) r = (c < 3 ? d(r) : c > 3 ? d(target, key, r) : d(target, key)) || r;
    return c > 3 && r && Object.defineProperty(target, key, r), r;
};
var __metadata = (this && this.__metadata) || function (k, v) {
    if (typeof Reflect === "object" && typeof Reflect.metadata === "function") return Reflect.metadata(k, v);
};
define(["require", "exports", "aurelia-framework"], function (require, exports, aurelia_framework_1) {
    "use strict";
    var ButtonBar = (function () {
        function ButtonBar(element) {
            this.save = false;
            this.element = element;
        }
        ButtonBar.prototype.attach = function () {
        };
        return ButtonBar;
    }());
    __decorate([
        aurelia_framework_1.bindable,
        __metadata("design:type", Object)
    ], ButtonBar.prototype, "printDelegate", void 0);
    __decorate([
        aurelia_framework_1.bindable,
        __metadata("design:type", String)
    ], ButtonBar.prototype, "addLink", void 0);
    __decorate([
        aurelia_framework_1.bindable,
        __metadata("design:type", String)
    ], ButtonBar.prototype, "deleteDelegate", void 0);
    __decorate([
        aurelia_framework_1.bindable,
        __metadata("design:type", String)
    ], ButtonBar.prototype, "cancelLink", void 0);
    __decorate([
        aurelia_framework_1.bindable,
        __metadata("design:type", Boolean)
    ], ButtonBar.prototype, "save", void 0);
    ButtonBar = __decorate([
        aurelia_framework_1.customElement('button-bar'),
        aurelia_framework_1.inject(Element),
        __metadata("design:paramtypes", [Object])
    ], ButtonBar);
    exports.ButtonBar = ButtonBar;
});
//# sourceMappingURL=data:application/json;base64,eyJ2ZXJzaW9uIjozLCJmaWxlIjoiYnV0dG9uQmFyLmpzIiwic291cmNlUm9vdCI6IiIsInNvdXJjZXMiOlsiYnV0dG9uQmFyLnRzIl0sIm5hbWVzIjpbXSwibWFwcGluZ3MiOiI7Ozs7Ozs7Ozs7O0lBS0EsSUFBYSxTQUFTO1FBT2xCLG1CQUFZLE9BQU87WUFGVCxTQUFJLEdBQVksS0FBSyxDQUFDO1lBRzVCLElBQUksQ0FBQyxPQUFPLEdBQUcsT0FBTyxDQUFDO1FBRTNCLENBQUM7UUFJRCwwQkFBTSxHQUFOO1FBRUEsQ0FBQztRQUVMLGdCQUFDO0lBQUQsQ0FBQyxBQWxCRCxJQWtCQztJQWpCYTtRQUFULDRCQUFROztvREFBb0I7SUFDbkI7UUFBVCw0QkFBUTs7OENBQWlCO0lBQ2hCO1FBQVQsNEJBQVE7O3FEQUF3QjtJQUN2QjtRQUFULDRCQUFROztpREFBb0I7SUFDbkI7UUFBVCw0QkFBUTs7MkNBQXVCO0lBTHZCLFNBQVM7UUFGckIsaUNBQWEsQ0FBQyxZQUFZLENBQUM7UUFDM0IsMEJBQU0sQ0FBQyxPQUFPLENBQUM7O09BQ0gsU0FBUyxDQWtCckI7SUFsQlksOEJBQVMifQ==