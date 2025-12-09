#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER app
WORKDIR /app
EXPOSE 8080
#EXPOSE 8081

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["CarRental.Api/CarRental.Api.csproj", "CarRental.Api/"]
COPY ["CarRental.Application/CarRental.Application.csproj", "CarRental.Application/"]
COPY ["CarRental.Domain/CarRental.Domain.csproj", "CarRental.Domain/"]
COPY ["CarRental.Utility/CarRental.Utility.csproj", "CarRental.Utility/"]
COPY ["CarRental.Infrastructure/CarRental.Infrastructure.csproj", "CarRental.Infrastructure/"]
RUN dotnet restore "./CarRental.Api/CarRental.Api.csproj"
COPY . .
RUN dotnet test "./CarRental.Tests/CarRental.UnitTests.csproj" -c $BUILD_CONFIGURATION \
&& dotnet test "./CarRental.IntegrationTests/CarRental.IntegrationTests.csproj" -c $BUILD_CONFIGURATION

WORKDIR "/src/CarRental.Api"
RUN dotnet build "./CarRental.Api.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./CarRental.Api.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "CarRental.Api.dll"]