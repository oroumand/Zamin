#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["Zamin.Extensions.DependentyInjection.Sample/Zamin.Extensions.DependentyInjection.Sample.csproj", "Zamin.Extensions.DependentyInjection.Sample/"]
RUN dotnet restore "Zamin.Extensions.DependentyInjection.Sample/Zamin.Extensions.DependentyInjection.Sample.csproj"
COPY . .
WORKDIR "/src/Zamin.Extensions.DependentyInjection.Sample"
RUN dotnet build "Zamin.Extensions.DependentyInjection.Sample.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Zamin.Extensions.DependentyInjection.Sample.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Zamin.Extensions.DependentyInjection.Sample.dll"]