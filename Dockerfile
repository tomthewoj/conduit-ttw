# ------------------------------
# Stage 1: Build the backend
# ------------------------------
FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
WORKDIR /app

# Copy all csproj files first (caching restore)
COPY Conduit.Api/*.csproj ./Conduit.Api/
COPY Conduit.Application/*.csproj ./Conduit.Application/
COPY Conduit.Domain/*.csproj ./Conduit.Domain/
COPY Conduit.Domain.Core/*.csproj ./Conduit.Domain.Core/
COPY Conduit.Infra.Data/*.csproj ./Conduit.Infra.Data/
COPY Conduit.Infra.IoC/*.csproj ./Conduit.Infra.IoC/

# Restore dependencies
RUN dotnet restore ./Conduit.Api/Conduit.csproj

# Copy all source code
COPY . .

# Publish the app for runtime
RUN dotnet publish ./Conduit.Api/Conduit.csproj -c Release -o /app/publish

# ------------------------------
# Stage 2: Runtime container
# ------------------------------
FROM mcr.microsoft.com/dotnet/aspnet:9.0
WORKDIR /app

# Copy the published output
COPY --from=build /app/publish .

# Expose the port the app will listen on
EXPOSE 5000

# Run the app
ENTRYPOINT ["dotnet", "Conduit.dll"]