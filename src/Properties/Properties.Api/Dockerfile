#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER app
WORKDIR /app
EXPOSE 5221

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["src/Properties/Properties.Api/Properties.Api.csproj", "src/Properties/Properties.Api/"]
COPY ["src/Properties/Properties.Infrastructure/Properties.Infrastructure.csproj", "src/Properties/Properties.Infrastructure/"]
COPY ["src/Properties/Properties.Application/Properties.Application.csproj", "src/Properties/Properties.Application/"]
COPY ["src/Common/Common.csproj", "src/Common/"]
COPY ["src/Properties/Properties.Domain/Properties.Domain.csproj", "src/Properties/Properties.Domain/"]
RUN dotnet restore "./src/Properties/Properties.Api/./Properties.Api.csproj"
COPY . .
WORKDIR "/src/src/Properties/Properties.Api"
RUN dotnet build "./Properties.Api.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./Properties.Api.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Properties.Api.dll"]