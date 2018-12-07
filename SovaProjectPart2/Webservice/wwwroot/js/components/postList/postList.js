define(['knockout', 'dataService', 'postman'], function (ko, ds, postman) {
    return function (params) {
        var posts = ko.observableArray([]);
        var next = ko.observable();
        var prev = ko.observable();
        var link = ko.observable();
        var apiString = ko.observable("api/posts");

        var currentComponent = ko.observable("post");

        var update = function (api) {
            ds.getPosts(function (data) {
                posts(data.items);
                next(data.next);
                prev(data.prev);
            }, api);

        }

        update(apiString());
        

        

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