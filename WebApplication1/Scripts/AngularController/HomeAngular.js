var app = angular.module("HomeApp", []);



app.controller("HomeController", function ($scope, $http) {

    $scope.btntext = "Save";

    // New User Creation
    $scope.savedata = function () {
        $scope.btntext = "Please Wait..";
        $http({

            method: 'POST',

            url: '/Home/Add_record',

            data: $scope.Records

        }).then(function (d) {
            $scope.btntext = "Save";

            $scope.Records = null;

            alert("Email sent and Record Saved Successully!");
            window.location.pathname = "/Home/Show_Details";

        }), (function (d) {

            alert('Failed');

        });

    };

    // Display all record

    $http.get("/Home/Get_data").then(function (d) {

        $scope.record = d.data;

    }, function (error) {

        $scope.record = d.data;

    }, function (error) {

        alert('Failed');

    });

    // Display record by id

    $scope.loadrecord = function (id) {

        $http.get("/Home/Get_databyid?id=" + id).then(function (d) {

            $scope.register = d.data[0];

        }, function (error) {

            alert('Failed');

        });

    };

    // Delete record

    $scope.deleterecord = function (id) {

        $http.get("/Home/delete_record?id=" + id).then(function (d) {

            alert(d.data);

            $http.get("/Home/Get_data").then(function (d) {

                $scope.record = d.data;

            }, function (error) {

                alert('Success');

            });

        }, function (error) {

            alert('Failed');

        });

    };

    // Update record

    $scope.updatedata = function () {
        $scope.btntext = "Please Wait..";
        $http({
            method: 'POST',
            url: '/Home/update_record',
            data: $scope.Records

        }).then(function (d) {
            $scope.btntext = "Save";

            $scope.Records = null;

            alert(d);
            window.location.pathname = "/Home/Show_Details";

        }), (function (d) {

            alert('Failed');

        });

    };

    // Show User list
    $scope.GetAllDetails = $http.get('/Home/Get_Details').then(function (d){
        $scope.Regdata = d.data;

    },function(error) {
        alert("Failed");
    });

    $scope.GetById = function (id) {
        $http.get('/Home/Get_DetailsById?id='+id).then(function (d) {
            $scope.Records = d.data[0];
        }, function (error) {
            alert("Load failed");
        
        });
    };

});
