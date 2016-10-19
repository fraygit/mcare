angular.module('mcareApp').controller('HomeController', ['$scope', '$http', 'SessionService', function ($scope, $http, SessionService) {

    SessionService.CheckSession();

    $('#today-calendar').fullCalendar({
        defaultView: 'agendaDay'
    })

}]);