angular.module("umbraco").controller("UmbracoFormExtensions.SettingTypes.ConditionalRedirectController",
	function ($scope, $routeParams, pickerResource) {

	    if (!$scope.setting.value) {
	        $scope.redirection = {
	            targets: [],
	            source: ''
	        };
	    } else {
	        $scope.redirection = JSON.parse($scope.setting.value);
	    }

	    pickerResource.getAllFields($routeParams.id).then(function (response) {
	        $scope.fields = response.data;
	    });

	    $scope.newResult = '';
	    $scope.newNode = 0;

	    $scope.addTarget = function () {
	        $scope.redirection.targets.push({
	            result: $scope.newResult,
	            node: $scope.newNode
	        });
	        $scope.newResult = '';
	        $scope.newNode = 0;

	    };

	    $scope.deleteTarget = function (index) {
	        $scope.redirection.targets.splice(index, 1);
	    };

	    $scope.$watch("redirection", function (newVal, oldVal) {
	        $scope.setting.value = JSON.stringify($scope.redirection);
	    }, true);
	});