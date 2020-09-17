[NuGet link](https://www.nuget.org/packages/CodenameGenerator)  [![Build status](https://ci.appveyor.com/api/projects/status/1fu843atedo0a2hn/branch/master?svg=true)](https://ci.appveyor.com/project/colinmxs/codenamegenerator/branch/master)

```C#
var generator = new Generator();
string name = generator.Generate(); //ex: "rambunctious arthropod"
```
#### Specify the separator:
```C#
generator.Separator = "-" // ex: "rambunctious-arthropod"
```
#### Specify the types of words used to construct the code name:
```C#
generator.SetParts(WordBank.Adjectives, WordBank.FirstNames, WordBank.LastNames); //ex: "stupid david jones"
```
#### Specify the casing:
```C#
generator.Casing = Casing.UpperCase; // ex: "RAMBUNCTIOUS ARTHROPOD"
```
#### Specify a suffix 
```C#
generator.Separator = ""; //no separator
generator.EndsWith = "@fakegmail.com"; // ex: "rambunctiousarthropod@fakegmail.com"
```

## Available WordBanks
+ Adjectives
+ Nouns 
+ Verbs 
+ Adverbs 
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
+ State Names (U.S. only) 
+ Cities 

## Available Casings
+ PascalCase (e.g. HelloWorld)
+ CamelCase (e.g. helloWorld)
+ LowerCase (e.g. helloworld)
+ UpperCase (e.g. HELLOWORLD)
