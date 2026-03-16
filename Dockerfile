# ------------------------------
# Stage 1: Build Angular frontend
# ------------------------------
FROM node:22 AS angular-build
WORKDIR /frontend

# Copy package.json and install dependencies
COPY ConduitAngular/package*.json ./
RUN npm install

# Copy all Angular source code
COPY ConduitAngular/ ./

# Build Angular for production
RUN npx ng build --configuration production

# ------------------------------
# Stage 2: Build .NET backend
# ------------------------------
FROM mcr.microsoft.com/dotnet/sdk:9.0 AS backend-build
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
COPY . ./

# Publish backend to /app/publish
RUN dotnet publish ./Conduit.Api/Conduit.csproj -c Release -o /app/publish

# Copy Angular build output into backend wwwroot
# This ensures ASP.NET Core serves Angular files automatically
COPY --from=angular-build /frontend/dist/ConduitAngular/browser/ /app/publish/wwwroot/

# ------------------------------
# Stage 3: Runtime container
# ------------------------------
FROM mcr.microsoft.com/dotnet/aspnet:9.0
WORKDIR /app

# Copy published backend + Angular files
COPY --from=backend-build /app/publish ./

# Expose port
EXPOSE 5000

# Run backend (DLL will exist because we published)
ENTRYPOINT ["dotnet", "Conduit.dll"]