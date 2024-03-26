#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src
COPY ["ApiApplication.Core/ApiApplication.Core.csproj", "ApiApplication.Core/"]
RUN dotnet restore "ApiApplication.Core/ApiApplication.Core.csproj"
COPY . .
COPY ["ApiApplication.Infrastructure/ApiApplication.Infrastructure.csproj", "ApiApplication.Infrastructure/"]
RUN dotnet restore "ApiApplication.Infrastructure/ApiApplication.Infrastructure.csproj"
COPY . .
COPY ["ApiApplication.Core/ApiApplication.Core.csproj", "ApiApplication.Core/"]
RUN dotnet restore "ApiApplication.Core/ApiApplication.Core.csproj"
COPY . .

COPY ["ApiApplication/ApiApplication.csproj", "ApiApplication/"]
RUN dotnet restore "ApiApplication/ApiApplication.csproj"
COPY . .

WORKDIR "/src/ApiApplication"
RUN dotnet build "ApiApplication.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "ApiApplication.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "ApiApplication.dll"]