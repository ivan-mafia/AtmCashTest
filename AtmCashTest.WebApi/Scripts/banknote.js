// Defining angularjs module
var app = angular.module('banknoteModule', []);

// Defining angularjs Controller and injecting ProductsService
app.controller('banknoteCtrl', function ($scope, $http, banknotesService) {

    $scope.balance = null;
    $scope.loading = 0;
    $scope.inputsum = 100;
    $scope.banknotes = [
        {
            Id: 1,
            Nominal: 100,
            Count: 0
        },
        {
            Id: 2,
            Nominal: 500,
            Count: 0
        },
        {
            Id: 3,
            Nominal: 1000,
            Count: 0
        },
            {
                Id: 4,
                Nominal: 5000,
                Count: 0
            }];

    $scope.clearBanknotes = function() {
        $scope.banknotes.forEach(function(item) {
            item.Count = 0;
        });
    }

    function onError(response) {
        alert("Error : " + response.data.ExceptionMessage);
    }

    function onFinally() {
        $scope.loading--;
    }

    $scope.getBalance = function () {
        $scope.loading++;
        banknotesService.GetBalance().then(function (response) {
            $scope.balance = response.data;
        }, onError).finally(onFinally);
    }

    $scope.putCash = function () {
        $scope.loading++;
        banknotesService.PutCash($scope.banknotes).then(function (response) {
            if (response.data != null) {
                var msg = "Cash that not accepted:";
                response.data.forEach(function (item) {
                    msg = msg + " " + item.Count + " of \"" + item.Nominal + "\" banknotes,";
                });
                alert(msg.slice(0, -1));
            }
            $scope.clearBanknotes();
            $scope.getBalance();
        }, onError).finally(onFinally);;
    }

    $scope.getCash = function () {
        $scope.loading++;
        banknotesService.GetCash($scope.inputsum).then(function (response) {
            var msg = "Cashed out:";
            response.data.forEach(function(item) {
                msg = msg + " " + item.Count + " of \"" + item.Nominal + "\" banknotes,";
            });
            alert(msg.slice(0, - 1));
            $scope.getBalance();
        }, onError).finally(onFinally);;
    }

    $scope.getBalance();

});

// Service factory.
app.factory('banknotesService', function ($http) {
    var fac = {};

    fac.GetBalance = function() {
        return $http.get('api/Banknote/GetBalance');
    }

    fac.PutCash = function(banknotes) {
        return $http.post('api/Banknote/PostCash', banknotes);
    }

    fac.GetCash = function(inputsum) {
        return $http.get('api/Banknote/GetCash', {
            params: {
                sum: inputsum
            }
        });
    }

    return fac;
});