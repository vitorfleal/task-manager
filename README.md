# Gerenciador de Tarefas

Este é um exemplo de projeto sobre como implementar uma API para gerenciamento de tarefas.

## Tecnologias

- `aspnet core 8`
- `sql server`
- `ef core 9`


## Preparando o ambiente

```bash
# Run migrations
docker compose up migrations

# Run tests 
docker compose up run-tests

# Run service
docker compose up api
```

## Informações

Esta solução contém uma API com rotinas para efetuar o CRUD completo referente a gerenciamento de tarefas.

### URLs
- Api: http://localhost:5001/swagger