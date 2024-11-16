ARG IMAGE_TAG=8.0

# Build runtime image
FROM mcr.microsoft.com/dotnet/aspnet:$IMAGE_TAG AS base
EXPOSE 80
EXPOSE 443

ENV ASPNETCORE_ENVIRONMENT=Development
ENV ASPNETCORE_URLS=http://+

FROM mcr.microsoft.com/dotnet/sdk:$IMAGE_TAG AS build
WORKDIR /app
COPY . .
RUN dotnet restore
WORKDIR "/app/src/TaskManager.Api"
RUN dotnet build "TaskManager.Api.csproj" -c Release -o /app/build

# Build and publish a release
FROM build AS publish
RUN dotnet publish -c Release -o /app /p:UseAppHost=false --no-restore

FROM base AS final
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "TaskManager.Api.dll"]