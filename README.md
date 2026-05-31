# TrainMaster API

API RESTful para gerenciamento de treinos, exercícios e sessões de academia, construída com **.NET 10** seguindo os princípios da **Clean Architecture**.

---

## Tecnologias

- **Framework:** ASP.NET Core 10
- **ORM:** Entity Framework Core 10 + PostgreSQL
- **Autenticação:** JWT Bearer + Refresh Token (HttpOnly Cookie)
- **Validação:** FluentValidation
- **Documentação:** OpenAPI (Scalar/Swagger)

---

## Arquitetura

O projeto segue Clean Architecture com separação em 4 camadas, onde as dependências sempre apontam para dentro:

```
TrainMaster.API           → Controllers, Middleware, Program.cs
TrainMaster.Application   → Services, DTOs, Interfaces, Validators, Mappings
TrainMaster.Domain        → Entities, Enums, Interfaces de repositório
TrainMaster.Infrastructure → EF Core, Repositories, JwtService, UnitOfWork
```

### Fluxo de uma requisição

```
Request → Controller → Service (Application) → Repository (Infrastructure) → Database
                     ↑                        ↑
                 Interfaces              IUnitOfWork
```

---

## Funcionalidades

- Autenticação completa com JWT + Refresh Token rotativo
- CRUD de músculos, subgrupos musculares e exercícios
- Criação e gerenciamento de treinos personalizados
- Registro de sessões de treino por usuário
- Controle de acesso por roles (`User` / `Admin`)
- Middleware global de tratamento de exceções

---

## Endpoints

### Auth
| Método | Rota | Acesso | Descrição |
|--------|------|--------|-----------|
| `POST` | `/api/auth/register` | Público | Cadastra novo usuário |
| `POST` | `/api/auth/login` | Público | Autentica e retorna tokens |
| `POST` | `/api/auth/refresh` | Público | Renova o par de tokens |
| `GET` | `/api/auth/me` | Autenticado | Retorna dados do usuário logado |
| `POST` | `/api/auth/logout` | Autenticado | Revoga o refresh token |

### Músculos
| Método | Rota | Acesso | Descrição |
|--------|------|--------|-----------|
| `GET` | `/api/muscle` | Autenticado | Lista todos os músculos |
| `GET` | `/api/muscle/{id}` | Autenticado | Busca músculo por ID |
| `POST` | `/api/muscle` | Admin | Cria músculo |
| `PUT` | `/api/muscle/{id}` | Admin | Atualiza músculo |

### Subgrupos
| Método | Rota | Acesso | Descrição |
|--------|------|--------|-----------|
| `GET` | `/api/subgroup` | Autenticado | Lista subgrupos musculares |
| `GET` | `/api/subgroup/{id}` | Autenticado | Busca subgrupo com músculo |
| `POST` | `/api/subgroup` | Admin | Cria subgrupo |
| `PUT` | `/api/subgroup/{id}` | Admin | Atualiza subgrupo |

### Exercícios
| Método | Rota | Acesso | Descrição |
|--------|------|--------|-----------|
| `GET` | `/api/workout` | Autenticado | Lista exercícios |
| `GET` | `/api/workout/{id}` | Autenticado | Busca exercício por ID |
| `POST` | `/api/workout` | Admin | Cria exercício |
| `PUT` | `/api/workout/{id}` | Admin | Atualiza exercício |

### Treinos
| Método | Rota | Acesso | Descrição |
|--------|------|--------|-----------|
| `GET` | `/api/training` | Autenticado | Lista treinos |
| `GET` | `/api/training/{id}` | Autenticado | Busca treino por ID |
| `POST` | `/api/training` | Autenticado | Cria treino (userId via JWT) |
| `PUT` | `/api/training/{id}` | Autenticado | Atualiza treino |

### Treino ↔ Exercício
| Método | Rota | Acesso | Descrição |
|--------|------|--------|-----------|
| `GET` | `/api/trainingworkout` | Autenticado | Lista vínculos |
| `GET` | `/api/trainingworkout/{id}` | Autenticado | Busca vínculo por ID |
| `POST` | `/api/trainingworkout` | Autenticado | Vincula exercício ao treino |
| `PUT` | `/api/trainingworkout/{id}` | Autenticado | Atualiza vínculo |

### Sessões de Treino
| Método | Rota | Acesso | Descrição |
|--------|------|--------|-----------|
| `GET` | `/api/trainingsession` | Autenticado | Lista sessões |
| `GET` | `/api/trainingsession/{id}` | Autenticado | Busca sessão por ID |
| `POST` | `/api/trainingsession` | Autenticado | Registra sessão (userId via JWT) |
| `PUT` | `/api/trainingsession/{id}` | Autenticado | Atualiza sessão |

