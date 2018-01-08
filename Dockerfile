FROM microsoft/aspnetcore

RUN curl -sL https://deb.nodesource.com/setup_9.x | bash -
RUN apt-get install --assume-yes nodejs

WORKDIR /app
COPY . .
CMD ASPNETCORE_URLS=http://*:$PORT dotnet WheelOfFate.Web.dll

# FROM microsoft/aspnetcore
# ARG source
# EXPOSE 80 5102
# ENV ASPNETCORE_URLS http://*:80
# RUN apt-get -qq update && apt-get -qqy --no-install-recommends install wget gnupg \
#     git \
#     unzip

# RUN curl -sL https://deb.nodesource.com/setup_6.x |  bash -
# RUN apt-get install -y nodejs
# WORKDIR /app
# COPY ${source:-obj/Docker/publish} .
# ENTRYPOINT ["dotnet", "Project.dll"]