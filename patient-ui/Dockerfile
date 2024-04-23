# Stage 1: Build the Angular application
FROM node:20 as build

WORKDIR /app

# Copy the source code and install dependencies
COPY patient-ui/ /app
RUN npm install -g @angular/cli
RUN npm install

# Build the project for production
RUN ng build --configuration production

# Stage 2: Serve the app with Nginx
FROM nginx:alpine

RUN rm -rf /usr/share/nginx/html/*

COPY --from=build /app/dist/patient-ui /usr/share/nginx/html
COPY patient-ui/nginx.conf /etc/nginx/nginx.conf
EXPOSE 8080
CMD ["nginx", "-g", "daemon off;"]
