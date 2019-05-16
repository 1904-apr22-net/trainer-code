# for multi-stage build, any stage we need to refer to, needs a name

FROM mcr.microsoft.com/dotnet/core/sdk:2.2 AS build

WORKDIR /app

# take advantage of docker's image layer caching
# (for faster image builds when not much changed since last build)

COPY ./*.csproj ./

RUN dotnet restore

# if the csproj file didn't change since last build,
# then those cached layers will be used isntead of re-built

# then, copy everything else
COPY . ./

RUN dotnet build --no-restore --configuration Release

# RUN dotnet test

RUN dotnet publish --no-build --configuration Release --output out

# multi-stage builds
FROM mcr.microsoft.com/dotnet/core/aspnet:2.2

WORKDIR /app

# copy the publish output from the build stage into this new stage
# with much smaller runtime base image
COPY --from=build /app/out ./

ENTRYPOINT [ "dotnet", "MvcDockerAdvancedBuild.dll" ]
