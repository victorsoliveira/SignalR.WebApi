angular.module('app', ['SignalR'])
    .value('toastr', toastr)
    .value('lodash', _)
    .factory('todoHub', ['$rootScope', 'Hub', function ($rootScope, hub) {


        var todoHub = new hub('todo', {
            listeners: {
                'notification': function (msg, status) {

                    switch (status) {
                        case 'success':
                            toastr.success(msg);
                            break;
                        case 'error':
                            toastr.error(msg);
                            break;
                        case 'warning':
                            toastr.warning(msg);
                            break;
                        case 'info':
                        default:
                            toastr.info(msg);
                    }
                },
                'added': function (todo) {
                    $rootScope.$broadcast('todoAdded', todo);
                },
                'removed': function (todo) {
                    $rootScope.$broadcast('todoRemoved', todo);
                }
            },
            errorHandler: function (error) {
                console.error(error);
            }
        });

        return {
            connection: todoHub.connection,
            start: function() {
                return todoHub.promise;
            }
        }

    }])
    .factory('todoRepo',['$http','toastr', '$window', function($http, toastr, $window) {

        var loadingElement = $('#loading-wrapper');

        var repo = {
            add: function (todo, connId) {
                loadingElement.show();
                $http.post($window.location + 'api/todo', {connId: connId, instance: todo})
                    .success(function () {
                        loadingElement.hide();
                    })
                    .error(function (data) {
                        loadingElement.hide();
                        toastr.error("Erro: " + data);
                });
            },
            remove: function (todo) {
                loadingElement.show();
                $http.delete($window.location + 'api/todo/' + todo.id)
                    .success(function () {
                        loadingElement.hide();
                    })
                    .error(function (data) {
                        loadingElement.hide();
                        toastr.error("Erro: " + data);
                    });
            },
            getAll: function() {
                loadingElement.show();
                return $http.get($window.location + 'api/todo')
                    .success(function() {
                        loadingElement.hide();
                    })
                    .error(function(data) {
                        loadingElement.hide();
                        toastr.error("Erro: " + data);
                    });
            }
        };

        return repo;

    }])
    .controller('todoCtrl', ['$scope', 'todoHub', 'todoRepo', 'lodash', function ($scope, todoHub, todoRepo, _) {

        var connectionId = '';

        initialize();

        $scope.$on('todoAdded', function (evt, todo) {
            $scope.list.push(todo);
            $scope.$apply();
        });

        $scope.$on('todoRemoved', function (evt, todo) {
            _.remove($scope.list, function (todoItem) {
                return todoItem.id === todo.id;
            });
            $scope.$apply();
        });

        $scope.add = function (description) {
            var newTodo = new TodoViewModel(ui_guid_generator(), description, connectionId);
            todoRepo.add(newTodo, connectionId);
        }

        $scope.remove = function (todo) {
            todoRepo.remove(todo);
        }

        function initialize() {

            $scope.list = [];

            todoHub.start().done(function() {
                connectionId = todoHub.connection.id;
            });

            todoRepo.getAll().success(function(data) {
                $scope.list = data;
            });
        }

    }]);