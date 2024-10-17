# Build stage
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build

WORKDIR /app

# Copy project files
COPY *.csproj ./
RUN dotnet restore

# Copy the source code then compile the app
COPY . ./
RUN dotnet publish -c Release -o out

# Final stage
FROM mcr.microsoft.com/dotnet/aspnet:8.0
WORKDIR /app

# Set env variable
ENV DATE_FORMAT="dd-MM-yyyy"

# Copy the published files
COPY --from=build /app/out .

# Expose port 8080
EXPOSE 8080

# Specify the entry point for the application
ENTRYPOINT ["dotnet", "WebApplication1.dll"]
