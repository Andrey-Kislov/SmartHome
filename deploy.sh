#!/bin/bash
set -ev

docker build -t andead/smarthome.presentation.api:latest publish/.

docker login -u $DOCKER_USERNAME -p $DOCKER_PASSWORD
docker push andead/smarthome.presentation.api:latest