define(['knockout', 'dataService', 'postman'], function (ko, ds, postman) {
    return function (params) {
        var posts = ko.observableArray([]);
        var next = ko.observable();
        var prev = ko.observable();
        var link = ko.observable();
        var comments = ko.observableArray([]);
        var commentsNext = ko.observable();
        var commentsPrev = ko.observable();

        //
        var isVisible = ko.observable(false);

        var apiString = ko.observable("api/posts");
        var apiStringComments = ko.observable();

        //kan bruges til at skifte mellem pages (pagination)
        var currentComponent = ko.observable("postSearch");
        var commentsComponent = ko.observable("postComment");


        var update = function (api) {
            ds.getPosts(function (data) {
                posts(data.items);
                next(data.next);
                prev(data.prev);
            }, api);
        };

        //update(apiString());

        var sendSinglePost = function (data) {
            postman.publish("postToShow", data);
            isVisible(true);
            currentComponent("postAnswers");
        };

        postman.subscribe("newPosts",
            function (data) {
                posts(data.items);
                prev(data.prev);
                next(data.next);
            });

        postman.subscribe("showAnswers", function (data) {
            posts(data.items);
            prev(data.prev);
            next(data.next);
            currentComponent("postAnswers");
        });

        postman.subscribe("showComments", function (urlInput) {
            updateComments(urlInput);

        });

        var updateComments = function (urlInput) {
            ds.getPosts(function (data) {
                comments(data.items);
                commentsNext(data.next);
                commentsPrev(data.prev);
            }, urlInput)
        }

        var prevPage = function () {
            apiString(prev());
            update(apiString());
        };

        var nextPage = function () {
            apiString(next());
            update(apiString());
        };

        var commentsPrevPage = function () {
            apiStringComments(commentsPrev());
            updateComments(apiStringComments());
        };

        var commentsNextPage = function () {
            apiStringComments(commentsNext());
            updateComments(apiStringComments());
        };

        return {
            posts,
            nextPage,
            prevPage,
            currentComponent,
            commentsComponent,
            sendSinglePost,
            isVisible,
            comments,
            commentsNextPage,
            commentsPrevPage
        };
    };
});