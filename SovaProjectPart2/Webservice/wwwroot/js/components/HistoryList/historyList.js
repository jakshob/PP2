define(['knockout', 'dataService', 'postman'], function (ko, ds, postman) {
    return function (params) {
        var histories = ko.observableArray([]);
        var favorites = ko.observableArray([]);
        var favoriteNext = ko.observable();
        var favoritePrev = ko.observable();
        var currentUser = ko.observable();

        var currentComponent = ko.observable("history");
        var favoritesComponent = ko.observable("favorite");

        postman.subscribe("user", function (user) {
            currentUser(user.Username);
        });

        var getUserInformation = function () {
            ds.getUser(function (data) {
                histories(data[1]); 
            }, currentUser());
        };

        var getUserFavorites = function () {
            ds.getFavorites(function (data) {
                favorites(data.items);
                favoriteNext(data.next);
                favoritePrev(data.Prev);
            }, currentUser());
        }

        postman.publish("needUserData");
        getUserInformation();
        getUserFavorites();

        return {
            histories,            
            currentComponent,
            favoritesComponent,
            favorites
        };
    };

});