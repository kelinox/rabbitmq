FROM microsoft/dotnet:2.2-aspnetcore-runtime AS base
WORKDIR /app
EXPOSE 80

FROM microsoft/dotnet:2.2-sdk AS build
WORKDIR /src
COPY ["counter/counter.csproj", "counter/"]
COPY ["core/core.csproj", "core/"]
RUN dotnet restore "./counter/counter.csproj"
COPY . .
WORKDIR /src
RUN dotnet build "./counter/counter.csproj" -c Debug -o /app

FROM build AS publish
RUN dotnet publish "./counter/counter.csproj" -c Debug -o /app

FROM base AS final
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "./counter.dll"]
