#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["Zamin.Extentions.ObjectMappers.AutoMapper.Sample/Zamin.Extentions.ObjectMappers.AutoMapper.Sample.csproj", "Zamin.Extentions.ObjectMappers.AutoMapper.Sample/"]
RUN dotnet restore "Zamin.Extentions.ObjectMappers.AutoMapper.Sample/Zamin.Extentions.ObjectMappers.AutoMapper.Sample.csproj"
COPY . .
WORKDIR "/src/Zamin.Extentions.ObjectMappers.AutoMapper.Sample"
RUN dotnet build "Zamin.Extentions.ObjectMappers.AutoMapper.Sample.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Zamin.Extentions.ObjectMappers.AutoMapper.Sample.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Zamin.Extentions.ObjectMappers.AutoMapper.Sample.dll"]