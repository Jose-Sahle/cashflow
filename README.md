# Cashflow

Este projeto é dividido em duas partes principais:

1. **FrontEnd**: Um projeto Angular responsável pela interface do usuário.
2. **Server**: Uma WebAPI desenvolvida em ASP.NET Core .NET 8, estruturada no conceito DDD (Domain-Driven Design).

## Estrutura do Projeto
cashflow 
   ├── FrontEnd 
   └── Server 
         ├── API 
		 ├── Application 
		 ├── Domain 
		 ├── Infrastructure 
		 └── Models


### Descrição da Estrutura e Funcionamento

O **projeto FrontEnd**, desenvolvido em Angular, acessa a **WebAPI** para incluir transações financeiras. Os dados são enviados para o back-end em formato JSON, onde são processados e encaminhados para uma fila no RabbitMQ.

No **Server**:
- A WebAPI possui um endpoint acessado pelo FrontEnd para envio das transações.
- Um serviço interno, do tipo `IHostedService`, chamado **RabbitMqConsumerService**, lê todas as mensagens da fila no RabbitMQ e as armazena em um banco de dados MongoDB.

### Kubernetes

O diretório `cashflow/kubernates` contém os arquivos de construção das imagens Docker e os manifestos para a criação dos pods.

## Imagens Docker

### Front-End
No diretório `FrontEnd`, há um `Dockerfile` para criar a imagem do aplicativo Angular.

Para construir e enviar a imagem, execute:

docker build -t josesahle/cashflow:client .
docker push josesahle/cashflow:client

### Server
No diretório Server, há um Dockerfile para criar a imagem da WebAPI.

Para construir e enviar a imagem, execute:

docker build -t josesahle/cashflow:server .
docker push josesahle/cashflow:server

## Kubernetes
No diretório kubernates, você encontra os manifestos necessários para criação dos pods. Para aplicar as configurações e implantar os serviços, execute:

kubectl apply -f rabbitmq-deployment.yaml
kubectl apply -f mongo-deployment.yaml
kubectl apply -f banktransactionservice-api-deployment.yaml
kubectl apply -f finance-client-deployment.yaml

# Estrutura de Pastas

- **FrontEnd**: Contém o projeto Angular.
- **Server**: Estrutura da WebAPI baseada em DDD:
  - **API**: Camada de apresentação dos endpoints.
  - **Application**: Lógica de aplicação, incluindo serviços de domínio.
  - **Domain**: Entidades de domínio e interfaces.
  - **Infrastructure**: Implementações de repositórios e integração com banco de dados e serviços externos.
  - **Models**: Modelos de dados usados para comunicação entre as camadas.

# Tecnologias Utilizadas

- **ASP.NET Core (.NET 8)** para o back-end.
- **Angular** para o front-end.
- **RabbitMQ** para mensageria.
- **MongoDB** para armazenamento de dados.
- **Kubernetes** para orquestração de containers.
- **Docker** para criação de imagens do front-end e do back-end.
