FROM mcr.microsoft.com/dotnet/runtime:7.0 AS base
WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["PatientDatabase/PatientDatabase.csproj", "PatientDatabase/"]
RUN dotnet restore "PatientDatabase/PatientDatabase.csproj"
COPY . .
WORKDIR "/src/PatientDatabase"
RUN dotnet build "PatientDatabase.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "PatientDatabase.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "PatientDatabase.dll"]
