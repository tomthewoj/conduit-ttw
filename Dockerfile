# ------------------------------
# Stage 1: Build Angular frontend
# ------------------------------
FROM node:22 AS angular-build
WORKDIR /ConduitAngular

# Copy package.json and install dependencies
COPY ConduitAngular/package*.json ./
RUN npm install

# Copy all Angular source code
COPY ConduitAngular/ ./

# Build Angular for production
RUN npm run build --configuration production

# ------------------------------
# Stage 2: Build .NET backend
# ------------------------------
FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
WORKDIR /app

# Copy csproj files first (for caching restore)
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

# Publish backend
RUN dotnet publish ./Conduit.Api/Conduit.csproj -c Release -o /app/publish

# ------------------------------
# Stage 3: Runtime
# ------------------------------
FROM mcr.microsoft.com/dotnet/aspnet:9.0
WORKDIR /app

# Copy backend publish output
COPY --from=build /app/publish ./

# Copy Angular build output into wwwroot
COPY --from=angular-build /ConduitAngular/dist/ConduitAngular/browser/ ./wwwroot/

# Expose port
EXPOSE 5000

# Run backend
ENTRYPOINT ["dotnet", "Conduit.Api.dll"]