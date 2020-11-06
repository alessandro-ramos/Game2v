FROM mcr.microsoft.com/dotnet/core/sdk:2.2
COPY bin/Release/netcoreapp2.2/publish/ App/
COPY Game2v.db App/
WORKDIR /App
EXPOSE 80
ENTRYPOINT ["dotnet", "Game2v.dll"]
