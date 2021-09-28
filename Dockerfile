FROM mcr.microsoft.com/dotnet/aspnet:5.0-focal AS base
WORKDIR /app
EXPOSE 5000
EXPOSE 5001

ENV ASPNETCORE_URLS=http://+:5000;https://+:5001

RUN adduser -u 5678 --disabled-password --gecos "" appuser && chown -R appuser /app
USER appuser

FROM mcr.microsoft.com/dotnet/sdk:5.0-focal AS build
WORKDIR /src
COPY ["Presentation/Presentation.csproj", "Presentation/"]
COPY ["Presentation.Tests/Presentation.Tests.csproj", "Presentation.Tests/"]
COPY ["Application/Application.csproj", "Application/"]
COPY ["Application.Tests/Application.Tests.csproj", "Application.Tests/"]
COPY ["API.Template.sln", "."]
RUN dotnet restore 

COPY . .
WORKDIR "/src/Presentation"
RUN dotnet build "Presentation.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Presentation.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .

ENTRYPOINT ["dotnet", "Presentation.dll"]