FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
WORKDIR /app


COPY *.csproj ./
RUN dotnet restore

# Copy the rest of the application code
COPY . ./

# Build the application
RUN dotnet publish -c Release -o /app/out

FROM mcr.microsoft.com/dotnet/aspnet:9.0
WORKDIR /app

# Add user Bomb and Defuser
RUN useradd -u 1000 -m bomb
RUN useradd -u 1001 -m defuser

# Copy the build output from the previous stage
COPY --from=build /app/out .

# Set the user
USER bomb

# Run the application
ENTRYPOINT ["dotnet", "IEDgame.dll"]