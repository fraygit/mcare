angular.module('mcareApp').controller('PostController', ['$scope', '$http', function ($scope, $http) {

    $scope.ListingInfo = { IsFree: true, ShippingType: '1'};
    $scope.ShowPricing = false;
    $scope.ShowShippingCost = false;

    $scope.ClickIsFree = function () {
        $scope.ShowPricing = !$scope.ListingInfo.IsFree;
    }

    $scope.ShippingTypeChange = function () {
        $scope.ShowShippingCost = $scope.ListingInfo.ShippingType == "3";
    }

}]);