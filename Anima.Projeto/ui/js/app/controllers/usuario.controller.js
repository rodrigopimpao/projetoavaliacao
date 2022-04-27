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
        
        //para usar no ng-init o metodo precisa ser sincrono, asssincrono não carrega os dados
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