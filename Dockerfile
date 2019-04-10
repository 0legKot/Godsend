FROM microsoft/dotnet:2.2-sdk AS build

RUN apt-get update \
    && apt-get install -y --no-install-recommends ca-certificates

RUN git clone git://github.com/0legKot/Godsend app

WORKDIR /app/TelegramBot
RUN dotnet restore 

WORKDIR /app/Godsend
RUN dotnet restore

WORKDIR /app/TelegramBot
RUN dotnet publish --no-restore -c Release -o out

FROM microsoft/dotnet:2.2-aspnetcore-runtime AS runtime
COPY --from=build /app/TelegramBot/out ./
ENTRYPOINT [ "dotnet", "TelegramBot.dll" ]
