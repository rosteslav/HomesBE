#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER app
WORKDIR /app
EXPOSE 5222

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["src/Admins/Admins.Api/Admins.Api.csproj", "src/Admins/Admins.Api/"]
COPY ["src/Admins/Admins.Infrastructure/Admins.Infrastructure.csproj", "src/Admins/Admins.Infrastructure/"]
COPY ["src/Admins/Admins.Application/Admins.Application.csproj", "src/Admins/Admins.Application/"]
COPY ["src/Common/Common.csproj", "src/Common/"]
COPY ["src/Admins/Admins.Domain/Admins.Domain.csproj", "src/Admins/Admins.Domain/"]
RUN dotnet restore "./src/Admins/Admins.Api/./Admins.Api.csproj"
COPY . .
WORKDIR "/src/src/Admins/Admins.Api"
RUN dotnet build "./Admins.Api.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./Admins.Api.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Admins.Api.dll"]