# Shipping Company Management System

A comprehensive shipping management system built with .NET Core and Angular, providing a robust solution for managing shipping operations, tracking shipments, and handling customer interactions.
>>>> Graduation Project Demo :-  https://youtu.be/EWDi_sxj6Fk
## 🚀 Technology Stack

### Backend (.NET Core)
- ASP.NET Core Web API
- Entity Framework Core
- SQL Server
- Clean Architecture
- CQRS Pattern
- MediatR
- AutoMapper
- Fluent Validation
- JWT Authentication

### Frontend (Angular)
- Angular 17+
- TypeScript
- Angular Material
- RxJS
- NgRx (State Management)
- Angular Router
- Angular Forms

## 📋 Project Structure

### Backend Structure
```
API/
├── ITI.Shipping.APIs/                 # API Layer
├── ITI.Shipping.Core.Domin/           # Domain Layer
├── ITI.Shipping.Core.Application/     # Application Layer
├── ITI.Shipping.Core.Application.Abstraction/  # Application Abstractions
├── ITI.Shipping.Infrastructure/       # Infrastructure Layer
├── ITI.Shipping.Infrastructure.Presistence/    # Persistence Layer
└── UnitTest/                          # Unit Tests
```

### Frontend Structure
```
UI/
├── src/
│   ├── app/           # Application Components
│   ├── assets/        # Static Assets
│   ├── environments/  # Environment Configurations
│   └── shared/        # Shared Modules
└── public/            # Public Assets
```

## 🛠️ Prerequisites

- .NET 8.0 SDK
- Node.js (v18 or later)
- Angular CLI
- SQL Server
- Visual Studio 2022 or VS Code
- Git

## 🚀 Getting Started

### Backend Setup

1. Clone the repository:
```bash
git clone [repository-url]
cd Shipping-Company-API
```

2. Navigate to the API directory:
```bash
cd API
```

3. Restore dependencies:
```bash
dotnet restore
```

4. Update the connection string in `appsettings.json`

5. Run database migrations:
```bash
dotnet ef database update
```

6. Start the API:
```bash
dotnet run
```

### Frontend Setup

1. Navigate to the UI directory:
```bash
cd UI
```

2. Install dependencies:
```bash
npm install
```

3. Start the development server:
```bash
ng serve
```

4. Open your browser and navigate to `http://localhost:4200`

## 🔑 Features

- User Authentication and Authorization
- Shipment Management
- Order Tracking
- Customer Management
- Driver Management
- Real-time Shipment Status Updates
- Reports and Analytics
- Admin Dashboard

## 🧪 Testing

### Backend Tests
```bash
cd API
dotnet test
```

### Frontend Tests
```bash
cd UI
ng test
```

## 📚 API Documentation

The API documentation is available through Swagger UI when running the backend:
```
http://localhost:5000/swagger
```

## 🔒 Security

- JWT-based authentication
- Role-based authorization
- HTTPS enabled
- Input validation
- XSS protection
- CORS configuration

## 🤝 Contributing

1. Fork the repository
2. Create your feature branch (`git checkout -b feature/AmazingFeature`)
3. Commit your changes (`git commit -m 'Add some AmazingFeature'`)
4. Push to the branch (`git push origin feature/AmazingFeature`)
5. Open a Pull Request

## 📝 License

This project is licensed under the MIT License - see the LICENSE file for details.

## 👥 Authors

- Mahmoud Nabil

## 🙏 Acknowledgments

- ITI Team
- All contributors who have helped shape this project

## 📞 Support

For support, email [e.mahomoudnabil@gmail.com] or create an issue in the repository. 
