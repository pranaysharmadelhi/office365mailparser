#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/core/aspnet:2.2-stretch-slim AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/core/sdk:2.2-stretch AS build
WORKDIR /src
COPY ["Office365MailParser.Web.Processor/Office365MailParser.Web.Processor.csproj", "Office365MailParser.Web.Processor/"]
COPY ["Office365MailParser.Core/Office365MailParser.Core.csproj", "Office365MailParser.Core/"]
RUN dotnet restore "Office365MailParser.Web.Processor/Office365MailParser.Web.Processor.csproj"
COPY . .
WORKDIR "/src/Office365MailParser.Web.Processor"
RUN dotnet build "Office365MailParser.Web.Processor.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Office365MailParser.Web.Processor.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Office365MailParser.Web.Processor.dll"]