angular.module('mcareApp').controller('PatientProfileFormController', ['$scope', '$http', 'SessionService', function ($scope, $http, SessionService) {

    SessionService.CheckSession();

    $scope.Dob = $("#txtLastNormalPeriod").datepicker({ format: 'dd/mm/yyyy', });
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
    $http.get(appGlobalSettings.apiBaseUrl + '/MaternityProfile?token=' + encodeURIComponent(token))
            .then(function (data) {
                $scope.Profile = data.data;
                setValueDatePicker(data.data.PatientProfile.DateOfBirth, "#txtDob");
                setValueDatePicker(data.data.Maternity.LastPeriod, "#txtLastNormalPeriod");
                //if (data.data.PatientProfile.DateOfBirth != undefined || !isBlank(data.data.PatientProfile.DateOfBirth)) {
                //    data.data.PatientProfile.DateOfBirth = new Date(data.data.PatientProfile.DateOfBirth);
                //    if (data.data.PatientProfile.DateOfBirth.getFullYear() != 1) {
                //        $("#txtDob").datepicker("update", new Date(data.data.PatientProfile.DateOfBirth));
                //    }
                //    else {
                //        $("#txtDob").datepicker("update", "");
                //    }
                //}
            },
            function (error) {
                $scope.ErrorMessage = "Error encountered. " + error.statusText;
                $("#ErrorMessage").slideDown('slow');
            });

    $scope.Update = function () {
        if (!isBlank($("#txtDob").val())) {
            $scope.Profile.PatientProfile.DateOfBirth = moment($("#txtDob").val(), "DD/MM/YYYY")._d;
        }

        if (!isBlank($("#txtLastNormalPeriod").val())) {
            $scope.Profile.Maternity.LastPeriod = moment($("#txtLastNormalPeriod").val(), "DD/MM/YYYY")._d;
        }

        $http.post(appGlobalSettings.apiBaseUrl + '/MaternityProfile?token=' + encodeURIComponent(token),
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