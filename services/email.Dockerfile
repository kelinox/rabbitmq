FROM microsoft/dotnet:2.2-aspnetcore-runtime AS base
WORKDIR /app
EXPOSE 80

FROM microsoft/dotnet:2.2-sdk AS build
WORKDIR /src
COPY ["email/email.csproj", "email/"]
COPY ["core/core.csproj", "core/"]
COPY ["infrastructure/infrastructure.csproj", "infrastructure/"]
RUN dotnet restore "./email/email.csproj"
COPY . .
WORKDIR /src
RUN dotnet build "./email/email.csproj" -c Release -o /app

FROM build AS publish
RUN dotnet publish "./email/email.csproj" -c Release -o /app

FROM base AS final
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "./email.dll"]
