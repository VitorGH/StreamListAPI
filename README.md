# StreamList

Aplicação fullstack para rastrear filmes e séries com integração ao TMDB. Desenvolvida com ASP.NET Core Web API no backend e Angular no frontend.

---

## Tecnologias

**Backend**
- ASP.NET Core 8 Web API
- Entity Framework Core com SQL Server
- Autenticação JWT (JSON Web Token)
- BCrypt para hash de senhas
- Integração com a [TMDB API](https://www.themoviedb.org/)

**Frontend**
- Angular 18 (standalone components)
- TypeScript
- SCSS
- RxJS (debounce, switchMap)
- HTTP Interceptor para injeção automática de JWT

---

## Funcionalidades

- Cadastro e login de usuários com JWT
- Busca de filmes e séries em tempo real via TMDB
- Watchlist pessoal por usuário (adicionar, atualizar status, avaliar, remover)
- Status por título: quero ver / assistindo / concluído
- Avaliação de 1 a 5 estrelas e notas pessoais

---

## Estrutura do projeto

```
/StreamList
├── /StreamListAPI                  ← ASP.NET Core Web API
│   ├── Controllers/
│   │   ├── AuthController.cs       ← register, login, geração de JWT
│   │   ├── MediaController.cs      ← busca no TMDB
│   │   └── ListController.cs       ← CRUD da watchlist
│   ├── DTOs/
│   │   ├── Auth/
│   │   │   ├── RegisterDto.cs
│   │   │   └── LoginDto.cs
│   │   └── Watchlist/
│   │       ├── WatchlistItemDto.cs
│   │       └── UpdateItemDto.cs
│   ├── Models/
│   │   ├── User.cs
│   │   └── WatchlistItem.cs
│   ├── Data/
│   │   └── AppDbContext.cs
│   └── Program.cs
│
└── /streamlist-frontend            ← Angular SPA
    └── src/app/
        ├── pages/
        │   ├── login/
        │   ├── register/
        │   ├── search/             ← busca TMDB com debounce
        │   └── my-list/
        ├── services/
        │   ├── auth.service.ts
        │   ├── media.service.ts
        │   └── watchlist.service.ts
        └── interceptors/
            └── auth.interceptor.ts ← injeta Bearer token em toda requisição
```

---

## Como rodar localmente

### Pré-requisitos

- [.NET 8 SDK](https://dotnet.microsoft.com/download)
- [Node.js 20+](https://nodejs.org/)
- [Angular CLI](https://angular.io/cli): `npm install -g @angular/cli`
- SQL Server (local ou Express)
- Conta no [TMDB](https://www.themoviedb.org/settings/api) para obter a API key gratuita

---

### 1. Configurar o backend

Clone o repositório e abra a solução no Visual Studio.

No arquivo `appsettings.json`, preencha as configurações:

```json
{
  "ConnectionStrings": {
    "Default": "Server=localhost;Database=StreamListDB;Trusted_Connection=True;TrustServerCertificate=True"
  },
  "Jwt": {
    "Key": "sua-chave-secreta-minimo-32-caracteres",
    "Issuer": "StreamListAPI",
    "Audience": "StreamListApp"
  },
  "Tmdb": {
    "ApiKey": "sua-api-key-do-tmdb"
  }
}
```

Rode as migrations para criar o banco:

```bash
# No Package Manager Console do Visual Studio
Add-Migration InitialCreate
Update-Database
```

Inicie a API (`F5` ou `Ctrl+F5`). Ela vai rodar em `http://localhost:5000`.

---

### 2. Configurar o frontend

```bash
cd streamlist-frontend
npm install
ng serve
```

O frontend vai rodar em `http://localhost:4200`.

---

### 3. Acessar

Abra `http://localhost:4200` no navegador. Com os dois projetos rodando em paralelo, a busca de filmes e séries já estará funcional.

---

## Arquitetura

```
Angular (4200)  ──────────────►  ASP.NET Web API (5000)  ──►  SQL Server
                  JSON + JWT               │
                                           └──────────────────►  TMDB API
```

O Angular se comunica exclusivamente com a Web API via HTTP. A API autentica as requisições via JWT e acessa o banco de dados (watchlist do usuário) e a TMDB (busca de títulos) de forma independente.

---

## Endpoints da API

| Método | Rota | Auth | Descrição |
|--------|------|------|-----------|
| POST | `/api/auth/register` | Não | Cadastro de usuário |
| POST | `/api/auth/login` | Não | Login, retorna JWT |
| GET | `/api/media/search?query=` | Sim | Busca no TMDB |
| GET | `/api/list` | Sim | Retorna watchlist do usuário |
| POST | `/api/list` | Sim | Adiciona título à lista |
| PATCH | `/api/list/{id}` | Sim | Atualiza status, nota ou avaliação |
| DELETE | `/api/list/{id}` | Sim | Remove da lista |

---

## Próximos passos

- [ ] Página de login e cadastro no Angular
- [ ] Guard de rotas (redirecionar para login se não autenticado)
- [ ] Página "Minha lista" com filtro por status
- [ ] Deploy: API no Railway · Frontend no Vercel

---

## Autor

Desenvolvido como projeto de portfólio para demonstrar integração entre ASP.NET Core Web API e Angular com autenticação JWT, CRUD completo e consumo de API externa.