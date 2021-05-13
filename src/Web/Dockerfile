FROM node:14.16.0-alpine3.13 AS build
ENV NODE_ENV=production
WORKDIR /app
COPY Web/package*.json ./
RUN npm install --production --silent
COPY Web/. .
RUN npm run build

FROM nginx:1.12-alpine
COPY --from=build /app/build /usr/share/nginx/html
EXPOSE 80
ENTRYPOINT ["nginx", "-g", "daemon off;"]
