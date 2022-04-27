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
 * Como possu�mos a variavel app definida acima com a inicializa��o do Angular
 * atrav�s do app.config conseguimos criar as configura��es
 * definimos que essa configura��o depende do $routeProvider e usamos na function($routeProvider)
 */
app.config(['$routeProvider', function ($routeProvider) {
    $routeProvider

        // aqui dizemos quando estivernos na url / vamos carregar o conte�do da pagina inicila a home
        // no segundo parametro definimos um objeto contendo o templateUrl da nossa pagina home e o controller que ir�
        // preparar o conteudo e processar outros eventos da p�gina que veremos posteriormente
        //.when("/", {templateUrl: "templates/home.html", controller: "HomeCtrl"})
        // configura��o das rotas bem parecidas para as outras paginas
        .when("/", {templateUrl: "templates/avaliacoes.html", controller: "AvaliacoesCtrl", authorize: true})
        .when("/avaliacao/:id", {templateUrl: "templates/avaliacao.html", controller: "AvaliacaoCtrl", authorize: true})
        .when("/estudante/:id", {templateUrl: "templates/estudante.html", controller: "EstudanteCtrl", authorize: true})
        .when("/estudantes", {templateUrl: "templates/estudantes.html", controller: "EstudantesCtrl"})
        /* aqui voc� pode adicionar rotas para outras paginas que desejar criar */
        // por �ltimo dizemos se nenhuma url digitada for encontrada mostramos a p�gina 404 que n�o existe no nosso servidor
        .when('/404', {templateUrl: "templates/404.html"})
        .otherwise("/404");
}]);