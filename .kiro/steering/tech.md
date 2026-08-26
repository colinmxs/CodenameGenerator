# Tech & Commands

.NET / C#, built with the `dotnet` SDK CLI **inside a container** (see
`docker-local.md`; there is no SDK on this machine).

- Library targets `netstandard2.0` and `net8.0`. `LangVersion latest`, `Nullable enable`,
  `GenerateDocumentationFile` on, .NET analyzers on, zero runtime dependencies.
- Tests target `net8.0` with MSTest 3.6.1 and Microsoft.NET.Test.Sdk 17.11.1.
- CI is GitHub Actions (`.github/workflows/ci.yml`): build + test on ubuntu and
  windows, then pack, then push to NuGet on `master`.

## Commands

```bash
docker build --target test .                        # restore, build both TFMs, run all tests
docker build --target pack -o out .                 # .nupkg + .snupkg into ./out
docker build --target pack --build-arg VERSION=2.0.7 -o out .   # stamp a version
```

With a local SDK the equivalents are `dotnet build -c Release`,
`dotnet test tests/CodenameGenerator.Tests -c Release` and
`dotnet pack src/CodenameGenerator -c Release -o artifacts`.

## Rules

- Verify changes by running the container test target before reporting them done. If
  that is impossible, say so explicitly rather than implying verification.
- The build must stay warning-free; CI builds with `-warnaserror`.
  `GenerateDocumentationFile` means every public member needs an XML doc comment
  (CS1591), including enum members.
- Code must compile for `netstandard2.0`, which lacks newer BCL APIs. Guard anything
  modern with `#if NET6_0_OR_GREATER` (as `Generator` does for `Random.Shared`).
- Release version is `VERSION_PREFIX` (in the workflow) plus the run number, passed as
  `-p:Version=` with a matching `-p:FileVersion=<version>.0`. Nothing rewrites the
  csproj. `<Version>` in the csproj is only the local default. `AssemblyVersion` stays
  pinned at `2.0.0.0` on purpose so per-build numbers can't break assembly binding;
  because `FileVersion` defaults to `AssemblyVersion`, it must be passed explicitly or
  it stays pinned too.
- Publishing needs the `NUGET_API_KEY` repository secret; the workflow warns and skips
  if it is absent. Commits containing `[chore]` don't publish.
- CI itself cannot be executed from here. Workflow changes are verified with
  `actionlint` and by running the same commands in a container, which is not the same
  as a green run on GitHub.
