# Projeto Avaliação Ecoa

## Diagrama Entidade Relacionamento(ER) da aplicação

https://github.com/rodrigopimpao/projetoavaliacao/tree/main/Docs

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

### Realizar Login Professor / Open
Pegar o token para uso em requests de função 'Professor'

```console
curl --request POST \
  --url http://localhost:5000/api/Usuario/login \
  --header 'Content-Type: application/json' \
  --data '{
	"login": "professor",
	"senha": "12345"
}'
```

### Realizar Login Estudante / Open
Pegar o token para uso em requests de função 'Estudante'

```console
curl --request POST \
  --url http://localhost:5000/api/Usuario/login \
  --header 'Content-Type: application/json' \
  --data '{
	"login": "estudante",
	"senha": "12345"
}'
```
### Listar Usuários / TOKEN Professor
```console
curl --request GET \
  --url http://localhost:5000/api/Usuario \
  --header 'Authorization: Bearer <TOKEN>'
```

### Busca Estudante por Id / TOKEN Professor
```console
curl --request GET \
  --url http://localhost:5000/api/Estudante/<ID_ESTUDANTE> \
  --header 'Authorization: Bearer <TOKEN>'
```
### Busca Professor por Id / TOKEN Professor
```console
curl --request GET \
  --url http://localhost:5000/api/Professor/<ID_PROFESSOR> \
  --header 'Authorization: Bearer <TOKEN>'
```
### Criar Avaliação / TOKEN Professor
```console
curl --request POST \
  --url http://localhost:5000/api/Avaliacao \
  --header 'Authorization: Bearer <TOKEN>' \
  --header 'Content-Type: application/json' \
  --data '{
	"nome": "Avaliação 1",
	"descricao": "Avaliação de Matemática"
}'
```
### Lista de avaliações / TOKEN Professor
```console
curl --request GET \
  --url http://localhost:5000/api/Avaliacao \
  --header 'Authorization: Bearer <TOKEN>'
```
### Criar Questão / TOKEN Professor
avaliacaoId = Id do retorno da criação da avaliação ou listagem
```console
curl --request POST \
  --url http://localhost:5000/api/Questao \
  --header 'Authorization: Bearer <TOKEN>' \
  --header 'Content-Type: application/json' \
  --data '{
	"enunciado": "Marque a alternativa correta: ",
	"avaliacaoId": "<avaliacaoId>"
}'
```
### Lista de questões / TOKEN Professor
```console
curl --request GET \
  --url http://localhost:5000/api/Questao \
  --header 'Authorization: Bearer <TOKEN>'
```
### Criar Alternativa / TOKEN Professor
questaoId = Id do retorno da criação da questão ou listagem
```console
curl --request POST \
  --url http://localhost:5000/api/Alternativa \
  --header 'Authorization: Bearer <TOKEN>' \
  --header 'Content-Type: application/json' \
  --data '{
	"descricao": "1 + 1 = 2",
	"questaoId": "<questaoId>",
	"correta": true
}'
```
### Busca Avaliação / TOKEN Professor
avaliacaoId = Id do retorno da criação da avaliação ou listagem
```console
curl --request GET \
  --url http://localhost:5000/api/Avaliacao/<avaliacaoId> \
  --header 'Authorization: Bearer <TOKEN>'
```

### Responta questão da avaliação / TOKEN Estudante
usuarioId = Id Listagem de Usuários
alternativaId e questaoId = retorno da busca de Avaliação
```console
curl --request POST \
  --url http://localhost:5000/api/RespostaEstudante \
  --header 'Authorization: Bearer <TOKEN>' \
  --header 'Content-Type: application/json' \
  --data '{
	"UsuarioId": "<usuarioId>",
	"AlternativaId": "<alternativaId>",
	"QuestaoId": "<questaoId>"
}'
```

### Resposta do Estudante por Questão / TOKEN Professor
usuarioId = Id do estudante
questoaId = Id da questão cadastrada na avaliação
```console
curl --request GET \
  --url http://localhost:5000/api/RespostaEstudante/<usuarioId>/<questaoId> \
  --header 'Authorization: Bearer <TOKEN>'
```

