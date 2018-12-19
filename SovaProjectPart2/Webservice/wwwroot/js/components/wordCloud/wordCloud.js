define(['jquery', 'knockout', 'dataService', 'postman', 'jqcloud'], function ($, ko, ds, postman, jqcloud) {
    return function (params) {

        var cloudString = ko.observable();
        var shouldShowCloud = ko.observable(false);
        var apiString = ko.observable("api/posts/relevantWords/");
        var text = ko.observable();
        var weight = ko.observable();

        var wordCloud = function () {
            shouldShowCloud(true);
            apiString("api/posts/relevantWords/");
            apiString(apiString() + cloudString());
            update(apiString());
        };

        var words = ko.observableArray([
            { text: "This", weight: 1 },
            { text: "is", weight: 1 },
            { text: "a", weight: 1 },
            { text: "placeholder", weight: 1 }
        ]);

        var update = function (api) {
            ds.getPosts(function (data) {
                text(data.text);
                weight(data.weight);
                $('#cloud').jQCloud(data);
                console.log(JSON.stringify(data));
            }, api);
        };


        return {
            words,
            wordCloud,
            cloudString,
            shouldShowCloud,
            text,
            weight
        };
    };
});