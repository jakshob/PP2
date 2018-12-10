define(['knockout', 'dataService', 'postman'], function (ko, ds, postman) {
	return function (params) {

		var posts = ko.observableArray([]);
		var next = ko.observable();
		var prev = ko.observable();
		
		var apiString = ko.observable("api/posts/searchQuestionsSortByScore/");

		var currentComponent = ko.observable("post");

		var update = function (api) {
			ds.getPosts(function (data) {
				posts(data.items);
				next(data.next);
				prev(data.prev);
			}, api);
		}

		var search = function () {
			
			apiString(apiString() + "java");
			update(apiString());
		}

		return {
			posts,
			next,
			prev,
			search,
			currentComponent
			

			
		};
	};
});