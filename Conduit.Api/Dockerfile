# Use the official .NET 7 SDK image
FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build

# Set working directory inside the container
WORKDIR /app

# Copy project files
COPY *.csproj ./
COPY Conduit.Infra.Data/*.csproj ./Conduit.Infra.Data/
COPY Conduit.Infra.IoC/*.csproj ./Conduit.Infra.IoC/
COPY . ./

# Restore and build
RUN dotnet restore
RUN dotnet build -c Release -o /app/build

# Expose the port the app will listen on
EXPOSE 5000

# Set the startup command
CMD ["dotnet", "run", "--project", "Conduit.csproj", "--urls", "http://0.0.0.0:5000"]