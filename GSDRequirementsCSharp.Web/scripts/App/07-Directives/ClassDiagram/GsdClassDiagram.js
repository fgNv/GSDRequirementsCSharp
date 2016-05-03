var Directives;
(function (Directives) {
    var app = angular.module(GSDRequirements.angularModuleName);
    var GsdClassDiagram = (function () {
        function GsdClassDiagram() {
            var _this = this;
            this.scope = {
                'classDiagram': '=classDiagram',
                'afterSave': '=afterSave',
                'currentClass': '=currentClass',
                'editingRelations': '=editingRelations'
            };
            this.controller = ['$timeout', '$scope', 'PackageResource', 'ClassDiagramResource',
                '$q',
                function ($timeout, $scope, PackageResource, ClassDiagramResource, $q) {
                    var graph = null;
                    var paper = null;
                    $scope.pendingRequests = 0;
                    $scope.currentClass = null;
                    $scope.editingRelations = false;
                    $scope.selectedClass = null;
                    $scope.relationsOnEdit = [];
                    $scope.placeholder = '';
                    _this.LoadPackagesOptions(PackageResource, $scope);
                    $scope.utility = {};
                    $scope.utility.contentContainsLocale =
                        function (i) { return $scope.content &&
                            $scope.content[i] &&
                            $scope.content[i].name; };
                    $scope.editRelations = function () {
                        window.location.href = "#/diagram/relations";
                        $scope.editingRelations = true;
                        $scope.relationsOnEdit = [];
                        _.each($scope.classDiagram.relations, function (relation) {
                            var clone = {};
                            for (var property in relation) {
                                clone[property] = relation[property];
                            }
                            $scope.relationsOnEdit.push(clone);
                        });
                    };
                    $scope.getClassOptions = function (relation) {
                        return $scope.classDiagram.classes;
                    };
                    $scope.backToList = function () {
                        $scope.classDiagram = null;
                        window.location.href = "#";
                    };
                    $scope.isDiagramVisible = function () {
                        return !$scope.currentClass && !$scope.editingRelations;
                    };
                    $scope.addRelation = function () {
                        $scope.relationsOnEdit.push({});
                    };
                    $scope.removeRelation = function (relation) {
                        $scope.relationsOnEdit = _.filter($scope.relationsOnEdit, function (r) { return r != relation; });
                    };
                    function removeRelationFromDiagram(relation) {
                        $scope.classDiagram.relations = _.filter($scope.classDiagram.relations, function (r) { return r != relation; });
                        relation.cell.remove();
                    }
                    function redrawRelations() {
                        _.each($scope.classDiagram.relations, function (relation) {
                            var cell = Views.ClassDiagram.buildRelation(relation);
                            if (!cell)
                                return;
                            relation.cell = cell;
                            $timeout(function () { graph.addCell(cell); });
                        });
                    }
                    $scope.saveRelations = function () {
                        if (!graph)
                            return;
                        _.each($scope.relations, function (relation) {
                            if (relation.cell != null) {
                                removeRelationFromDiagram(relation);
                            }
                        });
                        $scope.relations = [];
                        _.each($scope.relationsOnEdit, function (relation) {
                            var cell = Views.ClassDiagram.buildRelation(relation);
                            if (!cell)
                                return;
                            relation.cell = cell;
                            $scope.classDiagram.relations.push(relation);
                            $timeout(function () { graph.addCell(cell); });
                        });
                        $scope.relationsOnEdit = [];
                        $scope.backToDiagram();
                    };
                    $scope.selectClass = function (id) {
                        var classToBeSelected = _.find($scope.classDiagram.classes, function (c) { return c.cell.id == id; });
                        if (!classToBeSelected)
                            return;
                        $scope.selectedClass = classToBeSelected;
                        $scope.$digest();
                    };
                    function removeClass(classEntity) {
                        $scope.classDiagram.classes = _.filter($scope.classDiagram.classes, function (c) { return c != classEntity; });
                        classEntity.cell.remove();
                    }
                    $scope.removeSelectedClass = function () {
                        removeClass($scope.selectedClass);
                        $scope.selectedClass = null;
                    };
                    $scope.editSelectedClass = function () {
                        window.location.href = "#/diagram/form";
                        $scope.currentClass = $scope.selectedClass;
                    };
                    $scope.backToDiagram = function () {
                        $scope.currentClass = null;
                        $scope.editingRelations = false;
                        window.location.href = "#/diagram";
                    };
                    $scope.save = function () {
                        $scope.pendingRequests++;
                        var contents = _.chain($scope.content)
                            .filter(function (i) { return i.name; })
                            .value();
                        $scope.classDiagram.contents = contents;
                        var promise = $scope.classDiagram.id ?
                            ClassDiagramResource.update($scope.classDiagram).$promise :
                            ClassDiagramResource.save($scope.classDiagram).$promise;
                        promise.then(function () {
                            Notification.notifySuccess(Sentences.classDiagramSavedSuccessfully);
                            if ($scope.afterSave) {
                                $scope.afterSave();
                            }
                            window.location.href = "#";
                            $scope.classDiagram = null;
                        })
                            .catch(function (err) {
                            Notification.notifyError(Sentences.errorSavingClassDiagram, err.data.messages);
                        })
                            .finally(function () {
                            $scope.pendingRequests--;
                        });
                    };
                    $scope.$watch('classDiagram', function (newValue, oldValue) {
                        if (graph) {
                            graph.clear();
                            paper.remove();
                        }
                        if (!newValue) {
                            graph = null;
                            paper = null;
                            return;
                        }
                        _this.initializeContentData($scope, newValue.contents);
                        var paperDefer = $q.defer();
                        var classesDefer = $q.defer();
                        var drawClasses = function () {
                            $timeout(function () {
                                _.each(newValue.classes, function (c) {
                                    var cell = Views.ClassDiagram.buildClass(c);
                                    c.cell = cell;
                                    graph.addCell(cell);
                                });
                                classesDefer.resolve();
                            });
                            return classesDefer.promise;
                        };
                        var drawRelations = function () {
                            $timeout(function () {
                                _.each(newValue.relations, function (r) {
                                    var cell = Views.ClassDiagram.buildRelation(r);
                                    r.cell = cell;
                                    graph.addCell(cell);
                                });
                            });
                        };
                        var drawPaper = function () {
                            $timeout(function () {
                                var cellClickCallback = function (cellView) {
                                    $scope.selectClass(cellView.model.id);
                                };
                                var result = Views.ClassDiagram.startClassDiagram(cellClickCallback);
                                graph = result.graph;
                                paper = result.paper;
                                paperDefer.resolve();
                            });
                            return paperDefer.promise;
                        };
                        drawPaper()
                            .then(drawClasses())
                            .then(drawRelations());
                    });
                    $scope.classTypeOptions = Globals.enumerateEnum(Models.ClassType);
                    $scope.relationTypeOptions = Globals.enumerateEnum(Models.RelationType);
                    $scope.visibilityOptions = Globals.enumerateEnum(Models.Visibility);
                    $scope.newClass = function () {
                        window.location.href = "#/diagram/form";
                        $scope.selectedClass = null;
                        $scope.currentClass = new Models.ClassData();
                    };
                    $scope.saveClass = function (data) {
                        if (!graph)
                            return;
                        if (data.cell != null) {
                            removeClass(data);
                        }
                        var cell = Views.ClassDiagram.buildClass(data);
                        if (!cell)
                            return;
                        $scope.currentClass.cell = cell;
                        $scope.classDiagram.classes.push($scope.currentClass);
                        $scope.currentClass = null;
                        $timeout(function () {
                            graph.addCell(cell);
                            redrawRelations();
                        });
                        $scope.selectedClass = null;
                    };
                }];
            this.templateUrl = GSDRequirements.baseUrl + 'classDiagram/management';
        }
        GsdClassDiagram.prototype.LoadPackagesOptions = function (packageResource, $scope) {
            $scope.pendingRequests++;
            packageResource.query()
                .$promise
                .then(function (response) {
                $scope.packagesOptions = _.map(response, function (r) { return new Models.Package(r); });
            })
                .catch(function (err) {
                Notification.notifyError(Sentences.errorLoadingPackages, err.data.messages);
            })
                .finally(function () {
                $scope.pendingRequests--;
            });
        };
        GsdClassDiagram.prototype.initializeContentData = function ($scope, initialData) {
            $scope.contentData = {};
            $scope.contentData.locale = GSDRequirements.currentLocale;
            $scope.locales = _.map(GSDRequirements.localesAvailable, function (l) { return l.name; });
            $scope.content = {};
            _.each(GSDRequirements.localesAvailable, function (l) {
                $scope.content[l.name] = {};
                var previousContent = null;
                if (initialData)
                    previousContent = _.find(initialData, function (d) { return d.locale == l.name; });
                $scope.content[l.name].name = previousContent ? previousContent.name : '';
                $scope.content[l.name].locale = l.name;
            });
        };
        GsdClassDiagram.prototype.definePlaceholder = function ($scope, locale, $q) {
            var deferred = $q.defer();
            if ($scope.contentData.locale == locale) {
                deferred.reject();
                return deferred.promise;
            }
            var content = $scope.content[locale];
            if (!content.name) {
                deferred.reject();
                return deferred.promise;
            }
            $scope.placeholder = content.name;
            deferred.resolve();
            return deferred.promise;
        };
        GsdClassDiagram.Factory = function () {
            return new GsdClassDiagram();
        };
        return GsdClassDiagram;
    })();
    app.directive('gsdClassDiagram', GsdClassDiagram.Factory);
})(Directives || (Directives = {}));
//# sourceMappingURL=GsdClassDiagram.js.map