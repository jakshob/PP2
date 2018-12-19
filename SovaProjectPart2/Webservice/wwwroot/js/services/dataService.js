define(['jquery','postman', 'knockout'], function ($, postman, ko) {
    var username = ko.observable();
    
    postman.subscribe("user", function (data) {
        username(data.Username);
        console.log("SWOOP")
        });

	var getPosts = function (callback, api) {
		$.getJSON(api + "/" + username(), function (data) {
			callback(data);
		});
	};
    var getWords = function (callback, inputWord) {
        $.getJSON('api/posts/relevantWords/' + inputWord, function (data) {
            callback(data);
        });
    };

    var getForceGraph = function (word) {
        $.getJSON('api/posts/termNetwork/' + word)
    };

    var getUser = function (callback, user) {
        $.getJSON('api/user/myPage/' + user, function (data) {
            callback(data);
        });
    };

    var createUser = function (user)
    {
        $.ajax({
            url: 'api/user',
            dataType: 'json',
            type: 'POST',
            contentType: 'application/json;charset=utf-8',
            data: JSON.stringify(user),
        });
    }
  	
	return {
        getPosts,
        getWords,
        getUser,    
        createUser,
        getForceGraph
	};
});