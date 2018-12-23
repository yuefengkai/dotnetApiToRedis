FROM microsoft/dotnet:2.1-aspnetcore-runtime AS base
WORKDIR /app
EXPOSE 80

FROM microsoft/dotnet:2.1-sdk AS build
WORKDIR /src
COPY dotnetApiToRedis/dotnetApiToRedis.csproj dotnetApiToRedis/
RUN dotnet restore dotnetApiToRedis/dotnetApiToRedis.csproj
COPY . .
WORKDIR /src/dotnetApiToRedis
RUN dotnet build dotnetApiToRedis.csproj -c Release -o /app

FROM build AS publish
RUN dotnet publish dotnetApiToRedis.csproj -c Release -o /app

FROM base AS final
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "dotnetApiToRedis.dll"]
