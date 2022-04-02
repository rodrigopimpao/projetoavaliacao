# Projeto Avaliação Ecoa
## OpenApi
http://localhost:5000/swagger/v1/swagger.json

## Api Request

### Criar Professor / Open

```console
curl --request POST \
  --url http://localhost:5000/api/Usuario \
  --header 'Content-Type: application/json' \
  --data '{
	"nome": "Professor 1",
	"email": "professor@nobody.com.br",
	"CPF": "48951624005",
	"login": "professor",
	"senha": "12345",
	"funcao": "Professor"
}'
```

### Criar Estudante / Open

```console
curl --request POST \
  --url http://localhost:5000/api/Usuario \
  --header 'Content-Type: application/json' \
  --data '{
	"nome": "Estudante 1",
	"email": "estudante@nobody.com.br",
	"CPF": "41271103095",
	"login": "estudante",
	"senha": "12345",
	"funcao": "Estudante"
}'
```

