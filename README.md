# Stackbuld Technical Assessment

## Overview

[Description]
This is a production-grade stackbuld technical assessment. The application supports product catalog management, order processing, and stock validation.

## Folder Structure

```
|--- .vscode
|--- src
|    |--- stackbuldtechnicalassessment.Application
|    |--- stackbuldtechnicalassessment.Application.Test
|    |--- stackbuldtechnicalassessment.Domain
|    |--- stackbuldtechnicalassessment.Infrastructure
|    |--- stackbuldtechnicalassessment.web
|         |--- Controllers
|         |--- Program.cs
|         |--- appsettings.json
|         |--- appsettings.Development.json
|--- .gitignore
|--- stackbuldtechnicalassessment.Csharp.Web.sln
```

## Getting started

Ensure you have your computer setup correctly for development by installing the following

- .Net 8 with C# 12.0
- Visual studio 2019 or higher with ASP.Net web installation pack or
- Visual studio code with .Net and C# dev extensions installed
- Install .Net maui development pack for future uses

  ## Setup Instructions

### 1. Clone the Repository

First, clone the repository to your local machine using Git.

```sh
git clone https://github.com/OBE96/StackbuldTechnicalAssessment.git 
cd StackbuldTechnicalAssessment
```

### 2. Install Dependencies

Opening the solution in Visual studio should automatically restore all your dependencies, you can ensure this by right clicking on the solution explorer and clicking `Restore dependencies`.

If you are using Vscode with the required installations mentioned above, navigate to the project directory and install the required dependencies.

```sh
dotnet restore
```

### 3. Run the Development Server

Press `F5` on your keyboard to run the application in debug mode for both Visual studio and Vscode (You may need to open a .cs file to trigger this).

Alternatively you can `cd` into `src/Hng.Web` project and run the command

```sh
dotnet run
```

### 4. Verify the Setup

Depending on the IDE/code editor, you should be greeted with the Swagger documentation page else navigate to `/swagger` to view the documentation

## Database Connection
Update your appsettings.development file with details of your postgress connection

```
"DefaultConnectionString": "Server=server-here; Port=5432; Database=UserDb;User Id=postgres;Password=yourpasswordhere"
```

