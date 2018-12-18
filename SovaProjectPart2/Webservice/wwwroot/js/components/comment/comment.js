define(['knockout', 'dataService', 'postman'], function (ko, ds, postman) {
    return function (params) {
        var text = params.post.text;
        var score = params.post.score;
        var qa_UserId = params.post.qa_UserId;
        var creationDate = params.post.creationDate;

        return {
            score,
            creationDate,
            qa_UserId,
            text
        };
    };
});