# Stage 1: Build the backend
FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /app

# Copy all csproj files and restore
COPY Conduit.Api/*.csproj ./Conduit.Api/
COPY Conduit.Application/*.csproj ./Conduit.Application/
COPY Conduit.Domain/*.csproj ./Conduit.Domain/
COPY Conduit.Domain.Core/*.csproj ./Conduit.Domain.Core/
COPY Conduit.Infra.Data/*.csproj ./Conduit.Infra.Data/
COPY Conduit.Infra.IoC/*.csproj ./Conduit.Infra.IoC/

RUN dotnet restore ./Conduit.Api/Conduit.csproj

# Copy all source code
COPY . ./

# Build
RUN dotnet build ./Conduit.Api/Conduit.csproj -c Release -o /app/build

# Stage 2: Run
FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS runtime
WORKDIR /app

COPY --from=build /app/build ./

EXPOSE 5000

CMD ["dotnet", "Conduit.Api.dll", "--urls", "http://0.0.0.0:5000"]