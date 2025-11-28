# Build
FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build

WORKDIR /src

COPY ["EFCoreCourse.sln", "."]

# Copy csproj content 
COPY ["EFCoreCourse/EFCoreCourse.csproj", "EFCoreCourse/"]

RUN dotnet restore "EFCoreCourse/EFCoreCourse.csproj"

# Copy everything else and build
COPY . .
RUN dotnet publish "EFCoreCourse/EFCoreCourse.csproj" -c Release -o /app/publish /p:UseAppHost=false

# Runtime
FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS runtime
WORKDIR /app
COPY --from=build /app/publish .
ENTRYPOINT ["dotnet", "EFCoreCourse.dll"]