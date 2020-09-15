Imports DBExtenderLib
Imports System.Text
Public Class AngularManager

    Public Shared Function CreateSimpleAngularApp(ByVal DT As TableNameInfo) As String
        Dim sb As New StringBuilder()
        sb.AppendLine("(function() {")
        sb.AppendLine("    var module = angular.module(" & DT.TablePluralize.ToLower.QT & ", [" & "common".QT & "]);")
        sb.AppendLine("}());")


        Return sb.ToString

    End Function
    Public Shared Function CreateAdvanceAngularApp(ByVal DT As TableNameInfo) As String
        Dim sb As New StringBuilder()


        sb.AppendLine("(function () {")
        sb.AppendLine("    var module = angular.module(" & "app".QT & ",")
        sb.AppendLine("        [" & "ngAnimate".QT & ",")
        sb.AppendLine("           " & "common".QT & ",")
        sb.AppendLine("           " & "ui.router".QT & ",")
        sb.AppendLine("           " & "ui.bootstrap".QT)
        sb.AppendLine("        ]);")
        sb.AppendLine("")
        sb.AppendLine("    module.config(function ($stateProvider, $urlRouterProvider) {")
        sb.AppendLine("        $urlRouterProvider.otherwise(" & "/home".QT & ");")
        sb.AppendLine("")
        sb.AppendLine("        $stateProvider.state(" & "home".QT & ", { url: " & "/home".QT & ", templateUrl: " & "Apps/templates/home.html".QT & " })")
        sb.AppendLine("                      .state(" & "contact".QT & ", { url: " & "/contacts".QT & ", templateUrl:" & "Apps/templates/contacts.html".QT & " })")
        sb.AppendLine("                      .state(" & "profile".QT & ", { url: " & "/profile".QT & ", templateUrl:" & "Apps/templates/profile.html".QT & "})")
        sb.AppendLine("")
        sb.AppendLine("    });")
        sb.AppendLine("}());")
        Return sb.ToString

    End Function
    Public Shared Function CreateSimpleController(ByVal DT As TableNameInfo) As String
        Dim sb As New StringBuilder()

        sb.AppendLine("(function (module) {")
        sb.AppendLine()
        sb.AppendLine("    var " & DT.TableSingularize.ToLower & "Controller = function () {")
        sb.AppendLine("        var model = this;")
        sb.AppendLine("        model.get" & DT.TablePluralize & " = function () {")
        sb.AppendLine()
        sb.AppendLine("        };")
        sb.AppendLine("        model.get" & DT.TableSingularize & "ByID = function (id) {")
        sb.AppendLine()
        sb.AppendLine("        };")
        sb.AppendLine("        model.insert" & DT.TableSingularize & " = function (" & DT.TableSingularize.ToLower & ") {")
        sb.AppendLine()
        sb.AppendLine("        };")
        sb.AppendLine("        model.update" & DT.TableSingularize & " = function (" & DT.TableSingularize.ToLower & ") {")
        sb.AppendLine()
        sb.AppendLine("        };")
        sb.AppendLine("")
        sb.AppendLine("        model.delete" & DT.TableSingularize & " = function (" & DT.TableSingularize.ToLower & ") {")
        sb.AppendLine("")
        sb.AppendLine("        };")
        sb.AppendLine("    };")
        sb.AppendLine("")
        sb.AppendLine("    module.controller(" & (DT.TableSingularize.ToLower & "Controller").QT & " , " & DT.TableSingularize.ToLower & "Controller);")
        sb.AppendLine("}(angular.module('app')));")

        Return sb.ToString

    End Function
    Public Shared Function CreateDirectives(ByVal DT As TableNameInfo) As String
        Dim sb As New StringBuilder()
        sb.AppendLine("        (function (module) {")
        sb.AppendLine("    var workSpinner = function (requestCounter) {")
        sb.AppendLine("        return {")
        sb.AppendLine("            restrict: " & "EAC".QT & ".,")
        sb.AppendLine("            transclude: true,")
        sb.AppendLine("            scope: {},")
        sb.AppendLine("            template: " & "<ng-transclude ng-show='requestCount'></ng-transclude>,".QT)
        sb.AppendLine("            link: function (scope) {")
        sb.AppendLine("")
        sb.AppendLine("                scope.$watch(function () {")
        sb.AppendLine("                    return requestCounter.getRequestCount();")
        sb.AppendLine("                }, function(requestCount) {")
        sb.AppendLine("                    scope.requestCount = requestCount;")
        sb.AppendLine("                });")
        sb.AppendLine("")
        sb.AppendLine("            }")
        sb.AppendLine("        };")
        sb.AppendLine("    };")
        sb.AppendLine("")
        sb.AppendLine("    module.directive(" & "workSpinner".QT & ", workSpinner);")
        sb.AppendLine("")
        sb.AppendLine("}(angular.module(" & "app".QT & ")));")


        Return sb.ToString

    End Function
    Public Shared Function CreateBasicDirectives(ByVal DT As TableNameInfo) As String
        Dim sb As New StringBuilder()
        sb.AppendLine("(function (module) {")
        sb.AppendLine("    var myDirective = function (requestCounter) {")
        sb.AppendLine("        return {")
        sb.AppendLine("            restrict: " & "EAC".QT & ".,")
        sb.AppendLine("            transclude: true,")
        sb.AppendLine("            scope: {},")
        sb.AppendLine("            link: function (scope) {")
        sb.AppendLine("            }")
        sb.AppendLine("        };")
        sb.AppendLine("    };")
        sb.AppendLine("")
        sb.AppendLine("    module.directive(" & "myDirective".QT & ", myDirective);")
        sb.AppendLine("")
        sb.AppendLine("}(angular.module(" & "app".QT & ")));")


        Return sb.ToString

    End Function
    Public Shared Function CreateComplexController(ByVal DT As TableNameInfo) As String
        Dim sb As New StringBuilder()

        sb.AppendLine("(function (module) {")
        sb.AppendLine("    var " & DT.TableSingularize & "Controller = function (pageManager, " & DT.TableSingularize.ToLower & "Services, $scope, " & DT.TableSingularize.ToLower & "EditServices) {")
        sb.AppendLine("        var model = this;")
        sb.AppendLine("        model.title = " & ("List" & DT.TablePluralize).QT & ";")
        sb.AppendLine("        model.localdb = {")
        sb.AppendLine("            customers: null")
        sb.AppendLine("        }")
        sb.AppendLine("        model.page = new pageManager.paginator();")
        sb.AppendLine("        model.searchCustomers = '';")
        sb.AppendLine("        activate();")
        sb.AppendLine("        model.get" & DT.TableSingularize & "ByID = function (id) {")
        sb.AppendLine("        };")
        sb.AppendLine("        model.insert" & DT.TableSingularize & " = function (" & DT.TableSingularize.ToLower & ") {")
        sb.AppendLine("        };")
        sb.AppendLine("        model.update" & DT.TableSingularize & " = function (" & DT.TableSingularize.ToLower & ") {")
        sb.AppendLine("        };")
        sb.AppendLine("        model.edit" & DT.TableSingularize & " = function (" & DT.TableSingularize.ToLower & ") {")
        sb.AppendLine("            " & DT.TableSingularize & "EditServices(" & DT.TableSingularize & ").then(function (result) {")
        sb.AppendLine("                " & DT.TableSingularize & "Services.update" & DT.TableSingularize & "(result).success(function (updateResult) {")
        sb.AppendLine("                    console.log(updateResult);")
        sb.AppendLine("                }).error(function(data, status, headers,config) {")
        sb.AppendLine("                    console.log(data);")
        sb.AppendLine("                    console.log(status);")
        sb.AppendLine("                    console.log(headers);")
        sb.AppendLine("                    console.log(config);")
        sb.AppendLine("                });")
        sb.AppendLine("            });")
        sb.AppendLine("        };")
        sb.AppendLine("        model.delete" & DT.TableSingularize & " = function (" & DT.TableSingularize.ToLower & ") {")
        sb.AppendLine("        };")
        sb.AppendLine("        function activate() {")
        sb.AppendLine("            " & DT.TableSingularize & "Services.get" & DT.TablePluralize & "().then(function (result) {")
        sb.AppendLine("                model.localdb." & DT.TablePluralize.ToLower & " = result.data." & DT.TablePluralize.ToLower & ";")
        sb.AppendLine("                model.page.dataSource = model.localdb." & DT.TablePluralize.ToLower & ";")
        sb.AppendLine("                model.page.totalItems = model.localdb." & DT.TablePluralize.ToLower & ".length;")
        sb.AppendLine("        model.page.refresh()")
        sb.AppendLine("            });")
        sb.AppendLine("        };")
        sb.AppendLine("        $scope.$watch(" & ("vm.search" & DT.TableSingularize).QT & ", function (current, original) {")
        sb.AppendLine("            search" & DT.TablePluralize & "(current);")
        sb.AppendLine("        });")
        sb.AppendLine("        function search" & DT.TablePluralize & "(filter) {")
        sb.AppendLine("            if (filter !== '') {")
        sb.AppendLine("                var customerResults = _.filter(model.localdb." & DT.TablePluralize.ToLower & ", function (item) {")
        sb.AppendLine("                    return _.str.startsWith(item.companyName.trim().toLowerCase(), filter.trim().toLowerCase()) ||")
        sb.AppendLine("                         _.str.startsWith(item.contactName.trim().toLowerCase(), filter.trim().toLowerCase()) ||")
        sb.AppendLine("                        _.str.startsWith(item.country.trim().toLowerCase(), filter.trim().toLowerCase());")
        sb.AppendLine("                });")
        sb.AppendLine("                model.page.dataSource = " & DT.TableSingularize.ToLower & "Results;")
        sb.AppendLine("                model.page.totalItems = " & DT.TableSingularize.ToLower & "Results.length;")
        sb.AppendLine("                model.page.refresh();")
        sb.AppendLine("            } else {")
        sb.AppendLine("                if (model.localdb." & DT.TablePluralize & ") {")
        sb.AppendLine("                    model.page.dataSource = model.localdb." & DT.TablePluralize.ToLower & ";")
        sb.AppendLine("                    model.page.totalItems = model.localdb." & DT.TablePluralize.ToLower & ".length;")
        sb.AppendLine("                    model.page.refresh();")
        sb.AppendLine("                }")
        sb.AppendLine("            };")
        sb.AppendLine("        };")
        sb.AppendLine("    };")
        sb.AppendLine("    module.controller(" & (DT.TableSingularize & "Controller") & ", " & DT.TablePluralize & "Controller);")
        sb.AppendLine("}(angular.module(" & "app".QT & ")));")

        Return sb.ToString
    End Function

    Public Shared Function CreateAdvanceServices(ByVal DT As TableNameInfo) As String
        Dim sb As New StringBuilder()
        sb.AppendLine(" (function (module) {")
        sb.AppendLine("     var " & DT.TableSingularize & "Services = function ($http) {")
        sb.AppendLine("          var get" & DT.TablePluralize & " = function () {")
        sb.AppendLine("            return $http.get(" & ("api/" & DT.TableSingularize.ToLower & "/list" & DT.TableSingularize.ToLower).QT & ");")
        sb.AppendLine("          };")
        sb.AppendLine("          var get" & DT.TableSingularize & "ById = function (id) {")
        sb.AppendLine("              return $http.get("")")
        sb.AppendLine("          };")
        sb.AppendLine("          var addNew" & DT.TableSingularize & " = function (" & DT.TableSingularize.ToLower & ") {")
        sb.AppendLine("            return   $http.post(" & ("api/" & DT.TableSingularize & "/insert" & DT.TableSingularize).QT & ",  " & DT.TableSingularize.ToLower & ");")
        sb.AppendLine("          };")
        sb.AppendLine("          var update" & DT.TableSingularize & " = function (" & DT.TableSingularize.ToLower & ") {")
        sb.AppendLine("              return $http.put(" & ("api/" & DT.TableSingularize & "/Update" & DT.TableSingularize).QT & " , " & DT.TableSingularize.ToLower & ");")
        sb.AppendLine("          }")
        sb.AppendLine("          var delete" & DT.TableSingularize & " = function (" & DT.TableSingularize.ToLower & ") {")
        sb.AppendLine("             return  $http.delete(" & ("api/" & DT.TableSingularize & "/Delete" & DT.TableSingularize).QT & " , " & DT.TableSingularize.ToLower & ");")
        sb.AppendLine("          }")
        sb.AppendLine("          return {")
        sb.AppendLine("              get" & DT.TablePluralize & ": get" & DT.TablePluralize & ",")
        sb.AppendLine("              get" & DT.TableSingularize & "ById: get" & DT.TableSingularize & "ById,")
        sb.AppendLine("              addNew" & DT.TableSingularize & ": addNew" & DT.TableSingularize & ",")
        sb.AppendLine("              update" & DT.TableSingularize & ": update" & DT.TableSingularize & ",")
        sb.AppendLine("              delete" & DT.TableSingularize & ": delete" & DT.TableSingularize & "")
        sb.AppendLine("          };")
        sb.AppendLine("  };")
        sb.AppendLine("    module.factory(" & (DT.TableSingularize.ToLower & "Services").QT & ", " & DT.TableSingularize.ToLower & "Services);")
        sb.AppendLine(" }(angular.module(" & "app".QT & ")));")


        Return sb.ToString

    End Function
    Public Shared Function CreateBasicServices(ByVal DT As TableNameInfo) As String
        Dim sb As New StringBuilder()
        sb.AppendLine(" (function (module) {")
        sb.AppendLine("     var " & DT.TableSingularize & "Services = function ($http) {")

        sb.AppendLine("          return {")

        sb.AppendLine("          };")
        sb.AppendLine("  };")
        sb.AppendLine("    module.factory(" & (DT.TableSingularize.ToLower & "Services").QT & ", " & DT.TableSingularize.ToLower & "Services);")
        sb.AppendLine(" }(angular.module(" & "app".QT & ")));")


        Return sb.ToString

    End Function

    Public Shared Function CreateJustServices(ByVal DT As TableNameInfo) As String
        Dim sb As New StringBuilder()
        sb.AppendLine(" (function (module) {")
        sb.AppendLine("     var " & DT.TableSingularize & "Services = function ($http) {")

        sb.AppendLine("          return {")

        sb.AppendLine("          };")
        sb.AppendLine("  };")
        sb.AppendLine("    module.factory(" & (DT.TableSingularize.ToLower & "Services").QT & ", " & DT.TableSingularize.ToLower & "Services);")
        sb.AppendLine(" }(angular.module(" & "app".QT & ")));")


        Return sb.ToString

    End Function
    Public Shared Function CreateMedServices(ByVal DT As TableNameInfo) As String
        Dim sb As New StringBuilder()
        sb.AppendLine(" (function (module) {")

        sb.AppendLine("   var myServices = function ($http) {")
        sb.AppendLine("  var getlist = function () {")
        sb.AppendLine("      return $http.get(" & "api/GetItems".QT & ");")
        sb.AppendLine("  };")
        sb.AppendLine("  var getById = function (id) {")
        sb.AppendLine("      return $http.get(" & "/api/GetItems/".QT & " + id);")
        sb.AppendLine("  };")
        sb.AppendLine("  var insertItem = function (item) {")
        sb.AppendLine("      return $http.post(" & "/api/GetItems/".QT & ", item);")
        sb.AppendLine("  };")
        sb.AppendLine("  var updateItem = function (item) {")
        sb.AppendLine("      return $http.put((" & "/api/GetItems/".QT & ", item);")
        sb.AppendLine("  };")
        sb.AppendLine("  var deleteItem = function (id) {")
        sb.AppendLine("      return $http.delete((" & "/api/GetItems/".QT & " + id);")
        sb.AppendLine("  };")
        sb.AppendLine(" return {")
        sb.AppendLine("     getlist: getlist, ")
        sb.AppendLine("     getById: getById, ")
        sb.AppendLine("     insertItem: insertItem, ")
        sb.AppendLine("     updateitem: updateItem, ")
        sb.AppendLine("     deleteItem: deleteItem")
        sb.AppendLine("     };")
        sb.AppendLine("  };")
        sb.AppendLine("    module.factory(" & (DT.TableSingularize.ToLower & "Services").QT & ", " & DT.TableSingularize.ToLower & "Services);")
        sb.AppendLine(" }(angular.module(" & "app".QT & ")));")


        Return sb.ToString

    End Function
End Class
