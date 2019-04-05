# CodenameGenerator [![Build status](https://ci.appveyor.com/api/projects/status/1fu843atedo0a2hn/branch/master?svg=true)](https://ci.appveyor.com/project/colinmxs/codenamegenerator/branch/master)
Generates random codenames/usernames/whatever you want!

[NuGet link](https://www.nuget.org/packages/CodenameGenerator)

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

### Amazing Configurations
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
generator.EndsWith = "@gmail.com";
generator.Separator = ""; //no separator
var name = generator.Generate(); // ex: "rambunctiousarthropod@gmail.com"
```

## Available WordBanks
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

## Available Casings
+ PascalCase (e.g. HelloWorld)
+ CamelCase (e.g. helloWorld)
+ LowerCase (e.g. helloworld)
+ UpperCase (e.g. HELLOWORLD)
