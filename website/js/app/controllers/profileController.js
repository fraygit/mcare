angular.module('mcareApp').controller('ProfileController', ['$scope', '$http', 'SessionService', function ($scope, $http, SessionService) {

    SessionService.CheckSession();

    $scope.Dob = $("#txtDob").datepicker({ format: 'dd/mm/yyyy', });

    $scope.Profile = {};

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

    // Retrieve Profile
    $http.get(appGlobalSettings.apiBaseUrl + '/PractitionerProfile?token=' + encodeURIComponent(token))
            .then(function (data) {
                $scope.Profile = data.data;
                if (data.data.PractitionerProfile.DateOfBirth != undefined || !isBlank(data.data.PractitionerProfile.DateOfBirth)) {
                    data.data.PractitionerProfile.DateOfBirth = new Date(data.data.PractitionerProfile.DateOfBirth);
                    $("#txtDob").datepicker("update", new Date(data.data.PractitionerProfile.DateOfBirth));
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

    $scope.Update = function () {
        $scope.Profile.UserId = $scope.Profile.User.Id;
        $scope.Profile.PractitionerProfileId = $scope.Profile.PractitionerProfile.Id;

        if (!isBlank($("#txtDob").val())) {
            $scope.Profile.PractitionerProfile.DateOfBirth = moment($("#txtDob").val(), "DD/MM/YYYY")._d;
        }

        $http.post(appGlobalSettings.apiBaseUrl + '/PractitionerProfile?token=' + encodeURIComponent(token),
                JSON.stringify($scope.Profile))
                .then(function (data) {
                    document.location.href = "/";
                },
                function (error) {
                    $scope.ErrorMessage = "Error encountered. " + error.statusText;
                    $("#ErrorMessage").slideDown('slow');
                });
    };

}]);