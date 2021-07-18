# this file will build the react app and install nginx server + envsubst and replace ngnix default config file with nginx.config which can be found in this directory
FROM node:14.1-alpine AS builder
ENV NODE_ENV=production
WORKDIR /app
COPY Web/package*.json ./
RUN npm install
COPY Web/. ./
RUN npm run build

FROM nginx:1.17-alpine
RUN apk --no-cache add curl
RUN curl -L https://github.com/a8m/envsubst/releases/download/v1.1.0/envsubst-`uname -s`-`uname -m` -o envsubst && \
    chmod +x envsubst && \
    mv envsubst /usr/local/bin
COPY ./Web/nginx.config /etc/nginx/nginx.template
CMD ["/bin/sh", "-c", "envsubst < /etc/nginx/nginx.template > /etc/nginx/conf.d/default.conf && nginx -g 'daemon off;'"]
COPY --from=builder /app/build /usr/share/nginx/html

# FROM node:14.16.0-alpine3.13 AS build
# ENV NODE_ENV=production
# WORKDIR /app
# COPY Web/package*.json ./
# RUN npm install --production --silent
# COPY Web/. .
# RUN npm run build

# FROM nginx:1.12-alpine
# COPY --from=build /app/build /usr/share/nginx/html
# EXPOSE 80
# CMD ["nginx", "-g", "daemon off;"]