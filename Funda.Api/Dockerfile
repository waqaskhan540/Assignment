

FROM mcr.microsoft.com/dotnet/aspnet:5.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
WORKDIR /src
COPY ["Funda.Api/Funda.Api.csproj", "Funda.Api/"]
RUN dotnet restore "Funda.Api/Funda.Api.csproj"
COPY . .
WORKDIR "/src/Funda.Api"
RUN dotnet build "Funda.Api.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Funda.Api.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Funda.Api.dll"]