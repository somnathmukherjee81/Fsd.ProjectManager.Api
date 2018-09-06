dotnet restore
dotnet build
dotnet test
dotnet publish ./src/Fsd.ProjectManager.Api/Fsd.ProjectManager.Api.csproj -c Release

REM Uncomment to run on docker
REM docker rmi -f project-manager-api
REM docker build -t project-manager-api ./src/Fsd.ProjectManager.Api/
REM docker images
REM docker run -p 9090:9090 project-manager-api

REM Uncomment to run with dotnet standalone
dotnet ./src/Fsd.ProjectManager.Api/bin/release/netcoreapp2.0/publish/Fsd.ProjectManager.Api.dll