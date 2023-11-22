FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["src/Account/Account.API/Account.API.csproj", "src/Account/Account.API/"]
COPY ["src/Account/Account.Domain/Account.Domain.csproj", "src/Account/Account.Domain/"]
COPY ["src/Shared/Shared/Shared.csproj", "src/Shared/Shared/"]
RUN dotnet restore "src/Account/Account.API/Account.API.csproj"
COPY . .
WORKDIR "/src/src/Account/Account.API"
RUN dotnet build "Account.API.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Account.API.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Account.API.dll"]
