var mcareAppLogin = angular.module('mcareAppLogin', ['ui.router']);

mcareAppLogin.config(function ($stateProvider, $urlRouterProvider) {
    $urlRouterProvider.otherwise('/login');

    $stateProvider
        .state('home', {
            url: '',
            abstract: true,
            views: {
                'header':
                    {
                        templateUrl: 'js/app/partials/header.html'
                    }

            }
        })

        .state('home.index', {
            url: '/login',
            views: {
                'container@': {
                    templateUrl: 'js/app/templates/login.html',
                    controller: 'LoginController'
                }
            }
        })


}); 
