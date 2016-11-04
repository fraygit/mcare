angular.module('mcareApp').controller('PatientListController', ['$scope', '$http', 'SessionService', function ($scope, $http, SessionService) {

    SessionService.CheckSession();


    $scope.GoToPatientProfile = function () {
        document.location.href = "#/patientprofile";
    };

}]);