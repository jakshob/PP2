define(['jquery'], function ($) {
	var getPosts = function (callback, api) {
		$.getJSON(api, function (data) {
			callback(data);
		});
    };

	return {
		getPosts
	};
});