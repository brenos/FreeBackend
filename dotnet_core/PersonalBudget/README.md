# Personal Budget Project

## Descrição

Projeto com a proposta de ser uma api de Orçamento Pessoal.

## Informações Técnicas

Api versionada conectando ao Banco de Dados, podendo usar uma conexão de réplica para as consultas.
<br/>
Na pasta <b>MySQL Model</b> contém o DER da base de dados.
<br/>
Na pasta <b>PostmanCollection</b> contém uma coleção do postman com todos os endpoints.

## Install

#### Mac / Linux
dotnet build
<br/>
dotnet ef database update

Obs.: Alterar context, adicionar ou remover colunas ou tabelas
- dotnet ef migrations add AddColumnTable

#### Windows (Command line)
update-database

Obs.: Alterar context, adicionar ou remover colunas ou tabelas
- add-migration AddColumnTable

## Stack
- .Net Core 3.1
- Entity Framework
- ApiVersioning
- NUnit


## Referencias
https://docs.microsoft.com/en-us/dotnet/core/introduction
<br/>
https://docs.microsoft.com/en-us/ef/core/managing-schemas/migrations/?tabs=dotnet-core-cli
<br/>
https://docs.microsoft.com/en-us/dotnet/core/testing/unit-testing-with-nunit
