define(['knockout', 'postman'], function(ko, postman) {


    var title = "SOVA-APP";

    var menuItems = [
    { name: 'Home', component: 'home-view'},
    { name: 'MyPage', component: 'my-page-view' },
    { name: 'SingleResult', component: 'single-post' }

    ]; 

    var selectedMenu = ko.observable(menuItems[0]); 
    var selectedComponent = ko.observable('home-view'); 
    var selectedParams = ko.observable({});

    var changeMenu = function(menu) {
        selectedMenu(menu); 
        selectedComponent(menu.component);
    }; 

    postman.subscribe("postToShow", function (data) {
        selectedParams(data);
        changeMenu(menuItems[2]);
    });

    postman.subscribe("changeMenu", function(menuName){
        var menu = menuItems.find(function (m) {
            return m.name === menuName
        });
        if (menu) changeMenu(menu);
    });
        


	return {
        title,
        menuItems,
        changeMenu,
        selectedComponent,
        selectedParams
	};
});

