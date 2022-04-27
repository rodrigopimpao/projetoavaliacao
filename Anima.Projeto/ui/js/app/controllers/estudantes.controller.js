app.controller('EstudantesCtrl', function ($scope, $location, $http, $localStorage, $routeParams, $uibModal, config) {
    
    //alert($routeParams.id)
    //alert($localStorage.usuario.token)

    

    const route = config.baseUrl + "/Usuario/funcao/Estudante"
    let me = $scope
    me.user = ""
    if(typeof $localStorage.usuario !== 'undefined' && $localStorage.usuario.token !== '') {
        me.user = $localStorage.usuario.nome
        me.tipoUsuario = $localStorage.usuario.funcao
    }
    
    me.total = ""
    me.mediaId = ""
    me.id = ""
    me.list = []
    
    me.isAvaliado = false

    $scope.editMedia = function(estudante) {
        var dialogInst = $uibModal.open({
            ariaLabelledBy: 'modal-title',
            ariaDescribedBy: 'modal-body',
            templateUrl: 'views/media/edit-item.html',
            size: "lg",
            controller: 'EditDialogInstCtrl',
            windowClass: 'app-modal-window',
            backdropClass: 'black-backdrop',
          resolve: {
            selectedItem: function() {
              return estudante;
            }
          }
        });

        dialogInst.result.then(function (estudante) {
            me.id = estudante.id
            me.mediaId = estudante.media.id
            me.total = estudante.media.total
            me.saveMedia()
        }, function () {
            me.load()
        });

    };


    $scope.addMedia = function(estudante) {
        var dialogInst = $uibModal.open({
            ariaLabelledBy: 'modal-title',
            ariaDescribedBy: 'modal-body',
            templateUrl: 'views/media/add-item.html',
            controller: 'DialogInstCtrl',
            size: 'lg',
            windowClass: 'app-modal-window',
            backdropClass: 'black-backdrop',
            resolve: {
                selectedItem: function() {
                    return estudante;
                }
            }
        });
        
        dialogInst.result.then(function(item) {
            me.id = item.id
            me.total = item.media.total
            me.saveMedia()
        });
      };

    //para usar no ng-init o metodo precisa ser sincrono, asssincrono não carrega os dados
    me.load = () => {
        $http.get(route)
        .then(response => { 
            me.list = ""
            if(me.tipoUsuario == 'Estudante') {
                me.list = response.data.usuarios.filter(x => x.id == $localStorage.usuario.id)
            } else {
                me.list = response.data.usuarios
            }
            
            me.id = ""
            me.mediaId = ""
            me.total = ""
        })
        .catch(err => { 
            console.log('====================')
            console.error(err)
        })
    }

    me.saveMedia = async () => {
     
        var media = { Total:me.total , usuarioId: me.id }
        var method = "post"
        var path = config.baseUrl + "/Media/estudante"

        if(me.mediaId){
            method = "put"
            path += `/${me.id}`
        }

        try {
            var response = await $http[method](path, media)                
            await me.load()                
        } catch (error) {
            console.log(error)
        }
    }

    me.remove = async(id) => {
        try {
            var response = await $http.delete(route + `/${id}`)
            await me.load()
        } catch (error) {
            console.log(error)
        }
    }



});