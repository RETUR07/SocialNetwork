FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
WORKDIR /source
EXPOSE 80
EXPOSE 443

COPY *.sln .
COPY SocialNetwork/*.csproj ./SocialNetwork/
COPY application/*.csproj ./application/
COPY Entities/*.csproj ./Entities/
COPY Repository/*.csproj ./Repository/

COPY SocialNetwork/. ./SocialNetwork/
COPY application/. ./application/
COPY Entities/. ./Entities/
COPY Repository/. ./Repository/

RUN dotnet publish -c release -o /app



FROM mcr.microsoft.com/dotnet/aspnet:5.0
WORKDIR /app
COPY --from=build /app ./
EXPOSE 80
ENTRYPOINT ["dotnet", "SocialNetwork.dll"]