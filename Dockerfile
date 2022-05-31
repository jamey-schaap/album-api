# syntax=docker/dockerfile:1
FROM mcr.microsoft.com/dotnet/aspnet:5.0
COPY /Album.Api/bin/Debug/net5.0/publish App/
WORKDIR /App
EXPOSE 80
ENTRYPOINT ["dotnet", "Album.Api.dll"]
