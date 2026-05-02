# 🐉 DragonBall MVC

Aplicació web desenvolupada amb **ASP.NET Core 8.0 MVC** que permet explorar personatges de l'univers Dragon Ball mitjançant una API pública, i guardar-ne els favorits en una base de dades.

🌐 **Demo en viu:** [roy2026-001-site1.ltempurl.com](http://roy2026-001-site1.ltempurl.com)

---

## 📋 Justificació i motivació

L'estètica i la narrativa de l'univers Dragon Ball han sigut una font d'inspiració constant. Prèviament ja havia treballat amb l'API de Dragon Ball en altres entregues d'altres matèries, fet que em va permetre aprofundir en la seva estructura i treure-li més profit en aquest projecte. L'objectiu era construir una aplicació web completa i funcional que combinés el consum d'una API externa amb persistència de dades pròpia.

---

## 🏗️ Esquema d'arquitectura

El projecte segueix el patró **Model-Vista-Controlador (MVC)**, organitzat en tres mòduls principals:

| Mòdul | Model | Vista | Controlador |
|---|---|---|---|
| Pàgina principal | — | `Home/Index` | `HomeController` |
| Personatges | `Character` | `Characters/Index`, `Characters/Detail` | `CharactersController` |
| Favorits | `Favorite` | `Favorites/Index` | `FavoritesController` |

### Diagrama de flux

```
Usuari
  │
  ├──► HomeController ──► Vista Home
  │
  ├──► CharactersController
  │         │
  │         ├── DragonBallService ──► API externa (dragonball-api.com)
  │         │         └── Retorna List<Character> / Character
  │         └── Vistes: Index (llistat paginat) / Detail (detall)
  │
  └──► FavoritesController
            │
            ├── AppDbContext (Entity Framework Core)
            │         └── SQL Server (myASP.NET)
            └── Vistes: Index (llistat de favorits)
```

---

## 💻 Explicació detallada del codi

### Models (`/Models`)

#### `Character.cs`
Representa un personatge de Dragon Ball obtingut des de l'API externa. Conté les propietats: `Id`, `Name`, `Race`, `Gender`, `Ki`, `MaxKi`, `Affiliation`, `Description` i `Image`.

#### `Favorite.cs`
Model que es persisteix a la base de dades. Emmagatzema les dades essencials d'un personatge afegit a favorits: `CharacterId`, `CharacterName`, `CharacterImage`, `CharacterRace` i `AddedAt` (data d'afegit, automàtica).

#### `DragonBallApiResponse.cs`
Model de deserialització de la resposta paginada de l'API. Conté:
- `Items`: llista de personatges (`List<Character>`)
- `Meta`: metadades de paginació (`TotalItems`, `TotalPages`, `CurrentPage`, etc.)

---

### Servei (`/Services`)

#### `DragonBallService.cs`
S'encarrega de tota la comunicació amb l'API externa `https://dragonball-api.com/api`. Injectat via `HttpClient` (registrat a `Program.cs`).

- **`GetCharactersAsync(page, limit)`** — Obté un llistat paginat de personatges. Per defecte, 12 personatges per pàgina.
- **`GetCharacterByIdAsync(id)`** — Obté el detall complet d'un personatge per ID.

Fa servir `Newtonsoft.Json` per deserialitzar les respostes JSON de l'API.

---

### Controladors (`/Controllers`)

#### `HomeController.cs`
Controlador senzill que gestiona la pàgina d'inici (`/`) i la pàgina de privacitat.

#### `CharactersController.cs`
Gestiona la navegació pels personatges de Dragon Ball.

- **`Index(page)`** — Mostra el llistat paginat de personatges. Passa el número de pàgina actual i el total de pàgines a la vista via `ViewBag`.
- **`Detail(id)`** — Mostra el detall d'un personatge concret obtingut per ID.

Dep del `DragonBallService` injectat al constructor.

#### `FavoritesController.cs`
Gestiona els personatges marcats com a favorits, amb persistència a la base de dades.

- **`Index()`** — Mostra la llista de favorits ordenats per data d'afegit (més recent primer).
- **`Add(characterId, characterName, characterImage, characterRace)`** — Afegeix un personatge als favorits si encara no hi és (comprova duplicats). Redirigeix al detall del personatge.
- **`Remove(id)`** — Elimina un favorit per ID i redirigeix al llistat.

Dep del `AppDbContext` injectat al constructor.

---

### Base de dades (`/Data`)

#### `AppDbContext.cs`
Context d'Entity Framework Core que exposa el `DbSet<Favorite>`. La base de dades es crea automàticament a l'arrencada gràcies a `db.Database.EnsureCreated()` definit a `Program.cs`.

---

### Configuració (`Program.cs`)

```csharp
builder.Services.AddControllersWithViews();
builder.Services.AddHttpClient<DragonBallService>();
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));
```

La ruta per defecte apunta a `Characters/Index`:
```csharp
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Characters}/{action=Index}/{id?}");
```

---

## 🚀 Tecnologies utilitzades

- **ASP.NET Core 8.0 MVC**
- **Entity Framework Core 8.0** amb SQL Server - MSSQL
- **Newtonsoft.Json** — Deserialització JSON
- **dragonball-api.com** — API pública de Dragon Ball
- **myASP.NET** — Hosting i desplegament
- **FileZilla** — Transferència FTP. Enviar configuració appsettings.json, que per seguretat no l'he posat directament al repo del projecte.
- **GitHub** — Control de versions i deploy automàtic

---

## ⚙️ Instal·lació en local

```bash
# Clona el repositori
git clone https://github.com/R0hitjzw/DragonBallMVC-RohitJaswal.git
cd DragonBallMVC-RohitJaswal

# Restaura les dependències
dotnet restore

# Crea un appsettings.json amb la teva configuració (no inclòs al repo per seguretat)
# Exemple:
{
  "ConnectionStrings": {
    "DefaultConnection": "Data Source=.;Initial Catalog=dragonball;Integrated Security=True;"
  }
}

# Executa l'aplicació
dotnet run
```

---

## 💡 Propostes de millora i noves funcionalitats

- **Autenticació d'usuaris** — Cada usuari tindria la seva pròpia llista de favorits personalitzada.
- **Cercador de personatges** — Filtrar per nom, raça o afiliació directament des de la vista, ara només es pot per nom.
- **Pàgina de detall de favorits** — Afegir notes o valoracions personals a cada favorit.
- **Ordenació i filtres** — Ordenar els personatges per Ki, raça o afiliació.
- **Comparador de personatges** — Seleccionar dos personatges i comparar les seves estadístiques, benchmarking.
- **Mode fosc** — Adaptació del disseny amb tema dark/light, coherent amb l'estètica de Dragon Ball.
- **Multi-idioma** — Suport multilingüe (català, castellà, anglès).