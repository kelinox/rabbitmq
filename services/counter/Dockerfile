FROM microsoft/dotnet:2.2-aspnetcore-runtime AS base
WORKDIR /app
EXPOSE 80

FROM microsoft/dotnet:2.2-sdk AS build
WORKDIR /src
COPY ["counter.csproj", "./"]
RUN dotnet restore "./counter.csproj"
COPY . .
WORKDIR /src
RUN dotnet build "counter.csproj" -c Debug -o /app

FROM build AS publish
RUN dotnet publish "counter.csproj" -c Debug -o /app

FROM base AS final
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "counter.dll"]
