var mcareApp = angular.module('mcareApp', ['ui.router', 'ui.bootstrap']);

mcareApp.config(function ($stateProvider, $urlRouterProvider) {
    $urlRouterProvider.otherwise('/home');

    $stateProvider
        .state('home', {
            url: '',
            abstract: true,
            views: {
                'header':
                    {
                        templateUrl: 'js/app/partials/header.html'
                    },
                'sidebar':
                    {
                        templateUrl: 'js/app/partials/sidebar.html'
                    }

            }
        })

        .state('home.index', {
            url: '/home',
            views: {
                'container@': {
                    templateUrl: 'js/app/templates/home.html',
                    controller: 'HomeController'
                }
            }
        })

        .state('patientprofile', {
            url: '/patientprofile',
            views: {
                'container@': {
                    templateUrl: 'js/app/templates/patientprofile.html',
                    controller: 'PatientProfileController'
                }
            }
        })

        .state('profile', {
            url: '/profile',
            views: {
                'container@': {
                    templateUrl: 'js/app/templates/profile.html',
                    controller: 'ProfileController'
                }
            }
        })

        .state('patientprofileform', {
            url: '/patientprofileform',
            views: {
                'container@': {
                    templateUrl: 'js/app/templates/patientprofileform.html',
                    controller: 'PatientProfileFormController'
                }
            }
        })

});


//mcareApp.controller('TopBarController', ['$scope', '$http', function ($scope, $http) {

//    $scope.ShowWelcome = false;
//    $scope.ShowAcount = false;
//    $scope.ShowLogin = true;
//    $scope.ShowError = false;

//    $scope.LoginDetails = {};
//    $scope.UserDetails = {};
//    $scope.RegisterDetails = {};

//    var SuccessLogin = function () {
//        $scope.ShowWelcome = true;
//        $scope.ShowAcount = true;
//        $scope.ShowLogin = false;
//        $scope.UserDetails.Name = sessionStorage.getItem("FirstName") + ' ' + sessionStorage.getItem("LastName");
//    };

//    if (sessionStorage.getItem(appGlobalSettings.sessionTokenName) != null) {
//        SuccessLogin();
//    }

//    $scope.Logout = function () {
//        sessionStorage.clear();
//        $scope.ShowWelcome = false;
//        $scope.ShowAcount = false;
//        $scope.ShowLogin = true;
//    };

//    var DoLogin = function () {
//        if (!isBlank($scope.LoginDetails.Email) && !isBlank($scope.LoginDetails.Password)) {
//            $http.post(appGlobalSettings.apiBaseUrl + '/user',
//                JSON.stringify($scope.LoginDetails))
//                .then(function (data) {
//                    sessionStorage.setItem(appGlobalSettings.sessionTokenName, data.data.UserToken.Token);
//                    sessionStorage.setItem("FirstName", data.data.UserDetails.FirstName);
//                    sessionStorage.setItem("LastName", data.data.UserDetails.LastName);
//                    $('#popupLogin').modal('hide');
//                    SuccessLogin();
//                }, function (error) {
//                    $scope.LoginError = "Invalid username or password.";
//                    $("#ShowError").slideDown('slow');
//                    setTimeout(function () {
//                        $("#ShowError").slideUp('slow');
//                    }, 3000);
//                });
//        }
//        else {
//            $scope.LoginError = "Please input username and password."
//            $("#ShowError").slideDown('slow');
//            setTimeout(function () {
//                $("#ShowError").slideUp('slow');
//            }, 3000);
//        }

//    }

//    $scope.Login = function () {
//        DoLogin();
//    }

//    $scope.Register = function () {

//        if (!isBlank($scope.RegisterDetails.Email) && 
//            !isBlank($scope.RegisterDetails.Password) && 
//            !isBlank($scope.RegisterDetails.Password2) && 
//            !isBlank($scope.RegisterDetails.FirstName) && 
//            !isBlank($scope.RegisterDetails.LastName)
//            )
//        {
//            if ($scope.RegisterDetails.Password == $scope.RegisterDetails.Password2) {

//                var register = {
//                    Email: $scope.RegisterDetails.Email,
//                    FirstName: $scope.RegisterDetails.FirstName,
//                    LastName: $scope.RegisterDetails.LastName,
//                    Password: $scope.RegisterDetails.Password
//                };

//                $http.put(appGlobalSettings.apiBaseUrl + '/user',
//                    JSON.stringify(register))
//                    .then(function (data) {
//                        $scope.LoginDetails.Email = data.data.Email;
//                        $scope.LoginDetails.Password = $scope.RegisterDetails.Password;
//                        DoLogin();
//                    }, function (error) {
//                        $scope.RegisterError = error.statusText;
//                        $("#ShowRegisterError").slideDown('slow');
//                        setTimeout(function () {
//                            $("#ShowRegisterError").slideUp('slow');
//                        }, 3000);
//                    });
//            }
//            else {
//                $scope.RegisterError = "Password does not match."
//                $("#ShowRegisterError").slideDown('slow');
//                setTimeout(function () {
//                    $("#ShowRegisterError").slideUp('slow');
//                }, 3000);
//            }
//        }
//        else {
//            $scope.RegisterError = "Please input all fields."
//            $("#ShowRegisterError").slideDown('slow');
//            setTimeout(function () {
//                $("#ShowRegisterError").slideUp('slow');
//            }, 3000);
//        }
//    };


//}]);