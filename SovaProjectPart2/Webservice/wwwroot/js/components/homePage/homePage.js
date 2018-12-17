define(['jquery', 'knockout', 'dataService', 'postman', 'jqcloud'], function ($, ko, ds, postman) {
    return function (params) {

        var searchString = ko.observable();

        return {
            searchString,
        };
    };

});