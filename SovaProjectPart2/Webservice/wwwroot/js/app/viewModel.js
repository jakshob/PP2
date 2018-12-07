define(['jquery', 'knockout'], function ($, ko) {

    //Viewmodel for posts
	/*var posts = ko.observableArray([]);
	var next = ko.observable();
	var prev = ko.observable();
	var link = ko.observable();*/

    //Viewmodel for post
    var title = ko.observable();
    var creationDate = ko.observable();
    var score = ko.observable();
    var postBody = ko.observable();
    var answers = ko.observable();

    //Vievmodel for answers
    var answerList = ko.observableArray([]);
    var answerBody = ko.observable();


    var currentTemplate = ko.observable("posts");
    var apiString = ko.observable("api/posts");

    var update = function (apiString) {
        if (currentTemplate() === "posts") {

            $.getJSON(apiString, function (data) {
                posts(data.items);
                link(data.items.link);
                next(data.next);
                prev(data.prev);
            });
        }

        if (currentTemplate() === "post") {
            $.getJSON(apiString, function (data) {

                title(data.title);
                creationDate(data.creationDate);
                score(data.score);
                postBody(data.body);
                answers(data.answers);
            });
        }
    }

    var updatePost = function (data) {
        currentTemplate("posts");

        apiString(data.link);
        update(apiString());

    }


    return {
        currentTemplate,       
        update,
        updatePost,        
        title,
        postBody,
        answerBody,
        creationDate,
        score,
        answers,
        answerList
    };
});