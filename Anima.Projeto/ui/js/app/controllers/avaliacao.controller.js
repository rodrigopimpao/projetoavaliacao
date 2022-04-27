app.controller('AvaliacaoCtrl', function ($scope, $location, $http, $localStorage, $routeParams, $uibModal, $window, config) {
    
    //alert($routeParams.id)
    //alert($localStorage.usuario.token)

    

    const route = config.baseUrl + "/Avaliacao/"+$routeParams.id
    let me = $scope


    me.tinymceOptions = {
        onChange: function(e) {
          // put logic here for keypress and cut/paste changes
        },
        width: 800,  // I *think* its a number and not '400' string
        height: 300,
        inline: false,
        plugins : 'advlist autolink link image lists charmap preview',
        skin: 'oxide',
        theme : 'silver'
      };
    
    me.enunciado = ""
    me.avaliacaoId = ""
    me.id = ""

    me.questaoId = ""
    me.descricao = ""
    me.correta = ""
    me.isAvaliado = false

    me.tipoUsuario = $localStorage.usuario.funcao
   
    me.load = () => {
        $http.get(route)
        .then(response => { 
            var avaliacao = response.data
            me.avaliacao = avaliacao
            
            if (me.tipoUsuario == 'Estudante') {
                var nota = avaliacao.notas.filter(function (el)
                {
                return el.usuarioId == $localStorage.usuario.id;
                }
                );
                if(nota.length > 0) {
                    me.isAvaliado = true
                }
            } else {
                if(avaliacao.notas.length > 0) {
                    me.isAvaliado = true
                }
            }

        })
        .catch(err => { 
            console.error(err)
        })
    }

    

    me.loadResposta = (questao) => {
        delete questao.respostaAlternativaId
        delete questao.respostaId
        var path = config.baseUrl + "/RespostaEstudante/"+$localStorage.usuario.id+"/"+questao.id
        
        $http.get(path)
        .then(response => { 
            questao.respostaAlternativaId = response.data.resposta.id
            questao.respostaId = response.data.id
        })
        .catch(err => { 
            console.error(err)
        })
    }

    me.saveResposta = async (questao) => {
        var resposta = { UsuarioId: $localStorage.usuario.id, AlternativaId: questao.respostaAlternativaId, QuestaoId: questao.id }
        var method = "post"
        
        var path = config.baseUrl + "/RespostaEstudante"

        if(questao.respostaId){
            method = "put"
            path += `/${$localStorage.usuario.id}/${questao.id}`
        }
        
        try {
            var response = await $http[method](path, resposta)                
            await me.load()                
        } catch (error) {
            console.log(error)
        }
    }


    me.loadRespostaPromise = (questao) => {
        var path = config.baseUrl + "/RespostaEstudante/"+$localStorage.usuario.id+"/"+questao.id
        return new Promise((resolve ,reject)=>{
            
            $http.get(path)
            .then(response => { 
                resolve(response.data.resposta.id);
            })
            .catch(err => { 
                console.error(err)
                reject(err)
            })
        });
    }


    me.finalizarEnvio = async (avaliacao) => {
  
        var resposta = { EstudanteId: $localStorage.usuario.id, AvaliacaoId: avaliacao.id, Valor: '' }
        var method = "post"
        
        var path = config.baseUrl + "/Nota"

        try {
            var response = await $http[method](path, resposta)                
            $window.location.href = '/home.html';             
        } catch (error) {
            console.log(error)
        }
    }

    me.editQuestao = function(questao) {
        var dialogInst = $uibModal.open({
            ariaLabelledBy: 'modal-title',
            ariaDescribedBy: 'modal-body',
            templateUrl: 'views/questao/edit-item.html',
            size: "lg",
            controller: 'EditDialogInstCtrl',
            windowClass: 'app-modal-window',
            backdropClass: 'black-backdrop',
          resolve: {
            selectedItem: function() {
              return questao;
            }
          }
        });
    
        dialogInst.result.then(function (questao) {
            //$ctrl.selected = selectedItem;
            me.id = questao.id
            me.enunciado = questao.enunciado
            me.avaliacaoId = ""
            me.questaoSave()
        }, function () {
            me.load()
            //$log.info('Modal dismissed at: ' + new Date());
        });

    };


    me.responderQuestao = function(questao) {
        var dialogInst = $uibModal.open({
            ariaLabelledBy: 'modal-title',
            ariaDescribedBy: 'modal-body',
            templateUrl: 'views/questao/responder.html',
            size: "lg",
            controller: 'EditDialogInstCtrl',
            windowClass: 'app-modal-window',
            backdropClass: 'black-backdrop',
          resolve: {
            selectedItem: function() {
                me.loadResposta(questao)
                return questao;
            }
          }
        });
    
        dialogInst.result.then(function (questao) {
            //$ctrl.selected = selectedItem;
            //me.id = questao.id
            //me.enunciado = questao.enunciado
            //me.avaliacaoId = $routeParams.id
            //me.questaoSave()

            me.saveResposta(questao)
            

        }, function () {
            me.load()
            //$log.info('Modal dismissed at: ' + new Date());
        });

    };


    me.addQuestao = function(avaliacao) {
        var dialogInst = $uibModal.open({
            ariaLabelledBy: 'modal-title',
            ariaDescribedBy: 'modal-body',
            templateUrl: 'views/questao/add-item.html',
            controller: 'DialogInstCtrl',
            size: 'lg',
            windowClass: 'app-modal-window',
            backdropClass: 'black-backdrop',
            resolve: {
                selectedItem: function() {
                    return avaliacao;
                }
            }
        });
        
        dialogInst.result.then(function(item) {
            me.avaliacaoId = item.id
            me.enunciado = item.enunciado
            me.questaoSave()
        }, function () {
            //$log.info('Modal dismissed at: ' + new Date());
        });
      };

      me.editAlternativa = function(alternativa) {
        
        var dialogInst = $uibModal.open({
            ariaLabelledBy: 'modal-title',
            ariaDescribedBy: 'modal-body',
            templateUrl: 'views/alternativa/edit-item.html',
            size: "lg",
            controller: 'EditDialogInstCtrl',
            windowClass: 'app-modal-window',
            backdropClass: 'black-backdrop',
          resolve: {
            selectedItem: function() {
              return alternativa;
            }
          }
        });
    
        dialogInst.result.then(function (alternativa) {
            me.id = alternativa.id
            
            me.questaoId = ""
            me.descricao = alternativa.descricao
            me.correta = alternativa.correta

            me.alternativaSave()
        }, function () {
            me.load()
            //$log.info('Modal dismissed at: ' + new Date());
        });

    };

    me.addAlternativa = function(questao) {
        var dialogInst = $uibModal.open({
            ariaLabelledBy: 'modal-title',
            ariaDescribedBy: 'modal-body',
            templateUrl: 'views/alternativa/add-item.html',
            controller: 'DialogInstCtrl',
            size: 'lg',
            windowClass: 'app-modal-window',
            backdropClass: 'black-backdrop',
            resolve: {
                selectedItem: function() {
                    return questao;
                }
            }
        });
        
        dialogInst.result.then(function(item) {
            me.questaoId = item.id
            me.descricao = item.descricao
            me.correta = item.correta
            me.alternativaSave()
        }, function () {
            //$log.info('Modal dismissed at: ' + new Date());
        });
      };

    me.questaoSave = async () => {
            
        var questao = { enunciado: me.enunciado, avaliacaoId: me.avaliacaoId }
        var method = "post"
        var path = config.baseUrl + "/Questao"

        if(me.id){
            method = "put"
            path += `/${me.id}`
        }

        try {
            var response = await $http[method](path, questao)                
            await me.load()                
        } catch (error) {
            console.log(error)
        }
    }

    me.alternativaSave = async () => {
        var alternativa = { descricao: me.descricao, questaoId: me.questaoId, correta: JSON.parse(me.correta) }
        var method = "post"
        var path = config.baseUrl + "/Alternativa"

        if(me.id){
            method = "put"
            path += `/${me.id}`
        }

        try {
            var response = await $http[method](path, alternativa)                
            await me.load()                
        } catch (error) {
            console.log(error)
        }
    }

});