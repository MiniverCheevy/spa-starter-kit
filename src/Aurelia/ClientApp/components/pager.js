var __decorate = (this && this.__decorate) || function (decorators, target, key, desc) {
    var c = arguments.length, r = c < 3 ? target : desc === null ? desc = Object.getOwnPropertyDescriptor(target, key) : desc, d;
    if (typeof Reflect === "object" && typeof Reflect.decorate === "function") r = Reflect.decorate(decorators, target, key, desc);
    else for (var i = decorators.length - 1; i >= 0; i--) if (d = decorators[i]) r = (c < 3 ? d(r) : c > 3 ? d(target, key, r) : d(target, key)) || r;
    return c > 3 && r && Object.defineProperty(target, key, r), r;
};
var __metadata = (this && this.__metadata) || function (k, v) {
    if (typeof Reflect === "object" && typeof Reflect.metadata === "function") return Reflect.metadata(k, v);
};
define(["require", "exports", "./../models.generated", "aurelia-framework"], function (require, exports, Models, aurelia_framework_1) {
    "use strict";
    var Pager = (function () {
        function Pager(element) {
            var _this = this;
            this.resetPagingIfNeeded = function () {
                if (_this.gridState != null && _this.gridState.resetPaging)
                    _this.gridState.pageNumber = 1;
            };
            this.setup = function () {
                if (_this.gridState.totalRecords == undefined) {
                    _this.hasRecords = false;
                    return;
                }
                _this.hasRecords = _this.gridState.totalRecords !== 0;
                _this.totalPages = Math.ceil(_this.gridState.totalRecords / _this.gridState.pageSize);
                var blocks = [];
                var blockNumber = Math.ceil(_this.gridState.pageNumber / _this.blockSize) - 1;
                var blockStart = blockNumber * _this.blockSize;
                var min = blockStart + 1;
                var max = blockStart + _this.blockSize;
                for (var i = 1; i < _this.blockSize + 1; i++) {
                    if (blockStart + i < _this.totalPages + 1)
                        blocks.push({ page: blockStart + i, isActive: blockStart + i === _this.gridState.pageNumber });
                }
                _this.pageBlock = blocks;
                var pageNumber = _this.gridState.pageNumber;
                _this.prevBlockPage = min - 1;
                _this.nextBlockPage = max + 1;
                _this.isLastBlock = _this.pageBlock.length > 0 && max >= _this.totalPages;
                _this.isFirstBlock = min === 1;
                _this.isFirstPage = pageNumber === 1;
                _this.isLastPage = _this.totalPages === pageNumber;
                var startRow = ((pageNumber - 1) * _this.gridState.pageSize) + 1;
                var endRow = startRow + _this.gridState.pageSize - 1;
                if (_this.isLastPage)
                    endRow = _this.gridState.totalRecords;
                _this.recordsVerbiage = 'Showing ' + startRow + ' to ' + endRow + ' of ' + _this.gridState.totalRecords;
            };
            this.update = function () {
                _this.page(_this.gridState.pageNumber);
            };
            this.page = function (number) {
                _this.gridState.pageNumber = number;
                var e = new CustomEvent('change', {
                    bubbles: true
                });
                _this.element.dispatchEvent(e);
            };
            this.firstPage = function () {
                return _this.page(1);
            };
            this.prevBlock = function () {
                return _this.page(_this.prevBlockPage);
            };
            this.prevPage = function () {
                return _this.page(_this.gridState.pageNumber - 1);
            };
            this.nextPage = function () {
                return _this.page(_this.gridState.pageNumber + 1);
            };
            this.nextBlock = function () {
                return _this.page(_this.nextBlockPage);
            };
            this.lastPage = function () {
                return _this.page(_this.totalPages);
            };
            this.isLastBlock = false;
            this.isFirstBlock = false;
            this.isFirstPage = false;
            this.isLastPage = false;
            this.hasRecords = false;
            this.totalPages = 0;
            this.blockSize = 10;
            this.element = element;
            this.pageBlock = [];
            this.isLastBlock = false;
            this.isFirstBlock = false;
            this.isFirstPage = false;
            this.isLastPage = false;
            this.hasRecords = false;
            this.totalPages = 0;
            this.blockSize = 10;
            this.hasRecords = null;
            this.selectMatcher = function (left, right) {
                return left.value === right.value;
            };
        }
        Pager.prototype.gridStateChanged = function (newValue, oldValue) {
            if (newValue != null) {
                this.setup();
            }
        };
        return Pager;
    }());
    __decorate([
        aurelia_framework_1.bindable,
        __metadata("design:type", Object)
    ], Pager.prototype, "gridState", void 0);
    Pager = __decorate([
        aurelia_framework_1.customElement('pager'),
        aurelia_framework_1.inject(Element),
        __metadata("design:paramtypes", [Object])
    ], Pager);
    exports.Pager = Pager;
});
//# sourceMappingURL=data:application/json;base64,eyJ2ZXJzaW9uIjozLCJmaWxlIjoicGFnZXIuanMiLCJzb3VyY2VSb290IjoiIiwic291cmNlcyI6WyJwYWdlci50cyJdLCJuYW1lcyI6W10sIm1hcHBpbmdzIjoiOzs7Ozs7Ozs7OztJQU1BLElBQWEsS0FBSztRQUdkLGVBQVksT0FBTztZQUFuQixpQkFjQztZQU9NLHdCQUFtQixHQUFHO2dCQUN6QixFQUFFLENBQUMsQ0FBQyxLQUFJLENBQUMsU0FBUyxJQUFJLElBQUksSUFBSSxLQUFJLENBQUMsU0FBUyxDQUFDLFdBQVcsQ0FBQztvQkFDckQsS0FBSSxDQUFDLFNBQVMsQ0FBQyxVQUFVLEdBQUcsQ0FBQyxDQUFDO1lBQ3RDLENBQUMsQ0FBQztZQUNLLFVBQUssR0FBRztnQkFDWCxFQUFFLENBQUMsQ0FBQyxLQUFJLENBQUMsU0FBUyxDQUFDLFlBQVksSUFBSSxTQUFTLENBQUMsQ0FBQyxDQUFDO29CQUMzQyxLQUFJLENBQUMsVUFBVSxHQUFHLEtBQUssQ0FBQztvQkFDeEIsTUFBTSxDQUFDO2dCQUNYLENBQUM7Z0JBQ0QsS0FBSSxDQUFDLFVBQVUsR0FBRyxLQUFJLENBQUMsU0FBUyxDQUFDLFlBQVksS0FBSyxDQUFDLENBQUM7Z0JBRXBELEtBQUksQ0FBQyxVQUFVLEdBQUcsSUFBSSxDQUFDLElBQUksQ0FBQyxLQUFJLENBQUMsU0FBUyxDQUFDLFlBQVksR0FBRyxLQUFJLENBQUMsU0FBUyxDQUFDLFFBQVEsQ0FBQyxDQUFDO2dCQUNuRixJQUFJLE1BQU0sR0FBRyxFQUFFLENBQUM7Z0JBQ2hCLElBQUksV0FBVyxHQUFHLElBQUksQ0FBQyxJQUFJLENBQUMsS0FBSSxDQUFDLFNBQVMsQ0FBQyxVQUFVLEdBQUcsS0FBSSxDQUFDLFNBQVMsQ0FBQyxHQUFHLENBQUMsQ0FBQztnQkFDNUUsSUFBSSxVQUFVLEdBQUcsV0FBVyxHQUFHLEtBQUksQ0FBQyxTQUFTLENBQUM7Z0JBQzlDLElBQUksR0FBRyxHQUFHLFVBQVUsR0FBRyxDQUFDLENBQUM7Z0JBQ3pCLElBQUksR0FBRyxHQUFHLFVBQVUsR0FB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