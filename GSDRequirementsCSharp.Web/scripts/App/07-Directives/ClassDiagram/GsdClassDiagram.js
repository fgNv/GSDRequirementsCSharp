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
                function ($timeout, $scope, PackageResource, ClassDiagramResource) {
                    var graph = null;
                    var paper = null;
                    $scope.pendingRequests = 0;
                    $scope.currentClass = null;
                    $scope.editingRelations = false;
                    $scope.selectedClass = null;
                    $scope.classes = [];
                    $scope.relations = [];
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
                        _.each($scope.relations, function (relation) {
                            var clone = {};
                            for (var property in relation) {
                                clone[property] = relation[property];
                            }
                            $scope.relationsOnEdit.push(clone);
                        });
                    };
                    $scope.getClassOptions = function (relation) {
                        return $scope.classes;
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
                        $scope.relations = _.filter($scope.relations, function (r) { return r != relation; });
                        relation.cell.remove();
                    }
                    $scope.saveRelations = function () {
                        if (!graph)
                            return;
                        $scope.relations = [];
                        _.each($scope.relationsOnEdit, function (relation) {
                            if (relation.cell != null) {
                                removeRelationFromDiagram(relation);
                            }
                            var cell = Views.buildRelation(relation);
                            if (!cell)
                                return;
                            relation.cell = cell;
                            $scope.relations.push(relation);
                            $timeout(function () { graph.addCell(cell); });
                        });
                        $scope.relationsOnEdit = [];
                        $scope.backToDiagram();
                    };
                    $scope.selectClass = function (id) {
                        var classToBeSelected = _.find($scope.classes, function (c) { return c.cell.id == id; });
                        if (!classToBeSelected)
                            return;
                        $scope.selectedClass = classToBeSelected;
                        $scope.$digest();
                    };
                    function removeClass(classEntity) {
                        $scope.classes = _.filter($scope.classes, function (c) { return c != classEntity; });
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
                        $scope.classDiagram.classes = $scope.classes;
                        $scope.classDiagram.relations = $scope.relations;
                        ClassDiagramResource.save($scope.classDiagram)
                            .$promise
                            .then(function () {
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
                        _this.initializeContentData($scope);
                        $scope.classes = [];
                        $scope.relations = [];
                        if (graph) {
                            graph.clear();
                            paper.remove();
                        }
                        if (!newValue) {
                            graph = null;
                            paper = null;
                            return;
                        }
                        $timeout(function () {
                            var result = Views.startClassDiagram(function (cellView) {
                                $scope.selectClass(cellView.model.id);
                            });
                            graph = result.graph;
                            paper = result.paper;
                        });
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
                        var cell = Views.buildClass(data);
                        if (!cell)
                            return;
                        $scope.currentClass.cell = cell;
                        $scope.classes.push($scope.currentClass);
                        $scope.currentClass = null;
                        $timeout(function () { graph.addCell(cell); });
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
                Notification.notifyError(Sentences.errorLoadingPackages, err.messages);
            })
                .finally(function () {
                $scope.pendingRequests--;
            });
        };
        GsdClassDiagram.prototype.initializeContentData = function ($scope) {
            $scope.contentData = {};
            $scope.contentData.locale = GSDRequirements.currentLocale;
            $scope.locales = _.map(GSDRequirements.localesAvailable, function (l) { return l.name; });
            $scope.content = {};
            _.each(GSDRequirements.localesAvailable, function (l) {
                $scope.content[l.name] = {};
                $scope.content[l.name].name = '';
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