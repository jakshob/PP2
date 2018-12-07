define(['knockout', 'dataService', 'postman'], function (ko, ds, postman) {
    return function (params) {
        var posts = ko.observableArray([]);
        var next = ko.observable();
        var prev = ko.observable();
        var link = ko.observable();

        var currentComponent = ko.observable("post");

        ds.getPosts(function (data) {
            posts(data.items);

        });

        var prevPage = function () {
            apiString(prev());
            update(apiString());

        }

        var nextPage = function () {
            apiString(next());
            update(apiString());

        }

        return {
            posts,
            next,
            prev,
            link,
            nextPage,
            prevPage,
            currentComponent
        };
    };
});