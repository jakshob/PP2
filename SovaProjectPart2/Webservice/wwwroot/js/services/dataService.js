define(['jquery'], function ($) {
	var getPosts = function (callback, api) {
		$.getJSON(api, function (data) {
			callback(data);
		});
	};
    var getWords = function (callback) {
        $.getJSON('api/posts/relevantWords/java', function (data) {
            callback(data);
        });
    };
	
	return {
        getPosts,
        getWords
	};
});