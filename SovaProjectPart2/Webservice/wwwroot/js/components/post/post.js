define(['knockout', 'dataService', 'postman'], function (ko, ds, postman) {
	return function (params) {
        var title = params.post.title;
        var score = params.post.score;
        var body = params.post.body;
        

		return {
            title,
            score,
            body
		};
	};
});