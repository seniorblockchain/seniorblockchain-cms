#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["Directory.Packages.props", "."]
COPY ["Directory.Build.props", "."]
COPY ["src/powerpage/powerpage.csproj", "src/powerpage/"]
COPY ["src/powerpage.Common/powerpage.Common.csproj", "src/powerpage.Common/"]
COPY ["src/powerpage.Entities/powerpage.Entities.csproj", "src/powerpage.Entities/"]
COPY ["src/powerpage.Services/powerpage.Services.csproj", "src/powerpage.Services/"]
COPY ["src/powerpage.DataLayer/powerpage.DataLayer.csproj", "src/powerpage.DataLayer/"]
COPY ["src/powerpage.ViewModels/powerpage.ViewModels.csproj", "src/powerpage.ViewModels/"]
COPY ["src/powerpage.IocConfig/powerpage.IocConfig.csproj", "src/powerpage.IocConfig/"]
COPY ["src/powerpage.DataLayer.InMemoryDatabase/powerpage.DataLayer.InMemoryDatabase.csproj", "src/powerpage.DataLayer.InMemoryDatabase/"]
COPY ["src/powerpage.DataLayer.MSSQL/powerpage.DataLayer.MSSQL.csproj", "src/powerpage.DataLayer.MSSQL/"]
COPY ["src/powerpage.DataLayer.SQLite/powerpage.DataLayer.SQLite.csproj", "src/powerpage.DataLayer.SQLite/"]
RUN dotnet restore "src/powerpage/powerpage.csproj"
COPY . .
WORKDIR "/src/src/powerpage"
RUN dotnet build "powerpage.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "powerpage.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "powerpage.dll"]