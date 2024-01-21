FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["Expense_Management_Api/Expense_Management_Api.csproj", "Expense_Management_Api/"]
RUN dotnet restore "Expense_Management_Api/Expense_Management_Api.csproj"
COPY . .
WORKDIR "/src/Expense_Management_Api"
RUN dotnet build "Expense_Management_Api.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "Expense_Management_Api.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Expense_Management_Api.dll"]
