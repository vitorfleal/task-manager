ARG IMAGE_TAG=6.0

FROM mcr.microsoft.com/dotnet/sdk:$IMAGE_TAG AS build-env
EXPOSE 80

WORKDIR /app
COPY . .

# Restore as distinct layers
RUN dotnet restore

# Build and publish a release
RUN dotnet publish -c Release -o /app --no-restore

# Build runtime image
FROM mcr.microsoft.com/dotnet/aspnet:$IMAGE_TAG
WORKDIR /app
COPY --from=build-env /app .