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

	    postman.subscribe("newPosts",
		    function(data) {
				posts(data.items);
				prev(data.prev);
				next(data.next);
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
			nextPage,
            prevPage,
            currentComponent
        };
    };
});