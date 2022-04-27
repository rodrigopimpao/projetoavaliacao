app.controller('EstudanteCtrl', function ($scope, $location, $http, $localStorage, $routeParams, $uibModal, $window, config) {
    
    //alert($routeParams.id)
    //alert($localStorage.usuario.token)

    

    const route = config.baseUrl + "/Estudante/"+$routeParams.id
    let me = $scope

    me.tipoUsuario = $localStorage.usuario.funcao
   
    me.load = () => {
        $http.get(route)
        .then(response => { 
            var estudante = response.data
            me.estudante = estudante
        })
        .catch(err => { 
            console.error(err)
        })
    }

});