#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/core/aspnet:3.1-buster-slim AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/core/sdk:3.1-buster AS build
WORKDIR /src
COPY ["PokeDexWeb.API/PokeDexWeb.API.csproj", "PokeDexWeb.API/"]
RUN dotnet restore "PokeDexWeb.API/PokeDexWeb.API.csproj"
COPY . .
WORKDIR "/src/PokeDexWeb.API"
RUN dotnet build "PokeDexWeb.API.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "PokeDexWeb.API.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "PokeDexWeb.API.dll"]