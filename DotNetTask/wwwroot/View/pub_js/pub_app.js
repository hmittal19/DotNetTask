const icarApp = angular.module('icarApp', ['jcs-autoValidate']);
const _403Error = '<div class="error-403"><div class="base io"><h1 class="io">403</h1><h2>Access forbidden</h2><h5>(You don\'t have permission to access this page.)</h5></div></div>';

icarApp.config([
    '$routeProvider',
    function ($routeProvider) {
        
        $routeProvider
            .when('/home', {
                title: 'Home',
                templateUrl: '../View/home.html',
                controller: 'homeCtrl',
            })
            .when('/application-form', {
                title: 'application form',
                templateUrl: '../View/application-form.html',
                controller: 'homeCtrl',
            })
            .otherwise({ redirectTo: '/home' });
    },
]);

icarApp.run(['$rootScope', function ($rootScope) {
    
    sessionStorage.userSes = null;
    sessionStorage.role = null;
    $rootScope.$on('$routeChangeSuccess', function (event, current, previous) {
        
        if (current.$$route != undefined)
            $rootScope.title = current.$$route.title;
        sessionStorage.userSes = null;
        sessionStorage.role = null;
    });
}]);
icarApp.controller('homeCtrl', function ($scope, $http, $location) {
    home($scope, $http, $location);
}); 


