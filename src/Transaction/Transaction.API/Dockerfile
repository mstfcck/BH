FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["src/Transaction/Transaction.API/Transaction.API.csproj", "src/Transaction/Transaction.API/"]
COPY ["src/Transaction/Transaction.Domain/Transaction.Domain.csproj", "src/Transaction/Transaction.Domain/"]
COPY ["src/Shared/Shared/Shared.csproj", "src/Shared/Shared/"]
RUN dotnet restore "src/Transaction/Transaction.API/Transaction.API.csproj"
COPY . .
WORKDIR "/src/src/Transaction/Transaction.API"
RUN dotnet build "Transaction.API.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Transaction.API.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Transaction.API.dll"]
