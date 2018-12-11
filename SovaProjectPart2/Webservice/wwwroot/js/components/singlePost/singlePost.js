define(['knockout', 'dataService', 'postman'], function (ko, ds, postman) {
    return function (params) {

        var url = ko.observable();

        var title = ko.observable("Titel vil stå her:");
        var body = ko.observable();
        var score = ko.observable();
        var userId = ko.observable();
        var createDate = ko.observable();
        //var answers = ko.observableArray([]);

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


 

        return {
            title,
            body,
            score,
            userId,
            createDate
        
        };
    };
});