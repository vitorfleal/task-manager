# Gerenciador de Tarefas

Este é um exemplo de projeto sobre como implementar uma API para gerenciamento de tarefas.

## Tecnologias

- `aspnet core 6`
- `sql server`
- `ef core 7`


## Preparando o ambiente

```bash
# Run migrations
docker compose up migrations

# Run tests 
docker compose up run-tests

# Run service
docker compose up -d app-development
```


## Informações

Esta solução contém uma API com rotinas para efetuar o CRUD completo referente a gerenciamento de tarefas, na etapa "Run migrations" de "Preparando o Ambiente" é efetuado um Seed na base dados, o mesmo cadastrará os três Status da tarefa durante a criação do banco de dados.
Para efetuar o cadastro de uma tarefa é necessário executar a rota  v1/status que é verbo http GET para obter um identificador válido do status que será vinculado a tarefa na etapa de cadastro.

### URLs
- Api: http://localhost:5000/swagger/