angular.module('mcareApp').service('SessionService', function () {

    this.CheckSession = function () {
        if (sessionStorage.getItem(appGlobalSettings.sessionTokenName) == undefined) {
            document.location.href = "/login.html";
        }
    }

    this.MenuActive = function () {

    }

});