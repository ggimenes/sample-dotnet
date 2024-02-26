#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER app
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["SampleDotnet.Store.Api/SampleDotnet.Store.Api.csproj", "SampleDotnet.Store.Api/"]
RUN dotnet restore "./SampleDotnet.Store.Api/SampleDotnet.Store.Api.csproj"
COPY . .
WORKDIR "/src/SampleDotnet.Store.Api"
RUN dotnet build "./SampleDotnet.Store.Api.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./SampleDotnet.Store.Api.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "SampleDotnet.Store.Api.dll"]