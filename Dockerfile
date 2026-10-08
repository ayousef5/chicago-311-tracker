# Build the Angular app
FROM node:22-alpine AS build

WORKDIR /app

# Copy Angular package files
COPY frontend/package*.json ./frontend/

# Install dependencies
RUN cd frontend && npm ci

# Copy the Angular source code
COPY frontend/ ./frontend/

# Build the Angular app
RUN cd frontend && npm run build

# Serve the Angular app with nginx
FROM nginx:alpine

# Listen on Railway's PORT (80 if it is not set)
ENV PORT=80
COPY frontend/nginx.conf.template /etc/nginx/templates/default.conf.template

# Copy the built Angular files (Angular writes them to the browser/ folder)
COPY --from=build /app/frontend/dist/chicago-311-frontend/browser /usr/share/nginx/html