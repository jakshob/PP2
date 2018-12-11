define(['knockout', 'dataService', 'postman'], function (ko, ds, postman) {
    return function (params) {

        //var url = ko.observable();

        var title = ko.observable("Hej med mig");
        var body = ko.observable();
        var score = ko.observable();
        var userId = ko.observable();
        var createDate = ko.observable();
        //var answers = ko.observableArray([]);

        postman.subscribe("postToShow",
            function (data) {
                //url(data.url);
                ds.getPosts(function (data) {
                    title(data.title);
                    body(data.body);
                    score(data.score);
                    userId(data.qa_UserId)
                    createDate(data.creationDate)
                   
                }, data.url);

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