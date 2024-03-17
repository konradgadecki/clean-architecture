# Clean Architecture

## create first project
```
dotnet new webapi -o GymManagement.Api
dotnet new classlib -o GymManagement.Application
dotnet new classlib -o GymManagement.Infrastructure
dotnet new classlib -o GymManagement.Domain
dotnet new classlib -o GymManagement.Contracts
```

## create dependencies
```
dotnet add GymManagement.Api reference GymManagement.Application
dotnet add GymManagement.Infrastructure reference GymManagement.Application
dotnet add GymManagement.Application reference GymManagement.Domain
dotnet add GymManagement.Api reference GymManagement.Contracts
dotnet add GymManagement.Api reference GymManagement.Infrastructure
```

## create a solution
```
dotnet new sln --name "GymManagement"
```

## add all projects to the solution 
```
dotnet sln add **/**.csproj
```

## build the project 
```
dotnet build
```

## run the project 
```
dotnet run --project src/GymManagement.Api
```

## entity framework config
> Install SQLite VSCode extension

### install design package for migrations
```
dotnet add src/GymManagement.Api package Microsoft.EntityFrameworkCore.Design --version 7.0.0
```
  
### create a migration
```
dotnet ef migrations add InitialCreate -p src/GymManagement.Infrastructure -s src/GymManagement.Api
```
  
### update a database
```
dotnet ef database update -p src/GymManagement.Infrastructure -s src/GymManagement.Api
```