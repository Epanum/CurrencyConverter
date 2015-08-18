app.config(function($stateProvider, $urlRouterProvider) {

    // Ionic uses AngularUI Router which uses the concept of states
    // Learn more here: https://github.com/angular-ui/ui-router
    // Set up the various states which the app can be in.
    // Each state's controller can be found in controllers.js
    $stateProvider

    // setup an abstract state for the tabs directive
      .state('tab', {
          url: "/tab",
          abstract: true,
          templateUrl: "templates/tabs.html"
      })

    // Each tab has its own nav history stack:

    .state('tab.converter', {
        url: '/converter',
        views: {
            'tab-converter': {
                templateUrl: 'templates/tab-converter.html',
                controller: 'converterController'
            }
        }
    })

    .state('tab.exchangerates', {
        url: '/exchangerates',
        views: {
            'tab-exchangerates': {
                templateUrl: 'templates/tab-exchangerates.html',
                controller: 'exchangeratesController'
            }
        }
    })
      //.state('tab.chat-detail', {
      //    url: '/chats/:chatId',
      //    views: {
      //        'tab-chats': {
      //            templateUrl: 'templates/chat-detail.html',
      //            controller: 'ChatDetailCtrl'
      //        }
      //    }
      //})

    .state('tab.settings', {
        url: '/settings',
        views: {
            'tab-settings': {
                templateUrl: 'templates/tab-settings.html',
                controller: 'settingsController'
            }
        }
    });

    // if none of the above states are matched, use this as the fallback
    $urlRouterProvider.otherwise('/tab/converter');

});