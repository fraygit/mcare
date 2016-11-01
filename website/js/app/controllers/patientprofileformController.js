angular.module('mcareApp').controller('PatientProfileFormController', ['$scope', '$http', 'SessionService', function ($scope, $http, SessionService) {

    SessionService.CheckSession();

    $scope.Dob = $("#txtLastNormalPeriod").datepicker({ format: 'dd/mm/yyyy', });


}]);