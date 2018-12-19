define(['knockout', 'dataService', 'postman'], function (ko, ds, postman) {
    return function (params) {
        var title = params.favorite.title;
        var creationDate = params.favorite.creationDate;
        var score = params.favorite.score;
        var qa_UserId = params.favorite.qa_UserId;

        return {
            title,
            creationDate,
            score,
            qa_UserId
        };
    };
});