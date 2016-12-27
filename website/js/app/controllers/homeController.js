angular.module('mcareApp').controller('HomeController', ['$scope', '$http', 'SessionService', function ($scope, $http, SessionService) {

    SessionService.CheckSession();
    var token = sessionStorage.getItem(appGlobalSettings.sessionTokenName);

    $scope.Register = {};
    $scope.RegisterForm = { ShowError: false };

    $scope.NewAppointment = { Who: -1 };

    $scope.SelectAttendee;
    $scope.Attendees = [];
    $scope.DisplayAttendeeList = [];

    if (sessionStorage.getItem("UserType") == 'patient') {
        document.location.href = "#/patientprofile";
    }

    $('#today-calendar').fullCalendar({
        defaultView: 'agendaDay'
    })

    var data = {
        labels: ["January", "February", "March", "April", "May", "June", "July"],
        datasets: [
            {
                label: "Claims submitted to Moh",
                backgroundColor: 'rgba(54, 162, 235, 0.2)',
                borderColor: 'rgba(54, 162, 235, 1)',
                borderWidth: 1,
                data: [65, 59, 80, 81, 56, 55, 40],
            }
        ]
    };

    var claimsChartEL = document.getElementById("claimsChart");
    var claimsChart = new Chart(claimsChartEL, {
        type: "bar",
        data: data,
        options: {
            scales: {
                xAxes: [{
                    stacked: true
                }],
                yAxes: [{
                    stacked: true
                }]
            }
        }
    });


    var expenseData = {
        labels: ["January", "February", "March", "April", "May", "June", "July"],
        datasets: [
            {
                label: "Claims submitted to Moh",
                backgroundColor: 'rgba(255, 99, 132, 0.2)',
                borderColor: 'rgba(255,99,132,1)',
                borderWidth: 1,
                data: [65, 59, 80, 81, 56, 55, 40],
            }
        ]
    };

    var expenseChartEl = document.getElementById("expenseChart");
    var expenseChart = new Chart(expenseChartEl, {
        type: "bar",
        data: expenseData,
        options: {
            scales: {
                xAxes: [{
                    stacked: true
                }],
                yAxes: [{
                    stacked: true
                }]
            }
        }
    });

    $scope.AddPatient = function () {
        $("#registerPatientModal").modal('show');
    };

    $scope.SaveNewPatient = function () {
        $("#registerPatientModal").modal('hide');
        if (!isBlank($scope.Register.Email)) {

            $http.put(appGlobalSettings.apiBaseUrl + '/PatientList?token=' + encodeURIComponent(token),
                    JSON.stringify($scope.Register))
                    .then(function (data) {
                        $("#registerPatientModal").modal('hide');
                    },
                    function (error) {
                        $scope.RegisterForm.ErrorMessage = "Error encountered. " + error.statusText;
                        $scope.RegisterForm.ShowError = true;
                    });
        }
        else {
            $scope.RegisterForm.ErrorMessage = "Please input email address.";
            $scope.RegisterForm.ShowError = true;
        }
    };

    /*********************************************************************/


    $scope.PatientList = [];
    $http.get(appGlobalSettings.apiBaseUrl + '/PatientList?token=' + encodeURIComponent(token))
            .then(function (data) {
                $scope.PatientList = data.data;
            },
            function (error) {
            });


    $scope.AddAppointment = function () {
        $("#modalAddAppointment").modal('show');
    };

    $scope.AddAttendee = function () {
        if (!isBlank($scope.SelectAttendee)) {
            $scope.Attendees.push({
                Name: $("#ddAttendee option[value='" + $scope.SelectAttendee + "']").text(),
                Email: $scope.SelectAttendee
            });
        }
    };


}]);