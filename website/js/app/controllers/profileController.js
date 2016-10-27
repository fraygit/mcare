angular.module('mcareApp').controller('ProfileController', ['$scope', '$http', 'SessionService', function ($scope, $http, SessionService) {

    SessionService.CheckSession();

    $("#txtDob").datepicker();
}]);