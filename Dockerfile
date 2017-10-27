FROM microsoft/dotnet:2.0.0-sdk AS build
WORKDIR /app
# copy csproj and restore as distinct layers
COPY *.csproj ./
RUN dotnet restore
# copy everything else and build
COPY . ./
RUN dotnet publish -c Release -o out
# build runtime image
FROM microsoft/dotnet:2.0.0-runtime
WORKDIR /app
COPY --from=build /app/out .

ENTRYPOINT ["dotnet", "TodoScheduledJob.dll"]