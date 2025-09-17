# -----------------------------
# Build stage
# -----------------------------
FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
WORKDIR /app

# Copy individual project files for dependency restore
COPY src/LearnWell.Api/LearnWell.Api.csproj ./LearnWell.Api/
COPY src/LearnWell.Infrastructure/LearnWell.Infrastructure.csproj ./LearnWell.Infrastructure/
COPY src/LearnWell.Application/LearnWell.Application.csproj ./LearnWell.Application/
COPY src/LearnWell.Domain/LearnWell.Domain.csproj ./LearnWell.Domain/

# Restore dependencies
RUN dotnet restore ./LearnWell.Api/LearnWell.Api.csproj

# Copy full source code
COPY src/ .

# Publish the API project
RUN dotnet publish ./LearnWell.Api/LearnWell.Api.csproj -c Release -o /app/publish /p:UseAppHost=false

# -----------------------------
# Runtime stage
# -----------------------------
FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS final
WORKDIR /app

# Copy published output from build stage
COPY --from=build /app/publish .

# Expose API port
EXPOSE 8080

# Start the application
ENTRYPOINT ["dotnet", "LearnWell.Api.dll"]