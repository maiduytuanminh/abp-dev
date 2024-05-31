#!/bin/bash
while ping -c1 publish &> /dev/null 
do echo "Waiting for publishing to be done"; sleep 10; 
done;

echo "Expo app"

cd /app/app

echo "@smartsoftware:registry=http://verdaccio:4873" >> .npmrc

npx npm-check-updates --filter '/^@(smartsoftware)\/.*$/' --registry http://verdaccio:4873 --target greatest --packageFile package.json -u

yarn

yarn ng build --prod

cd dist/MyProjectName

npx http-server-spa . index.html 4200