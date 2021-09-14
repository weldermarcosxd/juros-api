#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:5.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
WORKDIR /src
COPY ["src/CalculoJurosService/CalculoJurosService.csproj", "src/CalculoJurosService/"]
COPY ["src/Infrastructure/Infrastructure.csproj", "src/Infrastructure/"]
COPY ["src/Application/Application.csproj", "src/Application/"]
RUN dotnet restore "src/CalculoJurosService/CalculoJurosService.csproj"
COPY . .
WORKDIR "/src/src/CalculoJurosService"
RUN dotnet build "CalculoJurosService.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "CalculoJurosService.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "CalculoJurosService.dll"]
