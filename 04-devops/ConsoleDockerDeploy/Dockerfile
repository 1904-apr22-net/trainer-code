# a Dockerfile is a template for building an image.

# step 1 in writing a Dockerfile... what is the base image?
#     what does my app need to run? the .NET Core runtime.
#   so... i should look for a base image that already has it.

# FROM is a Dockerfile command, to specify the base image
# for the subsequent commands.

# (default registry is docker hub)

#      registry       | org  | repository | tag
#           v         |  v   |    v       |  v
FROM mcr.microsoft.com/dotnet/core/runtime:2.2

# we should set our working directory inside the image.
# (thsi creates the dir if it doesn't exist, and sets it as
# current directory for subsequent commands)
WORKDIR /app

# copy some files from outside Docker into the image's file system.

# there is an ADD command that is quirky and not recommended

# we're going to run the app in Docker, but first, before we build the
# image, we'll build and publish outside Docker, to a folder named out/

# copy the contents of the out directory outside Docker
# into the /app directory inside Docker.
COPY out/ /app

CMD [ "dotnet", "/app/ConsoleDockerDeploy.dll" ]
# (the .net core runtime has a CLI, but it knows only how to
# take a DLL and run it)
