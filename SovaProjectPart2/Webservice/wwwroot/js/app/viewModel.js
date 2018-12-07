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

    var string = "hej";


    var apiString = ko.observable("http://localhost:1891/api/posts/");


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

            $.getJSON("http://localhost:1891/api/posts/19/answers", function (data) {
                console.log("hej");
                answerList(data);
                //answerBody(answerList.body);


            });

        }

        if (currentTemplate() === "answers") {
            $.getJSON("http://localhost:1891/api/posts/19/answers", function (data) {
                console.log("hej");
                answerList(data);
                //answerBody(answerList.body);


            });
        }
    }

    var updatePost = function (data) {
        currentTemplate("post");

        apiString(data.link);

        update(apiString());

    }


    //update(apiString());

	/*var prevPage = function ()
	{
		apiString(prev());
		update(apiString());

	}

	var nextPage = function ()
	{
		apiString(next());
		update(apiString());

	}*/

    return {
        currentTemplate,
        posts,
        update,
        updatePost,
        prevPage,
        nextPage,
        title,
        postBody,
        answerBody,
        creationDate,
        score,
        answers,
        answerList
    };
});