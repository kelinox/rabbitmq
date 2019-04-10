FROM microsoft/dotnet:2.2-aspnetcore-runtime AS base
WORKDIR /app
EXPOSE 80

FROM microsoft/dotnet:2.2-sdk AS build
WORKDIR /src
COPY ["workout/workout.csproj", "workout/"]
COPY ["core/core.csproj", "core/"]
COPY ["infrastructure/infrastructure.csproj", "infrastructure/"]
RUN dotnet restore "./workout/workout.csproj"
COPY . .
WORKDIR /src
RUN dotnet build "./workout/workout.csproj" -c Debuf -o /app

FROM build AS publish
RUN dotnet publish "./workout/workout.csproj" -c Debug -o /app

FROM base AS final
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "./workout.dll"]
