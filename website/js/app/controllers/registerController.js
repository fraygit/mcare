angular.module('mcareAppLogin').controller('RegisterController', ['$scope', '$http', function ($scope, $http) {
    $scope.registerData = { UserType: "-1"};
    $scope.ErrorMessage = "";

    $scope.RegisterAction = function () {
        $("#ErrorMessage").slideUp('slow');
        if ($scope.registerData.UserType != "-1") {
            if ($scope.registerData.Agree) {
                if ($scope.registerData.Password == $scope.registerData.RePassword) {
                    $http.put(appGlobalSettings.apiBaseUrl + '/User',
                    JSON.stringify($scope.registerData))
                        .then(function (data) {

                            /// LOGIN USER
                            var login = { Email: $scope.registerData.Email, Password: $scope.registerData.Password };
                            $http.post(appGlobalSettings.apiBaseUrl + '/User',
                                JSON.stringify(login))
                                .then(function (data) {
                                    sessionStorage.setItem(appGlobalSettings.sessionTokenName, data.data.UserToken.Token);
                                    sessionStorage.setItem(appGlobalSettings.sessionUserType, data.data.UserType);
                                    switch (data.data.UserType){
                                        case 'practitioner':
                                            document.location.href = "/#/profile";
                                            break;
                                        case 'patient':
                                            document.location.href = "/#/patientprofileform";
                                            break;
                                    }
                                }, function (error) {
                                    $scope.LoginError.Message = "Invalid username or password.";
                                    $scope.LoginError.ShowError = true;
                                });
                        },
                        function (error) {
                            $scope.ErrorMessage = "Error encountered. " + error.statusText;
                            $("#ErrorMessage").slideDown('slow');
                        });
                }
                else {
                    $scope.ErrorMessage = "Password not matching.";
                    $("#ErrorMessage").slideDown('slow');
                }
            }
            else {
                $scope.ErrorMessage = "Tick on agree.";
                $("#ErrorMessage").slideDown('slow');
            }
        }
        else {
            $scope.ErrorMessage = "Please select user type.";
            $("#ErrorMessage").slideDown('slow');
        }
    };


}]);