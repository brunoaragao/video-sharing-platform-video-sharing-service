# syntax=docker/dockerfile:experimental
FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY VideoSharing.API/VideoSharing.API.csproj .
RUN dotnet restore "VideoSharing.API.csproj"
COPY VideoSharing.API .
RUN dotnet publish -c Release -o /publish

FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS runtime
WORKDIR /app
COPY --from=build /publish .
EXPOSE 80
ENTRYPOINT ["dotnet", "VideoSharing.API.dll"]
