var __decorate = (this && this.__decorate) || function (decorators, target, key, desc) {
    var c = arguments.length, r = c < 3 ? target : desc === null ? desc = Object.getOwnPropertyDescriptor(target, key) : desc, d;
    if (typeof Reflect === "object" && typeof Reflect.decorate === "function") r = Reflect.decorate(decorators, target, key, desc);
    else for (var i = decorators.length - 1; i >= 0; i--) if (d = decorators[i]) r = (c < 3 ? d(r) : c > 3 ? d(target, key, r) : d(target, key)) || r;
    return c > 3 && r && Object.defineProperty(target, key, r), r;
};
var __metadata = (this && this.__metadata) || function (k, v) {
    if (typeof Reflect === "object" && typeof Reflect.metadata === "function") return Reflect.metadata(k, v);
};
define(["require", "exports", "aurelia-framework", "./../models.generated"], function (require, exports, aurelia_framework_1, Models) {
    "use strict";
    var Sorter = (function () {
        function Sorter(element) {
            var _this = this;
            this.member = '';
            this.text = '';
            this.gridState = { sortMember: '', sortDirection: '' };
            this.setup = function () {
                _this.currentSort = _this.gridState.sortDirection + _this.gridState.sortMember;
                _this.isCurrentSortAsc = _this.checkIfCurrentSortAsc(_this.member);
                _this.isCurrentSortDesc = _this.checkIfCurrentSortDesc(_this.member);
            };
            this.checkIfCurrentSortAsc = function (member) {
                //Don't triple the == unless you can figure out how to check for undefined
                if (_this.gridState === null || _this.gridState.sortMember == null || _this.gridState.sortDirection == null)
                    return false;
                return _this.gridState.sortMember.toUpperCase() === member.toUpperCase()
                    && _this.gridState.sortDirection.toUpperCase() === "ASC";
            };
            this.checkIfCurrentSortDesc = function (member) {
                //Don't triple the == unless you can figure out how to check for undefined
                if (_this.gridState === null || _this.gridState.sortMember == null || _this.gridState.sortDirection == null)
                    return false;
                return _this.gridState.sortMember.toUpperCase() === member.toUpperCase()
                    && _this.gridState.sortDirection.toUpperCase() === "DESC";
            };
            this.sort = function (member) {
                _this.setup();
                if (member.toUpperCase() === _this.gridState.sortMember.toUpperCase())
                    _this.gridState.sortDirection = _this.gridState.sortDirection === "ASC" ? "DESC" : "ASC";
                else
                    _this.gridState.sortDirection = "ASC";
                _this.gridState.sortMember = member;
                var e = new CustomEvent('change', {
                    bubbles: true
                });
                _this.element.dispatchEvent(e);
            };
            this.element = element;
            this.currentSortKey = '';
        }
        Sorter.prototype.gridStateChanged = function (newValue, oldValue) {
            if (newValue != null) {
                this.setup();
            }
        };
        return Sorter;
    }());
    __decorate([
        aurelia_framework_1.bindable,
        __metadata("design:type", String)
    ], Sorter.prototype, "member", void 0);
    __decorate([
        aurelia_framework_1.bindable,
        __metadata("design:type", String)
    ], Sorter.prototype, "text", void 0);
    __decorate([
        aurelia_framework_1.bindable,
        __metadata("design:type", Object)
    ], Sorter.prototype, "gridState", void 0);
    Sorter = __decorate([
        aurelia_framework_1.customElement('sorter'),
        aurelia_framework_1.inject(Element),
        __metadata("design:paramtypes", [Object])
    ], Sorter);
    exports.Sorter = Sorter;
});
//# sourceMappingURL=data:application/json;base64,eyJ2ZXJzaW9uIjozLCJmaWxlIjoic29ydGVyLmpzIiwic291cmNlUm9vdCI6IiIsInNvdXJjZXMiOlsic29ydGVyLnRzIl0sIm5hbWVzIjpbXSwibWFwcGluZ3MiOiI7Ozs7Ozs7Ozs7O0lBS0EsSUFBYSxNQUFNO1FBS2hCLGdCQUFZLE9BQU87WUFBbkIsaUJBR0U7WUFQUyxXQUFNLEdBQVcsRUFBRSxDQUFDO1lBQ3BCLFNBQUksR0FBVyxFQUFFLENBQUM7WUFDbEIsY0FBUyxHQUFzQixFQUFFLFVBQVUsRUFBRSxFQUFFLEVBQUUsYUFBYSxFQUFFLEVBQUUsRUFBQyxDQUFDO1lBYzlFLFVBQUssR0FBRztnQkFDSixLQUFJLENBQUMsV0FBVyxHQUFHLEtBQUksQ0FBQyxTQUFTLENBQUMsYUFBYSxHQUFHLEtBQUksQ0FBQyxTQUFTLENBQUMsVUFBVSxDQUFDO2dCQUM1RSxLQUFJLENBQUMsZ0JBQWdCLEdBQUcsS0FBSSxDQUFDLHFCQUFxQixDQUFDLEtBQUksQ0FBQyxNQUFNLENBQUMsQ0FBQztnQkFDaEUsS0FBSSxDQUFDLGlCQUFpQixHQUFHLEtBQUksQ0FBQyxzQkFBc0IsQ0FBQyxLQUFJLENBQUMsTUFBTSxDQUFDLENBQUM7WUFDdEUsQ0FBQyxDQUFBO1lBRUQsMEJBQXFCLEdBQUcsVUFBQyxNQUFNO2dCQUMzQiwwRUFBMEU7Z0JBQzFFLEVBQUUsQ0FBQyxDQUFDLEtBQUksQ0FBQyxTQUFTLEtBQUssSUFBSSxJQUFJLEtBQUksQ0FBQyxTQUFTLENBQUMsVUFBVSxJQUFJLElBQUksSUFBSyxLQUFJLENBQUMsU0FBUyxDQUFDLGFBQWEsSUFBSSxJQUFJLENBQUM7b0JBQ3RHLE1BQU0sQ0FBQyxLQUFLLENBQUM7Z0JBQ2pCLE1BQU0sQ0FBQyxLQUFJLENBQUMsU0FBUyxDQUFDLFVBQVUsQ0FBQyxXQUFXLEVBQUUsS0FBSyxNQUFNLENBQUMsV0FBVyxFQUFFO3VCQUNoRSxLQUFJLENBQUMsU0FBUyxDQUFDLGFBQWEsQ0FBQyxXQUFXLEVBQUUsS0FBSyxLQUFLLENBQUM7WUFDaEUsQ0FBQyxDQUFBO1lBRUQsMkJBQXNCLEdBQUcsVUFBQyxNQUFNO2dCQUM1QiwwRUFBMEU7Z0JBQzFFLEVBQUUsQ0FBQyxDQUFDLEtBQUksQ0FBQyxTQUFTLEtBQUssSUFBSSxJQUFJLEtBQUksQ0FBQyxTQUFTLENBQUMsVUFBVSxJQUFJLElBQUksSUFBSSxLQUFJLENBQUMsU0FBUyxDQUFDLGFBQWEsSUFBSSxJQUFJLENBQUM7b0JBQ3JHLE1BQU0sQ0FBQyxLQUFLLENBQUM7Z0JBQ2pCLE1BQU0sQ0FBQyxLQUFJLENBQUMsU0FBUyxDQUFDLFVBQVUsQ0FBQyxXQUFXLEVBQUUsS0FBSyxNQUFNLENBQUMsV0FBVyxFQUFFO3VCQUNoRSxLQUFJLENBQUMsU0FBUyxDQUFDLGFBQWEsQ0FBQyxXQUFXLEVBQUUsS0FBSyxNQUFNLENBQUM7WUFDakUsQ0FBQyxDQUFBO1lBRUQsU0FBSSxHQUFHLFVBQUMsTUFBTTtnQkFDVixLQUFJLENBQUMsS0FBSyxFQUFFLENBQUM7Z0JBQ2IsRUFBRSxDQUFDLENBQUMsTUFBTSxDQUFDLFdBQVcsRUFBRSxLQUFLLEtBQUksQ0FBQyxTQUFTLENBQUMsVUFBVSxDQUFDLFdBQVcsRUFBRSxDQUFDO29CQUNqRSxLQUFJLENBQUMsU0FBUyxDQUFDLGFBQWEsR0FBRyxLQUFJLENBQUMsU0FBUyxDQUFDLGFBQWEsS0FBSyxLQUFLLEdBQUcsTUFBTSxHQUFHLEtBQUssQ0FBQztnQkFDM0YsSUFBSTtvQkFDQSxLQUFJLENBQUMsU0FBUyxDQUFDLGFBQWEsR0FBRyxLQUFLLENBQUM7Z0JBQ3pDLEtBQUksQ0FBQyxTQUFTLENBQUMsVUFBVSxHQUFHLE1BQU0sQ0FBQztnQkFDbkMsSUFBSSxDQUFDLEdBQUcsSUFBSSxXQUFXLENBQUMsUUFBUSxFQUFFO29CQUM5QixPQUFPLEVBQUUsSUFBSTtpQkFDaEIsQ0FBQyxDQUFDO2dCQUNILEtBQUksQ0FBQyxPQUFPLENBQUMsYUFBYSxDQUFDLENBQUMsQ0FBQyxDQUFDO1lBQ2xDLENBQUMsQ0FBQTtZQTVDRyxJQUFJLENBQUMsT0FBTyxHQUFHLE9BQU8sQ0FBQztZQUN2QixJQUFJLENBQUMsY0FBYyxHQUFHLEVBQUUsQ0FBQztRQUM3QixDQUFDO1FBRU0saUNBQWdCLEdBQXZCLFVBQXdCLFFBQVEsRUFBRSxRQUFRO1lBQ3RDLEVBQUUsQ0FBQyxDQUFDLFFBQVEsSUFBSSxJQUFJLENBQUMsQ0FBQyxDQUFDO2dCQUNuQixJQUFJLENBQUMsS0FBSyxFQUFFLENBQUM7WUFFakIsQ0FBQztRQUNMLENBQUM7UUEwQ0wsYUFBQztJQUFELENBQUMsQUF6REQsSUF5REM7SUF4RGE7UUFBVCw0QkFBUTs7MENBQXFCO0lBQ3BCO1FBQVQsNEJBQVE7O3dDQUFtQjtJQUNsQjtRQUFULDRCQUFROzs2Q0FBcUU7SUFIckUsTUFBTTtRQUZsQixpQ0FBYSxDQUFDLFFBQVEsQ0FBQztRQUN2QiwwQkFBTSxDQUFDLE9BQU8sQ0FBQzs7T0FDSCxNQUFNLENBeURsQjtJQXpEWSx3QkFBTSJ9