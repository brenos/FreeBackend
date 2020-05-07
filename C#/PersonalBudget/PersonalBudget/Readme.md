default
Startup.cs onde fica a conexão com o banco de dados

Usei o ApiVersioning


mac / linux
dotnet build - baixa pacotes
dotnet ef database update
*alterar context, adicionar ou remover colunas ou tabelas
	dotnet ef migrations add AddProductReviews


windows
update-database
*alterar context, adicionar ou remover colunas ou tabelas
	add-migration AddProductReviews

referencias
https://docs.microsoft.com/en-us/ef/core/managing-schemas/migrations/?tabs=dotnet-core-cli