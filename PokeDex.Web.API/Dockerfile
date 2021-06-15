#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/core/aspnet:3.1-buster-slim AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/core/sdk:3.1-buster AS build
WORKDIR /src
COPY ["PokeDex.Web.API/PokeDex.Web.API.csproj", "PokeDex.Web.API/"]
RUN dotnet restore "PokeDex.Web.API/PokeDex.Web.API.csproj"
COPY . .
WORKDIR "/src/PokeDex.Web.API"
RUN dotnet build "PokeDex.Web.API.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "PokeDex.Web.API.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "PokeDex.Web.API.dll"]