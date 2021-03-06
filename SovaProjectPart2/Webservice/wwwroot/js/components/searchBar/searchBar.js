﻿define(['knockout', 'dataService', 'postman'], function (ko, ds, postman) {
	return function (params) {

		var posts = ko.observableArray([]);
		var next = ko.observable();
		var prev = ko.observable();

        var searchString = ko.observable();

        var shouldShowMessage = ko.observable(false);
		
		var apiString = ko.observable("api/posts/searchQuestionsSortByScore/");

		var currentComponent = ko.observable("post");

		var update = function (api) {
			ds.getPosts(function (data) {
				posts(data.items);
				next(data.next);
				prev(data.prev);
				postman.publish("newPosts", data);
			}, api);
		}

		var search = function () {
			apiString("api/posts/searchQuestionsSortByScore/");
			apiString(apiString() + searchString());
            update(apiString());
            shouldShowMessage(true);
			
		}

		return {
			posts,
			next,
			prev,
			search,
			searchString,
            currentComponent,
            shouldShowMessage
			

			
		};
	};
});