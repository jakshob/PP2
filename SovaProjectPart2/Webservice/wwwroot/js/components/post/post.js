define(['knockout', 'dataService', 'postman'], function (ko, ds, postman) {
	return function (params) {
        var title = params.post.title;
        var score = params.post.score;
        var body = params.post.body;
        var qa_UserId = params.post.qa_UserId;
        var postId = params.post.postId;
        var text = params.post.text;
        var creationDate = params.post.creationDate;

        var showComments = function (data) {
            var string = data;
            debugger;
        }
       
		return {
            title,
            score,
            body,
            creationDate,
            qa_UserId,
            text,
            postId,
            showComments
		};
	};
});