#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["Server/Server.csproj", "Server/"]
COPY ["DbManager/DbProvider.csproj", "DbManager/"]
COPY ["SharedModels/SharedModels.csproj", "SharedModels/"]
COPY ["RoomManager/RoomManager.csproj", "RoomManager/"]
RUN dotnet restore "Server/Server.csproj"
COPY . .
WORKDIR "/src/Server"
RUN dotnet build "Server.csproj" -c Debug -o /app/build

FROM build AS publish
RUN dotnet publish "Server.csproj" -c Debug -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Server.dll"]