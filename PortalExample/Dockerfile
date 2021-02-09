#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/core/aspnet:3.1-bionic AS base
WORKDIR /app
EXPOSE 5000
EXPOSE 5001

FROM mcr.microsoft.com/dotnet/core/sdk:3.1-bionic AS build
WORKDIR /src
COPY PortalExample/PortalExample.csproj PortalExample/
RUN dotnet restore "PortalExample/PortalExample.csproj"
COPY . .
WORKDIR "/src/PortalExample"
RUN dotnet build "PortalExample.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "PortalExample.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "PortalExample.dll"]
