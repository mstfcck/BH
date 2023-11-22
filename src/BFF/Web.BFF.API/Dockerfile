FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["src/BFF/Web.BFF.API/Web.BFF.API.csproj", "src/BFF/Web.BFF.API/"]
RUN dotnet restore "src/BFF/Web.BFF.API/Web.BFF.API.csproj"
COPY . .
WORKDIR "/src/src/BFF/Web.BFF.API"
RUN dotnet build "Web.BFF.API.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Web.BFF.API.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Web.BFF.API.dll"]
