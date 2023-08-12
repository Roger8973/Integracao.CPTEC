# API Integracao.CPTEC

## Objetivo

A API Integracao.CPTEC conta com endpoints para consumo de previsões climáticas para todos os aeroportos e cidades do Brasil.

## Base URL

```
https://aecintegracaocptec.azurewebsites.net
```

## Autenticação

Os métodos de autenticação consistem em dois endpoints, um para cadastro de usuário e outro para a autenticação em si. Também está disponível um endpoint para refresh-token.

## Endpoints

### Aeroportos

- **Obter condições climáticas de todos os aeroportos:**
  - **URL:** `/api/airport/getallairporttweatherforecasts`
  - **Método HTTP:** `GET`
  - **Descrição:** Obtém as condições climáticas atuais de todos os aeroportos do Brasil.
  - **Parâmetros:** Sem parâmetros.

- **Obter condições climáticas de um aeroporto específico:**
  - **URL:** `/api/airport/getweatherforecastairport?icaocode=SBAR`
  - **Método HTTP:** `GET`
  - **Descrição:** Obtém as condições climáticas atuais para um aeroporto específico.
  - **Parâmetros:** `ICAOCode -> string`

### Cidades

- **Obter informações de uma cidade pelo nome:**
  - **URL:** `/api/city/getcitybynome?cityname=Sao%20Paulo`
  - **Método HTTP:** `GET`
  - **Descrição:** Obtém as informações de uma cidade específica pelo seu nome.
  - **Parâmetros:** `CityName -> string`

- **Obter informações climáticas de uma cidade pelo ID:**
  - **URL:** `/api/city/getweatherforecastbycity?cityid=425`
  - **Método HTTP:** `GET`
  - **Descrição:** Obtém as informações climáticas de uma cidade específica pelo seu ID.
  - **Parâmetros:** `CityId -> int`

### Autenticação

- **Cadastrar usuário:**
  - **URL:** `/accounts/register`
  - **Método HTTP:** `POST`
  - **Descrição:** Cadastra o usuário para autenticação.
  - **Parâmetros:**
    ```json
    {
      "email": "user@example.com",
      "password": "Teste@123",
      "confirmPassword": "Teste@123"
    }
    ```

- **Autenticar usuário:**
  - **URL:** `/accounts/sign-in`
  - **Método HTTP:** `POST`
  - **Descrição:** Autentica o usuário e retorna o seu token de acesso juntamente com seu refresh-token.
  - **Parâmetros:**
    ```json
    {
      "email": "user@example.com",
      "password": "Teste@123"
    }
    ```

- **Refresh Token:**
  - **URL:** `/accounts/refresh-token`
  - **Método HTTP:** `POST`
  - **Descrição:** Obtém um novo token de acesso após a expiração do anterior.
  - **Parâmetros:**
    ```json
    {
      "refresh-token": "{seu token aqui}"
    }
    ```

## Banco de Dados

O banco de dados está em um servidor no Azure e está sendo utilizado o SQL Server.

## API

A API está em um App Service no Azure, e para deploy foi utilizado o Azure Piplines. A imagem Docker do projeto foi gerada no ACR (Azure Container Register) e lançada para o ACI (Azure Container Instances). Não foi utilizado Kubernetes para orquestração de containers visto a baixa disponibilidade que o sistema necessita. A string de conexão com o banco de dados está sendo passada pelo Azure Key Vault na geração do build da aplicação.

## Resumo

Para o desenvolvimento do projeto foram utilizados alguns dos princípios do Clean Architecture juntamente com alguns princípios do DDD (Domain-Driven-Desing). Foi utilizado o padrão CQRS (Command Query Responsibility Segregation) juntamente com Mediator para separação dos comandos e das queries. Para os testes unitários foi utilizado o X-Unit e também a biblioteca Fluent Assertions.
