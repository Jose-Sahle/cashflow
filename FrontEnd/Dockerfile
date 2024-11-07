# Use a imagem oficial do Node.js para construir a aplicação Angular
FROM node:14 AS build
# Defina o diretório de trabalho
WORKDIR /app
# Copie o package.json e o package-lock.json
COPY package*.json ./
# Instale as dependências
RUN npm install
# Copie o restante do código da aplicação
COPY . .
# Construa a aplicação Angular
RUN npm run build --prod
# Use uma imagem do Nginx para servir a aplicação Angular
FROM nginx:alpine
# Copie os arquivos construídos para o diretório padrão do Nginx
COPY --from=build /app/dist /usr/share/nginx/html
# Copie o arquivo de configuração para o diretório de assets
COPY src/assets/appconfig-docker.json /usr/share/nginx/html/assets/appconfig.json
# Exponha a porta 80
EXPOSE 80
# Comando para iniciar o Nginx
CMD ["nginx", "-g", "daemon off;"]
