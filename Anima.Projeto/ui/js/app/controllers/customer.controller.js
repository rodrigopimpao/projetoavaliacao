(() => {

    app.controller('CustomerController', ($scope, $http, config) => {

        const route = config.baseUrl + "/customer"    
        let me = $scope

        me.name = ""
        me.email = ""
        me.list = []
        
        //para usar no ng-init o metodo precisa ser sincrono, asssincrono não carrega os dados
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