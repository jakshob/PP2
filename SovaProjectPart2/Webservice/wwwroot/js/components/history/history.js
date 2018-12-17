define(['knockout', 'dataService', 'postman'], function (ko, ds, postman) {
    return function (params) {
        var username = params.history.sovA_UserUsername;
        var creationDate = params.history.creationDate;
        var searchText = params.history.searchText;


        return {
            username,
            creationDate,
            searchText
        };
    };
});