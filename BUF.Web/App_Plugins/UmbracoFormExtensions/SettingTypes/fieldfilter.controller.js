angular.module("umbraco").controller("UmbracoFormExtensions.SettingTypes.FieldFilterController",
	function ($scope, $routeParams, pickerResource) {

	    if (!$scope.setting.value) {
	        $scope.filters = [];
	    } else {
	        $scope.filters = JSON.parse($scope.setting.value);
	    }

        pickerResource.getAllFields($routeParams.id).then(function (response) {
            $scope.fields = response.data;
        });

        $scope.addFilter = function () {
            $scope.filters.push({
                field: $scope.filterField,
                value: $scope.filterValue
            });
            $scope.filterField = '';
            $scope.filterValue = '';
	        $scope.setting.value = JSON.stringify($scope.filters);
        };

	    $scope.deleteFilter = function(index) {
	        $scope.filters.splice(index, 1);
	        $scope.setting.value = JSON.stringify($scope.filters);
	    };
	});