define(['knockout', 'dataService', 'postman'], function (ko, ds, postman) {
	return function (params) {
		var title = params.post.title;
		var score = params.post.score;

		return {
			title,
			score
		};
	};
});