#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:5.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
WORKDIR /src
COPY ["SampleDotnet.Shipment.Api/SampleDotnet.Shipment.Api.csproj", "SampleDotnet.Shipment.Api/"]
RUN dotnet restore "SampleDotnet.Shipment.Api/SampleDotnet.Shipment.Api.csproj"
COPY . .
WORKDIR "/src/SampleDotnet.Shipment.Api"
RUN dotnet build "SampleDotnet.Shipment.Api.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "SampleDotnet.Shipment.Api.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "SampleDotnet.Shipment.Api.dll"]