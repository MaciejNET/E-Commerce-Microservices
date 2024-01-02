FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /app
COPY . .
RUN dotnet publish src/Services/Users/ECommerce.Services.Users.Api -c release -o out

FROM mcr.microsoft.com/dotnet/sdk:7.0
WORKDIR /app
COPY --from=build /app/out .
ENV ASPNETCORE_URLS http://*:80
ENV ASPNETCORE_ENVIRONMENT docker
ENTRYPOINT dotnet ECommerce.Services.Users.Api.dll