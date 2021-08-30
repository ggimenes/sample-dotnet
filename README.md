

# Documentation
- [Business documentation page](https://github.com/ggimenes/sample-dotnet/tree/main/doc)
- [Event Storming](https://github.com/ggimenes/sample-dotnet/tree/main/doc/diagrams/event-storming)

# Instalation

**SKIP STEPS IF YOU ALREADY HAVE THE FOLLOWING INSTANCES RUNNING ON YOUR MACHINE**

**MongoDb**

folder deploy/mongodb
run:
"docker build -t dockerfile/mongo:latest  ."

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