#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:5.0 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
WORKDIR /src
COPY ["OdevezApi/Odevez.API.csproj", "OdevezApi/"]
COPY ["Business/Odevez.Business.csproj", "Business/"]
COPY ["DTO/Odevez.DTO.csproj", "DTO/"]
COPY ["Odevez.Utils/Odevez.Utils.csproj", "Odevez.Utils/"]
COPY ["Odevez.Services/Odevez.Services.csproj", "Odevez.Services/"]
COPY ["Repository/Odevez.Repository.csproj", "Repository/"]
RUN dotnet restore "OdevezApi/Odevez.API.csproj"
COPY . .
WORKDIR "/src/OdevezApi"
RUN dotnet build "Odevez.API.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Odevez.API.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Odevez.API.dll"]