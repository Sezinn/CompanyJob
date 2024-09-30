#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER app
WORKDIR /app
EXPOSE 5000
EXPOSE 5001

ENV TZ=Europe/Istanbul
RUN echo $TZ > /etc/timezone && \
    apt-get update && apt-get install -y tzdata && \
    rm /etc/localtime && \
    ln -snf /usr/share/zoneinfo/$TZ /etc/localtime && \
    dpkg-reconfigure -f noninteractive tzdata && \
    apt-get clean

ENV DOTNET_SYSTEM_GLOBALIZATION_INVARIANT=false \
    LC_ALL=tr_TR.UTF-8 \
    LANG=tr_TR.UTF-8

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["EmployerJob.API/EmployerJob.API.csproj", "EmployerJob.API/"]
COPY ["EmployerJob.Application/EmployerJob.Application.csproj", "EmployerJob.Application/"]
COPY ["EmployerJob.Domain/EmployerJob.Domain.csproj", "EmployerJob.Domain/"]
COPY ["EmployerJob.Infrastructure/EmployerJob.Infrastructure.csproj", "EmployerJob.Infrastructure/"]
RUN dotnet restore "./EmployerJob.API/EmployerJob.API.csproj"
COPY . .
WORKDIR "/src/EmployerJob.API"
RUN dotnet build "./EmployerJob.API.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./EmployerJob.API.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "EmployerJob.API.dll"]