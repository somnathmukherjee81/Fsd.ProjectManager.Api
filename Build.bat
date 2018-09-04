dotnet restore
dotnet build
dotnet test
dotnet publish ./src/Fsd.ProjectManager.Api/Fsd.ProjectManager.Api.csproj -c Release
docker rmi -f project-manager-api
docker build -t project-manager-api ./src/Fsd.ProjectManager.Api/
docker images
REM docker run -p 9090:9090 project-manager-api