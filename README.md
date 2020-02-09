[![Build status](https://dev.azure.com/SourceOps/Source/_apis/build/status/Source-WebApi-TemplatePack-CI)](https://dev.azure.com/SourceOps/Source/_build/latest?definitionId=3)

...

---
uid: source-webapi-get-started
---
# Getting Started with EISK Web Api

EISK makes it easy to write scalable and secured web api on top of Microsoft's new cutting edge .net core technologies. 

With an optional set of customizable utility classes, samples and tools, it lets you creating new web api straight away without wide technical experience or learning curve.

## Sample Use Case

Using a simple table entity 'Employee' it demonstrates all aspect of web development including layered architecture following DDD, micro service, unit and integration tests, building and deploying in cloud environment. 

Here is a simple CRUD use case illustrated in the default template:

* Creating a new employee record
* Read existing employee records
* Update an existing employee record
* Delete existing employee records

## Core Technology Areas

* ASP.NET Core 2.0 (Web Api)
* Entity Framework Core 2.0
* C# 7.0
* Visual Studio 2017
* Azure App Services 

## System Requirements (Development)

* Visual Studio 2017 ([Free](https://visualstudio.microsoft.com/vs/community/) Community Edition or higher)

## QuickStart Guide

Getting started with EISK Web Api is pretty easy. 

You can either [clone](https://github.com/EISK/source.webapi.git) from github or simply run the following `dotnet new` command in command prompt to create a new project from EISK:

* Command to install EISK template in your machine: `dotnet new -i source.webapi`
* Command to create a new project: `dotnet new sourcewebapi -n Source`

Once the contents are available, just open the created solution, select "Source.WebApi" as startup project and press F5!

That's it!

## What's Next?

After running the created project successfully, you'll get an understanding about how the sample use case has been used to explore cutting edge technologies for building a web api.

Next - you can try some hands-on experience by creating your own api on top of your custom entity and see how quickly you can roll out an enterprise quality web api with similar quality and productivity. 

Utilities and code samples as provided in EISK have intentionally been designed to be self explaining. You may still want to get deeper understanding by exploring the documentations:

* [Live Demo](https://sourcewebapi.azurewebsites.net)
* [Hands-on Walk-through](https://source.github.io/source.webapi/docs/application-development/handson-walkthrough-create-service-api.html)
* [Logical Layer Architecture](https://source.github.io/source.webapi/docs/architecture/logical-layers.html)
* [Technology Stack](https://source.github.io/source.webapi/docs/technical-reference/technology-stack.html)
