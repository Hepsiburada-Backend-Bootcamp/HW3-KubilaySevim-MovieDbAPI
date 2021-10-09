#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:5.0 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
WORKDIR /src
COPY ["MovieDbInf.Api/MovieDbInf.API.csproj", "MovieDbInf.Api/"]
COPY ["MovieDbInf.Infrastructure/MovieDbInf.Infrastructure.csproj", "MovieDbInf.Infrastructure/"]
COPY ["MovieDbInf.Domain/MovieDbInf.Domain.csproj", "MovieDbInf.Domain/"]
COPY ["MovieDbInf.Application/MovieDbInf.Application.csproj", "MovieDbInf.Application/"]
RUN dotnet restore "MovieDbInf.Api/MovieDbInf.API.csproj"
COPY . .
WORKDIR "/src/MovieDbInf.Api"
RUN dotnet build "MovieDbInf.API.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "MovieDbInf.API.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "MovieDbInf.API.dll"]