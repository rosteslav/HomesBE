#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER app
WORKDIR /app
EXPOSE 5220

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["src/Auth/Auth.Api/Auth.Api.csproj", "src/Auth/Auth.Api/"]
COPY ["src/Auth/Auth.Infrastucture/Auth.Infrastructure.csproj", "src/Auth/Auth.Infrastucture/"]
COPY ["src/Common/Common.csproj", "src/Common/"]
COPY ["src/Auth/Auth.Application/Auth.Application.csproj", "src/Auth/Auth.Application/"]
COPY ["src/Auth/Auth.Domain/Auth.Domain.csproj", "src/Auth/Auth.Domain/"]
RUN dotnet restore "./src/Auth/Auth.Api/./Auth.Api.csproj"
COPY . .
WORKDIR "/src/src/Auth/Auth.Api"
RUN dotnet build "./Auth.Api.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./Auth.Api.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Auth.Api.dll"]