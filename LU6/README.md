# LU6: Docker

[Creating images and publishing](https://youtu.be/XlNHJQqEpW0)<br>


[Multi-container orchestration](https://youtu.be/1MOlJq4tTUc) <br> <br>

Remember the following when creating your .yml file for multi-container orchestratrion:

```yaml

services: #this tag defines the individual containers. Every child element of this tag is a container 
  
  sqlserver: #child element of services, therefore this is a container
    image: mcr.microsoft.com/mssql/server:latest 
    container_name: sqlserver_db
    environment:
      ACCEPT_EULA: "Y"
      SA_PASSWORD: #server-admin password
    ports:
      - "1433:1433"
    volumes: #for persisting data storage. Everything we create will be stored in mssqldata -> this is managed by docker
      - mssqldata:/var/opt/mssql

  api: #child element of services, therefore this is a container
    build:
      context: .
      dockerfile: #The dockerfile responsible for setting up the RESTFul API
    container_name: csharp_api
    ports:
      - "7185:8080" #5000 didn't work for me, so I changed it to my API, if 5000 works for you, that's fine
    environment:
      - ASPNETCORE_ENVIRONMENT=Development
      - ConnectionStrings__DefaultConnection #The database you're connecting to. For our class activity, we used our Tasks Database. Refer to that exercise
    depends_on:
      - sqlserver

  web: #child element of services, therefore this is a container
    build:
      context: . #this just means, "set context to the current folder"
      dockerfile: #The dockerfile responsible for setting up the MVC app
    container_name: csharp_mvc_web
    ports:
      - "7122:8080" #5000 didn't work for me, so I changed it to my MVC app, if 5000 works for you, that's fine
    environment:
      - ASPNETCORE_ENVIRONMENT=Development
      - ApiSettings__BaseUrl=http://api:8080
    depends_on:
      - api
#Remember, depends_on will determine the startup order. In our Tasks example, we want the SqlServer to run first, then the API then the MVC app

volumes:
  mssqldata:
```
