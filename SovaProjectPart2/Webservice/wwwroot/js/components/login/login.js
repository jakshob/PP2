define(['knockout', 'dataService', 'postman'], function (ko, ds, postman) {
    return function (params) {
        var username = ko.observable();
        var password = ko.observable();

        var login = function () {
            postman.publish("login", {
                    username,
                    password
            });
            getUserInformation();
        }
        var newUser = function () {
            postman.publish("createNewUser");
        };

        var getUserInformation = function () {
            ds.getUser(function (data) {
                username(data.username);
                password(data.password);
                postman.publish("newUserData", data);
            }, username());
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