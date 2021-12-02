#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:5.0 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
WORKDIR /src
COPY ["MyBudgetAPI/MyBudgetAPI.csproj", "MyBudgetAPI/"]
COPY ["MyBudgetApi.Services/MyBudgetApi.Services.csproj", "MyBudgetApi.Services/"]
COPY ["MyBudgetApi.Core/MyBudgetApi.Core.csproj", "MyBudgetApi.Core/"]
COPY ["MyBudgetApi.Services.Abstractions/MyBudgetApi.Services.Abstractions.csproj", "MyBudgetApi.Services.Abstractions/"]
COPY ["MyBudgetApi.Data/MyBudgetApi.Data.csproj", "MyBudgetApi.Data/"]
RUN dotnet restore "MyBudgetAPI/MyBudgetAPI.csproj"
COPY . .
WORKDIR "/src/MyBudgetAPI"
RUN dotnet build "MyBudgetAPI.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "MyBudgetAPI.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "MyBudgetAPI.dll"]