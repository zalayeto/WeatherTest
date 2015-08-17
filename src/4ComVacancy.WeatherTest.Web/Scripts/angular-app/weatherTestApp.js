angular.module('weatherTestApp',[])
    .controller('mainController', function ($scope, apiService) {

        $scope.location = null;
        $scope.temperatureUnit = null;
        $scope.windUnit = null;

        $scope.tempValue = null;
        $scope.windValue = null;

        $scope.init = function (defaultTempUnit, defaultWindUnit) {
            $scope.location = "";
            $scope.temperatureUnit = defaultTempUnit;
            $scope.windUnit = defaultWindUnit;
        };

        $scope.getWeather = function () {

            $scope.reset();

            apiService.getWeather($scope.location, $scope.temperatureUnit, $scope.windUnit)
                .success(function (data) {
                    $scope.tempValue = data.Temperature;
                    $scope.windValue = data.Wind;
                })
                .finally(function () {

                });
        };

        $scope.reset = function (newValue, oldValue) {

            $scope.tempValue = null;
            $scope.windValue = null;
        };
    })
    .service('apiService', function ($http) {
        return {
            getWeather: function (location, tempUnit, windUnit) {

                var promise = $http.get('/api/weather/' + location + '?tempUnit=' + tempUnit + '&windUnit=' + windUnit);
                return promise;
            }
        };
    });