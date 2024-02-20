# TheNewYorkTimes

## 🚀 Começando

Essas instruções permitirão que você obtenha uma cópia do projeto em operação na sua máquina local para fins de desenvolvimento e teste.

## 📋 Objetivo

Criar uma API que cadastre o usuario e faça o login retornando um token de autenticação para acessar os endpoints restritos da API. O usuário autenticado poderá cadastrar uma nova noticia, consultar uma notícia específica ou listar todas as noticias cadastradas. Apenas os usuários cadastrados com o perfil administrador conseguem listar todos os usuários cadastrados ou consultar um usuário específico.

## 🛠️ Construído com

Abaixo estão listadas as ferramentas utilizadas para o desenvolvimento dos projetos

### 📌 IDE's utilizadas:
- Visual Studio 2022

### 📌 Frameworks utilizados:

- Docker
- Kubernetes
- AKS
- Azure Database SQL

#### Backend 
- .Net Core 7.0 

#### Versionamento
- Git Hub

### 🔧 Instalação

#### Clone do projeto

Para clonar o projeto do repositório remoto para o repositório local, execute o comando abaixo.

"git clone -b develop https://github.com/smmota/TheNewYorkTimesWebApi.git"

Para realizar alterações no projeto, após clone do repositório remoto, crie sua branch local utilizando o prefixo "feature/nome_da_branch"

#### Executando o projeto backend no Visual Studio 2022

Na pasta criada após clone é possível visualizar todos os arquivos do projeto. Para executar o projeto backend utilizando a IDE do Visual Studio 2022, efetue o duplo clique no arquivo da solução "TheNewYorkTimes.sln" e aguarde o carregamento.

Com a solução carregada na IDE, altere o projeto StartUp para "TheNewYorkTimes" e clique no botão "https".

A solução será compilada e o swaager da Web API será carregado no browser

## ⚙️ Executando os testes

Para rodar os testes unitários e testes de integração utilize a ide do visual studio ou execute o comando "dotnet test" no Package Manager Console.

Os testes foram realizados com dados mock para validar se os dados de integração e unitarios foram realizados corretamente, se os parâmetros de entrada estao corretos e se as validações funcionam.