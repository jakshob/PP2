define(['knockout', 'dataService', 'postman'], function (ko, ds, postman) {
    return function (params) {
        var username = ko.observable();
        var password = ko.observable();

        var login = function () {
            postman.publish("login", {
                Username: username(),
                Password: password()
            });            
        }
        var newUser = function () {
            postman.publish("createNewUser");
        };

        var createUser = function () {
            ds.createUser({
                Username: username(),
                Password: password()
            });
            console.log("button pushed");
        };

        var validateForm = function () {
            var x = document.forms["frmLogin"]["username"].value;
            //var y = document.form["frmLogin"]["password"].value;
            if (x == "") {
                alert("Name must be filled out");
                return false;
            } else login();
        }

        return {
            username,
            password,
            login,
            newUser,
            createUser,
            validateForm
        };
    };
});