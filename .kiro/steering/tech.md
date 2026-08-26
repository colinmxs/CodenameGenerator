# Tech & Commands

.NET / C#, built with the `dotnet` SDK CLI **inside a container** (see
`docker-local.md`; there is no SDK on this machine).

- Library targets `netstandard2.0` and `net8.0`. `LangVersion latest`, `Nullable enable`,
  `GenerateDocumentationFile` on, .NET analyzers on, zero runtime dependencies.
- Tests target `net8.0` with MSTest 3.6.1 and Microsoft.NET.Test.Sdk 17.11.1.
- `GeneratePackageOnBuild` is true; `Version` lives in
  `src/CodenameGenerator/CodenameGenerator.csproj` and is rewritten by CI.

## Commands

```bash
docker build --target test .            # restore, build both TFMs, run all tests
docker build --target pack -o out .     # .nupkg + .snupkg into ./out
```

With a local SDK the equivalents are `dotnet build -c Release` and
`dotnet test tests/CodenameGenerator.Tests -c Release`.

## Rules

- Verify changes by running the container test target before reporting them done. If
  that is impossible, say so explicitly rather than implying verification.
- The build must stay warning-free. `GenerateDocumentationFile` means every public
  member needs an XML doc comment (CS1591), including enum members.
- Code must compile for `netstandard2.0`, which lacks newer BCL APIs. Guard anything
  modern with `#if NET6_0_OR_GREATER` (as `Generator` does for `Random.Shared`).
- Don't hand-edit `<Version>` for releases; AppVeyor rewrites it from the build number
  via XPath (`//Version`), so keep exactly one `<Version>` element in the csproj.
- Don't touch the `appveyor.yml` secret (`api_key`) or the NuGet push step. CI itself
  cannot be verified from here; changes to it are unverified by definition.
