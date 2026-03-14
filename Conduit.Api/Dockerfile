# ------------------------------
# Stage 1: Build the backend
# ------------------------------
FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build

# Set working directory inside container
WORKDIR /app

# Copy all project files first (for caching restore)
COPY Conduit.Api/*.csproj ./Conduit.Api/
COPY Conduit.Application/*.csproj ./Conduit.Application/
COPY Conduit.Domain/*.csproj ./Conduit.Domain/
COPY Conduit.Domain.Core/*.csproj ./Conduit.Domain.Core/
COPY Conduit.Infra.Data/*.csproj ./Conduit.Infra.Data/
COPY Conduit.Infra.IoC/*.csproj ./Conduit.Infra.IoC/

# Restore only the API project (references all others)
RUN dotnet restore ./Conduit.Api/Conduit.csproj

# Copy all source code
COPY . ./

# Build the API project in Release mode
RUN dotnet build ./Conduit.Api/Conduit.csproj -c Release -o /app/build

# ------------------------------
# Stage 2: Publish for runtime
# ------------------------------
FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS runtime
WORKDIR /app

# Copy the build output
COPY --from=build /app/build ./

# Expose the port the app will listen on
EXPOSE 5000

# Command to run the API
CMD ["dotnet", "Conduit.Api.dll", "--urls", "http://0.0.0.0:5000"]