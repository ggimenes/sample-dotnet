
# Sample project
This is a sample of a microservice based project

## Tecnologies / Patterns
- Kubernetes
- Docker
- Net 5.0
- MongoDb
- Masstransit
- Seq
- Serilog
- Zipkin
- Tye
- RabbitMq
- Swagger
- SignalR 
- Angular
- Typescript
- DDD
- SOLID
- Saga

## Architecture Overview
- 1 database per microservice
- microservices based on bounded contexts
- communication through queues using RabbitMq and Masstransit
- Front-end communication using HTTP and SignalR
- Resilience using retries
- Centralized log server
- Distributed trancing

# Documentation
- [Business documentation page](https://github.com/ggimenes/sample-dotnet/tree/main/doc)
- [Event Storming](https://github.com/ggimenes/sample-dotnet/tree/main/doc/diagrams/event-storming)

# Instalation

**SKIP STEPS IF YOU ALREADY HAVE THE FOLLOWING INSTANCES RUNNING ON YOUR MACHINE**

**MongoDb**

folder deploy/mongodb
run:
"docker build -t mongo:latest  ."

run image:
"docker run -d --name mongo -p 27017:27017 dockerfile/mongo"


**RabbitMq**

get image
run:
"docker pull bitnami/rabbitmq:latest"

run image:
"docker run -d --name rabbitmq -p 5672:5672 -p 15672:15672 --restart=always --hostname rabbitmq-master bitnami/rabbitmq:latest"


**Seq (log server)**

run image:
"docker run --name seq -d --restart unless-stopped -e ACCEPT_EULA=Y -p 5341:80 datalust/seq:latest"

UI:
http://localhost:5341/

**Tye**

run:
"dotnet tool install -g Microsoft.Tye --version 0.9.*"

folder sample-dotnet/src
run:
"tye run --watch"

# Prints

**Seq**  
![Seq](imgs/seq.JPG?raw=true "Seq")

**Database per microservice**
![database per microservice](imgs/database-per-microservice.JPG?raw=true "Database per microservice")

**Saga**  
![masstransit-saga](imgs/masstransit-saga.JPG?raw=true "Saga")

**Tye Dashboard**  
![Tye Dashboard](imgs/tye-dashboard.JPG?raw=true "Tye Dashboard")
