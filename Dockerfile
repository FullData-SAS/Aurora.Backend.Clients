#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.
# Use the official .NET 8 SDK image as the base image
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build

# Set the working directory
WORKDIR /src

# Copy csproj and restore dependencies
COPY ./*.sln ./
COPY Aurora.Backend.Clients/*.csproj ./Aurora.Backend.Clients/
COPY Aurora.Backend.Clients.Services/*.csproj ./Aurora.Backend.Clients.Services/
RUN dotnet restore "Aurora.Backend.Clients.Services/Aurora.Backend.Clients.Services.csproj"
RUN dotnet restore "Aurora.Backend.Clients/Aurora.Backend.Clients.csproj"

# Copy everything else and build the API project
COPY Aurora.Backend.Clients/. ./Aurora.Backend.Clients/
COPY Aurora.Backend.Clients.Services/. ./Aurora.Backend.Clients.Services/
RUN dotnet publish ./Aurora.Backend.Clients -c Release -o /app

# Use the .NET 8 runtime image as the final base image
FROM mcr.microsoft.com/dotnet/aspnet:8.0
RUN apt-get update && apt-get install -y libgdiplus
RUN apt-get install -y libfontconfig1
RUN apt-get update && apt-get install -y apt-utils libgdiplus libc6-dev

# Set the working directory
WORKDIR /app

# Copy the built API from the build image
COPY --from=build /app .

# Expose port 80 for the Web API traffic
ENV ASPNETCORE_URLS=http://+:5000
EXPOSE 5000

# Run the API when the container is run
ENTRYPOINT ["dotnet", "Aurora.Backend.Clients.dll"]
