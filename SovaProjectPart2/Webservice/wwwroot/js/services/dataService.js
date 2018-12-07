define(['jquery'], function ($) {
    var getPosts = function (callback) {
        $.getJSON('api/posts', function (data) {
            callback(data);
        });
    };


    return {
        getPosts
    };
});