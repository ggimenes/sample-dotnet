

# Documentation
- [Business documentation page](https://github.com/ggimenes/sample-dotnet/tree/main/doc)
- [Event Storming](https://github.com/ggimenes/sample-dotnet/tree/main/doc/diagrams/event-storming)

# Instalation

**SKIP STEPS IF YOU ALREADY HAVE THE FOLLOWING INSTANCES RUNNING ON YOUR MACHINE**

MongoDb

folder deploy/mongodb
run:
"docker build -t dockerfile/mongo:latest  ."

run image:
"docker run -d --name mongo -p 27017:27017 dockerfile/mongo"
