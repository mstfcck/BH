FROM mcr.microsoft.com/dotnet/runtime:7.0 AS base
WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["tests/Transaction.API.UnitTests/Transaction.API.UnitTests.csproj", "tests/Transaction.API.UnitTests/"]
COPY ["src/Transaction/Transaction.API/Transaction.API.csproj", "src/Transaction/Transaction.API/"]
COPY ["src/Transaction/Transaction.Domain/Transaction.Domain.csproj", "src/Transaction/Transaction.Domain/"]
COPY ["src/Shared/Shared/Shared.csproj", "src/Shared/Shared/"]
RUN dotnet restore "tests/Transaction.API.UnitTests/Transaction.API.UnitTests.csproj"
COPY . .
WORKDIR "/src/tests/Transaction.API.UnitTests"
RUN dotnet build "Transaction.API.UnitTests.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Transaction.API.UnitTests.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Transaction.API.UnitTests.dll"]
