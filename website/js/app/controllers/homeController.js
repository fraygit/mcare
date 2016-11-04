angular.module('mcareApp').controller('HomeController', ['$scope', '$http', 'SessionService', function ($scope, $http, SessionService) {

    SessionService.CheckSession();

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

}]);