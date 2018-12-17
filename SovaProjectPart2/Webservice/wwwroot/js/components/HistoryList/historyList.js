define(['knockout', 'dataService', 'postman'], function (ko, ds, postman) {
    return function (params) {
        var histories = ko.observableArray([]);
        var favorite = ko.observableArray([]);

        var currentComponent = ko.observable("history");

        var update = function () {
            postman.publish("needUserData");
        }

        postman.subscribe("newUserData", function (data) {
                favorite(data[0]);
                histories(data[1]);
        });

        return {
            histories,
            update,
            currentComponent

        };
    };

});