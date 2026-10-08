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

# Copy the built Angular files
COPY --from=build /app/frontend/dist/chicago-311-frontend /usr/share/nginx/html

# Start nginx
CMD ["nginx", "-g", "daemon off;"]