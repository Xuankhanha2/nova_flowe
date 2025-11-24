# Step 1: Build stage
FROM node:18-alpine AS build

WORKDIR /app

# Copy file package.json và package-lock.json (nếu có)
COPY package*.json ./

# Cài dependencies
RUN npm install

# Copy toàn bộ project vào container
COPY . .

# Build production code (thường tạo folder dist/)
RUN npm run build

# Step 2: Serve với web server tĩnh (vd: nginx)
FROM nginx:alpine

# Copy file build từ bước build sang folder phục vụ của nginx
COPY --from=build /app/dist /usr/share/nginx/html

EXPOSE 80

CMD ["nginx", "-g", "daemon off;"]