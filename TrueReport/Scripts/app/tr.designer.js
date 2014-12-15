tr.designer = {
    idValue: 0,
    defaultElementX: 0,
    defaultElementY: 0,
    defaultElementWidth: 90,
    defaultElementHeight: 30,
    minElementSize: 20,
    layoutX: 0,
    layoutY: 0,
    layoutWidth: 595,
    layoutHeight: 842,
    
    labelType : 0,
    editType : 1,

    viewModel: {},
    
    init: function () {
        var self = tr.designer;
        self.viewModel = {
            isResizing: ko.observable(false),
            isMoving: ko.observable(false),

            elements: ko.observableArray(),
            selectedElement: ko.observable(null),
            
            dataSourceList: ko.observableArray(),
            selectedDataSource: ko.observable(),
            dataSourceName: ko.observable(),

            createElement: function(x, y, width, height, name, type, bindValue) {
                var newId = self.getId();
                if (name == null)
                    name = self.getName(type);
                var element = {};
                element.realX = ko.observable(x);
                element.realY = ko.observable(y);
                element.realWidth = ko.observable(width);
                element.realHeight = ko.observable(height);

                element.width = ko.computed(function() { return self.calculateSize(element.realX(), element.realWidth(), self.layoutX, self.layoutWidth, self.viewModel.isResizing()); });
                element.height = ko.computed(function() { return self.calculateSize(element.realY(), element.realHeight(), self.layoutY, self.layoutHeight, self.viewModel.isResizing()); });
                element.x = ko.computed(function() { return self.calculateCoordinate(element.realX(), element.width(), self.layoutX, self.layoutWidth, self.viewModel.isMoving()); });
                element.y = ko.computed(function() { return self.calculateCoordinate(element.realY(), element.height(), self.layoutY, self.layoutHeight, self.viewModel.isMoving()); });

                element.name = ko.observable(name);
                element.type = ko.observable(type);
                element.bindValue = ko.observable(bindValue);
                element.id = ko.observable(newId);

                element.isSelected = ko.computed(function() {
                    return (self.viewModel.selectedElement() != null && self.viewModel.selectedElement().id() == element.id());
                });
                element.classType = ko.computed(function() {
                    var result = "element ";
                    switch (type) {
                        case tr.designer.editType:
                            result += "editElement";
                            if (element.isSelected())
                                result += " selectedEditElement";
                            break;
                        case tr.designer.labelType:
                            result += "labelElement";
                            if (element.isSelected())
                                result += " selectedLabelElement";
                            break;
                    }
                    return result;
                });

                return element;
            },
        };
        
        //creating, adding removing elements
        self.getId = function () {
            return self.idValue++;
        };
        
        self.getName = function (type) {
            var i = 1;
            while (self.isNameExist(type + i)) {
                i++;
            }
            return type + i;
        };

        self.isNameExist = function (name) {
            var result = false;
            self.viewModel.elements().forEach(function (el) {
                if (el.name() == name)
                    result = true;
            });
            return result;
        };
        
        self.viewModel.addLabel = function() {
            self.viewModel.elements.push(self.viewModel.createElement(self.defaultElementX, self.defaultElementY, self.defaultElementWidth, self.defaultElementHeight, null, self.labelType));
        };
        self.viewModel.addEdit = function() {
            self.viewModel.elements.push(self.viewModel.createElement(self.defaultElementX, self.defaultElementY, self.defaultElementWidth, self.defaultElementHeight, null, self.editType));
        };
        self.viewModel.removeElement = function() {
            if (self.viewModel.selectedElement())
                self.viewModel.elements.remove(self.viewModel.selectedElement());
            self.viewModel.selectedElement(null);
        };
        
        self.viewModel.removeAllElements = function() {
            tr.confirm(null, function() {
                self.viewModel.elements.removeAll();
                self.viewModel.selectedElement(null);
            });
        };
        
        //printing
        self.viewModel.printReport = function() {
            tr.ajaxProxy.printReport(ko.toJSON(self.viewModel), self.onPrint);
        },
        
        self.onPrint = function (reportName) {
            window.open(tr.urls.openReport + reportName);
        };
        
        //Element selecting, moving and resizing
        self.viewModel.selectElement = function (element) {
            self.viewModel.selectedElement(element);
        };
        self.viewModel.resetSelected = function () {
            self.viewModel.selectedElement(null);
        };
        self.resizeElementEnd = function (id) {
            self.viewModel.elements().forEach(function (el) {
                if (el.id() == id) {
                    el.realWidth(el.width());
                    el.realHeight(el.height());
                }
            });
        };
        self.isMovingPossible = function (coord, size, moving, layoutCoord, layoutSize) {
            return (coord + moving > layoutCoord && coord + size + moving < layoutCoord + layoutSize);
        };
        self.resizeElement = function (id, dx, dy) {
            self.viewModel.elements().forEach(function (el) {
                if (el.id() == id) {
                    el.realWidth(Math.max(el.realWidth() + dx, self.minElementSize));
                    el.realHeight(Math.max(el.realHeight() + dy, self.minElementSize));
                }
            });
        };
        self.moveElement = function (id, dx, dy) {
            self.viewModel.elements().forEach(function (el) {
                if (el.id() == id) {
                    el.realX(el.realX() + dx);
                    el.realY(el.realY() + dy);
                }
            });
        };
        self.moveElementEnd = function (id) {
            self.viewModel.elements().forEach(function (el) {
                if (el.id() == id) {
                    el.realX(el.x());
                    el.realY(el.y());

                }
            });
        };
        self.calculateCoordinate = function(elCoord, elSize, layoutCoord, layoutSize, isMoving) {
            var result = elCoord;
            if (isMoving) {
                if (elCoord < layoutCoord)
                    result = layoutCoord;
                if (elCoord > layoutCoord - elSize + layoutSize)
                    result = layoutCoord - elSize + layoutSize;
            }
            return result;
        };
        self.calculateSize = function(elCoord, elSize, layoutCoord, layoutSize, isResizing) {
            var result = elSize;

            if (isResizing) {
                if (elCoord + elSize >= layoutCoord + layoutSize)
                    result = layoutCoord + layoutSize - elCoord;
            }
            return result;
        };
        
        //Data loading and initialization
        self.viewModel.dataSourceName.subscribe(function (newValue) {
            tr.ajaxProxy.getDataSource(ko.toJSON({ dataSourceName: newValue }), self.updateDataSource);
        });
        self.updateDataSource = function (data) {
            self.viewModel.selectedDataSource(data.Content);
        };
        self.viewModel.saveTemplate = function () {
            tr.confirm(tr.messages.beforeSaveTemplate, function () {
                tr.ajaxProxy.saveTemplate(ko.toJSON(self.viewModel.elements));
            });
        };
        self.viewModel.loadTemplate = function () {
            tr.confirm(tr.messages.beforeLoadTemplate, function () {
                tr.ajaxProxy.loadTemplate(self.updateModel);
            });
        };
        self.viewModel.loadDemoTemplate = function () {
            tr.confirm(tr.messages.beforeLoadTemplate, function () {
                tr.ajaxProxy.loadDemoTemplate(self.updateModel);
            });
        };
        self.onInitialized = function (data) {
            self.viewModel.dataSourceList.removeAll();
            data.Content.Entities.forEach(function (el) {
                self.viewModel.dataSourceList.push({ name: el });
                if (self.viewModel.dataSourceName() == null) {
                    self.viewModel.dataSourceName(el);
                }
            });
        };
        self.updateModel = function (data) {
            self.viewModel.elements.removeAll();
            data.Content.Entities.forEach(function (el) {
                self.viewModel.elements.push(self.viewModel.createElement(el.X, el.Y, el.Width, el.Height, el.Name, el.Type, el.BindValue));
            });
        };

        //interact tools
        self.selectElementById = function (id) {
            self.viewModel.elements().forEach(function (el) {
                if (el.id() == id) {
                    self.viewModel.selectElement(el);
                }
            });
        };

        //Initialization  and view model binding
        tr.ajaxProxy.initReportControls(self.onInitialized);
        ko.applyBindings(self.viewModel);
    },
    interact: function () {
        interact('.element')
            .draggable({
                max: Infinity,
                onstart: function (event) {
                    tr.designer.viewModel.isMoving(true);
                    var id = event.target.getAttribute('id');
                    tr.designer.selectElementById(id);
                },
                onmove: function (event) {
                    var id = event.target.getAttribute('id');
                    tr.designer.moveElement(id, event.dx, event.dy);
                },
                onend: function (event) {
                    var id = event.target.getAttribute('id');
                    tr.designer.moveElementEnd(id);
                    tr.designer.viewModel.isMoving(false);
                },
            })
            .resizable({
                max: Infinity,
                onstart: function(event) {
                    tr.designer.viewModel.isResizing(true);
                    var id = event.target.getAttribute('id');
                    tr.designer.selectElementById(id);
                },
                onmove: function (event) {
                    var id = event.target.getAttribute('id');
                    tr.designer.resizeElement(id, event.dx, event.dy);
                },
                onend: function (event) {
                    var id = event.target.getAttribute('id');
                    tr.designer.resizeElementEnd(id);
                    tr.designer.viewModel.isResizing(false);
                },
            })
            .restrict({
                drag: "parent",
                endOnly: true,
                elementRect: { top: 0, left: 0, bottom: 1, right: 1 }
            });

        interact.maxInteractions(Infinity);

        interact('.drag-zone')
            .on('tap', function() {
                tr.designer.viewModel.resetSelected();
            });
    },
};

$(tr.designer.init);
$(tr.designer.interact);

$(document).ready(function () {
    /* swap open/close side menu icons */
    $('[data-toggle=collapse]').click(function () {
        // toggle icon
        $(this).find("i").toggleClass("glyphicon-chevron-right glyphicon-chevron-down");
    });
});