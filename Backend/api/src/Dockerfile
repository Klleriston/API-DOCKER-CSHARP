FROM klleriston/databasemovienet:1.0 AS db

FROM --platform=$BUILDPLATFORM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG configuration=Release
WORKDIR /src
COPY ["./", "Backend/api/src/"]
RUN dotnet restore "Backend/api/src/src.csproj"
COPY . .
WORKDIR "/src/Backend/api/src"

RUN dotnet tool install --global dotnet-ef

ENV PATH="$PATH:/root/.dotnet/tools"

RUN dotnet ef database update

RUN dotnet build "src.csproj" -c $configuration -o /app/build

FROM build AS publish
ARG configuration=Release
RUN dotnet publish "src.csproj" -c $configuration -o /app/publish /p:UseAppHost=false

FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS final
WORKDIR /app
COPY --from=publish /app/publish .
EXPOSE 8081
ENV ASPNETCORE_URLS=http://+:8081
USER app
ENTRYPOINT ["dotnet", "src.dll"]
