# syntax=docker/dockerfile:1
#
# Build + test CodenameGenerator in a container. Nothing is installed on the host.
#
#   docker build --target test .                 # run the unit tests
#   docker build --target pack -o out .          # emit the .nupkg + .snupkg to ./out
#
# The library multi-targets netstandard2.0 and net8.0; the tests run on net8.0.
# Both are buildable by the SDK image alone, so no container-only MSBuild
# overrides are needed.

ARG SDK_IMAGE=mcr.microsoft.com/dotnet/sdk:8.0

FROM ${SDK_IMAGE} AS base
ENV DOTNET_CLI_TELEMETRY_OPTOUT=1 \
    DOTNET_NOLOGO=1 \
    DOTNET_SKIP_FIRST_TIME_EXPERIENCE=1 \
    NUGET_XMLDOC_MODE=skip
WORKDIR /src

# Restore first, on project files only, so edits to .cs don't invalidate the cache.
FROM base AS restore
COPY src/CodenameGenerator/CodenameGenerator.csproj src/CodenameGenerator/
COPY tests/CodenameGenerator.Tests/CodenameGenerator.Tests.csproj tests/CodenameGenerator.Tests/
RUN --mount=type=cache,target=/root/.nuget/packages,sharing=locked \
    dotnet restore tests/CodenameGenerator.Tests/CodenameGenerator.Tests.csproj

FROM restore AS build
COPY README.md ./
COPY src/ src/
COPY tests/ tests/
RUN --mount=type=cache,target=/root/.nuget/packages,sharing=locked \
    dotnet build src/CodenameGenerator/CodenameGenerator.csproj -c Release --no-restore

FROM build AS test
RUN --mount=type=cache,target=/root/.nuget/packages,sharing=locked \
    dotnet test tests/CodenameGenerator.Tests/CodenameGenerator.Tests.csproj \
        -c Release \
        --logger "console;verbosity=normal"

# `docker build --target pack -o out .` drops the packages on the host.
FROM scratch AS pack
COPY --from=build /src/src/CodenameGenerator/bin/Release/*.nupkg /
COPY --from=build /src/src/CodenameGenerator/bin/Release/*.snupkg /
