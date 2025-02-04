FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build-env
WORKDIR /app

COPY LibraNet.csproj ./
RUN dotnet restore

COPY . ./
RUN dotnet publish -c Release -o /app/out

FROM mcr.microsoft.com/dotnet/aspnet:8.0
WORKDIR /app

COPY --from=build-env /app/out .

EXPOSE 80
EXPOSE 443

ENV ASPNETCORE_URLS=http://0.0.0.0:80

CMD ["dotnet", "LibraNet.dll", "--urls", "http://0.0.0.0:80"]