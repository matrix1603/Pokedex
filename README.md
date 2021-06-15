# Pokedex API


## Prerequisites
  
- [NET Core SDK 3.1](https://dotnet.microsoft.com/download)
- [Docker client](https://www.docker.com/products/docker-desktop)
- [Git](https://git-scm.com/download)

## Download code

```
git clone https://github.com/sarbjit16/Pokedex.git
```

## Run in a Linux container

- In the Docker client, switch to Linux containers.
- Navigate to the Dockerfile folder at *Pokedex/PokeDex.Web.API/*
- Run the following commands to build and run the sample in Docker:

Console
```
docker build -t pokedox-api -f Dockerfile ..
docker run -it --rm -p 8080:80 --name PokedoxApi pokedox-api
```

- Go to http://localhost:8080 in a browser to view API endpoints

![image](https://user-images.githubusercontent.com/41857451/122128318-6bf47d80-ce2c-11eb-931f-a4f874a19102.png)


## Endpoints
