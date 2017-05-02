angular.module("umbraco.directives")
    .directive('umbFormsContentMapperPrevalueEditor', function (dialogService, entityResource, iconHelper) {
        return {
            restrict: 'E',
            replace: true,
            templateUrl: '/App_Plugins/UmbracoFormExtensions/Directives/umb-forms-content-mapper-prevalue-editor.html',
            require: "ngModel",
            link: function (scope, element, attr, ctrl) {

                scope.prevalues = {
                    targets: [],
                    source: ''
                };

                scope.newPrevalueResult = '';
                scope.newPrevalueNode = 0;

                ctrl.$render = function () {
                    if (Object.prototype.toString.call(ctrl.$viewValue) === '[object Object]') {
                        scope.prevalues = ctrl.$viewValue;
                    }
                };

                scope.addTarget = function () {
                    addTarget();
                };

                scope.updateModel = function () {
                    updateModel();
                };

                scope.deleteTarget = function (index) {
                    deleteTarget(index);
                };

                function addTarget() {
                    scope.prevalues.targets.push({
                        result: scope.newPrevalueResult,
                        node: scope.newPrevalueNode
                    });
                    scope.newPrevalueResult = '';
                    scope.newPrevalueNode = 0;

                    updateModel();
                }

                function deleteTarget(index) {
                    scope.prevalues.targets.splice(index, 1);
                    updateModel();
                }

                function updateModel() {
                    ctrl.$setViewValue(scope.prevalues);
                }
            }
        };
    });
