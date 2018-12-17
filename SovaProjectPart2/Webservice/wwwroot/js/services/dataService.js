define(['jquery'], function ($) {
	var getPosts = function (callback, api) {
		$.getJSON(api, function (data) {
			callback(data);
		});
	};
    var getWords = function (callback, inputWord) {
        $.getJSON('api/posts/relevantWords/' + inputWord, function (data) {
            callback(data);
        });
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
        createUser
	};
});