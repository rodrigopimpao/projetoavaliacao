app.controller('AvaliacoesCtrl', function ($scope, $location, $http, $localStorage, $routeParams, $uibModal, config) {
    
    //alert($routeParams.id)
    //alert($localStorage.usuario.token)

    

    const route = config.baseUrl + "/Avaliacao"
    let me = $scope
    me.user = ""
    if(typeof $localStorage.usuario !== 'undefined' && $localStorage.usuario.token !== '') {
        me.user = $localStorage.usuario.nome
        me.tipoUsuario = $localStorage.usuario.funcao
    }
    
    me.nome = ""
    me.descricao = ""
    me.id = ""
    me.list = []
    
    me.isAvaliado = false

    

    me.editAvaliacao = function(avaliacao) {
        var dialogInst = $uibModal.open({
            ariaLabelledBy: 'modal-title',
            ariaDescribedBy: 'modal-body',
            templateUrl: 'views/avaliacao/edit-item.html',
            size: "lg",
            controller: 'EditDialogInstCtrl',
            windowClass: 'app-modal-window',
            backdropClass: 'black-backdrop',
          resolve: {
            selectedItem: function() {
              return avaliacao;
            }
          }
        });
    
        dialogInst.result.then(function (avaliacao) {
            //$ctrl.selected = selectedItem;
            me.id = avaliacao.id
            me.nome = avaliacao.nome
            me.descricao = avaliacao.descricao
            me.save()
        }, function () {
            me.load()
            //$log.info('Modal dismissed at: ' + new Date());
        });

    };


    me.addAvaliacao = function() {
        me.item = [];
        me.item = {nome: '', descricao: ''};
        var dialogInst = $uibModal.open({
            ariaLabelledBy: 'modal-title',
            ariaDescribedBy: 'modal-body',
            templateUrl: 'views/avaliacao/add-item.html',
            controller: 'DialogInstCtrl',
            size: 'lg',
            windowClass: 'app-modal-window',
            backdropClass: 'black-backdrop',
            resolve: {
                selectedItem: function() {
                    return me.item;
                }
            }
        });
        
        dialogInst.result.then(function(newAvaliacao) {
            me.nome = newAvaliacao.nome
            me.descricao = newAvaliacao.descricao
            me.save()
        }, function () {
            //$log.info('Modal dismissed at: ' + new Date());
        });
      };

    me.respondida = (avaliacaoId) => {

        var isAvaliado = false
        var avaliacao = me.list.filter(function (el)
        {
            return el.id == avaliacaoId;
        }
        );

        if(avaliacao){
            if(avaliacao[0].notas) {
                if(avaliacao[0].notas.length > 0) {
                    isAvaliado = true
                }
            }
        }
        
        
        return isAvaliado
    }

    //para usar no ng-init o metodo precisa ser sincrono, asssincrono não carrega os dados
    me.load = () => {
        $http.get(route)
        .then(response => { 
            me.list = response.data.avaliacaos
            me.nome = ""
            me.descricao = ""
            me.id = ""
        })
        .catch(err => { 
            console.error(err)
        })
    }

    me.save = async () => {
     
        var avaliacao = { nome:me.nome , descricao: me.descricao }
        
        var method = "post"
        var path = route

        if(me.id){
            method = "put"
            path += `/${me.id}`
        }

        try {
            var response = await $http[method](path, avaliacao)                
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