#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:5.0 AS base
WORKDIR /app
EXPOSE 80

ENV ASPNETCORE_URLS=http://+:8000;http://+:80;
ENV ASPNETCORE_ENVIRONMENT=Development

FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
WORKDIR /src
COPY ["OdevezApi/Odevez.API.csproj", "OdevezApi/"]
COPY ["Business/Odevez.Business.csproj", "Business/"]
COPY ["Repository/Odevez.Repository.csproj", "Repository/"]
COPY ["Odevez.Utils/Odevez.Utils.csproj", "Odevez.Utils/"]
COPY ["DTO/Odevez.DTO.csproj", "DTO/"]
RUN dotnet restore "OdevezApi/Odevez.API.csproj"
COPY . .
WORKDIR "/src/OdevezApi"
RUN dotnet build "Odevez.API.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Odevez.API.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Odevez.API.dll"]