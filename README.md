# Holodex.NET
[![NuGet](https://img.shields.io/nuget/v/Holodex.NET.svg)](https://www.nuget.org/packages/Holodex.NET)

![](https://i.imgur.com/iRyguLg.png)

A C# wrapper for the [Holodex](https://holodex.net/home) API. Uses .NET Standard 2.0.
## Features
- Fully asynchronous
- Uses StringBuilder to build request strings
- Provides built-in HttpClient or you can provide your own
- Provides all valid endpoint parameters in enums and classes, so you dont have to consult the Holodex documentation
- Supports all current GET endpoints

## Dependencies
- Newtonsoft.Json v13.0.1 or greater

## Installation
```
PM> Install-Package Holodex.NET
```

## Getting Started
```csharp
HolodexClient client = new HolodexClient("api key");
Channel ch = await client.GetChannel("UC1DCedRgGHBdm81E1llLhOQ");
Console.WriteLine(ch.Name); // Pekora Ch. 兎田ぺこら
Console.WriteLine(ch.EnglishName); // Usada Pekora
Console.WriteLine(ch.Subscribers); // 1480000 
```
## Documentation
Documentation can be found using either Intellisense or the [Official Documentation](https://ebro912.gitbook.io/holodex-net/).

## Issues and Bugs
Any issues and/or bugs should be reported via the issue tracker, or on the [Holodex Discord](https://discord.gg/A24AbzgvRJ), and reported to me, All Toasters Toast Toast #0001.
