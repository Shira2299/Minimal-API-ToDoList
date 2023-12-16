# WebAPI

## .NET
#### * In this project we will use Visual Studio Code as our code editor
  
For this purpose, this plugin must be installed:

vscode:extension/ms-dot net tools.vscode-dotnet-pack

### Create a project
The command to create a new Web Api project:

dotnet new web -o TodoApi

#### * The command to run the application:

dotnet watch run

#### * Installed Packages:

Microsoft.EntityFrameworkCore 
Microsoft.EntityFrameworkCore.Design
Pomelo.EntityFrameworkCore.MySql
Microsoft.EntityFrameworkCore.Tools

#### * Create the Model:

dotnet ef dbcontext scaffold Name=ToDoDB Pomelo.EntityFrameworkCore.MySql  -f -c ToDoDbContext

#### * In case the changes are not reflected in the run, we will run a build command:

dotnet build

followed by again:   

dotnet watch run

### * CONTAIN: CORS, SWAGGER, JWT

## React

### Tasks app
Actions CRUD

## MYSQL
Database

ש

ש



