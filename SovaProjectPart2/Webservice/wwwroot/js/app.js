define(['knockout', 'postman'], function (ko, postman) {


    var title = "SOVA-APP";
    var user = ko.observable();
    var username = ko.observable("");

    var menuItems = [
        { name: 'Home', component: 'home-view' },
        { name: 'MyPage', component: 'my-page-view' },
        { name: 'SingleResult', component: 'single-post' },
        { name: 'Log In', component: 'login-view' }

    ];

    var selectedMenu = ko.observable(menuItems[0]);
    var selectedComponent = ko.observable('home-view');
    var selectedParams = ko.observable({});

    var changeMenu = function (menu) {
        selectedMenu(menu);
        selectedComponent(menu.component);
    };

    postman.subscribe("postToShow", function (data) {
        selectedParams(data);
        changeMenu(menuItems[2]);
    });

    postman.subscribe("changeMenu", function (menuName) {
        var menu = menuItems.find(function (m) {
            return m.name === menuName
        });
        if (menu) changeMenu(menu);
    });

    postman.subscribe("login", function (data) {
        user(data);
        username(data.Username);
        changeMenu(menuItems[1]);
    });
    postman.subscribe("needUserData", function () {
        postman.publish("user", user());
        console.log("Mypage requesting userdata");
    });

    postman.subscribe("createNewUser", function () {
        selectedComponent("create-user-view");
        console.log("Create new user!");
    });



    return {
        title,
        username,
        menuItems,
        changeMenu,
        selectedComponent,
        selectedParams
    };
});
