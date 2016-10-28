angular.module('mcareApp').controller('ProfileController', ['$scope', '$http', 'SessionService', function ($scope, $http, SessionService) {

    SessionService.CheckSession();

    $("#txtDob").datepicker();

    var token = sessionStorage.getItem(appGlobalSettings.sessionTokenName);
    $http.get(appGlobalSettings.apiBaseUrl + '/UserHasLogon?token=' + encodeURIComponent(token))
            .then(function (data) {
                if (!data.data) {
                    $("#welcomeModal").modal('show');
                }
            },
            function (error) {
                $scope.ErrorMessage = "Error encountered. " + error.statusText;
                $("#ErrorMessage").slideDown('slow');
            });


    // update to true
    $http.post(appGlobalSettings.apiBaseUrl + '/UserHasLogon?token=' + encodeURIComponent(token))
            .then(function (data) {
            },
            function (error) {
                $scope.ErrorMessage = "Error encountered. " + error.statusText;
                $("#ErrorMessage").slideDown('slow');
            });

}]);