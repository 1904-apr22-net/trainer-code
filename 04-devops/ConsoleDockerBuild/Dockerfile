FROM mcr.microsoft.com/dotnet/core/sdk:2.2

WORKDIR /app/src

# copying all the current dir contents into the image
COPY ./ /app/src

# # with this, we would be compiling the app every time we start a new container
# CMD ["dotnet", "run"]

# "RUN"
RUN dotnet build
RUN dotnet publish -o /app/out

# above here, the code runs when you build th eimage
# but CMD (or ENTRYPOINT) run only
# when the container is started

WORKDIR /app/out

CMD ["dotnet", "ConsoleDockerBuild.dll"]

# docker build -t consoledockerbuild:0.1 .

# build happens inside Docker... maximum repeatability
# for build as well as for runtime.
