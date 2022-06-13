#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["Zamin.Utilities.SoftwarePartDetector.Sample/Zamin.Utilities.SoftwarePartDetector.Sample.csproj", "Zamin.Utilities.SoftwarePartDetector.Sample/"]
RUN dotnet restore "Zamin.Utilities.SoftwarePartDetector.Sample/Zamin.Utilities.SoftwarePartDetector.Sample.csproj"
COPY . .
WORKDIR "/src/Zamin.Utilities.SoftwarePartDetector.Sample"
RUN dotnet build "Zamin.Utilities.SoftwarePartDetector.Sample.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Zamin.Utilities.SoftwarePartDetector.Sample.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Zamin.Utilities.SoftwarePartDetector.Sample.dll"]