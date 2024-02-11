#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER app
WORKDIR /app
EXPOSE 5223

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["src/Images/Images.Api/Images.Api.csproj", "src/Images/Images.Api/"]
COPY ["src/Common/Common.csproj", "src/Common/"]
COPY ["src/Images/Images.Infrastructure/Images.Infrastructure.csproj", "src/Images/Images.Infrastructure/"]
COPY ["src/Images/Images.Application/Images.Application.csproj", "src/Images/Images.Application/"]
COPY ["src/Images/Images.Domain/Images.Domain.csproj", "src/Images/Images.Domain/"]
RUN dotnet restore "./src/Images/Images.Api/./Images.Api.csproj"
COPY . .
WORKDIR "/src/src/Images/Images.Api"
RUN dotnet build "./Images.Api.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./Images.Api.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Images.Api.dll"]