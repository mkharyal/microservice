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