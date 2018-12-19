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

    var getForceGraph = function (word, callback) {
        var url = 'api/posts/termNetwork/' + word;
        console.log(url);
        $.ajax({
            contentType: "text/plain",
            url: url,
            success: function (data) {
                callback(JSON.parse(data));
            },
            error: function (x) {
                console.log(x);
            }
        });
        //$.getJSON(url, function(data) {
        //	callback(data);
        //});
    };

    var getUser = function (callback, user) {
        $.getJSON('api/user/myPage/' + user, function (data) {
            callback(data);
        });
    };
    var getFavorites = function (callback, user) {
        $.getJSON('api/favorites/' + user, function (data) {
            callback(data);
        });
    };

    var createUser = function (user) {
        $.ajax({
            url: 'api/user',
            dataType: 'json',
            type: 'POST',
            contentType: 'application/json;charset=utf-8',
            data: JSON.stringify(user),
        });
    }
    var createFavorite = function (id) {
        debugger;
        $.ajax({
            url: 'api/favorites?id=' + id,
            type: 'POST',
        });
    }
  	
	return {
        getPosts,
        getWords,
        getUser,
        createUser,
        getForceGraph,
        createFavorite,
        getFavorites
	};
});