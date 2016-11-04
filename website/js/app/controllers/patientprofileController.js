angular.module('mcareApp').controller('PatientProfileController', ['$scope', '$http', 'SessionService', function ($scope, $http, SessionService) {

    SessionService.CheckSession();

    var data = {
        labels: [new Date(2016, 1, 1), new Date(2016, 2, 1), new Date(2016, 3, 5), new Date(2016, 4, 1), new Date(2016, 5, 1), new Date(2016, 5, 15), new Date(2016, 5, 31), new Date(2016, 6, 1), new Date(2016, 6, 14)],
        datasets: [
            {
                backgroundColor: 'rgba(54, 162, 235, 0.2)',
                borderColor: 'rgba(54, 162, 235, 1)',
                label: "Weight",
                data: [65, 59, 80, 81, 56, 55, 40, 60, 70]
            }]
    };

    var chtWeightEl = document.getElementById("chtWeight");
    var chtWeight = new Chart(chtWeightEl, {
        type: 'line',
        data: data,
        options: {
            scales: {
                xAxes: [{
                    type: 'time',
                    time: {
                        displayFormats: {
                            'millisecond': 'MMM DD',
                            'second': 'MMM DD',
                            'minute': 'MMM DD',
                            'hour': 'MMM DD',
                            'day': 'MMM DD',
                            'week': 'MMM DD',
                            'month': 'MMM DD',
                            'quarter': 'MMM DD',
                            'year': 'MMM DD',
                        }
                    }
                }]
            }
        }
    });

    var dataBP = {
        labels: [new Date(2016, 1, 1), new Date(2016, 2, 1), new Date(2016, 3, 5), new Date(2016, 4, 1), new Date(2016, 5, 1), new Date(2016, 5, 15), new Date(2016, 5, 31), new Date(2016, 6, 1), new Date(2016, 6, 14)],
        datasets: [
            {
                backgroundColor: 'rgba(75, 192, 192, 0.2)',
                borderColor: 'rgba(75, 192, 192, 1)',
                label: "Systolic",
                data: [120, 120, 80, 100, 90, 120, 85, 100, 110],
                strokeColor: "rgba(200,200,200,1)"
            },
            {
                backgroundColor: 'rgba(255, 206, 86, 0.2)',
                borderColor: 'rgba(255, 206, 86, 1)',
                label: "Diastolic",
                data: [80, 75, 80, 60, 65, 80, 85, 70, 90],
                strokeColor: "rgba(200,200,200,1)"
            }
        ]
    };
    var chtBPEl = document.getElementById("chtBP");
    var chtBP = new Chart(chtBPEl, {
        type: 'line',
        data: dataBP,
        options: {
            scales: {
                xAxes: [{
                    type: 'time',
                    time: {
                        displayFormats: {
                            'millisecond': 'MMM DD',
                            'second': 'MMM DD',
                            'minute': 'MMM DD',
                            'hour': 'MMM DD',
                            'day': 'MMM DD',
                            'week': 'MMM DD',
                            'month': 'MMM DD',
                            'quarter': 'MMM DD',
                            'year': 'MMM DD',
                        }
                    }
                }]
            }
        }
    });


    $('#tab2').click(function (e) {
        jQuery(e).tab('show');
        e.preventDefault();
    });


}]);