---

## Autenticação

O sistema usa **JWT Bearer** com **Refresh Token rotativo**.

- O `accessToken` é retornado no body da resposta (vida curta: 15 min)
- O `refreshToken` é armazenado em **HttpOnly Cookie** (vida longa: 7 dias)
- O refresh token é armazenado no banco como **hash SHA-256** — nunca o valor puro
- A cada `/refresh`, um novo par de tokens é gerado e o anterior é invalidado

### Exemplo de login

```http
POST /api/auth/login
Content-Type: application/json

{
  "email": "usuario@email.com",
  "password": "Senha@123"
}
```

**Resposta:**
```json
{
  "userId": "...",
  "name": "João Silva",
  "email": "joao@email.com",
  "role": "User",
  "accessToken": "eyJ...",
  "accessTokenExpiresAt": "2026-05-27T10:15:00Z"
}
```

Use o `accessToken` no header das requisições autenticadas:

```http
Authorization: Bearer eyJ...
```

---

## Modelos de Dados

### Usuário
| Campo | Tipo | Descrição |
|-------|------|-----------|
| `name` | string | Nome completo |
| `email` | string | E-mail único |
| `password` | string | Mínimo 8 chars, 1 maiúscula, 1 número |
| `birthday` | DateTime | Data de nascimento |
| `goal` | enum | `LoseWeight`, `GainMuscle`, `MaintainWeight` |

### Exercício (Workout)
| Campo | Tipo | Descrição |
|-------|------|-----------|
| `name` | string | Nome do exercício |
| `description` | string | Descrição |
| `muscleId` | Guid | Músculo principal |
| `subgroupId` | Guid | Subgrupo muscular |
| `type` | enum | `bodybuilding`, `aerobic` |
| `level` | enum | `beginner`, `intermediate`, `advanced` |
| `url_video` | string? | URL do vídeo demonstrativo |
| `url_image` | string? | URL da imagem |

---

## Como Rodar Localmente

### Pré-requisitos

- [.NET 10 SDK](https://dotnet.microsoft.com/download)
- [PostgreSQL](https://www.postgresql.org/)

### Configuração

1. Clone o repositório:
```bash
git clone https://github.com/seu-usuario/trainmaster.git
cd trainmaster
```

2. Configure a string de conexão e as variáveis JWT em `appsettings.Development.json`:
```json
{
  "ConnectionStrings": {
    "DefaultConnection": "Host=localhost;Port=5432;Database=trainmaster;Username=postgres;Password=suasenha"
  },
  "Jwt": {
    "Secret": "sua-chave-secreta-com-minimo-32-caracteres",
    "Issuer": "TrainMaster",
    "Audience": "TrainMaster",
    "AccessTokenExpirationMinutes": "15",
    "RefreshTokenExpirationDays": "7"
  }
}
```

3. Aplique as migrations:
```bash
dotnet ef database update --project TrainMaster.Infrastructure --startup-project TrainMaster.API
```

4. Execute a aplicação:
```bash
dotnet run --project TrainMaster.API
```

A API estará disponível em `http://localhost:5212`.

A documentação OpenAPI estará em `http://localhost:5212/openapi/v1.json` (ambiente de desenvolvimento).

---

## Estrutura de Pastas

```
TrainMaster/
├── TrainMaster.API/
│   ├── Controllers/
│   ├── Middleware/
│   └── Program.cs
├── TrainMaster.Application/
│   ├── DTOs/
│   ├── Interfaces/
│   ├── Mappings/
│   ├── Services/
│   └── Validators/
├── TrainMaster.Domain/
│   ├── Common/
│   ├── Entities/
│   ├── Enums/
│   └── Interfaces/
└── TrainMaster.Infrastructure/
    ├── Data/              ← DbContext + Configurations
    ├── Migrations/
    ├── Repositories/
    └── Services/          ← JwtService
```

---

## Variáveis de Ambiente

| Variável | Descrição |
|----------|-----------|
| `ConnectionStrings__DefaultConnection` | String de conexão com o PostgreSQL |
| `Jwt__Secret` | Chave secreta para assinar os tokens (mínimo 32 chars) |
| `Jwt__Issuer` | Emissor do token |
| `Jwt__Audience` | Audiência do token |
| `Jwt__AccessTokenExpirationMinutes` | Expiração do access token em minutos |
| `Jwt__RefreshTokenExpirationDays` | Expiração do refresh token em dias |

---

## Licença

Este projeto está sob a licença MIT.
