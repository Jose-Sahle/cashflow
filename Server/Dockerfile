# Use a imagem oficial do .NET SDK para construir a aplicação
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
# Defina o diretório de trabalho
WORKDIR /src
# Copie o arquivo de solução e os arquivos de projeto
COPY *.sln .
COPY API/*.csproj ./API/
COPY Application/*.csproj ./Application/
COPY Domain/*.csproj ./Domain/
COPY Infrastructure/*.csproj ./Infrastructure/
COPY Models/*.csproj ./Models/
# Restaure as dependências
RUN dotnet restore
# Copie o restante do código da aplicação
COPY . .
# Defina o diretório de trabalho para a API
WORKDIR /src/API
# Construa a aplicação
RUN dotnet publish -c Release -o /app/publish
# Use a imagem oficial do .NET Runtime para rodar a aplicação
FROM mcr.microsoft.com/dotnet/aspnet:8.0
# Defina o diretório de trabalho
WORKDIR /app
# Copie os arquivos construídos para o diretório de trabalho
COPY --from=build /app/publish .
# Copie e renomeie o arquivo de configuração para appsettings.json
COPY API/appsettings-docker.json /app/appsettings.json         
# Exponha a porta 80
EXPOSE 80

ENV DOTNET_EnableDiagnostics=0
# Comando para iniciar a aplicação
CMD ["dotnet", "BankTransactionService.API.dll"]