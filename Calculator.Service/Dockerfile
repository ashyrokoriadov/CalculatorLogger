FROM microsoft/dotnet:2.2-aspnetcore-runtime AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM microsoft/dotnet:2.2-sdk AS build
WORKDIR /src
COPY ["Calculator.Service/Calculator.Service.csproj", "Calculator.Service/"]
RUN dotnet restore "Calculator.Service/Calculator.Service.csproj"
COPY . .
WORKDIR "/src/Calculator.Service"
RUN dotnet build "Calculator.Service.csproj" -c Release -o /app

FROM build AS publish
RUN dotnet publish "Calculator.Service.csproj" -c Release -o /app

FROM base AS final
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "Calculator.Service.dll"]