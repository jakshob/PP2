require.config({
    baseUrl: "js",
    paths: {
        knockout: "lib/knockout/dist/knockout.debug",
        jquery: "lib/jQuery/dist/jquery",

        text: 'lib/text/text',
        dataService: 'services/dataService',
        postman: 'services/postman'
    },
    shim: {
        // set default deps
        'jqcloud': ['jquery']
    }
});

// register components
require(['knockout'], function (ko) {
    ko.components.register("post-list", {
        viewModel: { require: 'components/postList/postList' },
        template: { require: 'text!components/postList/postListView.html' }
    });

    ko.components.register("post", {
        viewModel: { require: 'components/post/post' },
        template: { require: 'text!components/post/postView.html' }
    });


});

require(['knockout', 'app'], function (ko, app) {
    ko.applyBindings(app);
});