### Postar nota do estudante / TOKEN Professor
estudanteId = Id do estudante que respondeu avaliação
avaliacaoId = Id da avaliação respondida
```console
curl --request POST \
  --url http://localhost:5000/api/Nota \
  --header 'Authorization: Bearer <TOKEN>' \
  --header 'Content-Type: application/json' \
  --data '{
	"Valor": "9.1",
	"EstudanteId" : "<estudanteId>",
	"AvaliacaoId" : "<avaliacaoId>"
}'
```

### Postar media do estudante / TOKEN Professor
usuarioId = Id do estudante
```console
curl --request POST \
  --url http://localhost:5000/api/Media \
  --header 'Authorization: Bearer <TOKEN>' \
  --header 'Content-Type: application/json' \
  --data '{
	"Total": "9.1",
	"usuarioId" : "<usuarioId>"
}'
```


### Busca Nota do estudante / TOKEN Professor ou Estudante
estudanteId = Id do estudante com notas
```console
curl --request GET \
  --url http://localhost:5000/api/Nota/estudante/<estudanteId> \
  --header 'Authorization: Bearer <TOKEN>'
```
notaId = Id da nota postada anteriormente pelo professor
```console
curl --request GET \
  --url http://localhost:5000/api/Nota/<notaId> \
  --header 'Authorization: Bearer <TOKEN>'
```

### Buscar Media do Estudante / TOKEN Professor ou Estudante
estudanteId = Id do estudante com media
```console
curl --request GET \
  --url http://localhost:5000/api/Media/estudante/<estudanteId> \
  --header 'Authorization: Bearer <TOKEN>'
```
mediaId = Id da media postada anteriormente pelo professor
```console
curl --request GET \
  --url http://localhost:5000/api/Media/<mediaId> \
  --header 'Authorization: Bearer <TOKEN>'
```
### Mudar resposta de questão / TOKEN Estudante
estudanteId = Id do estudante que respondeu questão

questaoId = Id da questão respondida

alternativaId = Id da alternativa nova
```console
curl --request PUT \
  --url http://localhost:5000/api/RespostaEstudante/<estudanteId>/<questaoId> \
  --header 'Authorization: Bearer <TOKEN>' \
  --header 'Content-Type: application/json' \
  --data '{
	"AlternativaId": "<alternativaId>"
}'
```

### Mudar nota do aluno / TOKEN Professor
notaId = Id da nota postada pelo professor
```console
curl --request PUT \
  --url http://localhost:5000/api/Nota/57487271-4846-45f6-af11-f8c11c3df1d3 \
  --header 'Authorization: Bearer eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJ1bmlxdWVfbmFtZSI6IlByb2Zlc3NvciAxIiwicm9sZSI6IlByb2Zlc3NvciIsIm5iZiI6MTY0ODkyNjM1NiwiZXhwIjoxNjQ4OTI5OTU2LCJpYXQiOjE2NDg5MjYzNTZ9.cIQEQnQPHrT4CwgdKUXMW-4AVeC5CSVXV_oduqOq1jc' \
  --header 'Content-Type: application/json' \
  --data '{
	"Valor": "9.1"
}'
```
estudanteId = Id do estudante que deseja mudar a nota
avaliacaoId = Id da avaliação ao qual a nota pertence
```console
curl --request PUT \
  --url http://localhost:5000/api/Nota/<estudanteId>/<avaliacaoId> \
  --header 'Authorization: Bearer <TOKEN>' \
  --header 'Content-Type: application/json' \
  --data '{
	"Valor": "9.1"
}'
```
### Mudar media do aluno / TOKEN Professor
mediaId = Id da media cadastrada anteriormente

```console
curl --request PUT \
  --url http://localhost:5000/api/Media/<mediaId> \
  --header 'Authorization: Bearer <TOKEN>' \
  --header 'Content-Type: application/json' \
  --data '{
	"Total": "8.1"
}'
```
estudanteId = Id do estudante que deseja mudar a média
```console
curl --request PUT \
  --url http://localhost:5000/api/Media/estudante/<estudanteId> \
  --header 'Authorization: Bearer <TOKEN>' \
  --header 'Content-Type: application/json' \
  --data '{
	"Total": "9.1"
}'
```

