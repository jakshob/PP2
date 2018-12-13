define(['jquery', 'knockout', 'dataService', 'postman', 'jqcloud'], function ($, ko, ds, postman) {
    return function (params) {

        var searchString = ko.observable();
        var shouldShowCloud = ko.observable(false);

        var wordCloud = function () {
            shouldShowCloud(true);

        }

        ds.getWords(function (data) {
            //words(data);
            $('#cloud').jQCloud(data);
            console.log(JSON.stringify(data));
        })

        var words = ko.observableArray([
            { text: "A", weight: 13 },
            { text: "Lorem", weight: 12 },
            { text: "Ipsom", weight: 5 },
            { text: "Dolores", weight: 9 },
            { text: "B", weight: 10.5 }]);

        return {
            words,
            wordCloud,
            searchString,
            shouldShowCloud
        };
    };

});