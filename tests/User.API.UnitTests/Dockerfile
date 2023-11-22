FROM mcr.microsoft.com/dotnet/runtime:7.0 AS base
WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["tests/User.API.UnitTests/User.API.UnitTests.csproj", "tests/User.API.UnitTests/"]
COPY ["src/User/User.API/User.API.csproj", "src/User/User.API/"]
COPY ["src/User/User.Domain/User.Domain.csproj", "src/User/User.Domain/"]
COPY ["src/Shared/Shared/Shared.csproj", "src/Shared/Shared/"]
RUN dotnet restore "tests/User.API.UnitTests/User.API.UnitTests.csproj"
COPY . .
WORKDIR "/src/tests/User.API.UnitTests"
RUN dotnet build "User.API.UnitTests.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "User.API.UnitTests.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "User.API.UnitTests.dll"]
