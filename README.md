# Holodex.NET
![](https://i.imgur.com/iRyguLg.png)

A C# wrapper for the [Holodex](https://holodex.net/home) API. Uses .NET Standard 2.0.
## Features
- Fully asynchronous
- Provides built-in HttpClient or you can provide your own
- Provides all valid endpoint parameter strings, so you dont have to consult the Holodex documentation
- Supports all current GET endpoints

## Dependencies
- Newtonsoft.Json v13.0.1 or greater

## Installation
Todo

## Getting Started
```csharp
HolodexClient client = new HolodexClient("api key");
Channel ch = await client.GetChannel("UC1DCedRgGHBdm81E1llLhOQ");
Console.WriteLine(ch.Name); // Pekora Ch. 兎田ぺこら
Console.WriteLine(ch.EnglishName); // Usada Pekora
Console.WriteLine(ch.Subscribers); // 1480000 
```
## Documentation
Todo
