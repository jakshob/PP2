define(['knockout', 'dataService', 'postman'], function (ko, ds, postman) {
    return function (params) {

        var url = ko.observable();

        var title = ko.observable();
        var body = ko.observable();
        var score = ko.observable();
        var userId = ko.observable();
        var createDate = ko.observable();
        var postId = ko.observable();
        
        var answerVisible = ko.observable(false);
        var commentVisible = ko.observable(false);


        url("api/posts/");
        url(url() + params.postId);
        ds.getPosts(function (data) {
            title(data.title);
            body(data.body);
            score(data.score);
            userId(data.qa_UserId);
            createDate(data.creationDate);
            postId(data.postId);


        }, url());

        var showAnswers = function (data) {
            answerVisible(true);
            url("api/posts/answersToQuestion/" + params.postId);
            ds.getPosts(function (data) {
                postman.publish("showAnswers", data)
            }, url());
        };

        var showComments = function (data) {
            commentVisible(true);
            var urlComments = "api/comments/fromPost/" + postId();
            postman.publish("showComments", urlComments);
        };

        var likeButtonPressed = function () {
            debugger;
            ds.createFavorite(params.postId);
        } 

            //var answers = ko.observableArray([]);

            /* Postman unødvendig her, grundet at data kommer gennem Params i toppen
            postman.subscribe("postToShow",
                function (data) {
                    url("api/posts/");
                    url(url() + data.postId);
                    ds.getPosts(function (data) {
                        title(data.title);
                        body(data.body);
                        score(data.score);
                        userId(data.qa_UserId);
                        createDate(data.creationDate);
                       
                    }, url());
    
                });
    
    */


            return {
                title,
                body,
                score,
                userId,
                createDate,
                showAnswers,
                showComments,
                answerVisible,
                commentVisible,
                likeButtonPressed

            };
        };
    });
