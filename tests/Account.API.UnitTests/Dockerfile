FROM mcr.microsoft.com/dotnet/runtime:7.0 AS base
WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["tests/Account.API.UnitTests/Account.API.UnitTests.csproj", "tests/Account.API.UnitTests/"]
COPY ["src/Account/Account.API/Account.API.csproj", "src/Account/Account.API/"]
COPY ["src/Shared/Shared/Shared.csproj", "src/Shared/Shared/"]
COPY ["src/Account/Account.Domain/Account.Domain.csproj", "src/Account/Account.Domain/"]
RUN dotnet restore "tests/Account.API.UnitTests/Account.API.UnitTests.csproj"
COPY . .
WORKDIR "/src/tests/Account.API.UnitTests"
RUN dotnet build "Account.API.UnitTests.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Account.API.UnitTests.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Account.API.UnitTests.dll"]
