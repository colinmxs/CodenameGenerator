# CodenameGenerator [![Build status](https://ci.appveyor.com/api/projects/status/1fu843atedo0a2hn/branch/master?svg=true)](https://ci.appveyor.com/project/colinmxs/codenamegenerator/branch/master)
Generates random codenames/usernames/whatever you want!

[NuGet link](https://www.nuget.org/packages/CodenameGenerator)

Targets .NET Standard 2.0 (.NET Framework 4.6.1+, .NET Core 2.0+) and .NET 8.

## Examples
### Default Configuration
```C#
var generator = new Generator();
var name = generator.Generate(); //ex: "rambunctious arthropod"
```
`.Generate()` will return a string consisting of an adjective and a noun with a space in between the two. The result will be lowercase.

```C#
var names = generator.GenerateMany(5);

//ex: "rambunctious arthropod"
//ex: "green man"
//ex: "laborious documentation"
//ex: "angelic lobotomy"
//ex: "abyssal hotdog"
```

### Configurations
#### Specify the separator:
```C#
generator.Separator = "-"
var name = generator.Generate(); // ex: "rambunctious-arthropod"
```
#### Specify the types of words used to construct the code name:
```C#
generator.SetParts(WordBank.Adjectives, WordBank.FirstNames, WordBank.LastNames);
var name = generator.Generate(); //ex: "stupid david jones"
```
#### Specify the casing:
```C#
generator.Casing = Casing.UpperCase;
var name = generator.Generate(); // ex: "RAMBUNCTIOUS ARTHROPOD"
```
#### Specify a suffix 
```C#
generator.EndsWith = "@email.com";
generator.Separator = ""; //no separator
var name = generator.Generate(); // ex: "rambunctiousarthropod@gmail.com"
```
#### Avoid names you have already handed out
```C#
var name = generator.GenerateUnique(new[] { "rambunctious arthropod" });
```
Throws `InvalidOperationException` if it cannot find an unused name, rather than
retrying forever. Widen the range by adding parts or larger word banks.

#### Reproducible output
```C#
var generator = new Generator(seed: 1234);
```
Two generators with the same seed produce the same sequence.

### Threading
An unseeded `Generator` is safe to share between threads. A seeded one is not: a
deterministic sequence only holds if a single thread draws from it.

## Available WordBanks
Enumerate them with `WordBank.All`, or use them directly:

+ Adjectives (sourced from [ashley-bovan.co.uk](http://www.ashley-bovan.co.uk/words/partsofspeech.html))
+ Nouns (sourced from [ashley-bovan.co.uk](http://www.ashley-bovan.co.uk/words/partsofspeech.html))
+ Verbs (sourced from [ashley-bovan.co.uk](http://www.ashley-bovan.co.uk/words/partsofspeech.html))
+ Adverbs (sourced from [ashley-bovan.co.uk](http://www.ashley-bovan.co.uk/words/partsofspeech.html))
+ First Names 
 + Female First Names
 + Male First Names
+ Last Names
+ Titles
 + Male Titles
 + Female Titles
+ Months
+ Days
+ Job Titles
+ Countries
+ State Names (U.S. only) (sourced from [datasets-io](https://github.com/datasets-io/us-states-names))
+ Cities (sourced from [MAXMIND](https://www.maxmind.com/en/free-world-cities-database))

Word lists ship as embedded resources and are read on first use, so a bank you
never touch costs you nothing. You can also bring your own:

```C#
var pets = new WordBank(Word.Noun, "Pets", new WordRepository(new[] { "dog", "cat" }));
generator.SetParts(WordBank.Adjectives, pets);
```

## Available Casings
+ PascalCase (e.g. HelloWorld)
+ CamelCase (e.g. helloWorld)
+ LowerCase (e.g. helloworld)
+ UpperCase (e.g. HELLOWORLD)

Casing is applied with the invariant culture, so output does not change with the
current locale.

## Building
```bash
dotnet build -c Release
dotnet test tests/CodenameGenerator.Tests -c Release
```
Or without a local SDK, using the included Dockerfile:
```bash
docker build --target test .            # run the tests
docker build --target pack -o out .     # produce the NuGet package
```

## Changes in 2.0.0
+ Word lists moved from compiled string arrays into embedded resources, loaded lazily. Much smaller assembly, and only the banks you use are read into memory.
+ Unseeded generators are thread-safe.
+ Casing uses invariant culture.
+ `GenerateUnique` is bounded and throws instead of recursing without limit.
+ `Generate()` no longer throws when there are no parts; an empty word bank now gives a clear error.
+ Added `WordBank.All`. Fixed the Months list, which was missing April.
+ **Breaking:** dropped `net452`, `netstandard1.0` and `netstandard1.3`.
