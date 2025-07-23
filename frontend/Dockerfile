FROM node:18 AS build
WORKDIR /app

#Prod
# COPY . . 

# RUN npm install

#Prod
# RUN npm run build 

#Prod
# FROM nginx:alpine 

#Prod
# COPY --from=build /app/dist /usr/share/nginx/html 

# Dev
COPY package*.json ./
RUN npm install
CMD ["npm", "run", "dev", "--", "--host", "0.0.0.0"] 