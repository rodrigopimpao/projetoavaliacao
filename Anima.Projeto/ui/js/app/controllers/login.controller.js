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