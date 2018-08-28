FROM microsoft/aspnetcore:2.0
ARG source
WORKDIR /app
EXPOSE 9090
EXPOSE 9091
COPY ${source:-src/Fsd.Capsule.TaskManagerApi/bin/release/netcoreapp2.0/publish} .
ENTRYPOINT ["dotnet", "Fsd.Capsule.TaskManagerApi.dll"]