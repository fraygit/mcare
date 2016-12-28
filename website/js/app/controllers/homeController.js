angular.module('mcareApp').controller('HomeController', ['$scope', '$http', 'SessionService', function ($scope, $http, SessionService) {

    SessionService.CheckSession();
    var token = sessionStorage.getItem(appGlobalSettings.sessionTokenName);

    $scope.Register = {};
    $scope.RegisterForm = { ShowError: false };
    $scope.AddAppointmentErrorPanel = { ShowError: false };

    $scope.NewAppointment = { Who: -1 };

    $scope.SelectAttendee;
    $scope.Attendees = [];
    $scope.DisplayAttendeeList = [];

    $scope.Dob = $("#txtFromDate").datepicker({ format: 'dd/mm/yyyy', });
    $scope.Dob = $("#txtToDate").datepicker({ format: 'dd/mm/yyyy', });
    $('.clockpicker').clockpicker({
        donetext: 'Done',
        autoclose: true
    });

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

    $scope.SaveAppointment = function () {

        if (!isBlank($("#txtFromDate").val()) && !isBlank($("#txtFromTime").val())) {
            if (!isBlank($("#txtToDate").val()) && !isBlank($("#txtToTime").val())) {

                var datetimeFrom = moment($("#txtFromDate").val() + ' ' + $("#txtFromTime").val(), "DD/MM/YYYY HH:mm")._d
                datetimeFromValid = moment($("#txtFromDate").val() + ' ' + $("#txtFromTime").val(), "DD/MM/YYYY HH:mm").isValid();
                if (datetimeFromValid) {
                    var datetimeTo = moment($("#txtToDate").val() + ' ' + $("#txtToTime").val(), "DD/MM/YYYY HH:mm")._d
                    var datetimeToValid = moment($("#txtToDate").val() + ' ' + $("#txtToTime").val(), "DD/MM/YYYY HH:mm").isValid();
                    if (datetimeToValid) {
                        if (moment($("#txtFromDate").val() + ' ' + $("#txtFromTime").val(), "DD/MM/YYYY HH:mm").isBefore(moment($("#txtToDate").val() + ' ' + $("#txtToTime").val(), "DD/MM/YYYY HH:mm"))) {
                            // Start processing..
                        }
                        else {
                            $("#pnlAddAppointmentError").slideDown('slow');
                            $scope.AddAppointmentErrorPanel.ErrorMessage = "From date time must be before the to date to.";
                            setTimeout(function () {
                                $("#pnlAddAppointmentError").slideUp('slow');
                            }, 3000);
                        }
                    }
                    else {
                        $("#pnlAddAppointmentError").slideDown('slow');
                        $scope.AddAppointmentErrorPanel.ErrorMessage = "Invalid to date time.";
                        setTimeout(function () {
                            $("#pnlAddAppointmentError").slideUp('slow');
                        }, 3000);
                    }
                }
                else {
                    $("#pnlAddAppointmentError").slideDown('slow');
                    $scope.AddAppointmentErrorPanel.ErrorMessage = "Invalid from date time.";
                    setTimeout(function () {
                        $("#pnlAddAppointmentError").slideUp('slow');
                    }, 3000);
                }
                
            }
            else {
                $("#pnlAddAppointmentError").slideDown('slow');
                $scope.AddAppointmentErrorPanel.ErrorMessage = "Please input a valid to date time.";
                setTimeout(function () {
                    $("#pnlAddAppointmentError").slideUp('slow');
                }, 3000);
            }
        }
        else {
            $("#pnlAddAppointmentError").slideDown('slow');
            $scope.AddAppointmentErrorPanel.ErrorMessage = "Please input a valid from date time.";
            setTimeout(function () {
                $("#pnlAddAppointmentError").slideUp('slow');
            }, 3000);
        }
    };


}]);