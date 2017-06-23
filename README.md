# CodenameGenerator
Generates random codenames/usernames/whatever you want!

[NuGet link](https://www.nuget.org/packages/CodenameGenerator/1.0.0)

## Examples
Generate code name:
```C#
var generator = new Generator();
var name = generator.Generate();

Console.WriteLine(name); //ex: "rambunctious arthropod"
```
Generate many code names:
```C#
var generator = new Generator();
var names = generator.GenerateMany(5);

foreach(var name in names)
{
     Console.WriteLine(name);	
}
//"ex: rambunctious arthropod"
//"ex: green man"
//"ex: laborious documentation"
//"ex: angelic lobotomy"
//"ex: abyssal hotdog"
```
Specify the separator:
```C#
var generator = new Generator();
generator.Separator = "-"
var name = generator.Generate();

Console.WriteLine(name); // "ex: rambunctious-arthropod"
```
Specify the types of words used to construct the code name:
```C#
var generator = new Generator();
generator.SetParts(WordBank.Adjectives, WordBank.FirstNames, WordBank.LastNames);
var name = generator.Generate();

Console.WriteLine(name); //"ex: stupid david jones"
```
```C#
var generator = new Generator();
generator.SetParts(WordBank.Nouns);
var name = generator.Generate();

Console.WriteLine(name); //"ex: hospital"
```
Specify the casing:
```C#
var generator = new Generator();
generator.Casing = Casing.UpperCase;
var name = generator.Generate();

Console.WriteLine(name); // "ex: RAMBUNCTIOUS ARTHROPOD"
```
```C#
var generator = new Generator();
generator.Casing = Casing.CamelCase;
generator.Separator = ""; //no separator
var name = generator.Generate();

Console.WriteLine(name); // "ex: rambunctiousArthropod"
```
Contrived example usage:
```C#
var generator = new Generator();
generator.Casing = Casing.LowerCase;
generator.Separator = ".";
generator.SetParts(WordBank.Titles, WordBank.FirstNames, WordBank.Adjectives, WordBank.Nouns, WordBank.LastNames);
var name = generator.Generate();

Console.WriteLine(name); // "ex: president.donald.orange.douche.trump"
```

## Default Configuration
By default, the name generator will generate a code name consisting of an adjective followed by a noun with a space in between the two. The result will be lowercase.

## Available WordBanks
+ Adjectives
+ Nouns
+ First Names 
 + Female First Names
 + Male First Names
+ Last Names
+ Titles
 + Male Titles
 + Female Titles
+ State Names
+ Months
+ Days
+ Job Titles

## Available Casings
+ PascalCase (e.g. HelloWorld)
+ CamelCase (e.g. helloWorld)
+ LowerCase (e.g. helloworld)
+ UpperCase (e.g. HELLOWORLD)