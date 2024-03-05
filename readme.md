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