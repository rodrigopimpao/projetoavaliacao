'use strict';

const app = angular.module("app", ["ngStorage", "ngMaterial", "ngRoute", "ui.bootstrap", "ui.tinymce", "ngSanitize", "ui.utils.masks"]);

app.factory('AuthInterceptor', function ($window, $q, $localStorage) {
    return {
        request: function(config) {
          config.headers = config.headers || {};
          if(typeof $localStorage.usuario !== 'undefined' && $localStorage.usuario.token !== '') {
                config.headers['Authorization'] = 'Bearer ' + $localStorage.usuario.token;
            }
    
          return config;
        },
    
        responseError: function(response) {
          if (response.status === 401 || response.status === 403) {
            delete $localStorage.usuario
            $window.location.href = '/index.html';
          }
    
          return $q.reject(response);
        }
      }
});


// Register the previously created AuthInterceptor.
app.config(function ($httpProvider) {
    $httpProvider.interceptors.push('AuthInterceptor');
});
  
app.run(function ($rootScope, $location, $window, $localStorage) {
    $rootScope.$on('$routeChangeStart', function (event, next, current) {
      if (next.authorize) {
        if(!(typeof $localStorage.usuario !== 'undefined' && $localStorage.usuario.token !== '')) {
          $rootScope.$evalAsync(function () {
            //$window.location.href = '/index.html';
        })
        }
      }
    });

  });

/**
 * Como possuímos a variavel app definida acima com a inicialização do Angular
 * através do app.config conseguimos criar as configurações
 * definimos que essa configuração depende do $routeProvider e usamos na function($routeProvider)
 */
