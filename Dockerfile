# syntax=docker/dockerfile:1
FROM mcr.microsoft.com/dotnet/aspnet:5.0
COPY /Album.Api/bin/Debug/net5.0 App/
WORKDIR /App
ENTRYPOINT ["dotnet", "Album.Api.dll"]
