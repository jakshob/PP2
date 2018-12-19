define(['jquery', 'knockout', 'dataService', 'postman', 'jqcloud'], function ($, ko, ds, postman, jqcloud) {
    return function (params) {

        var networkString = ko.observable("java");
        var shouldShowNetwork = ko.observable(false);
		var apiString = ko.observable("api/posts/termNetwork/");

	    var update = function (api) {
			ds.getForceGraph(networkString(), function(data) {

			});
	    };

		var termNetwork = function () {
            shouldShowNetwork(true);
            apiString("api/posts/relevantWords/");
            apiString(apiString() + networkString());
            update(apiString());
        };

        var words = ko.observableArray([
            { text: "This", weight: 1 },
            { text: "is", weight: 1 },
            { text: "a", weight: 1 },
            { text: "placeholder", weight: 1 }
        ]);

       

        return {
            words,
            termNetwork,
            networkString,
            shouldShowNetwork
        };
    };
});