app.config(['$routeProvider', function ($routeProvider) {
    $routeProvider

        // aqui dizemos quando estivernos na url / vamos carregar o conteúdo da pagina inicila a home
        // no segundo parametro definimos um objeto contendo o templateUrl da nossa pagina home e o controller que irá
        // preparar o conteudo e processar outros eventos da página que veremos posteriormente
        //.when("/", {templateUrl: "templates/home.html", controller: "HomeCtrl"})
        // configuração das rotas bem parecidas para as outras paginas
        .when("/", {templateUrl: "templates/avaliacoes.html", controller: "AvaliacoesCtrl", authorize: true})
        .when("/avaliacao/:id", {templateUrl: "templates/avaliacao.html", controller: "AvaliacaoCtrl", authorize: true})
        .when("/estudante/:id", {templateUrl: "templates/estudante.html", controller: "EstudanteCtrl", authorize: true})
        .when("/estudantes", {templateUrl: "templates/estudantes.html", controller: "EstudantesCtrl"})
        /* aqui você pode adicionar rotas para outras paginas que desejar criar */
        // por último dizemos se nenhuma url digitada for encontrada mostramos a página 404 que não existe no nosso servidor
        .when('/404', {templateUrl: "templates/404.html"})
        .otherwise("/404");
}]);
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

    //para usar no ng-init o metodo precisa ser sincrono, asssincrono nÃ£o carrega os dados
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
(() => {

    app.controller('CustomerController', ($scope, $http, config) => {

        const route = config.baseUrl + "/customer"    
        let me = $scope

        me.name = ""
        me.email = ""
        me.list = []
        
        //para usar no ng-init o metodo precisa ser sincrono, asssincrono nÃ£o carrega os dados
        me.load = () => {
            $http.get(route)
            .then(response => { 
                me.list = response.data.data 
                me.name = ""
                me.email = ""
                me.id = ""
            })
            .catch(err => { console.error(err) })            
        }

        me.edit = (id) => {
            var customer = me.list.find(e => e.id == id)
            me.name = customer.name
            me.email = customer.email
            me.id = id
        }

        me.save = async () => {
            
            var customer = { name:me.name , email : me.email }
            var method = "post"
            var path = route

            if(me.id){
                method = "put"
                path += `/${me.id}`
            }

            var customer = { name:me.name , email : me.email }

            try {
                var response = await $http[method](path, customer)                
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
    })
})();
app.controller('DialogInstCtrl', function($scope, $uibModalInstance, selectedItem) {
    $scope.item = selectedItem;
    
    $scope.submitItem = function() {
      $uibModalInstance.close($scope.item);
    };
    
    $scope.cancel = function() {
      $uibModalInstance.dismiss('cancel');
    };
  });
app.controller('EditDialogInstCtrl', function($scope, $uibModalInstance, selectedItem){
    $scope.item = selectedItem;

    $scope.save = function(){
      $uibModalInstance.close($scope.item);
    };
    
    $scope.cancel = function(){
      $uibModalInstance.dismiss('cancel');
    }
  });
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

    //para usar no ng-init o metodo precisa ser sincrono, asssincrono nÃ£o carrega os dados
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
(() => {

    app.controller('LoginController', ($scope, $http, $localStorage, $window, config) => {

        const route = config.baseUrl + "/Usuario"    
        let me = $scope

        me.login = ""
        me.senha = ""
        me.funcao = "Estudante"
        me.token = ""

        me.alerts = [
        ];
    
        me.addAlert = function(mensagem) {
            me.alerts.push({ type: 'danger', msg: mensagem });
        };
    
        me.closeAlert = function(index) {
            me.alerts.splice(index, 1);
        };

        me.logar = () => {
            var data = { login : me.login, senha : me.senha }
            var path = route + '/login'
            
            try {
                $http.post(path, data)                
                .then(response => { 
                    var usuario = {
                        nome: response.data.nome,
                        funcao: response.data.funcao,
                        token: response.data.token,
                        id: response.data.id
                    };
                    
                   $localStorage.usuario = usuario;
                   me.redirect()
                })
                .catch(err => { 
                    console.error(err)
                    me.addAlert("Falha ao logar");
                    //$localStorage.message = "Falha ao logar";
                    //'alert-warning'
                    //'alert-danger'
                    //'alert-success'
                })     
                
            } catch (error) {
                console.log(error)
            }
        }

        me.save = async () => {
         
            var usuario = { nome:me.nome , email:me.email, CPF:me.cpf, login:me.login, senha:me.senha, funcao:me.funcao }
            
            var method = "post"
            var path = route

            if(me.id){
                method = "put"
                path += `/${me.id}`
            }

            try {
                var response = await $http[method](path, usuario)
                $localStorage.message = "Usuario Salvo!!"
                $window.location.href = '/index.html';
            } catch (error) {
                me.addAlert("Falha ao cadastrar");
                console.log(error)
            }
        }
        me.logout = () => {
            delete $localStorage.usuario
            $window.location.href = '/index.html';
        }

        me.load = () => {
            me.message = $localStorage.message
            me.redirect() 
        }
        me.redirect = () => {
            if(typeof $localStorage.usuario !== 'undefined' && $localStorage.usuario.token !== '') {
                if($localStorage.usuario.funcao == 'Estudante') {
                    //rediret to Estudante
                    $window.location.href = '/home.html';
                } else {
                    if($localStorage.usuario.funcao == 'Professor') {
                        //rediret to Professor
                        $window.location.href = '/home.html';
                    }
                }
            }
        }
    })
})();
(() => {

    app.controller('UsuarioController', ($scope, $http, $localStorage, config) => {

        const route = config.baseUrl + "/Usuario"    
        let me = $scope

        me.nome = ""
        me.email = ""
        me.cpf = ""
        me.login = ""
        me.senha = ""
        me.funcao = ""
        me.list = []
        
        //para usar no ng-init o metodo precisa ser sincrono, asssincrono nÃ£o carrega os dados
        me.load = () => {
            $http.get(route)
            .then(response => { 
                me.list = response.data.usuarios 
                me.nome = ""
                me.email = ""
                me.cpf = ""
                me.login = ""
                me.senha = ""
                me.funcao = ""
                me.id = ""
            })
            .catch(err => { console.error(err) })            
        }

        me.edit = (id) => {
            var usuario = me.list.find(e => e.id == id)
            me.nome = usuario.nome
            me.email = usuario.email
            me.cpf = usuario.cpf
            me.login = usuario.login
            me.senha = usuario.senha
            me.funcao = usuario.funcao
            me.id = id
        }

        me.save = async () => {
         
            var usuario = { nome:me.nome , email : me.email, CPF : me.cpf, login : me.login, senha : me.senha, funcao : me.funcao }
            var method = "post"
            var path = route

            if(me.id){
                method = "put"
                path += `/${me.id}`
            }

            try {
                var response = await $http[method](path, usuario)                
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
    })
})();
(() => {
	app.value('config', {
		baseUrl: 'http://localhost:5000/api'
	})
})();