FROM microsoft/dotnet:2.2-aspnetcore-runtime AS base
WORKDIR /app
EXPOSE 80

FROM microsoft/dotnet:2.2-sdk AS build
WORKDIR /src
COPY ["user/user.csproj", "user/"]
COPY ["core/core.csproj", "core/"]
RUN dotnet restore "./user/user.csproj"
COPY . .
WORKDIR /src
RUN dotnet build "./user/user.csproj" -c Debuf -o /app

FROM build AS publish
RUN dotnet publish "./user/user.csproj" -c Debug -o /app

FROM base AS final
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "./user.dll"]
