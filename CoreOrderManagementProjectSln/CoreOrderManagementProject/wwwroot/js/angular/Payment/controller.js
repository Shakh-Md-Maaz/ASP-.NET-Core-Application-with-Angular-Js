app.controller('myController', function ($scope, myService) {
    PaymentList();


    $scope.newPayment = {};
    $scope.clickedPayment = {};
    $scope.message = "";

    function PaymentList() {
        myService.paymentList().then(function (result) {
            $scope.Payments = result.data;
            console.log(result.data);
        });
    }

    $scope.addPayment = function () {
        myService.addPayment($scope.newPayment)
            .then(function (result) {
                $scope.newPayment = {};
                $scope.message = "Data saved successfully";
                PaymentList();
                //console.log(result.data);
            });

        //, function (result) {
        //    alert(result)
        //}
        //);

    };

    $scope.selectedPayment = function (payment) {
        $scope.clickedPayment = payment;
    };

    $scope.updatePayment = function () {


        myService.updatePayment($scope.clickedPayment).then(function (result) {
            $scope.message = "Data Update successfully";
            $scope.Payments = result;
            PaymentList();
        }, function (result) {
            alert(result);
        });

    };

    $scope.deletePayment = function () {
        myService.deletePayment($scope.clickedPayment.Id).then(function (result) {
            $scope.message = "Data Delete successfully";
            PaymentList();
        }, function (result) {
            alert(result);
        });

    };

    $scope.clearInfo = function () {
        $scope.message = "";
    };

});

