# Build stage
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

# Copy csproj files and restore
COPY ["Sphere.Api/Sphere.Api.csproj", "Sphere.Api/"]
COPY ["Sphere.Application/Sphere.Application.csproj", "Sphere.Application/"]
COPY ["Sphere.Domain/Sphere.Domain.csproj", "Sphere.Domain/"]
COPY ["Sphere.Infrastructure/Sphere.Infrastructure.csproj", "Sphere.Infrastructure/"]
RUN dotnet restore "Sphere.Api/Sphere.Api.csproj"

# Copy all source files and build
COPY . .
WORKDIR "/src/Sphere.Api"
RUN dotnet publish "Sphere.Api.csproj" -c Release -o /app/publish /p:UseAppHost=false

# Runtime stage
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS runtime
WORKDIR /app

# Create non-root user for security
RUN adduser --disabled-password --gecos "" appuser && chown -R appuser /app
USER appuser

COPY --from=build /app/publish .

EXPOSE 80
EXPOSE 443

# Health check
HEALTHCHECK --interval=30s --timeout=10s --start-period=5s --retries=3 \
  CMD curl -f http://localhost:80/health || exit 1

ENTRYPOINT ["dotnet", "Sphere.Api.dll"]
