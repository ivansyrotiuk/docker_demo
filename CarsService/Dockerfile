FROM mcr.microsoft.com/dotnet/core/aspnet:2.2-stretch-slim AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/core/sdk:2.2-stretch AS build
WORKDIR /src
COPY ["CarsService/CarsService.csproj", "CarsService/"]
RUN dotnet restore "CarsService/CarsService.csproj"
COPY . .
WORKDIR "/src/CarsService"
RUN dotnet build "CarsService.csproj" -c Release -o /app

FROM build AS publish
RUN dotnet publish "CarsService.csproj" -c Release -o /app

FROM base AS final
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "CarsService.dll"]
