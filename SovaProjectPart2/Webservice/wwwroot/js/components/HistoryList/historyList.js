define(['knockout', 'dataService', 'postman'], function (ko, ds, postman) {
    return function (params) {
        var histories = ko.observableArray([]);
        var favorite = ko.observableArray([]);
        var currentUser = ko.observable();

        var currentComponent = ko.observable("history");   

        postman.subscribe("user", function (user) {
            currentUser(user.Username);
        });

        var getUserInformation = function () {
            ds.getUser(function (data) {
                favorite(data[0]);
                histories(data[1]); 
            }, currentUser());
        };

        postman.publish("needUserData");
        getUserInformation();

        return {
            histories,            
            currentComponent,
        };
    };

});