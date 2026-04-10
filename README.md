# 🚀 Microservices Project (.NET + Kubernetes)

This repository showcases a hands-on implementation of a microservices architecture using modern cloud-native technologies like .NET, Docker, and Kubernetes.

It is not just a project, but a learning journey into building scalable, loosely coupled, and production-ready systems.

## 🙏 Acknowledgment

This project is inspired by the amazing tutorial from
Les Jackson

## 🎥 Tutorial: https://youtu.be/DgVjEo3OGBI

Your teaching style—simple, honest, and filled with humor—made complex concepts feel approachable.
The way you embrace mistakes and turn them into learning moments truly sets you apart.

## 🧩 Project Structure
├── CommandsService/     # Handles command-related operations
├── PlatformService/     # Manages platform data
├── K8S/                 # Kubernetes manifests for deployment

## 🛠️ Tech Stack & Architecture

**Technology Stack:**
- **Runtime**: .NET 10.0
- **Service Communication**: 
  - REST APIs (HTTP/JSON)
  - gRPC for high-performance inter-service calls
- **Message Broker**: RabbitMQ for asynchronous event-driven communication
- **Database**: Microsoft SQL Server with Entity Framework Core ORM
- **Containerization**: Docker for service packaging
- **Orchestration**: Kubernetes for deployment and service management
- **API Mapping**: AutoMapper for DTO transformations
- **IPC Protocols**: Protocol Buffers (Protobuf) for gRPC message definitions

**Non-Functional Requirements & Design Goals:**
- **Scalability**: Horizontally scalable microservices deployable as Kubernetes pods
- **Loose Coupling**: Services communicate asynchronously via message queues to minimize dependencies
- **High Performance**: gRPC services for low-latency, high-throughput inter-service communication
- **Resilience**: Containerized services with restart policies and health checks
- **Cloud-Native**: Full Kubernetes support with declarative infrastructure manifests
- **Asynchronous Processing**: Event-driven architecture using publish-subscribe patterns
- **Data Isolation**: Each service maintains its own database following microservices principles

**Architectural Limitations (By Design for Learning):**
- No authentication/authorization mechanisms
- No centralized logging or observability stack
- Minimal error handling and validation
- No automated CI/CD pipeline
- No service mesh, API gateway, or rate limiting
- Basic health checks without advanced monitoring

## 🧠 What This Project Demonstrates

This project is designed to help understand real-world microservices concepts:

⚙️ Building RESTful APIs with .NET
🔗 Service-to-service communication using:
HTTP (synchronous)
gRPC (high-performance communication)
📩 Asynchronous messaging patterns
🐳 Containerization using Docker
☸️ Deployment and orchestration using Kubernetes
🧱 Designing loosely coupled and scalable services

## 🚀 Getting Started

Follow these steps to run the project locally:

Clone the repository

git clone [<your-repo-url>](https://github.com/mkharyal/microservice.git)
Open the solution in:
Visual Studio
or VS Code

Build the services

dotnet build

Run locally (optional)

dotnet run

Deploy to Kubernetes

kubectl apply -f K8S/

## 💡 Key Learnings

While building this project, you will gain practical exposure to:

Breaking monoliths into microservices
Managing inter-service communication
Handling real-world development challenges
Thinking in terms of scalability and maintainability

## ⚠️ Disclaimer

This project is created for learning and demonstration purposes.
It may not include production-grade features like:

Authentication & Authorization
Centralized logging
Advanced error handling
Full CI/CD pipelines

## 🌱 Final Thoughts

This project is more than just code—it reflects:

Curiosity to learn
Courage to try
Willingness to make mistakes and grow

If you are starting your journey into microservices, this is a great place to begin.

## ❤️ Thank You

A heartfelt thanks again to Les Jackson

Your authenticity, humor, and teaching philosophy made this learning experience not just educational—but enjoyable.
2. Build the projects.
3. Deploy the Kubernetes manifests in `K8S/` if using a local cluster.

## Notes

This project is based on a tutorial by Les Jackson.

## Thank You

Special thanks to Les Jackson for teaching with warmth, humor, and honest self-reflection. Your clear guidance, relatable mistakes, and encouragement made this project easier to follow and more enjoyable to build.