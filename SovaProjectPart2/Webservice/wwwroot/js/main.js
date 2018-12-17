require.config({
    baseUrl: "js",
    paths: {
        knockout: "lib/knockout/dist/knockout.debug",
        jquery: "lib/jQuery/dist/jquery",

        jqcloud: 'lib/jqcloud2/dist/jqcloud',
        text: 'lib/text/text',
        dataService: 'services/dataService',
        postman: 'services/postman'
    },
    shim: {
        // set default deps
        'jqcloud': ['jquery']
    }
});
/*
// register components
require(['knockout'], function (ko) {
    ko.components.register("post-list", {
        viewModel: { require: 'components/postList/postList' },
        template: { require: 'text!components/postList/postListView.html' }
    });
    
});
*/
require(['knockout'], function (ko) {
    ko.components.register("post-list", {
        viewModel: { require: 'components/postList/postList' },
        template: { require: 'text!components/postList/postListView.html' }
	});

	ko.components.register("post", {
		viewModel: { require: 'components/post/post' },
		template: { require: 'text!components/post/postView.html' }
	});

    ko.components.register("postDetailed", {
        viewModel: { require: 'components/post/post' },
        template: { require: 'text!components/post/postDetailView.html' }
    });

    ko.components.register("singlePostWithAnswers", {
        viewModel: { require: 'components/singlePostWithAnswers/singlePostWithAnswers' },
        template: { require: 'text!components/singlePostWithAnswers/singlePostWithAnswersView.html'}
    });
    
	ko.components.register("searchbar", {
		viewModel: { require: 'components/searchBar/searchBar' },
		template: { require: 'text!components/searchBar/searchBarView.html' }
	});

    ko.components.register("single-post", {
        viewModel: { require: 'components/singlePost/singlePost' },
        template: { require: 'text!components/singlePost/singlePostView.html' }
    });

    ko.components.register("home-view", {
        viewModel: {require: 'components/homePage/homePage' },
        template: {require: 'text!components/homePage/homeView.html' }
    }); 

    ko.components.register("my-page-view", {
        viewModel: {require: 'components/myPage/myPage' },
        template: {require: 'text!components/myPage/myPageView.html' }
    });

    ko.components.register("history", {
        viewModel: { require: 'components/history/history' },
        template: { require: 'text!components/history/historyView.html' }
    });

    ko.components.register("history-list-view", {
        viewModel: { require: 'components/historyList/historyList' },
        template: { require: 'text!components/historyList/historyListView.html' }
    });

    ko.components.register("login-view", {
        viewModel: { require: 'components/login/login' },
        template: { require: 'text!components/login/loginView.html' }
    });

    ko.components.register("create-user-view", {
        viewModel: { require: 'components/login/login' },
        template: { require: 'text!components/login/createUserView.html' }
    });
    

});

require(['knockout', 'app'], function (ko, app) {
	ko.applyBindings(app);
});
