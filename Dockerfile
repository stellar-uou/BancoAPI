# Stage 1: Build
FROM mcr.microsoft.com/dotnet/sdk:10.0 AS build
WORKDIR /app
ENV ASPNETCORE_URLS=http://+:8080

# Copia os arquivos de projeto e restaura pacotes
COPY *.csproj ./
RUN dotnet restore

# Copia todo o código e publica
COPY . ./
RUN dotnet publish -c Release -o out

# Stage 2: Runtime
FROM mcr.microsoft.com/dotnet/aspnet:10.0 AS runtime
WORKDIR /app

# Copia os arquivos publicados da stage de build
COPY --from=build /app/out ./

# Expõe a porta padrão do Kestrel (pode ser alterada se você quiser)
EXPOSE 8080

# Define a entrada da aplicação
ENTRYPOINT ["dotnet", "BancoAPI.dll"]
