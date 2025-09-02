# Build aşaması
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

# csproj dosyalarını kopyala ve restore yap
COPY ["AdPlacement.API/AdPlacements.Api.csproj", "AdPlacement.API/"]
COPY ["AdPlacement.Application/AdPlacements.Application.csproj", "AdPlacement.Application/"]
COPY ["AdPlacement.Domain/AdPlacement.Domain.csproj", "AdPlacement.Domain/"]
COPY ["AdPlacement.Infrastructure/AdPlacement.Infrastructure.csproj", "AdPlacement.Infrastructure/"]
RUN dotnet restore "AdPlacements.Api/AdPlacements.Api.csproj"

# tüm kodu kopyala ve publish et
COPY . .
WORKDIR "/src/AdPlacement.API"
RUN dotnet publish "AdPlacement.API.csproj" -c Release -o /app/publish

# runtime aşaması
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS final
WORKDIR /app
COPY --from=build /app/publish .
ENTRYPOINT ["dotnet", "AdPlacement.API.dll"]
