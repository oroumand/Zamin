#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["Zamin.Extensions.Caching.Distributed.Redis.Sample/Zamin.Extensions.Caching.Distributed.Redis.Sample.csproj", "Zamin.Extensions.Caching.Distributed.Redis.Sample/"]
RUN dotnet restore "Zamin.Extensions.Caching.Distributed.Redis.Sample/Zamin.Extensions.Caching.Distributed.Redis.Sample.csproj"
COPY . .
WORKDIR "/src/Zamin.Extensions.Caching.Distributed.Redis.Sample"
RUN dotnet build "Zamin.Extensions.Caching.Distributed.Redis.Sample.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Zamin.Extensions.Caching.Distributed.Redis.Sample.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Zamin.Extensions.Caching.Distributed.Redis.Sample.dll"]