angular.module('mcareApp').controller('PatientListController', ['$scope', '$http', 'SessionService', function ($scope, $http, SessionService) {

    SessionService.CheckSession();
    var token = sessionStorage.getItem(appGlobalSettings.sessionTokenName);

    $scope.GoToPatientProfile = function (email) {
        sessionStorage.setItem("currentPatientEmail", email);
        document.location.href = "#/patientprofile";
    };

    $scope.PatientList = [];

    $http.get(appGlobalSettings.apiBaseUrl + '/PatientList?token=' + encodeURIComponent(token))
            .then(function (data) {
                $scope.PatientList = data.data;
            },
            function (error) {
                $scope.ErrorMessage = "Error encountered. " + error.statusText;
                $("#ErrorMessage").slideDown('slow');
            });

}]);