app.service('myService', function ($http) {

    this.paymentList = function () {
        var response = $http.get('Payment/PaymentList');
        return response;
    };

    this.addPayment = function (payment) {
        //alert(product);
        var response = $http({
            method: 'post',
            url: 'Payment/AddPayment',
            data: JSON.stringify(payment)
        });
        return response;
    };

    this.updatePayment = function (payment) {
        //alert(product);
        var response = $http({
            method: 'post',
            url: 'Payment/UpdatePayment',
            data: JSON.stringify(payment)
        });
        return response;
    };

    this.deletePayment = function (id) {
        var response = $http({
            method: 'post',
            url: 'Payment/DeletePayment',
            params: { Id: JSON.stringify(id) }
        });
        return response;
    };

});
