var Directives;
(function (Directives) {
    var app = angular.module(GSDRequirements.angularModuleName);
    var GsdUseCaseDiagram = (function () {
        function GsdUseCaseDiagram() {
            var _this = this;
            this.scope = {
                'useCaseDiagram': '=useCaseDiagram',
                'afterSave': '=afterSave',
                'currentUseCase': '=currentUseCase',
                'currentActor': '=currentActor',
                'editingRelations': '=editingRelations'
            };
            this.controller = ['$timeout', '$scope', 'PackageResource', 'UseCaseDiagramResource',
                '$q',
                function ($timeout, $scope, PackageResource, UseCaseDiagramResource, $q) {
                    var graph = null;
                    var paper = null;
                    $scope.pendingRequests = 0;
                    $scope.currentActor = null;
                    $scope.currentUseCase = null;
                    $scope.editingRelations = false;
                    $scope.selectedActor = null;
                    $scope.selectedUseCase = null;
                    $scope.relationsOnEdit = [];
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
                        _.each($scope.useCaseDiagram.relations, function (relation) {
                            var clone = {};
                            for (var property in relation) {
                                clone[property] = relation[property];
                            }
                            $scope.relationsOnEdit.push(clone);
                        });
                    };
                    $scope.getRelationEntityOptions = function (relation) {
                        return _.union($scope.useCaseDiagram.useCases, $scope.useCaseDiagram.actors);
                    };
                    $scope.backToList = function () {
                        $scope.useCaseDiagram = null;
                        window.location.href = "#";
                    };
                    $scope.isDiagramVisible = function () {
                        return !$scope.currentUseCase &&
                            !!$scope.currentActor && !$scope.editingRelations;
                    };
                    $scope.addRelation = function () {
                        $scope.relationsOnEdit.push({});
                    };
                    $scope.removeRelation = function (relation) {
                        $scope.relationsOnEdit = _.filter($scope.relationsOnEdit, function (r) { return r != relation; });
                    };
                    function removeRelationFromDiagram(relation) {
                        $scope.useCaseDiagram.relations = _.filter($scope.useCaseDiagram.relations, function (r) { return r != relation; });
                        relation.cell.remove();
                    }
                    function redrawRelations() {
                        _.each($scope.useCaseDiagram.relations, function (relation) {
                            var cell = Views.UseCaseDiagram.buildRelation(relation);
                            if (!cell)
                                return;
                            relation.cell = cell;
                            $timeout(function () { graph.addCell(cell); });
                        });
                    }
                    $scope.saveRelations = function () {
                        if (!graph)
                            return;
                        _.each($scope.useCaseDiagram.relations, function (relation) {
                            if (relation.cell != null) {
                                removeRelationFromDiagram(relation);
                            }
                        });
                        $scope.useCaseDiagram.relations = [];
                        _.each($scope.relationsOnEdit, function (relation) {
                            var cell = Views.UseCaseDiagram.buildRelation(relation);
                            if (!cell)
                                return;
                            relation.cell = cell;
                            $scope.useCaseDiagram.relations.push(relation);
                            $timeout(function () { graph.addCell(cell); });
                        });
                        $scope.relationsOnEdit = [];
                        $scope.backToDiagram();
                    };
                    $scope.selectEntity = function (id) {
                        if (_.any($scope.useCaseDiagram, function (uc) { return uc.id == id; }))
                            $scope.selectedUseCase(id);
                        else
                            $scope.selectedActor(id);
                    };
                    $scope.selectActor = function (id) {
                        var actorToBeSelected = _.find($scope.useCaseDiagram.actors, function (c) { return c.cell.id == id; });
                        if (!actorToBeSelected)
                            return;
                        $scope.selectedActor = actorToBeSelected;
                        $scope.$digest();
                    };
                    $scope.selectUseCase = function (id) {
                        var useCaseToBeSelected = _.find($scope.useCaseDiagram.useCases, function (c) { return c.cell.id == id; });
                        if (!useCaseToBeSelected)
                            return;
                        $scope.selectedUseCase = useCaseToBeSelected;
                        $scope.$digest();
                    };
                    function removeUseCase(useCase) {
                        $scope.useCaseDiagram.useCases = _.filter($scope.useCaseDiagram.useCases, function (c) { return c != useCase; });
                        useCase.cell.remove();
                    }
                    function removeActor(actor) {
                        $scope.useCaseDiagram.actors = _.filter($scope.useCaseDiagram.actors, function (c) { return c != actor; });
                        actor.cell.remove();
                    }
                    $scope.removeSelectedActor = function () {
                        removeActor($scope.selectedActor);
                        $scope.selectedActor = null;
                    };
                    $scope.removeSelectedUseCase = function () {
                        removeUseCase($scope.selectedUseCase);
                        $scope.selectedUseCase = null;
                    };
                    $scope.editSelectedUseCase = function () {
                        window.location.href = "#/diagram/useCaseForm";
                        $scope.currentUseCase = $scope.selectedUseCase;
                    };
                    $scope.editSelectedActor = function () {
                        window.location.href = "#/diagram/actorForm";
                        $scope.currentActor = $scope.selectedActor;
                    };
                    $scope.backToDiagram = function () {
                        $scope.currentUseCase = null;
                        $scope.currentActor = null;
                        $scope.editingRelations = false;
                        window.location.href = "#/diagram";
                    };
                    $scope.save = function () {
                        $scope.pendingRequests++;
                        var contents = _.chain($scope.content)
                            .filter(function (i) { return i.name; })
                            .value();
                        $scope.useCaseDiagram.contents = contents;
                        var promise = $scope.useCaseDiagram.id ?
                            UseCaseDiagramResource.update($scope.useCaseDiagram).$promise :
                            UseCaseDiagramResource.save($scope.useCaseDiagram).$promise;
                        promise.then(function () {
                            Notification.notifySuccess(Sentences.classDiagramSavedSuccessfully);
                            if ($scope.afterSave) {
                                $scope.afterSave();
                            }
                            window.location.href = "#";
                            $scope.useCaseDiagram = null;
                        })
                            .catch(function (err) {
                            Notification.notifyError(Sentences.errorSavingClassDiagram, err.data.messages);
                        })
                            .finally(function () {
                            $scope.pendingRequests--;
                        });
                    };
                    $scope.$watch('useCaseDiagram', function (newValue) {
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
                        var entitiesDefer = $q.defer();
                        var drawEntities = function () {
                            $timeout(function () {
                                _.each(newValue.actors, function (a) {
                                    var cell = Views.UseCaseDiagram.buildActor(a);
                                    a.cell = cell;
                                    graph.addCell(cell);
                                });
                                _.each(newValue.useCases, function (uc) {
                                    var cell = Views.UseCaseDiagram.buildUseCase(uc);
                                    uc.cell = cell;
                                    graph.addCell(cell);
                                });
                                entitiesDefer.resolve();
                            });
                            return entitiesDefer.promise;
                        };
                        var drawRelations = function () {
                            $timeout(function () {
                                _.each(newValue.relations, function (r) {
                                    var cell = Views.UseCaseDiagram.buildRelation(r);
                                    r.cell = cell;
                                    graph.addCell(cell);
                                });
                            });
                        };
                        var drawPaper = function () {
                            $timeout(function () {
                                var cellClickCallback = function (cellView) {
                                    $scope.selectEntity(cellView.model.id);
                                };
                                var result = Views.UseCaseDiagram.startDiagram(cellClickCallback);
                                graph = result.graph;
                                paper = result.paper;
                                paperDefer.resolve();
                            });
                            return paperDefer.promise;
                        };
                        drawPaper()
                            .then(drawEntities())
                            .then(drawRelations());
                    });
                    $scope.newUseCase = function () {
                        window.location.href = "#/diagram/formUseCase";
                        $scope.selectedUseCase = null;
                        $scope.currentUseCase = new Models.UseCase();
                    };
                    $scope.newActor = function () {
                        window.location.href = "#/diagram/formActor";
                        $scope.selectedActor = null;
                        $scope.currentActor = new Models.Actor();
                    };
                    $scope.saveUseCase = function (data) {
                        if (!graph)
                            return;
                        if (data.cell != null) {
                            removeUseCase(data);
                        }
                        var cell = Views.UseCaseDiagram.buildUseCase(data);
                        if (!cell)
                            return;
                        $scope.currentUseCase.cell = cell;
                        $scope.useCaseDiagram.useCases.push($scope.currentUseCase);
                        $scope.currentUseCase = null;
                        $timeout(function () {
                            graph.addCell(cell);
                            redrawRelations();
                        });
                        $scope.selectedUseCase = null;
                    };
                    $scope.saveActor = function (data) {
                        if (!graph)
                            return;
                        if (data.cell != null) {
                            removeActor(data);
                        }
                        var cell = Views.UseCaseDiagram.buildActor(data);
                        if (!cell)
                            return;
                        $scope.currentActor.cell = cell;
                        $scope.useCaseDiagram.actors.push($scope.currentActor);
                        $scope.currentActor = null;
                        $timeout(function () {
                            graph.addCell(cell);
                            redrawRelations();
                        });
                        $scope.selectedActor = null;
                    };
                }];
            this.templateUrl = GSDRequirements.baseUrl + 'classDiagram/management';
        }
        GsdUseCaseDiagram.prototype.LoadPackagesOptions = function (packageResource, $scope) {
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
        GsdUseCaseDiagram.prototype.initializeContentData = function ($scope, initialData) {
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
        GsdUseCaseDiagram.Factory = function () {
            return new GsdUseCaseDiagram();
        };
        return GsdUseCaseDiagram;
    })();
    app.directive('gsdUseCaseDiagram', GsdUseCaseDiagram.Factory);
})(Directives || (Directives = {}));
//# sourceMappingURL=GsdUseCaseDiagram.js.map