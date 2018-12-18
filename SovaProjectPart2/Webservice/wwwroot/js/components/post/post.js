define(['knockout', 'dataService', 'postman'], function (ko, ds, postman) {
	return function (params) {
        var title = params.post.title;
        var score = params.post.score;
        var body = params.post.body;
        var qa_UserId = params.post.qa_UserId;
        var postId = params.post.postId;
        var text = params.post.text;
        var creationDate = params.post.creationDate;

        var showCommentsVisible = ko.observable(false);

        var comments = ko.observable([]);
        var commentsNext = ko.observable();
        var commentsPrev = ko.observable();
        var apiStringComments = "api/comments/fromPost/" + postId;

        var commentsComponent = ko.observable("comment");

        ds.getPosts(function (data) {
            comments(data.items);
            commentsNext(data.next);
            commentsPrev(data.prev);
        }, apiStringComments)


        var showComments = function () {
            showCommentsVisible(true);
            
        }

        var updateComments = function (urlInput) {
            ds.getPosts(function (data) {
                comments(data.items);
                commentsNext(data.next);
                commentsPrev(data.prev);
            }, urlInput);
            showCommentsVisible(true);
        }

        var commentsPrevPage = function () {
            apiStringComments(commentsPrev());
            updateComments(apiStringComments());
        };

        var commentsNextPage = function () {
            apiStringComments(commentsNext());
            updateComments(apiStringComments());
        };

		return {
            title,
            score,
            body,
            creationDate,
            qa_UserId,
            text,
            postId,
            showComments,
            commentsNextPage,
            commentsPrevPage,
            showCommentsVisible,
            commentsComponent,
            comments
		};
	};
});