FROM node:10 as build

WORKDIR /app

COPY package.json package-lock.json ./

RUN npm ci

COPY . ./

RUN npx ng build --prod

FROM nginx:1.15

RUN rm -rf /usr/share/nginx/html/*
COPY nginx-custom.conf /etc/nginx/conf.d/default.conf

COPY --from=build /app/dist/* /usr/share/nginx/html/
