FROM mcr.microsoft.com/dotnet/core/aspnet:2.2-stretch-slim AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/core/sdk:2.2-stretch AS build
WORKDIR /src
COPY ["CustomersService/CustomersService.csproj", "CustomersService/"]
RUN dotnet restore "CustomersService/CustomersService.csproj"
COPY . .
WORKDIR "/src/CustomersService"
RUN dotnet build "CustomersService.csproj" -c Release -o /app

FROM build AS publish
RUN dotnet publish "CustomersService.csproj" -c Release -o /app

FROM base AS final
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "CustomersService.dll"]
