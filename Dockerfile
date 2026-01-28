# B2B API - Render / Docker
FROM mcr.microsoft.com/dotnet/sdk:10.0 AS build
WORKDIR /src

COPY B2B.API/B2B.API.csproj B2B.API/
COPY B2B.Core/B2B.Core.csproj B2B.Core/
COPY B2B.Infrastructure/B2B.Infrastructure.csproj B2B.Infrastructure/

RUN dotnet restore B2B.API/B2B.API.csproj
COPY . .
RUN dotnet publish B2B.API/B2B.API.csproj -c Release -o /app/publish --no-restore

FROM mcr.microsoft.com/dotnet/aspnet:10.0
WORKDIR /app
COPY --from=build /app/publish .
# Render PORT env kullanilir
EXPOSE 5000
ENTRYPOINT ["dotnet", "B2B.API.dll"]
