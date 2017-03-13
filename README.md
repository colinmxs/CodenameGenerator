# CodenameGenerator
Generates random codenames/usernames/whatever you want!

##Examples
Generate code name:
```C#
var generator = new Generator();
var name = generator.Generate();

Console.WriteLine(name); //Outputs a random code name with default configuration, example: "rambunctious arthropod"
```
Generate many code names:
```C#
var generator = new Generator();
var names = generator.GenerateMany(5);
```
Specify the separator:
```C#
var generator = new Generator();
generator.Separator = "-"
var name = generator.Generate();

Console.WriteLine(name); // "rambunctious-arthropod"
```
Specify the types of words that should be used to construct the code name:
```C#
var generator = new Generator();
generator.SetParts(TestWordBank.Adjectives, TestWordBank.FirstNames, TestWordBank.LastNames);
var name = generator.Generate();

Console.WriteLine(name); //"stupid david jones"
```
```C#
var generator = new Generator();
generator.SetParts(TestWordBank.Nouns);
var name = generator.Generate();

Console.WriteLine(name); //"hospital"
```
Specify the casing:
```C#
var generator = new Generator();
generator.Casing = Casing.UpperCase;
var name = generator.Generate();

Console.WriteLine(name); // "RAMBUNCTIOUS ARTHROPOD"
```
