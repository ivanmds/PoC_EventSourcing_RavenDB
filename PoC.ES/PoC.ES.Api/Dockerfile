FROM mcr.microsoft.com/dotnet/core/aspnet:2.2-stretch-slim AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/core/sdk:2.2-stretch AS build
WORKDIR /src
COPY ["PoC.ES.Api/PoC.ES.Api.csproj", "PoC.ES.Api/"]
RUN dotnet restore "PoC.ES.Api/PoC.ES.Api.csproj"
COPY . .
WORKDIR "/src/PoC.ES.Api"
RUN dotnet build "PoC.ES.Api.csproj" -c Release -o /app

FROM build AS publish
RUN dotnet publish "PoC.ES.Api.csproj" -c Release -o /app

FROM base AS final
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "PoC.ES.Api.dll"]
