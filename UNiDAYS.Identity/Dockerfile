FROM microsoft/dotnet:2.1-aspnetcore-runtime AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM microsoft/dotnet:2.1-sdk AS build
WORKDIR /src
COPY ["UNiDAYS.Identity/UNiDAYS.Identity.csproj", "UNiDAYS.Identity/"]
RUN dotnet restore "UNiDAYS.Identity/UNiDAYS.Identity.csproj"
COPY . .
WORKDIR "/src/UNiDAYS.Identity"
RUN dotnet build "UNiDAYS.Identity.csproj" -c Release -o /app

FROM build AS publish
RUN dotnet publish "UNiDAYS.Identity.csproj" -c Release -o /app

FROM base AS final
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "UNiDAYS.Identity.dll"]