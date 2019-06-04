FROM mcr.microsoft.com/dotnet/core/sdk:2.2 AS build

WORKDIR /app

COPY *.sln ./
COPY DogRestService.API/*.csproj DogRestService.API/
COPY DogRestService.DAL/*.csproj DogRestService.DAL/

RUN dotnet restore

COPY . ./

RUN dotnet build --no-restore --configuration Release

# RUN dotnet test

RUN dotnet publish DogRestService.API --no-build --configuration Release --output /app/out

FROM mcr.microsoft.com/dotnet/core/aspnet:2.2

WORKDIR /app

COPY --from=build /app/out ./

ENTRYPOINT [ "dotnet", "DogRestService.API.dll" ]
