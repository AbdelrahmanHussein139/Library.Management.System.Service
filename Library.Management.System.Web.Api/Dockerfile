# --------------------------
# Stage 1: Build
# --------------------------
FROM mcr.microsoft.com/dotnet/sdk:10.0 AS build
WORKDIR /src

# Copy csproj and restore dependencies  
COPY Library.Management.System.Web.Api/*.csproj ./Library.Management.System.Web.Api/
COPY Library.Management.System.Application/*.csproj ./Library.Management.System.Application/
COPY Library.Management.System.Domain/*.csproj ./Library.Management.System.Domain/
COPY Library.Management.System.Infrastructure/*.csproj ./Library.Management.System.Infrastructure/

RUN dotnet restore ./Library.Management.System.Web.Api/Library.Management.System.Web.Api.csproj

# Copy all source code
COPY . ./

# Build the app
RUN dotnet publish ./Library.Management.System.Web.Api/Library.Management.System.Web.Api.csproj -c Release -o /app/publish /p:UseAppHost=false

# --------------------------
# Stage 2: Runtime
# --------------------------
FROM mcr.microsoft.com/dotnet/aspnet:10.0 AS runtime
WORKDIR /app

# Copy published output from build stage
COPY --from=build /app/publish .

# Expose port (default Kestrel port)
EXPOSE 5000

ENV ASPNETCORE_URLS=http://0.0.0.0:5000

# Entry point
ENTRYPOINT ["dotnet", "Library.Management.System.Web.Api.dll"]
