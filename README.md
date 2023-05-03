# All in One REST API com ASP.NET
[![jwt_log](https://jwt.io/img/badge-compatible.svg)](https://jwt.io)

O [ASP.NET](https://dotnet.microsoft.com/en-us/apps/aspnet) hoje é um dos principais frameworks para criar REST API, conta com uma vasta variedade de recursos, incluindo suporte para autenticação, autorização, documentação de API com Swagger, roteamento, versionamento, além de uma grande comunidade de desenvolvedores para ajudar a solucionar problemas. Para aqueles que querem começar com o framework ou aqueles que querem rever algum conceito, este artigo vai servir todos público, este é um All in One (Tudo em Um), vou cobrir vários assuntos básicos e alguns poucos avançados de forma mais completa possível, mostrando como e porque fazer, além de explicar todos os códigos que serão mostrados aqui.

Para quem nunca usou o .NET talvez se sinta perdido, este é um artigo para quem quer começar com ASP.NET, portanto você já deve saber os conceitos básicos da plataforma-base que carrega o framework além da linguagem C#.

Para não ficar perdido ou se quiser ver apenas um conteúdo específico aqui está a tabela de conteúdo:

- [O que é o ASP.NET](https://github.com/LuanRoger/AllInOneAspNet#o-que-%C3%A9-aspnet)
- [REST.API](https://github.com/LuanRoger/AllInOneAspNet#rest-api)
- [Criar um projeto](https://github.com/LuanRoger/AllInOneAspNet#criar-um-projeto)
- [Criar as primeiras rotas](https://github.com/LuanRoger/AllInOneAspNet#criar-as-primeiras-rotas)
- [Modelos](https://github.com/LuanRoger/AllInOneAspNet#modelos)
- [DI (Dependency Injection)](https://github.com/LuanRoger/AllInOneAspNet#di-dependency-injection)
- [Entity Framework](https://github.com/LuanRoger/AllInOneAspNet#entity-framework)
- [Usar o Entity Framework com SQLite](https://github.com/LuanRoger/AllInOneAspNet#usar-o-entity-framework-com-sqlite)
- [Repository Pattern](https://github.com/LuanRoger/AllInOneAspNet#repository-pattern)
- [Logging com Serilog](https://github.com/LuanRoger/AllInOneAspNet#logging-com-serilog)
- [Criar validadores](https://github.com/LuanRoger/AllInOneAspNet#criar-validadores)
- [Autenticação e Autorização com JWT usando chave simétrica](https://github.com/LuanRoger/AllInOneAspNet#autentica%C3%A7%C3%A3o-e-autoriza%C3%A7%C3%A3o-com-jwt-usando-chave-sim%C3%A9trica)
- [Criar controladores](https://github.com/LuanRoger/AllInOneAspNet#criar-controladores)
- [Implementar os endpoints](https://github.com/LuanRoger/AllInOneAspNet#implementar-endpoints)
- [Iniciar o Swagger](https://github.com/LuanRoger/AllInOneAspNet#iniciar-e-configurar-o-swagger)

# O que é ASP.NET

O ASP.NET é um framework para construir aplicação web e serviços na plataforma .NET usando C#, Ele totalmente gratuito e open-source e ao contrário do que muitas pessoas pensam ele é multiplataforma, funcionando no Windows, Linux e MacOS.

Ele não é usado somente para criar APIs REST, o ASP.NET é uma plataforma completa para criar suas aplicações, fornece recursos para construir desde a UI até APIs, ou seja, criar aplicações full-stack inteiras sozinhas, além de ser fácil de separa partes de sua aplicação para criar microsserviço, ele dá um dos melhores suporte para Docker que existe hoje, pois ele consegue construir sua aplicação direto em um container sem um Dockerfile.

O ASP.NET faz parte da plataforma .NET o que significa que todo ecossistema também pode ser usado para construir sua aplicação, como o Entity Framework, um ORM (Object Relational Mapper) que te ajuda a manipular banco de dados de vários provedores (neste artigo será abordado Entity Framework), SignalR para comunicação em tempo real, esta biblioteca usa o protocolo RPC (Remote Procedure Call) para atingir tal objetivo, mas esta não é a única forma de criar aplicações que se comunicam em tempo real, o ASP.NET também conta com um bom suporte a WebSockets, e para acompanhar o funcionamento de tudo isso temos o Serilog para criar *logs* e diagnósticos, exportando-os para diferentes fontes com sua funcionalidade de ***[sinks](https://github.com/serilog/serilog/wiki/Provided-Sinks)*** que pode ser usado para escrever os logs aonde você quiser. Algumas dessas bibliotecas veremos aqui, pois são fundamentais para qualquer aplicação, mas que fique claro que não são as únicas que existem e tem várias outras que servem o mesmo propósito e outras que facilitam ainda mais seus dia a dia.

# REST API

Primeiro, uma API (Application Programming Interface) pode ser dito como um conjunto de definições que usa protocolos para se comunicar entre diferentes partes que uma aplicação, como no seu nome especifica, API é uma interface, ou seja, define um contrato para comunicação, onde quem vai consumir da API também estará ciente de tais contratos para que não possa haver mal-entendido entre as partes. Por exemplo, uma API Web define interfaces para comunicação no contexto da web, onde se usa o protocolo HTTP para comunicação e as respostas são enviadas em formato JSON ou XML, sendo JSON o mais comum.

O REST (Representational State Transfer) define algumas restrições para as API Web para que os serviços sejam mais flexíveis, escaláveis e fáceis de manter, onde cada recurso é identificado por uma URI (Uniform Resource Identifier) e as operações são realizadas através dos métodos HTTP: GET, POST, PUT, PATCH e DELETE. As API que usam o REST também podem ser denominadas como RESTful.

Se quiser saber mais sobre o propósito de cada método acesse o [MDN Docs](https://developer.mozilla.org/docs/Web/HTTP/Methods).

Uma API RESTful deve respeitar as seguintes restrições:

- Cliente-servidor: separação entre a interface do usuário e a lógica do servidor.
- Sem estado: cada requisição deve conter todas as informações necessárias para a realizar a operação desejada, ou seja, nenhuma informação deve ser armazenada entre solicitações.
- Cacheável: as respostas devem ser cacheáveis.
- Interface uniforme: a API deve seguir um conjunto predefinido de recursos e verbos.
- Sistema em camadas: a API pode ser escalada através da adição de camadas intermediárias.

Se quiser saber mais sobre APIs REST acesse o [artigo da RedHat](https://www.redhat.com/topics/api/what-is-a-rest-api).

Com o ASP.NET, é possível criar uma API seguindo essas restrições, utilizando os recursos já disponíveis no framework.

Não é o foco deste artigo mostra como que faz uma requisição HTTP, pois isso depende do seu sistema operacional já que alguns veem com diferentes ferramentas para este propósito, neste artigo estarei utilizando o [Postman](https://www.postman.com) para que não precise criar um cliente em uma outra linguagem para consumir da API, mas fica a seu critério usar a ferramenta do seu sistema, criar um programa ou usar qualquer outro cliente para consumir da API que vamos criar.

# Criar um projeto

A partir daqui será abordado o conteúdo sobre a criação da API em si, mas antes de criar os projetos vamos definir o que será feito.

### O que será feito?

Será criado uma Minimal API completa para cadastro de usuários e cliente usando os conceitos listados na tabela de conteúdo. Os usuários são os que usarão o sistema, eles podem: 

- Cadastrar clientes;
- Ver as informações dos clientes cadastrados pelo usuário que está requisitando;
- Remover cliente;
- Atualizar informações dos clientes;

Tudo só será possível se caso o usuário esteja autenticado. Para exclarecer, mesmo que o cliente não tenha sido cadastrado pelo usuário que está requisitando a mudança ele ainda poderá ser removido e atualizado, mas não deverar ver as informações. Um sistema bem simples que será suficiente para o propósito do artigo.

Minimal API é um conceito introduzido no .NET 6 onde define apenas o essencial para que a API funcione e possa receber requisições, eliminando o código gigantesco que precisava antigamente para que a API funcionasse. As Minimal APIs se assemelhar com a forma de como o [Express](https://expressjs.com/) no NodeJS cria APIs, esta forma deixa o código muito mais limpo e simples, mas mantendo as funcionalidades que tornam o [ASP.NET](http://ASP.NET) tão completo.

### Esclarecimentos

Os conteúdos não serão tratados separadamente com explicações muito teoricas, todo os conceitos que vamos usar aqui serão explicados e aplicados, caso queira estudar a parte mais teórica dos assuntos veja as referências do artigo onde estará todas as fontes usadas, explicadas de forma mais aprofundada.

Estarei usando o .NET 7 para este projeto juntamente com o [Rider](https://www.jetbrains.com/rider/) como IDE, mas pode usar qualquer uma outra, já que irei usar o terminal tanto para cria a aplicação quanto para rodar a API.

Para criar o projeto é necessário que você já tenha o SDK do .NET instalado, não é necessário instalar nada relacionado ao [ASP.NET](http://ASP.NET) em si, pois ele já vem com o SDK.

Para criar um projeto, no seu terminal execute:

```powershell
dotnet new web -o AllInOneAspNet
```

- **dotnet** - Define o programa que será usado.
- **new** - Parâmetro para criar uma nova aplicação.
- **web** - O template que será usado, neste caso será o template para criar uma aplicação web vazia.
- **-o AllInOneAspNet** - output dos arquivos do projeto/onde o projeto será criado, neste casso será criado na pasta **AllInOneAspNet**.

A seguinte estrutura de pastas será criada:

```powershell
.
├── obj
├── Properties
├── AllInOneAspNet.csproj
├── appsettings.Development.json
├── appsettings.json
└── Program.cs
```

O arquivo mais importante é o **Program.cs** onde contém código principal da nossa aplicação. Conforme formos prosseguindo no artigo será falado mais sobre os outros arquivos e diretórios.

Baseado no template que escolhemos para criar o projeto, o Program.cs deve estar inicialmente dessa forma:

```csharp
var builder = WebApplication.CreateBuilder(args); // 1
var app = builder.Build(); // 2

app.MapGet("/", () => "Hello World!"); // 3

app.Run(); // 4
```
*./Program.cs*

1. Cria um construtor para os serviços da aplicação
2. Cria uma aplicação que será usada para configurar as rotas HTTP usando o `builder`.
3. Mapeia a rota `/` com o método GET que apenas retorna “Hello World”.
4. Executa a API.

Para executar a API devemos executar o comando no diretório raiz do projeto:

```powershell
dotnet watch
```

- **dotnet** - Define o programa que será usado.
- **watch** - Inicia a aplicação e observa por modificações.

Os logs exibidos devem ser parecidos com esse:

```powershell
dotnet watch 🔥 Hot reload enabled. For a list of supported edits, see https://aka.ms/dotnet/hot-reload.
  💡 Press "Ctrl + R" to restart.
dotnet watch 🔧 Building...
  Determinando os projetos a serem restaurados...
  Todos os projetos estão atualizados para restauração.
  AllInOneAspNet -> ...\AllInOneAspNet\bin\Debug\net7.0\AllInOneAspNet.dll
dotnet watch 🚀 Started
info: Microsoft.Hosting.Lifetime[14]
      Now listening on: http://localhost:5111
info: Microsoft.Hosting.Lifetime[0]
      Application started. Press Ctrl+C to shut down.
info: Microsoft.Hosting.Lifetime[0]
      Hosting environment: Development
info: Microsoft.Hosting.Lifetime[0]
      Content root path: ...\AllInOneAspNet
```

Provavelmente uma página foi aberta no seu navegador padrão na rota `/` da aplicação. Note que no meu caso, a aplicação está rodando em [http://localhost:5111](http://localhost:5111) mas a sua pode estar sendo executada em outra URI, portanto, verifique os logs.

Caso não queira que execute o navegador toda vez que executar a API, vá em `./Properties/launchSettings.json` que é onde estão as configurações de execução do projeto, incluindo os perfis de execução, e altere a propriedade `launchBrowser` no perfil que estiver executando.

```json
"profiles": {
    "http": {
      "commandName": "Project",
      "dotnetRunMessages": true,
      "launchBrowser": true,
      "applicationUrl": "http://localhost:5111",
      "environmentVariables": {
        "ASPNETCORE_ENVIRONMENT": "Development"
      }
    },
...
```
*./Properties/launchSettings.json*

Vamos fazer uma requisição `GET` para o endpoint `/`, isso deve retornar o “Hello World” como resposta:

```
Hello World
```

# Criar as primeiras rotas

Vamos começar a criar nossas primeiras rotas mantendo as restrições do REST. Primeiro vamos definir os endpoints baseado no proposito do sistema:

| Endpoint | Descrição | Corpo da requisição | Cabeçalho | Reposta esperada | Possíveis códigos de resposta |
| --- | --- | --- | --- | --- | --- |
| ```POST user/signin``` | Cadastrar novo usuário | ```UserSigninRequestModel``` objeto | Não | JWT | 201; 400; 409 |
| ```POST user/login``` | Autentica um usuário | ```UserLoginRequestModel``` objeto | Não | JWT | 200; 404; 400 |
| ```GET client/``` | Resgatar todos os clientes cadastrados por um usuário específico | Não | JWT de autenticação do usuário | Lista de cliente cadastrados pelo usuário. | 200; 404 |
| ```POST client/``` | Cadastrar um novo cliente | ```ClientRegisterRequestModel``` objeto | JWT de autenticação do usuário | Informações do cliente cadastrado | 201; 400; 404 |
| ```PUT client/{id:int}``` | Atualizar cliente com base no ID | ```ClientUpdateRequestModel``` objeto | JWT de autenticação do usuário | ID do cliente afetado | 200; 400; 404; |
| ```DELETE cliente/{id:int}``` | Deletar cliente com base no ID | Não | JWT de autenticação do usuário | ID do cleinte afetado | 200; 404 |

Lembre-se que todas as respostas de todos os endpoints também retornaram um código de status. A autenticação e autorização com JWT será implementada depois, devemos primeiro mapear os endpoints, criar os modelos, controladores, etc.

Mapear um endpoint pode ser feito da seguinte maneira:

```csharp
//...
app.MapPost("user/signin", (HttpContext context) =>
{
    return Results.Ok("Cadastrar usuário");
});
app.MapPost("user/login", (HttpContext context) =>
{
    return Results.Ok("Logar usuário");
});
app.MapGet("client", (HttpContext context) =>
{
    return Results.Ok("Buscar cliente");
});
app.MapPut("client/{id:int}", (HttpContext context, int id) =>
{
    return Results.Ok($"Atualizar cliente com ID {id}");
});
//...
app.Run();
```
*./Program.cs; Exemplo*

Os endpoints são sempre mapeados em `app` com os métodos `Map` e o método HTTP que será usado, onde neste exemplo usamos:

- `MapPost`
- `MapGet`
- `MapPut`

Não vamos usar todos os métodos HTTP que existem, pois simplesmente não precisamos, mas caso queira saber qual quais são possíveis mapear com [ASP.NET](http://ASP.NET) veja a referência para `EndpointRouteBuilderExtensions`. Note que na função anônima que estamos passando como parâmetro dos métodos tem um parâmetro `HttpContext` que será usado futuramente para recuperar as *Claims* do JWT, mas pode ser usada para recuperar qualquer outra informação sobre a requisição.

Note que ao invés de retornar a *string* crua no endpoint estamos usando `Results` para retornar algum resultado, esta classe é muito útil para retornar objetos com algum código HTTP específico.

Esta forma de mapear endpoints não é a única que temos e não é a que vamos usar, pois note que alguns endpoints possuem parte de sua rota em comum como `user/signin` e `user/login` que possuem `user/` em comum, portanto vamos mapear nossas rotas usando `MapGroup`, que agrupa nossos endpoints com rotas em comum, que é algo perfeito para nosso caso.

Para mapear usando `MapGroup` podemos fazer da seguinte forma:

```csharp
public static class UserEndpoints
{
    public static RouteGroupBuilder MapUserEndpoints(this RouteGroupBuilder group)
    {
        group.MapPost("signin", (HttpContext context) => 
						Results.Created("user/signin", null));
        group.MapPost("login", (HttpContext context) => 
            Results.Ok("Logar usuario"));

        return group;
    }
}
```
*./Endpoints/UserEndpoints.cs*

```csharp
public static class ClientEndpoints
{
    public static RouteGroupBuilder MapClientEndpoints(this RouteGroupBuilder group)
    {
				group.MapGet("/", (HttpContext context) => 
            Results.Ok("Resgatar clientes"));
        group.MapPost("/", (HttpContext context) => 
            Results.Created("client/", null));
        group.MapPut("{id:int}", (HttpContext context, int id) => 
            Results.Ok($"Atualizar cliente com ID {id}"));
        group.MapDelete("{id:int}", (HttpContext context, int id) => 
            Results.Ok($"Deletar cliente com ID {id}"));
        
        return group;
    }
}
```
*./Endpoints/UserEndpoints.cs*

Primeiro criamos duas classes estáticas que terá um método de extensão para `RouteGroupBuilder` onde nesses métodos é que vamos mapear os endpoints em si, mas perceba que em `UserEndpoints` e `ClientEndpoints` estão faltando `user/` e `client/` respectivamente nas suas rotas, isso é porque vamos especificá-los agora que criarmos os grupos e usarmos os métodos de extensão para mapear os endpoints que definimos nas classes:

```csharp
RouteGroupBuilder userGroup = app.MapGroup("user");
userGroup.MapUserEndpoints();

RouteGroupBuilder clientGroup = app.MapGroup("client");
clientGroup.MapClientEndpoints();
```
*./Program.cs*

A parte em comum que as rotas devem ser passadas por parâmetro para o método `MapGroup` e usando o método de extensão em `userGroup` e `clientGroup` para mapeá-los.

Para ter certeza está tudo funcionando, vamos fazer uma requisição para um endpoint de cada grupo:

```POST``` [http://localhost:5111/user/login](http://localhost:5111/user/login):

```
"Logar usuario"
```
*Status: 200 OK*


```DELETE``` [http://localhost:5111/client/1](http://localhost:5111/client/1):

```
"Deletar cliente com ID 1"
```
*Status: 200 OK*

Portanto nossa API está retornando os resultados que deveriam. Por hora não vamos mais mexer no nosso endpoints, vamos agora criar os controladores e modelos que usaremos na aplicação.

# Modelos

Os modelos são extremamente importantes para qualquer aplicação e o quando construímos uma API web devemos dar mais atenção para os modelos, já que as APIs recebem e mandam informações. Criar um modelo só para dois propósitos diferentes pode ser difícil de manter ou fazer modificações no futuro, além disso teremos também o Entity Framework, sendo um ORM, usa os modelos para mapear as tabelas e colunas no banco de dados, por agora não vamos nos preocupar em mapear os modelos para o Entity Framework, vamos desenvolvendo e evoluindo nossa aplicação conforme o necessário.

Para manter os modelos organizados entre requisições e respostas vamos separá-los em modelos diferentes:

Primeiro criaremos um modelo geral:

```csharp
public class UserModel
{
    public int id { get; set; }
    public string username { get; set; }
    public string email { get; set; }
    public string password { get; set; }
}
```
*./Models/UserModels/UserModel.cs*

```csharp
public class ClientModel
{
    public int id { get; set; }
    public string username { get; set; }
    public UserModel createdBy { get; set; }
}
```
*./Models/ClientModels/ClientModel.cs*

Estes modelos serão usados no futuro para serem mapeados com Entity Framework.

Vamos também criar modelos para leitura dos modelos, isso servirá para quando quisermos mandar informações do usuário ou cliente como resposta, pois não queremos mandar informações como `id` ou `password` na resposta da requisição:

```csharp
public class UserReadModel
{
    public string username { get; init; }
    public string email { get; init; }
    
    /// <summary>
    /// Converte um <c>UserModel</c> em um <c>UserReadModel</c> 
    /// </summary>
    /// <param name="userModel"><c>UserModel</c> que será convertido</param>
    /// <returns>Um novo <c>UserReadModel</c> referente ao <c>UserModel</c></returns>
    public static UserReadModel FromUserModel(UserModel userModel) => new()
    {
        username = userModel.username,
        email = userModel.email
    };
}
```
*./Models/UserModels/UserModel.cs*


```csharp
public class ClientReadModel
{
    public int id { get; init; }
    public string username { get; init; }
    public UserReadModel createdBy { get; init; }
    
    /// <summary>
    /// Converte um <c>ClientModel</c> em um <c>ClientReadModel</c> 
    /// </summary>
    /// <param name="clientModel"><c>ClientModel</c> que será convertido</param>
    /// <returns>Um novo <c>ClientReadModel</c> referente ao <c>ClientModel</c></returns>
    public static ClientReadModel FromClientModel(ClientModel clientModel) => new()
    {
        id = clientModel.id,
        username = clientModel.username,
        createdBy = UserReadModel.FromUserModel(clientModel.createdBy)
    };
}
```
*./Models/ClientModels/ClientReadModel.cs*

Agora vamos criar os modelos de ação que serão usados para serem enviados e recebidos nas requisições, eles vão conter apenas propriedades que queremos mandar para o consumidor que está requisitando, lembre-se que em requisições `POST` vamos passar essas informações no corpo da requisição.

```csharp
public class UserSigninRequestModel
{
    public string username { get; init; }
    public string email { get; init; }
    public string password { get; init; }
}
```
*./Models/UserModels/UserSigninRequestModel.cs*

```csharp
public class UserLoginRequestModel
{
    public string username { get; init; }
    public string password { get; init; }
}
```
*./Models/UserModels/UserLoginRequestModel.cs*

Note que as propriedades serão apenas inicializadas com *Object Initializers* por conta do `init` isso será suficiente para nós já que depois de receber as informações via requisição do cliente não queremos mudar por exemplo. Agora vamos criar o modelo do para receber as informações do cliente para cadastrá-lo e atualizar suas informações:

```csharp
public class ClientRegisterRequestModel
{
    public string username { get; init; }
}
```
*./Models/ClientModels/ClientRegisterRequestModel.cs*

```csharp
public class ClientUpdateRequestModel
{
    public string username { get; init; }
}
```
*./Models/ClientModels/ClientUpdateRequestModel.cs*

Os modelos para criação e modificação das informações do usuário são idênticos, mas para manter o propósito do uso de cada modelos vamos manter separados, isso pode até ajudar no futuro caso seja necessário adicionar um campo para guardar a data da última modificação por exemplo, onde essa data será passada somente quando atualizar, ou seja, ele estará presente apenas no `ClientUpdateRequestModel`.

Agora temos todos os modelos que precisamos para começar a criar nossos controladores, mas antes devemos falar sobre algo muito importante no [ASP.NET](http://ASP.NET) e que será muito útil para tornar nossos controladores modulares e manter o baixo acoplamento.

# DI (Dependency Injection)

Injeção de dependência é um padrão de projeto que consiste em fornecer as dependências necessárias para uma classe através de construtores ou propriedades, em vez de criá-las dentro da própria classe. No ASP.NET, a injeção de dependência é implementada através do uso de um container de DI, como o `IServiceProvider` que já vem integrado.

![DI_ASPNET.png](https://raw.githubusercontent.com/LuanRoger/AllInOneAspNet/main/images/DI_ASPNET.png)

### Analogia com o mundo real

Imagine um automóvel, ele possue peças que podem ser trocadas como motor, câmbio, volante, faróis, etc., isso pode ajudar para quando alguma peça der defeito ou quisermos colocar alguma outra peça de um fabricante diferente, como um novo pneu. Se precisarmos trocar um pneu por exemplo, queremos um que se comporte como um pneu, mesmo que o material ou o raio seja diferente precisamos trocar um pneu por um outro, parece obvio, mas esse é exatamente o propósito da DI, trocar partes do software por outras que apesar da implementação seja diferente, funciona da mesma forma.

Explicar injeção de dependência é uma tarefa complicada já que o conceito do DI é complexo e pode não parecer claro a necessidade de cada definição que compõe o todo, mas aprender como usar DI, não só no ASP.NET, é crucial por conta de suas vantagens:

- Reduz o acoplamento entre as classes do sistema.
- Permite a criação de testes unitários mais facilmente.
- Facilita a manutenção do código.

Um dos principais componentes do DI, já mencionado anteriormente, é o container de DI, ele é quem vai gerenciar nossas dependências, injetando-as em outros serviços também cadastrados no container e garantindo o tempo de vida definido.

No ASP.NET temos três tempos de vida que podemos definir para nosso serviço:

- Transient: É criado cada vez que o serviço for requisitado ao container.
- Scoped: Em aplicações web, indica que o serviço será criado apenas uma vez por requisição. Em outros tipos de aplicação, ele terá um escopo onde será destruído no final dele.
- Singleton: São criados apenas uma vez, sendo está a primeira vez que o serviço for requisitado ao container, e será mantido esta instancia até o final do programa. Assim cada requisição ao container de DI subsequente será devolvida sempre a mesma instancia.

Usaremos mais o Scoped, pois define exatamente o que precisamos: Cada requisição terá uma nova instancia dos serviços que precisa. Isso será de extrema importância quando formos usar o Entity Framework. Além desses tempos de vida, algumas bibliotecas podem implementar novos por meio de métodos de extensão, mas todos eles implementam pelo menos um desses três como base por baixo dos panos.

Para que o container saiba qual dependência injetar e onde, devemos criar uma interface que define os métodos que os outros serviços ou clientes irão usar, no nosso caso vamos criar primeiramente uma interface que será implementado pelo nosso controlador:

```csharp
public interface IClientController
{
    /// <summary>
    /// Cadastrar um novo cliente
    /// </summary>
    /// <param name="registerRequestModel">Requisição de cadastro do cliente</param>
    /// <returns>O ID do novo cliente</returns>
    public Task<int> RegisterClient(ClientRegisterRequestModel registerRequestModel, int userId);
    
    /// <summary>
    /// Recupera os clientes cadastrados por um usuário
    /// </summary>
    /// <param name="userId">ID do usuario</param>
    /// <returns>Uma lista dos clientes cadastrados pelo usuario</returns>
    public Task<IReadOnlyList<ClientReadModel>> GetUserClients(int userId);
    
    /// <summary>
    /// Atualiza os dados de um cliente
    /// </summary>
    /// <param name="updateRequest">Requisição de atualização com as novas informações do cliente</param>
    /// <param name="clientId">ID do cliente que será atualizado</param>
    /// <returns>O ID do cliente atualizado</returns>
    public Task<int> UpdateClient(ClientUpdateRequestModel updateRequest, int clientId);
    
    /// <summary>
    /// Deleta um cliente
    /// </summary>
    /// <param name="clientId">ID do cliente que será deletado</param>
    /// <returns>O ID do cliente deletado</returns>
    public Task<int> DeleteClient(int clientId);
}
```
*./Controllers/IClientController.cs*

```csharp
public interface IUserController
{
    /// <summary>
    /// Cadastra um novo usuário
    /// </summary>
    /// <param name="signinRequest">Requisição de cadastro do usuário</param>
    /// <returns>Retorna um novo JWT para o usuário</returns>
    public Task<string> SigninUser(UserSigninRequestModel signinRequest);
    
    /// <summary>
    /// Autentica um usuário
    /// </summary>
    /// <param name="loginRequest">Requisição de login do usuário</param>
    /// <returns>Retorna um novo JWT para o usuário</returns>
    public Task<string> LoginUser(UserLoginRequestModel loginRequest);
}
```
*./Controllers/IUserController.cs*

O controlador terá apenas uma implementação no caso do nosso projeto, então por que vamos criar uma interface para cadastrá-lo no container de DI?

Porque vamos tratar os controladores como serviços que depende de outros serviços (dependências), e como nosso controlador será um serviço ele pode ser injetado na função anônima dos endpoints visto anteriormente.

Antes de implementar as interfaces e cadastrá-las no container de DI vamos inicializar nossas outras dependências que serão usadas nos controladores.

# Entity Framework

O Entity Framework Core é um mapeado de objetos (ORM) com bancos de dados SQL, tornando possível usar objetos como abstrações de tabelas e suas propriedades como colunas, ele dá suporte a alterações de esquemas do banco por meio de migrações. Ele é compatível com vários motores de banco de dados como SQLite, MySQL, PostgreSQL Azure Cosmos DB, etc. Veja a lista completa [aqui](https://learn.microsoft.com/pt-br/ef/core/providers/?tabs=dotnet-core-cli).

Neste artigo estaremos utilizando o Entity Framework Core 7 compatível com o .NET 7 juntamente com o SQLite, mas como tido anteriormente ele suporta vários outros motores. Para instalar execute este comando na raiz do seu projeto:

```powershell
dotnet add package Microsoft.EntityFrameworkCore.Sqlite
```

O Entity tem duas formas de mapear os objetos, uma delas é *fluent API*, que encadeia chamadas de métodos para mapear modelos:

```csharp
internal class MyContext : DbContext
{
    public DbSet<Blog> Blogs { get; set; }

    #region Required
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Blog>()
            .Property(b => b.Url)
            .IsRequired();
    }
    #endregion
}

public class Blog
{
    public int BlogId { get; set; }
    public string Url { get; set; }
}
```
Fonte: https://learn.microsoft.com/en-us/ef/core/modeling/#use-fluent-api-to-configure-a-model

O exemplo acima mostra como é feita com usando *fluent API*, usando essa abordagem é necessário sobrescrever o método `OnModelCreating`. Mas a forma que iremos mapear nossos objetos é usando atributos:

```csharp
internal class MyContext : DbContext
{
    public DbSet<Blog> Blogs { get; set; }
}

[Table("Blogs")]
public class Blog
{
    public int BlogId { get; set; }

    [Required]
    public string Url { get; set; }
}
```
Fonte: https://learn.microsoft.com/en-us/ef/core/modeling/#use-data-annotations-to-configure-a-model

Veja que usa dos função de atributos do C# para definir uma tabela, onde também define o nome da tabela por parâmetro, e o campo `Url` como obrigatório. Acho esta forma bem mais simples e concisa, mas caso prefira a outra abordagem para mapear seus objetos sinta-se livre para usar, pois o resultado final será o mesmo.

Então vamos voltar aos nossos modelos base e mapeá-los com os atributos:

```csharp
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AllInOneAspNet.Models.UserModels;

[Table("Users")]
public class UserModel
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    [Required]
    [Column("ID")]
    public int id { get; set; }
    
    [Required]
    [Column("Username")]
    public string username { get; set; }
    
    [Column("Email")]
    public string email { get; set; }
    
    [Required]
    [Column("Password")]
    public string password { get; set; }
}
```
*./Models/UserModels/UserModel.cs*

```csharp
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using AllInOneAspNet.Models.UserModels;

namespace AllInOneAspNet.Models.ClientModels;

[Table("Clients")]
public class ClientModel
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    [Required]
    [Column("ID")]
    public int id { get; set; }
    
    [Required]
    [MaxLength(100)]
    [Column("Username")]
    public string username { get; set; }
    
    [Required]
    [Column("CreatedBy")]
    public UserModel createdBy { get; set; }
}
```
*./Models/ClientModels/ClientModel.cs*

As anotações vêm de `System.ComponentModel.DataAnnotations` e `System.ComponentModel.DataAnnotations.Schema`. Na classe usamos o atributo `Table` que podemos definir o nome da tabela, ela não é obrigatória para definir uma tabela, mas caso não tenha ela, a sua tabela no banco de dados terá o mesmo nome da classe. Isso acontece também com as propriedades, que serão mapeadas em colunas daquela tabela, portanto, usamos o atributo `Column` para definir suas propriedades.

O resto dos atributos define/específica:

- `Key` - Define uma ou mais propriedades como chave primaria da tabela.
- `DatabaseGenerated` - Específica como o banco de dados irá gerar os valores para esta propriedade. No nosso caso, usamos para gerar valores incrementais para a chave primaria.
- `Required` - Especifica que a propriedade não poderá ter valor nulo no banco.
- `MaxLength` - Especifica o comprimento máximo da coluna no banco.

Agora que já temos os nossos modelos mapeados e suas propriedades especificadas, vamos agora criar nosso `DbContext`.

# Usar o Entity Framework com SQLite

Mencionado anteriormente, o `DbContext` é uma representação de uma conexão ao seu banco de dados, se você já mexeu um pouco com o ADO.NET sabe que cada operação que formos fazer no nosso banco temos que instanciar uma nova conexão para que as operações sejam paralelas, e aqui não é diferente, sempre que formos fazer uma operação devemos instanciar um novo `DbContext`. Mas primeiro devemos cria-lo.

O `DbContext` não é instanciado sem nenhuma configuração antes, pois ele ainda não sabe quais são os modelos que mapeamos e vamos usar por exemplo, então criaremos uma nova classe instanciando-o:

```csharp
public class DatabaseContext : DbContext
{
    public DbSet<ClientModel> client { get; set; } = null!;
    public DbSet<UserModel> user { get; set; } = null!;

    public DatabaseContext(DbContextOptions<DatabaseContext> options) : base(options) { }
}
```
*./Repositories/Contexts/DatabaseContext.cs*

Especificamos nossos dois modelos como propriedades da classe como tipo de um `DbSet` e não atribuímos valor nenhum a eles, isso porque o Entity Framework é quem vai cuidar disso para nós, então apenas suprimimos os avisos com `null!`, ela é quem será usada para executar as nossas transações. Além disso o construtor está vazio, apenas recebendo um `DbContextOptions<DatabaseContext>` como parâmetro e encaminhando-o para o construtor do `DbContext`, isso porque vamos configurar a criação de cada nova instancia do `DbContext` para cada operação que formos fazer, e quem vai cuidar de instanciar um novo `DbContext` quando precisarmos é o container de DI.

Assim a cada nova requisição que recebermos na nossa API, uma nova instancia do `DbContext` será criada e injetada nos nossos **REPOSITÓRIOS**, e não diretamente nos controladores, isso vai garantir que todas as operações das requisições sejam paralelas.

Como mencionado anteriormente, não iremos injetar o `DbContext` direto no controlador, usaremos um padrão chamado *Repository Pattern*, mas antes de falar sobre ele, vamos cadastrar nosso `DbContext` no container de DI, sendo este o primeiro serviço que vamos cadastrar, para isso:

```csharp
builder.Services.AddDbContext<DatabaseContext>(options =>
{
    const string connectionString = @"Data Source=AllInOneDatabase.db;";
    options.UseSqlite(connectionString);
});
```
*./Program.cs*

Acessamos os serviços pelo `builder` e cadastramos o `DatabaseContext` por meio do método `AddDbContext`, sendo este um método de extensão que vem juntamente com o pacote do Entity Framework que adicionamos, mas que tem o tempo de vida Scoped. Para configurar como cada instancia será criada, passamos uma função anônima com um parâmetro `DbContextOptionsBuilder options`, que é o `options` que recebemos como parâmetro do construtor de `DatabaseContext`.

Sinta-se livre para definir uma string de conexão diferente, esta que estamos usando criará o banco no mesmo diretório que estamos, veja as referências do artigo onde tem um site que mostra as possíveis strings de conexão para o SQLite. 

Em seguida, usamos o método `UseSqlite` para usar o SQLite como provedor e passamos a string de conexão como parâmetro, caso tenha adicionado outro provedor de banco de dados, tente usar o prefixo `Use`  para tentar descobrir o método do provedor que você adicionou, se não tiver encontrado, consulte a documentação.

Vale lembrar que, por padrão, o Entity Framework não cria o banco de dados caso ele não exista, então vamos assegurar que sempre que nossa API for iniciada ele esteja criado antes de fazer qualquer operação com ele, para isso vamos chamar ele do nosso container de DI e usar um método do próprio Entity Framework para criar o banco.

```csharp
//...
WebApplication app = builder.Build();

using (IServiceScope scope = app.Services.CreateScope()) // 1
{
    DatabaseContext dbContext = scope.ServiceProvider.GetRequiredService<DatabaseContext>(); // 2
    dbContext.Database.EnsureCreated(); // 3
}
//...
```
*./Program.cs*

1. Cria um escopo para acessar os serviços em uma diretiva `using`. Já que nosso serviço tem o tempo de vida Scoped isso vai definir até onde ele vive. Esta forma é utilizada caso não tenham um outro serviço específico para usá-lo como dependência, já estamos usando-o solto no `Program.cs`.
2. Acessa o provedor de serviços e pega um serviço obrigatório, isso quer dizer que se o `DatabaseContext` não estiver cadastrado no container de DI ele jogará um erro.
3. Usa o método `EnsureCreated` para segurar que o banco de dados esteja criado.

Agora que temos nosso `DbContext` criado e cadastrado no container de DI, vamos criar os repositórios que usarão ele.

# Repository Pattern

Este *pattern* define uma camada de acesso aos dados, isso para separa a nossa camada de domínio ou logica de negócio da camada de acesso ao banco de dados em si, ou seja, nossos controladores não irão acessar o banco diretamente, mesmo que tenhamos o Entity Framework para acessar o banco, vamos separar esta unidade de trabalho em uma camada de infraestrutura que estará também os repositórios que criaremos.

![EntityWRepositoryPattern.png](https://raw.githubusercontent.com/LuanRoger/AllInOneAspNet/main/images/EntityWRepositoryPattern.png)

No final teremos os nossos controladores na camada de domínio separada da camada de infraestrutura. Os conceitos de camadas, domínios, repositórios, etc. são parte do DDD (Domain-Driven Design), não vou entrar em detalhes sobre o que é esta arquitetura, apenas mostrar o quão importante é o *Repository Pattern* e o porquê vamos usar ele, se quiser saber mais sobre esta arquitetura, veja as referências do artigo. Dito isso, vamos criar nossos repositórios.

Vou criar um repositório para cada controladores que temos:

### Interfaces dos repositórios

```csharp
public interface IUserRepository
{
    /// <summary>
    /// Cadastra um novo usuário
    /// </summary>
    /// <param name="user">Usuário para cadastrar</param>
    /// <returns>Retonar o ususário cadastrado</returns>
    public Task<UserModel> RegisterUser(UserModel user);

    /// <summary>
    /// Resgatar um usuário pelo ID
    /// </summary>
    /// <param name="userId">ID do usuário</param>
    /// <returns>Retorna o usuário que possui o ID indicado ou <c>null</c></returns>
    public Task<UserModel?> GetUserById(int userId);
    
    /// <summary>
    /// Resgatar um usuário pelo <c>username</c>
    /// </summary>
    /// <param name="username">Nome do usuario</param>
    /// <returns>Retorna um usuario que possua o nome de usuario especificado.</returns>
    public Task<UserModel?> GetUserByUsername(string username);
    
    /// <summary>
    /// Apply all changes made to the database
    /// </summary>
    public Task FlushChanges();
}
```
*./Repositories/IUserRepository.cs*

```csharp
public interface IClientRepository
{
    /// <summary>
    /// Cadastra um novo cliente
    /// </summary>
    /// <param name="client">Modelo contendo as infomações do cliente para cadastro</param>
    /// <returns>Novo cliente cadastrado</returns>
    public Task<ClientModel> RegisterClient(ClientModel client);
    
    /// <summary>
    /// Resgata um cliente pelo ID
    /// </summary>
    /// <param name="clientId">ID do cliente</param>
    /// <returns>Retona o cliente encontrado ou <c>null</c></returns>
    public Task<ClientModel?> GetClientById(int clientId);
    
    /// <summary>
    /// Resgata todos os clientes cadastrados por um determinado usuário
    /// </summary>
    /// <param name="userId">ID do usuário</param>
    /// <returns>Lista contendo todos os clientes cadastrados pelo usuário</returns>
    public Task<IReadOnlyList<ClientModel>> GetUserRelatedClient(int userId);
    
    /// <summary>
    /// Deleta um cliente
    /// </summary>
    /// <param name="clientModel">Cliente para deletar</param>
    /// <returns>Informações do cliente deletado</returns>
    public ClientModel DeleteClient(ClientModel clientModel);
    
    /// <summary>
    /// Apply all changes made to the database
    /// </summary>
    public Task FlushChanges();
}
```
*./Repositories/IClientRepository.cs*

### Implementação dos repositórios

```csharp
public class UserRepository : IUserRepository
{
    private DatabaseContext dbContext { get; }
    
    public UserRepository(DatabaseContext dbContext)
    {
        this.dbContext = dbContext;
    }
    
    public async Task<UserModel> RegisterUser(UserModel user) => 
        (await dbContext.user.AddAsync(user)).Entity;

    public async Task<UserModel?> GetUserById(int userId) => 
        await dbContext.user.FindAsync(userId);

    public async Task<UserModel?> GetUserByUsername(string username) =>
        await dbContext.user
            .FirstOrDefaultAsync(user => user.username == username);
    
    public async Task FlushChanges() =>
        await dbContext.SaveChangesAsync();
}
```
*./Repositories/UserRepository.cs*

```csharp
public class ClientRepository : IClientRepository
{
    private DatabaseContext dbContext { get; }
    
    public ClientRepository(DatabaseContext dbContext)
    {
        this.dbContext = dbContext;
    }
    
    public async Task<ClientModel> RegisterClient(ClientModel client) =>
        (await dbContext.client.AddAsync(client)).Entity;

    public async Task<ClientModel?> GetClientById(int clientId) =>
        await dbContext.client.FindAsync(clientId);

    public async Task<IReadOnlyList<ClientModel>> GetUserRelatedClient(int userId) =>
        await dbContext.client
            .Where(client => client.createdBy.id == userId)
            .OrderBy(client => client.id)
            .ToListAsync();

    public ClientModel DeleteClient(ClientModel clientModel) =>
        dbContext.client.Remove(clientModel).Entity;
    
    public async Task FlushChanges() =>
        await dbContext.SaveChangesAsync();
}
```
*./Repositories/ClientRepository.cs*

A implementação com o Entity Framework acaba sendo bastante simples já que não precisamos usar SQL cru, além dos métodos serem bastante claros e conciso. Note que estamos sempre retornando `Task`, pois nossos controladores também retornam `Task` e executarão as requisições dos nossos repositórios de forma assíncrona, no final quem irá executar os métodos dos nossos controladores serão os endpoints.

Para executar uma transação no banco de dados acessamos o `DbContext` e nele, o `DbSet` do tipo que queremos executar a transação, lembre-se que as classes que mapeamos vão se comportar como tabelas, a partir daí podemos executar métodos como `AddAsync` para adicionar, `FindAsync` para recuperar uma entidade do banco pela chave primaria, `Remove` para remover e por aí vai. Para resgatar entidades de alguma outra forma além de pôr chave primaria, podemos usar consultas [LINQ](https://learn.microsoft.com/pt-br/dotnet/csharp/programming-guide/concepts/linq/introduction-to-linq-queries) como mostrado em `GetUserRelatedClient`.

Se estiver se perguntando como que atualizamos as informações do cliente já que não temos um método propriamente para isso, é porque para atualizar uma informação devemos primeiro resgatar uma entidade usando o Entity Framework, atualizar as informações que queremos, depois chamar `SaveChangesAsync`.

Cada operação que fazermos (adicionar, atualizar ou remover entidades) no banco devemos sempre, ao final, chamar `FlushChanges` pois ele é quem irá efetivar todas as alterações que fizermos no banco, então mantenha isso em mente quando formos criar os controladores.

Agora vamos cadastrar ambos os repositórios no container de DI, dessa forma as propriedades `dbContext` serão injetadas:

```csharp
builder.Services.AddScoped<UserRepository>();
builder.Services.AddScoped<ClientRepository>();
```
*./Program.cs*

Ao contrário do `DatabaseContext`, não precisamos nem inicializar o construtor de `UserRepository` ou `ClientRepository`, pois o container identifica os parâmetros do único construtor que temos e injeta os serviços se que já estejam cadastrados no container, caso um serviço dependa de um outro serviço que não esteja cadastrado no container, um erro será jogado. Vale mencionar que o container prefere instanciar construtores sem parâmetro dos serviços, portanto não os tenha no seu serviço caso queira que as dependências sejam injetadas corretamente.

# Logging com Serilog

Log é de extrema importância para observação do sistema, ele pode registrar qualquer acontecimento que ocorre no sistema, categorizando por severidades e mostrando informações relevantes como data e hora do ocorrido, ambiente em que o sistema estava rodando e muito mais.

Neste artigo usaremos o Serilog para registrar nossos logs, ele é uma biblioteca externa portanto vale ressaltar que o .NET possui um logger padrão que o que está sendo usado desde o princípio, além de ele já está no container de DI e pode ser injetado em qualquer serviço que use `ILogger` como dependência, não pense que isso será um problema pois ele pode ser facilmente pelo Serilog.

Um dos principais diferenciais do Serilog é que ele é baseado em eventos e altamente extensível com o que ele chama de *Sinks*, que proveem vários storage de logs, você pode ver todos [aqui](https://github.com/serilog/serilog/wiki/Provided-Sinks). Vale sempre lembrar que você pode deixar como está para que assim use o logger padrão do .NET se preferir.

Vamos adicioná-lo ao nosso projeto, e junto com o *core* dele vamos também adicionar o sink para registrar nossos logs no console.

Execute os seguintes comandos na raiz do projeto:

```powershell
dotnet add package Serilog.AspNetCore
```

```powershell
dotnet add package Serilog.Sinks.Console
```

Vamos iniciar e configurar um novo logger para registrar os logs no console:

```csharp
using ILogger = Serilog.ILogger;
//...
ILogger logger = new LoggerConfiguration()
    .WriteTo.Console()
    .CreateLogger();
```
*./Program.cs*

O Srilog pode registrar os logs em vários provedores ao mesmo tempo, caso tenha mais de um provedor basta encadear chamadas `WriteTo` seguindo com o método do provedor (alguns precisam de configurações extra).

Agora vamos configurar [ASP.NET](http://ASP.NET) para usar o logger que criamos:

```csharp
builder.Logging.ClearProviders(); // 1
builder.Logging.AddSerilog(logger); // 2
builder.Host.UseSerilog(logger); // 3
```
*./Program.cs*

1. Limpamos todos os provedores (provedores padrão do ASP.NET)
2. Adicionamos o nosso logger na coleção de loggers que compõem nossa aplicação. Onde tem apenas o nosso `logger`, já que limpamos os toda a coleção anteriormente.
3. Redirecionamos os logs criados pelo host (API) para o nosso logger.

Não precisamos cadastrar o logger em `builder.Services`, pois `builder.Logging` já injetara-lo nos serviços que dependem dele.

Para garantir que o Serilog está funcionando, execute a API e se os logs estiverem com um formato diferente da primeira vez que executou então está funcionando. O formato pode ser semelhante a este:

```csharp
[00:00:00 INF] Application started. Press Ctrl+C to shut down.
```

Onde mostra a hora do registro, severidade e em seguida a mensagem.

# Criar validadores

Essa afirmação deveria ser tratada como uma regra: Toda informação que recebemos do usuário devemos validar, este artigo não estaria completo se não cobríssemos este tópico também. Para esta tarefa vamos usar a biblioteca ****FluentValidation****, que podemos definir regras de validação para nossos modelos e checar se o modelo que recebemos está de acordo com elas.

Para começar vamos adicioná-lo ao nosso projeto:

```csharp
dotnet add package FluentValidation
```

Para definir as regras das classes devemos criar uma classe que herda de `AbstractValidator<T>`, onde `T` é o tipo do objeto que será validado, neste primeiro código vou criar o validador de `UserSigninRequestModel`. 

Vamos criar validadores apenas para modelos que recebemos do consumidor da API, e é importante levar em consideração as restrições que definimos no banco de dados também, lembre-se que na classe `ClientModel` definimos o atributo `[MaxLength(100)]` na propriedade `username`, além de várias propriedades como `[Required]`, ou seja, não podem ser nulas ou vazias.

Vamos definir as regras para nossos modelos:

```csharp
public class UserSigninRequestValidator : AbstractValidator<UserSigninRequestModel>
{
    public UserSigninRequestValidator()
    {
        RuleFor(model => model.username)
            .NotNull()
            .NotEmpty();
        RuleFor(model => model.password)
            .NotNull()
            .NotEmpty();
        RuleFor(model => model.email)
            .EmailAddress();
    }
}
```
*./Validators/UserValidators/UserSigninRequestValidator.cs*

```csharp
public class UserLoginRequestValidator : AbstractValidator<UserLoginRequestModel>
{
    public UserLoginRequestValidator()
    {
        RuleFor(model => model.username)
            .NotNull()
            .NotEmpty();
        RuleFor(model => model.password)
            .NotNull()
            .NotEmpty();
    }
}
```
*./Validators/UserValidators/UserSigninRequestValidator.cs*

Usamos os métodos `RuleFor` passando um lambda que retornando a propriedade para qual queremos criar uma regra, e em seguida encadeamos com as regras em si. Por exemplo, primeira regra que definimos é para a propriedade `username` onde restringimos que ela não pode ser vazia nem nula. O FluentValidation possui vários validadores embutidos que você pode conferir todos [aqui](https://docs.fluentvalidation.net/en/latest/built-in-validators.html), mas caso não possua o que você, pode criar encadeando a regra `Custom`.

Agora vamos criar os validadores do cliente:

```csharp
public class ClientRegisterRequestValidator : AbstractValidator<ClientRegisterRequestModel>
{
    public ClientRegisterRequestValidator()
    {
        RuleFor(model => model.username)
            .NotNull()
            .NotEmpty()
            .MaximumLength(100);
    }
}
```
*./Validators/ClientValidators/ClientRegisterRequestValidator.cs*

```csharp
public class ClientUpdateRequestValidator : AbstractValidator<ClientUpdateRequestModel>
{
    public ClientUpdateRequestValidator()
    {
        RuleFor(model => model.username)
            .NotNull()
            .NotEmpty()
            .MaximumLength(100);
    }
}
```
*./Validators/ClientValidators/ClientUpdateRequestValidator.cs*

Agora que temos todos os nossos validadores prontos vamos cadastrá-los no container de DI para que sejam injetados nos nossos controladores, onde não usaremos o tipo `ClientUpdateRequestValidator` explicitamente, usaremos a interface `IValidator<T>` onde `T` é o tipo do modelo que estamos validando, da mesma forma que fizermos com `AbstractValidator`, assim podemos usar os métodos definidos em `IValidator`.

Para que container de DI saiba que quando quisermos consumir `IValidator<UserSigninRequestModel>` ele injetar `UserSigninRequestValidator` por exemplo, vamos simplesmente para dois tipos em `AddScoped`:

```csharp
builder.Services.AddScoped<IValidator<UserSigninRequestModel>, UserSigninRequestValidator>();

builder.Services.AddScoped<IValidator<UserLoginRequestModel>, UserLoginRequestValidator>();

builder.Services.AddScoped<IValidator<ClientRegisterRequestModel>, ClientRegisterRequestValidator>();

builder.Services.AddScoped<IValidator<ClientUpdateRequestModel>, ClientUpdateRequestValidator>();~~~~
```
*./Program.cs*

Isso só é possível porque nossas classes herdam de `AbstractValidator` que por sua vez herdam de `IValidator`.

# Autenticação e Autorização com JWT usando chave simétrica.

Quando queremos lidar com autenticação de usuários na nossa solução uma das principais opções é usar JWT para lidar com *Claims*, que são formas de carregar identificação do usuário ou qualquer outro tipo de *payload* que quisermos receber para nos ajudar, inclusive, para autorização do usuário.

Antes de nos aprofundar em JWT, precisamos rapidamente, esclarecer a diferença entre autenticação e autorização.

### Autenticação e autorização

Autenticação diz respeito a identificação do usuário, quem ele é no sistema por exemplo. Enquanto a autorização diz o que o usuário pode fazer nos sistemas, ou seja, define restrições que se aplicam a determinado tipo de usuário, algo que podemos receber nas *Claims* do JWT.

### JWT

O JWT (JSON Web Token) é um padrão que define uma forma compacta de transferir informações entre duas partes, estas informações podem ser assinadas e usando uma chave simétrica ou um par de chaves (pública e privada)

- Cabeçalho - Define, principalmente, o tipo de algoritmo que o token foi assinado.
- *Payload* - Esta parte contém as *claims*, esta define as informações dos usuários, como ID, tipo de acesso, etc. Além deles, podemos passar informações ditas públicas como *issuer*, quem produz a token, *expiration time*, tempo de expiração do token, *audience*, quem irá receber o token, etc.
- Assinatura - É importante para verificar a veracidade e autenticidade do token, a forma que esta parte é dada depende do algoritmo que escolhemos usar. Um exemplo que é dado na documentação do JWT é usando HMAC SHA256 que é criado da seguinte forma:

```csharp
HMACSHA256(
  base64UrlEncode(header) + "." +
  base64UrlEncode(payload),
  secret)
```
*Fonte: https://jwt.io/introduction*

Como mencionado anteriormente, o JWT pode ser assinado usando chaves assimétricas, mas neste artigo usaremos apenas chave simétrica apenas pelo fato ser serem mais simples de implementar.

Assim vamos iniciar adicionando o pacote necessário para usar JWT:

```csharp
dotnet add package Microsoft.AspNetCore.Authentication.JwtBearer
```

Agora vamos criar um serviço para gerar o token:

```csharp
public static class JwtConsts
{
    public const string JWT_SIMETRIC_KEY_SHA256 = 
        "6d12dce15aee4d33e6f4b3ed4902604bd01b99af9f400f931a2d043c52ee0ec3";
    public const string CLAIM_ID = "ID";
    public const string JWT_ISSUER = "AllInOneAspNet";
}
```
*./Services/Jwt/JwtConsts.cs*

```csharp
public class JwtService
{
    private byte[] simetricKey { get; }
    
    public JwtService(byte[] simetricKey)
    {
        this.simetricKey = simetricKey;
    }
    
    public string GenerateToken(int userId)
    {
        JsonWebTokenHandler handler = new();
        string jwt = handler.CreateToken(new SecurityTokenDescriptor
        {
            Subject = new(new []
            {
                new Claim(JwtConsts.CLAIM_ID, userId.ToString())
            }),
            SigningCredentials = new(new SymmetricSecurityKey(simetricKey), 
                SecurityAlgorithms.HmacSha256Signature),
            Issuer = JwtConsts.JWT_ISSUER,
            IssuedAt = DateTime.Now
        });
        
        return jwt;
    }
}
```
*./Services/Jwt/JwtService.cs*

A classe `JsonWebTokenHandler` é a que usamos para criar o token com o método `CreateToken`, passando `SecurityTokenDescriptor` onde atribuiremos todas as propriedades que queremos receber do usuário futuramente. 

Começando com o `Subject` que irá receber as nossas *Claims,* onde aqui estou passando apenas o ID do usuário, mas como já havia dito, pode ser passado qualquer outra informação que possa te ajudar a identificar o usuário. A propriedade `SigningCredentials` define as credenciais da assinatura, como estamos usando chave simétrica, então instanciaremos `SymmetricSecurityKey` passando a chave simétrica, em seguida passamos o algoritmo que usamos para a chave simétrica.

Esta são as propriedades mais importantes, mas você pode especificar vários outros parâmetros que estarão no *payload*, como *issuer*, tempo de expiração, etc.

Você pode estar se perguntando onde podemos colocar a nossa chave simétrica, para deixar este artigo simples vou colocar a chave na função anônima de criação do serviço pelo container de DI, mas é de extrema importância que você coloque pelo menos como variável de ambiente ou usar algum serviço de gerenciamento de segredos, assim:

```csharp
builder.Services.AddSingleton<JwtService>(_ =>
{
    byte[] byteKey = Encoding.UTF8.GetBytes(JwtConsts.JWT_SIMETRIC_KEY_SHA256);
    
    return new(byteKey);
});
```
*./Program.cs*

Note que estamos usando `AddSingleton` para adicionar no container de DI o que quer dizer que ele só será instanciado uma vez e está instancia será usada pelo resto do programa.

Agora que os nossos outros serviços podem usar o `JwtService` para criar tokens, vamos criar o verificador para os tokens que recebemos, com o pacote que adicionamos, ele já possui um *middleware* que podemos usar para configurar como os token serão verificados. Para esclarecer, um *middleware* ele é um de código que ocorre em algum momento durante a o *pipeline* de execução da requisição, no casso do middleware do JWT, ele será executado antes da execução dos nossos controladores, o que faz sentido, já que não queremos executar os nossos endpoints em si sem antes verificar se o token que recebemos é realmente valido.

Assim iniciar e configurar o nosso *middleware*:

```csharp
builder.Services.AddAuthentication(options => // 1
{
    options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(options => // 2
{
    byte[] byteKey = Encoding.UTF8.GetBytes(JwtConsts.JWT_SIMETRIC_KEY_SHA256);
    
    options.TokenValidationParameters = new()
    {
        ValidateIssuer = true,
        ValidateIssuerSigningKey = true,
        IssuerSigningKey = new SymmetricSecurityKey(byteKey),
        ValidAlgorithms = new [] { SecurityAlgorithms.HmacSha256Signature }
    };
});
```
*./Program.cs*

1. Registra um serviço de autenticação (ainda não é o *middleware*, mas será usado por ele) e define os esquemas de verificação para os esquemas padrão do JWT. Veja o que cada propriedade significa [aqui](https://learn.microsoft.com/en-us/dotnet/api/microsoft.aspnetcore.authentication.authenticationoptions?view=aspnetcore-7.0).
2. Adiciona a autenticação por JWT baseado nos esquemas que definimos anteriormente. Aqui configuramos como será verificado o token que recebemos, por exemplo, definimos que queremos validar o *issuer* com `ValidateIssuer`, a chave que usamos para assinar o JWT que geramos em `JwtService`será verificada seguindo as propriedades `IssuerSigningKey` e `ValidAlgorithms`. Estes são apenas alguns parâmetros que podemos definir para verificação do token, veja todos os parâmetros de validação [aqui](https://learn.microsoft.com/en-us/dotnet/api/microsoft.identitymodel.tokens.tokenvalidationparameters?view=msal-web-dotnet-latest).

Quanto a autorização, vamos apenas adicioná-la nos serviços sem nenhuma configuração extra, pois vamos adicionar as políticas nos grupos dos endpoints que queremos que tais sejam aplicadas.

```csharp
builder.Services.AddAuthorization();
```
*./Program.cs*

Para que usar estas configurações que especificamos, vamos adicionar os middlewares de autenticação e autorização a nossa pipeline de execução, para isso podemos simplesmente especificar o uso deles:

```csharp
app.UseAuthentication();
app.UseAuthorization();
```
*./Program.cs*

É muito importante que `UseAuthentication` esteja sendo executado primeiro que `UseAuthorization`, caso contrário irá ser jogado um erro, já que não podemos autorizar um usuário que não está autenticado.

Um dos nossos requisitos que listamos no começo era que apenas usuários autenticados poderão usar o endpoint `client/`, então devemos esclarecer uma política de acesso ao endpoint. Vale lembrar que nosso endpoints estão mapeados com `MapGroup` então podemos aplicar uma regra para o grupo inteiro, não sendo necessário por exemplo, especificar uma política para cada endpoint, pois quando aplicamos uma política ao um grupo todos os endpoints também são afetados.

Então vamos apenas encadear um método ao nosso método `MapGroup` onde podemos especificar peculiaridades da política:

```csharp
RouteGroupBuilder clientGroup = app.MapGroup("client")
    .RequireAuthorization(policyBuilder => policyBuilder.RequireClaim(JwtConsts.CLAIM_ID));
```
*./Program.cs*

Encadeamos o método `RequireAuthorization` passando uma função anônima que podemos definir políticas extras de acesso aos endpoints do grupo, usamos ela para definir que apenas usuários que tiverem um token com a *Claim* “ID” podem acessar os endpoints do grupo `client`.

No grupo `user` podemos especificar que não é necessário ter qualquer tipo de autenticação ou autorização para usar os endpoints do grupo, para isso podemos encadear o método `AllowAnonymous`:

```csharp
RouteGroupBuilder userGroup = app.MapGroup("user")
    .AllowAnonymous();
```
*./Program.cs*

Agora que temos todos os nossos serviços criados e cadastrados no container de DI, vamos finalmente criar nossos controladores

# Criar controladores

Os controladores nas Minimal API no [ASP.NET](http://ASP.NET) não é muito bem-visto e a criação ou não deles podem depender mais do escopo e proposito do projeto, pois você pode usar suas dependências na função anônima do endpoint, nesse projeto eu irei criar controladores para ilustrar melhor quando falarmos de injeção de dependências, mas saiba que neste modelo não é necessário a criação deles e você pode desenvolver toda a lógica na função anônima dos endpoints.

Agora que temos todos as dependências que iremos usar nos controladores, vamos finalmente implementá-los, que anteriormente, criamos apenas a interface na seção de **DI (Dependency Injection)**.

Para usarmos nossos serviços nos controladores, vamos apenas criar uma propriedade as quais queremos usar e iniciá-los com o construtor. Apenas recebendo os serviços no nosso construtor, o container de DI é capaz de identificá-los e injetá-los.

Assim:

```csharp
//...
using ILogger = Serilog.ILogger;

public class UserController : IUserController
{
    private UserRepository repository { get; }
    private JwtService jwtService { get; }
    private ILogger logger { get; }
    private IValidator<UserLoginRequestModel> loginValidator { get; }
    private IValidator<UserSigninRequestModel> signinValidator { get; }
    
    public UserController(
        UserRepository repository, JwtService jwtService, ILogger logger, 
        IValidator<UserLoginRequestModel> loginValidator, 
        IValidator<UserSigninRequestModel> signinValidator)
    {
        this.repository = repository;
        this.jwtService = jwtService;
        this.logger = logger;
        this.loginValidator = loginValidator;
        this.signinValidator = signinValidator;
    }
    
    public async Task<string> SigninUser(UserSigninRequestModel signinRequest)
    {
        throw new NotImplementedException();
    }

    public async Task<string> LoginUser(UserLoginRequestModel loginRequest)
    {
        throw new NotImplementedException();
    }
}
```
*./Controllers/UserController.cs - Parte 1*

Agora usaremos nossos serviços para criar a implementação dos nossos dois métodos do controlador.

```csharp
public async Task<string> SigninUser(UserSigninRequestModel signinRequest)
{
    #region Validation
    logger.Information("Validating user signin request");
    
    ValidationResult validationResult = await signinValidator.ValidateAsync(signinRequest);
     if(!validationResult.IsValid)
     {
         InvalidRequestInfoException invalidInfoException = new(validationResult.Errors
             .Select(e => e.ErrorMessage));
         logger.Error("Invalid user signin request: {InvalidInfoException}", invalidInfoException.Message);
         throw invalidInfoException;
     }
     UserModel? allreadyRegisteredUser = await repository.GetUserByUsername(signinRequest.username);
     if(allreadyRegisteredUser is not null)
     {
         UserAllreadyRegisteredException allreadyRegisteredException = new(signinRequest.username);
            logger.Error("User Username[{Username}] allready registered: {AllreadyRegisteredException}", 
                signinRequest.username, allreadyRegisteredException.Message);
            throw allreadyRegisteredException;
     }

     logger.Information("User signin request is valid");
     #endregion
     
     #region Register user
     UserModel user = new()
     {
         username = signinRequest.username,
         email = signinRequest.email,
         password = signinRequest.password
     };
     UserModel registeredUser = await repository.RegisterUser(user);
     await repository.FlushChanges();
     
     logger.Information("User Username[{Username}] registered", user.username);
     #endregion

     #region Create JWT
     logger.Information("Creating JWT for user Username[{Username}]", user.username);
     string newUserJwt = jwtService.GenerateToken(registeredUser.id);
     #endregion
     
     return newUserJwt;
}
```
*./Controllers/UserController.cs - Parte 2*

```csharp
public async Task<string> LoginUser(UserLoginRequestModel loginRequest)
{
    #region Validation
    logger.Information("Validating user login request");
    ValidationResult validationResult = await loginValidator.ValidateAsync(loginRequest);
    if(!validationResult.IsValid)
    {
        InvalidRequestInfoException invalidInfoException = new(validationResult.Errors
            .Select(e => e.ErrorMessage));
        logger.Error("Invalid user login request: {InvalidInfoException}", invalidInfoException.Message);
        throw invalidInfoException;
    }
    #endregion
    
    #region Login user
    logger.Information("Logging in user Username[{Username}]", loginRequest.username);
    UserModel? user = await repository.GetUserByUsername(loginRequest.username);
    if(user is null)
    {
        UserNotFoundException userNotFoundException = new(loginRequest.username);
        logger.Error("User Username[{Username}] not found: {UserNotFoundException}", 
            loginRequest.username, userNotFoundException.Message);
        throw userNotFoundException;
    }
    if(user.password != loginRequest.password)
    {
        WrongUserPasswordException wrongUserPasswordException = new(loginRequest.username);
        logger.Error("Invalid password for user Username[{Username}]: {InvalidPasswordException}", 
            loginRequest.username, wrongUserPasswordException.Message);
        throw wrongUserPasswordException;
    }
    logger.Information("User logged in Username[{Username}]", loginRequest.username);
    #endregion
    
    #region Create JWT
    logger.Information("Creating JWT for user Username[{Username}]", loginRequest.username);
    string userJwt = jwtService.GenerateToken(user.id);
    #endregion
    
    return userJwt;
}
```
*./Controllers/UserController.cs - Parte 3*

Separei o método em regiões para facilitar a visualização do que está acontecendo. A primeira região, antes de qualquer coisa, valida o que estamos recebendo no controlador como parâmetro, caso tenha algum problema, instanciamos uma `Exception`, registramos ela no log e a jogamos, isso será útil para identificar o tipo de problema ocorreu quando chamarmos o controlador no endpoint já que vamos retornar um código de status diferente para cada problema. Note que no método `SigninUser` também verificamos se este `username` já não está cadastrado.

Na outra região ocorre a ação de cadastro ou de login

O controlador do usuário já está pronto, agora vamos implementar o do cliente começando com definindo as dependências que vamos usar.

```csharp
//...
using ILogger = Serilog.ILogger;

public class ClientController : IClientController
{
    private ClientRepository repository { get; }
    private UserRepository userRepository { get; }
    private ILogger logger { get; }
    private IValidator<ClientRegisterRequestModel> clientRegisterValidator { get; }
    private IValidator<ClientUpdateRequestModel> clientUpdateValidator { get; }

    public ClientController(
        ClientRepository repository, UserRepository userRepository, 
        ILogger logger,
        IValidator<ClientRegisterRequestModel> clientRegisterValidator, 
        IValidator<ClientUpdateRequestModel> clientUpdateValidator)
    {
        this.repository = repository;
        this.userRepository = userRepository;
        this.logger = logger;
        this.clientRegisterValidator = clientRegisterValidator;
        this.clientUpdateValidator = clientUpdateValidator;
    }
    
    public async Task<int> RegisterClient(ClientRegisterRequestModel registerRequestModel)
    {
        throw new NotImplementedException();
    }

    public async Task<IReadOnlyList<ClientReadModel>> GetUserClients(int userId)
    {
        throw new NotImplementedException();
    }

    public async Task<int> UpdateClient(ClientUpdateRequestModel updateRequest, int clientId)
    {
        throw new NotImplementedException();
    }

    public async Task<int> DeleteClient(int clientId)
    {
        throw new NotImplementedException();
    }
}
```
*./Controllers/ClientController.cs - Parte 1*

Veja que temos como dependência dois repositórios, isso porque usaremos `UserRepository` para regatar o usuário que está cadastrando um novo cliente por exemplo, pois estamos fazendo essa relação no nosso modelo, onde em `ClientModel` tem a propriedade `createdBy`.

Agora da mesma forma que implementamos os métodos do usuário, vamos implementar o do cliente, onde também irei separar em regiões.

```csharp
public async Task<int> RegisterClient(ClientRegisterRequestModel registerRequestModel, int userId)
{
    #region Validation
    logger.Information("Validating client register request");
    ValidationResult validationResult = await clientRegisterValidator.ValidateAsync(registerRequestModel);
    if(!validationResult.IsValid)
    {
        InvalidRequestInfoException invalidInfoException = new(validationResult.Errors
            .Select(e => e.ErrorMessage));
        logger.Error("Invalid client register request: {InvalidInfoException}", invalidInfoException.Message);
        throw invalidInfoException;
    }
    UserModel? relatedUser = await userRepository.GetUserById(userId);
    if(relatedUser is null)
    {
        UserNotFoundException userNotFoundException = new(userId.ToString());
        logger.Error("User ID[{Id}] not found: {UserNotFoundException}", 
            userId, userNotFoundException.Message);
        throw userNotFoundException;
    }
    #endregion
    
    #region Register client
    logger.Information("Registering client...");
    ClientModel client = new()
    {
        username = registerRequestModel.username,
        createdBy = relatedUser
    };
    int newClientId = await repository.RegisterClient(client);
    await repository.FlushChanges();
    #endregion
    
    logger.Information("Client registered successfully");
    return newClientId;
}
```
*./Controllers/ClientController.cs - Parte 2*

```csharp
public async Task<IReadOnlyList<ClientReadModel>> GetUserClients(int userId)
{
    #region Validation
    logger.Information("Validating user ID[{Id}]", userId);
    UserModel? user = await userRepository.GetUserById(userId);
    if(user is null)
    {
        UserNotFoundException userNotFoundException = new(userId.ToString());
        logger.Error("User ID[{Id}] not found: {UserNotFoundException}", 
            userId, userNotFoundException.Message);
        throw userNotFoundException;
    }
    #endregion
    
    #region Get user clients
    logger.Information("Getting user ID[{Id}] clients", userId);
    var userClients = await repository.GetUserRelatedClient(userId);
    var userClientRead = 
        userClients.Select(ClientReadModel.FromClientModel).ToList();
    #endregion
    
    return userClientRead;
}
```
*./Controllers/ClientController.cs - Parte 3*

Uma nota importante sobre esta implementação: Na região `Get user clients` estamos resgatando todos os clientes cadastrados pelo usuário e logo em seguida estamos mapeando todos eles para o modelo de leitura (`ClientReadModel`) antes de retorná-los.

```csharp
public async Task<int> UpdateClient(ClientUpdateRequestModel updateRequest, int clientId)
{
    #region Validation
    logger.Information("Validating client update request");
    ValidationResult validationResult = await clientUpdateValidator.ValidateAsync(updateRequest);
    if(!validationResult.IsValid)
    {
        InvalidRequestInfoException invalidInfoException = new(validationResult.Errors
            .Select(e => e.ErrorMessage));
        logger.Error("Invalid client update request: {InvalidInfoException}", invalidInfoException.Message);
        throw invalidInfoException;
    }
    ClientModel? client = await repository.GetClientById(clientId);
    if(client is null)
    {
        ClientNotFoundException clientNotFoundException = new(clientId.ToString());
        logger.Error("Client ID[{Id}] not found: {InvalidInfoException}", 
            clientId, clientNotFoundException.Message);
        throw clientNotFoundException;
    }
    #endregion

    #region Check for updates
    bool hasUpdates = false;
    logger.Information("Checking for updates");
    if(client.username != updateRequest.username)
    {
        logger.Information("Client username changed from {OldUsername} to {NewUsername}", 
            client.username, updateRequest.username);
        client.username = updateRequest.username;
        hasUpdates = true;
    }
    #endregion

    #region Update client
    if(!hasUpdates)
    {
        logger.Warning("There are no updates to be made on ID[{Id}]", client.id);
        return client.id;
    }
    logger.Information("Updating client ID[{Id}]", client.id);
    await repository.FlushChanges();
    #endregion
    
    logger.Information("Client ID[{Id}] updated successfully", client.id);
    return client.id;
}
```
*./Controllers/ClientController.cs - Parte 4*

Este também é um caso particular, que se você estiver notado antes, não temos um método de atualizar no repositório, isso é porque para atualizar uma entidade no Entity Framework apenas regatamos ela, modificamos e salvamos, que no nosso caso, é com o comando `Flush` do repositório. É importante que se modifique o modelo que você resgatou do Entity Framework, pois é só ele que o Entity Framework está rastreando.

```csharp
public async Task<int> DeleteClient(int clientId)
{
    #region Validation
    logger.Information("Validating client ID[{Id}]", clientId);
    ClientModel? client = await repository.GetClientById(clientId);
    if(client is null)
    {
        ClientNotFoundException clientNotFoundException = new(clientId.ToString());
        logger.Error("Client ID[{Id}] not found: {InvalidInfoException}", 
            clientId, clientNotFoundException.Message);
        throw clientNotFoundException;
    }
    #endregion

    #region Delete client
    logger.Information("Deleting client ID[{Id}]", client.id);
    ClientModel deletedClient = repository.DeleteClient(client);
    await repository.FlushChanges();
    #endregion
    
    logger.Information("Client ID[{Id}] deleted successfully", deletedClient.id);
    return deletedClient.id;
}
```
*./Controllers/ClientController.cs - Parte 5*

Para que os controladores possam ser usados nos endpoints vamos adicioná-los ao container de DI:

```csharp
builder.Services.AddScoped<UserController>();
builder.Services.AddScoped<ClientController>();
```
*./Program.cs*

# Implementar endpoints

Temos tudo que precisamos para finalmente implementar os nossos endpoints, começaremos implementando os endpoints do usuário (`user/`):

```csharp
group.MapPost("signin", 
     async ([FromServices] UserController controller, 
         [FromBody] UserSigninRequestModel signinRequest) =>
{
    string newUserJwt;
    try
    {
        newUserJwt = await controller.SigninUser(signinRequest);
    }
    catch(InvalidRequestInfoException e)
    {
        return Results.BadRequest(e.Message);
    }
    catch(UserAllreadyRegisteredException e)
    {
        return Results.Conflict(e.Message);
    }
    
    return Results.Ok(newUserJwt);
});
```
*./Endpoints/UserEndpoints.cs*

Podemos usar os atributos `FromServices`, `FromBody`, `FromQuery` entre outros para definir de onde determinado parâmetro está vindo, mas isso não é obrigatório já que o [ASP.NET](http://ASP.NET) consegue identificar os parâmetros de acordo. Caso esteja se perguntando como vamos receber este objeto `UserSigninRequestModel` do corpo sendo que o passaremos ele como JSON, e isso é uma das coisas mais legais do ASP.NET, ele faz serialização e deserialização de JSONs com maestria, da mesma forma se retornássemos um objeto, ele iria serializar e retornar para o cliente como JSON, se não ficou muito claro, mais a frente iremos fazer uma rápida requisição para um dos nossos endpoints que possa ajudar a entender melhor como isso acontece.

Veja que estamos tradando as exceções que jogamos do método `SigninUser` e retornando o `Results` baseado na exceção que é jogada, dessa forma fica bem mais fácil de controlar o que nossa API retorna para o cliente. Da mesma forma vamos implementar o endpoint `user/login`:

```csharp
group.MapPost("login", 
     async ([FromServices] UserController controller,
     [FromBody] UserLoginRequestModel loginRequest) =>
{
    string userJwt;
    try
    {
        userJwt = await controller.LoginUser(loginRequest);
    }
    catch(Exception e) when (e is InvalidRequestInfoException or 
                                 UserNotFoundException or
                                 WrongUserPasswordException)
    {
        return Results.BadRequest(e.Message);
    }
    
    return Results.Ok(userJwt);
});
```
*./Endpoints/UserEndpoints.cs*

Para garantir que tudo até aqui está funcionando, vamos rodar nossa API e fazer uma requisição `POST` para o endpoint `user/signin`:

```POST``` [http://localhost:5111/user/signin](http://localhost:5111/user/signin):

```json
{
    "username": "test",
    "email": "test@test.com",
    "password": "test123"
}
```
*Corpo da requisição*

```json
"eyJhbGciOiJodHRwOi8vd3d3LnczLm9yZy8yMDAxLzA0L3htbGRzaWctbW9yZSNobWFjLXNoYTI1NiIsInR5cCI6IkpXVCJ9.eyJJRCI6IjIiLCJpc3MiOiJBbGxJbk9uZUFzcE5ldCIsImlhdCI6MTY4Mjc3NTkxMiwiZXhwIjoxNjgyNzc5NTEyLCJuYmYiOjE2ODI3NzU5MTJ9.cF_A0uMbwe0AL4EPWLZrKQacjc_Al6DL6ta9Cokny-E"
```
*Status: 201 Created*

Sim! as validações foram feitas, o usuário cadastrado e o JWT criado e retornado. Veja as informações do JWT no site oficial do JWT: [JSON Web Tokens - jwt.io](https://jwt.io/)

Caso fizermos uma requisição faltando algum dos parâmetros no corpo da requisição ou que infrinja alguma de nossas validações um erro será jogado.

```POST``` [http://localhost:5111/user/signin](http://localhost:5111/user/signin):

```json
{
    "username": "test",
    "email": "test",
    "password": "test123"
}
```
*Corpo da requisição - Com o ```email``` errado/invalido.*

```json
"Invalid request parameters: 'email' é um endereço de email inválido."
```
*Status: 400 Bad Request*

Para quem lembra, também verificamos se o usuário já estaria cadastrado, mas a verificação dos campos é feita primeiro e caso tenha algo errado o erro será jogado antes da verificação de usuário existente aconteça.

Agora que sabemos que tudo está funcionando com a autenticação, vamos implementar os endpoints do cliente, começando pelo `GET client/`.

Este endpoint retornará uma lista dos clientes cadastrados por um usuário, e aqui que usaremos as *Claims* que recebemos no JWT.

```csharp
group.MapGet("/", 
    async (HttpContext context, 
        [FromServices] ClientController controller) =>
{
    int userClaimId = int.Parse(context.User.Claims // 1
        .First(claim => claim.Type == JwtConsts.CLAIM_ID).Value);
    
    IReadOnlyList<ClientReadModel> userClients;
    try
    {
        userClients = await controller.GetUserClients(userClaimId);
    }
    catch(UserNotFoundException e)
    {
        return Results.BadRequest(e.Message);
    }
    
    return Results.Ok(userClients);
});
```
*./Endpoints/ClientEndpoints.cs*

1. Usamos `HttpContext` para regatar a lista de *Claims* do usuário autenticado atualmente, mas selecionamos apenas a *Claim* onde o `Type` é “ID”, em seguida convertê-lo para `int`, já que `GetUserClients` recebe um `int`.

`POST client/`:

```csharp
group.MapPost("/", 
    async (HttpContext context,
        [FromServices] ClientController controller,
        [FromBody] ClientRegisterRequestModel registerRequest) =>
{
    int userClaimId = int.Parse(context.User.Claims
        .First(claim => claim.Type == JwtConsts.CLAIM_ID).Value);
    
    int newClientId;
    try
    {
        newClientId = await controller.RegisterClient(registerRequest, userClaimId);
    }
    catch(Exception e) when (e is InvalidRequestInfoException or 
                                 UserNotFoundException)
    {
        return Results.BadRequest(e.Message);
    }
    
    return Results.Created($"client/{newClientId}", newClientId);
});
```
*./Endpoints/ClientEndpoints.cs*

`PUT client/{int:id}`:

```csharp
group.MapPut("{id:int}", 
    async (HttpContext context, 
        [FromRoute] int id,
        [FromServices] ClientController controller,
        [FromBody] ClientUpdateRequestModel updateRequest) =>
{
    int userClaimId = int.Parse(context.User.Claims
        .First(claim => claim.Type == JwtConsts.CLAIM_ID).Value);
    
    int updatedClientId;
    try
    {
        updatedClientId = await controller.UpdateClient(updateRequest, userClaimId);
    }
    catch(Exception e) when (e is InvalidRequestInfoException or 
                                 ClientNotFoundException)
    {
        return Results.BadRequest(e.Message);
    }
    
    return Results.Ok(updatedClientId);
});
```
*./Endpoints/ClientEndpoints.cs*

`DELETE client/{int:id}`

```csharp
group.MapDelete("{id:int}", 
        async (HttpContext context,
    [FromRoute] int id,
    [FromServices] ClientController controller) =>
{
    int deletedClientId;
    try
    {
        deletedClientId = await controller.DeleteClient(id);
    }
    catch(ClientNotFoundException e)
    {
        return Results.BadRequest(e.Message);
    }
    
    return Results.Ok(deletedClientId);
});
```
*./Endpoints/ClientEndpoints.cs*

Veja que todos os endpoints seguem um mesmo fluxo: Inicialização, Execução, Tratamento e Retorno. Agora que temos todos os endpoints do cliente implementados, vamos fazer uma requisição para cada um deles.

```GET``` [http://localhost:5111/client/](http://localhost:5111/client/):

```json

```
*Status: 401 Unauthorized*

Não recebemos nada como resposta e o status da requisição foi *401 Unauthorized*, isso quer dizer que nossa autenticação está funcionando, então como nos autenticamos? Faça login ou signin nos seus respectivos endpoints e lhe será gerado o JWT daí simplesmente passe ele no cabeçalho de cada requisição para `client/` com a chave `Authorization`, que é onde estamos tentando requisitar. Gerei um JWT com as mesmas informações que usamos para testar os endpoints `user/` e partir de agora irei passar em todas as requisições para `client/`, então mantenha isso em mente, pois não irei mostrar o cabeçalho de cada requisição que estou fazendo.

```
{ Authorization: "Bearer eyJhbGciOiJodHRwOi8vd3d3LnczLm9yZy8yMDAxLzA0L3htbGRzaWctbW9yZSNobWFjLXNoYTI1NiIsInR5cCI6IkpXVCJ9.eyJJRCI6IjEiLCJpc3MiOiJBbGxJbk9uZUFzcE5ldCIsImlhdCI6MTY4Mjg1Njc1OSwiZXhwIjoxNjgyODYwMzU5LCJuYmYiOjE2ODI4NTY3NTl9.pPlKXtl4We3nNLYSE5sK38C6nYO2Bbmgyu4wHqxjeWU" }
```
*Cabeçalho das requisições para client/.*

Note que antes do token temos que especificar que o token é *Bearer* que significa que foi gerado pelo servidor, e separando ele coloque espaço depois o token.

Antes de requisitar os clientes do usuário, vamos primeiro cadastrar um:

```POST``` [http://localhost:5111/client/](http://localhost:5111/client/):

```json
{
    "username": "Nome cliente"
}
```
*Corpo da requisição*

```
1
```
*Status: 201 Created*

Este número significa o ID do cliente cadastrado.

```GET``` [http://localhost:5111/client/](http://localhost:5111/client/):

```json
[
    {
        "id": 1,
        "username": "Nome cliente",
        "createdBy": {
            "username": "test",
            "email": "test@test.com"
        }
    }
]
```
*Status: 200 OK*

Agora vamos apagar o cliente que cadastramos e requisitar os clientes novamente.

```DELETE``` [http://localhost:5111/client/1](http://localhost:5111/client/1):

```
1
```
*Status: 200 OK*

`GET [http://localhost:5111/client/](http://localhost:5111/client/)`:

```json
[]
```
*Status: 200 OK*

Agora vamos cadastrar um novo usuário e alterar o `username` dele.

```POST``` [http://localhost:5111/client/](http://localhost:5111/client/):

```
2
```
*Status: 201 Created*

```PUT``` [http://localhost:5111/client/2](http://localhost:5111/client/2):

```json
{
    "username": "Nome novo"
}
```
*Corpo da requisição*

```json
2
```
*Status: 200 OK*

```GET``` [http://localhost:5111/client/](http://localhost:5111/client/):

```json
[
    {
        "id": 2,
        "username": "Nome novo",
        "createdBy": {
            "username": "test",
            "email": "test@test.com"
        }
    }
]
```
*Status: 200 OK*

Um dos requisitos que tínhamos era que um usuário só conseguiria resgatar um cliente cadastrado por ele no endpoint `GET client/`, então vamos criar outro usuário e requisitar os clientes cadastrado por ele.

```POST``` [http://localhost:5111/user/signin](http://localhost:5111/user/signin):

```json
{
    "username": "luanroger",
    "email": "luan.roger.2003@gmail.com",
    "password": "senha123kk"
}
```
*Corpo da requisição*

```json
"eyJhbGciOiJodHRwOi8vd3d3LnczLm9yZy8yMDAxLzA0L3htbGRzaWctbW9yZSNobWFjLXNoYTI1NiIsInR5cCI6IkpXVCJ9.eyJJRCI6IjIiLCJpc3MiOiJBbGxJbk9uZUFzcE5ldCIsImlhdCI6MTY4Mjg2MDExMCwiZXhwIjoxNjgyODYzNzEwLCJuYmYiOjE2ODI4NjAxMTB9.-W97aCjqUedq0DyFZQtlPcv8r2JYXCeigPM8HGN3gCQ"
```
*Status: 201 Created*

```GET``` [http://localhost:5111/client/](http://localhost:5111/client/):

```
{ Authorization: "Bearer eyJhbGciOiJodHRwOi8vd3d3LnczLm9yZy8yMDAxLzA0L3htbGRzaWctbW9yZSNobWFjLXNoYTI1NiIsInR5cCI6IkpXVCJ9.eyJJRCI6IjIiLCJpc3MiOiJBbGxJbk9uZUFzcE5ldCIsImlhdCI6MTY4Mjg2MDExMCwiZXhwIjoxNjgyODYzNzEwLCJuYmYiOjE2ODI4NjAxMTB9.-W97aCjqUedq0DyFZQtlPcv8r2JYXCeigPM8HGN3gCQ" }
```
*Cabeçalho da requisição*

```json
[]
```
*Status: 200 OK*

Sucesso! Não recebemos os clientes cadastrados por outro usuário, lembre-se que mesmo que os usuários não consigam ver os clientes cadastrado uns pelos outros não quer dizer que os usuários não consigam atualizar ou deletar clientes cadastrados por outros usuários, pois não verificamos isso nos controladores, fica como atividade extra para fazer 💪. Mas não pense que acabou, falta ainda um último detalhe para deixar a API no jeito.

# Iniciar e configurar o Swagger

OpenAPI é uma especificação que descreve APIs REST para que se possa entender facilmente os a estrutura da API. No geral, o Swagger é um conjunto de ferramentas que ajuda na documentação e desenvolvimento de APIs REST, tendo como base as especificações da OpenAPI, dessas ferramentas a que vamos utilizar é o Swagger UI, que mostra de forma visual, os endpoints, esquemas, grupos e mais da nossa API, além de podermos fazer requisições HTTP na própria UI.

Para isso precisamos adicionar uma biblioteca externa, que vale ressaltar, não é a única pra trabalhar com as ferramentas do Swagger, ela se chama `Swashbuckle.AspNetCore`, para adicional execute:

```powershell
dotnet add package Swashbuckle.AspNetCore
```

Da mesma forma que fizermos na autenticação e autorização, primeiro devemos adicionar o Swagger Gen ao container de DI para depois usá-lo, ele é responsável por gerar a documentação da nossa API, nele podemos configurar alguns parâmetros.

```csharp
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new()
    {
        Title = "AllInOneAspNet",
        Version = "v1",
        Description = "Web API example with ASP.NET Core"
    });
});
```
*./Program.cs*

Note que antes de configurar o Swagger Gen, adicionamos `AddEndpointsApiExplorer`, isso é para que o Swagger reconheça os endpoints da nossa API. Depois estamos configurando como nossa documentação será gerada com o método `SwaggerDoc`, definindo um nome do nosso documento para “v1” juntamente com alguns metadados.

Para usarmos o Swagger, devemos cadastrar o middleware dele no pipeline de execução da API:

```csharp
app.UseSwagger();
app.UseSwaggerUI();
```
*./Program.cs*

Cadastramos tanto o Swagger quanto o SwaggerUI, pois um irá gerar o documento `.json` que especifica nossa API de acordo com os parâmetros que configuramos anteriormente, e o outro irá ser usado para interpretar este documento e expor um URI para que possamos vê-lo de forma interativa.

E por incrível que pareça, está tudo pronto. Para acessar a documentação acesse [`http://localhost:5111/swagger`](http://localhost:5111/swagger).

![swaggerUI_end.png](https://raw.githubusercontent.com/LuanRoger/AllInOneAspNet/main/images/swaggerUI_end.png)

Todos os nossos endpoints estão sendo reconhecidos e mais abaixo vemos também os esquemas que podem receber. Como dito anteriormente, você também pode fazer requisições usando esta interface, escolhendo o endpoint e clicando em “Try it out”.

Se você está acompanhando, deve ter lembrado que algum de nossos endpoints precisam que estejamos autenticados para serem executados, mas isso não é problema, já que podemos configurar um esquema de autenticação para que posamos fazer requisições pelo SwaggerUI sem problema, para isso vamos configurar como que o Swagger deve tratar nossa autenticação. Voltando para `AddSwaggerGen`:

```csharp
builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new()
    {
        Title = "AllInOneAspNet",
        Version = "v1",
        Description = "Web API example with ASP.NET Core"
    });
    options.AddSecurityDefinition("Bearer", new() // Adicionado
    {
        Name = "Authorization",
        Type = SecuritySchemeType.Http, 
        Scheme = "Bearer",
        BearerFormat = "JWT",
        In = ParameterLocation.Header,
        Description = "JWT Authorization header using the Bearer scheme."
    });
    options.AddSecurityRequirement(new() // Adicionado
    {
        {
            new()
            {
                Reference = new()
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "Bearer"
                }
            },
            Array.Empty<string>()
        }
    });
});
```
*./Program.cs*

Veja que adicionamos duas outras definições as opções de geração: `AddSecurityDefinition` e `AddSecurityRequirement`, a primeira qual esquema iremos utilizar, onde será colocado o token que vamos passar (`In`), a chave do cabeçalho (`Name`), Tipo do esquema de segurança (`Type`) e mais. Em `AddSecurityRequirement` definimos requerimentos de segurança global, ou seja, todos os endpoints irão receber o token se estivermos autenticados, mas isso não é problema, pois mesmo que o token seja passado para um endpoint que não precise ele simplesmente irá ignorar.

Quando executarmos a API e acessar o SwaggerUI, note que agora temos um botão no canto superior direito da nossa lista de endpoints, clicando nele, podemos especificar o JWT que os nossos endpoints vão receber onde especificamos, no nosso caso, no cabeçalho da requisição.

![swaggerUI_authorizeButton.png](https://raw.githubusercontent.com/LuanRoger/AllInOneAspNet/main/images/swaggerUI_authorizeButton.png)

# Considerações finais

Depois de tanto conteúdo chegamos ao fim, não pude cobrir tudo até porque é bastante coisa e provavelmente alguma informação pode ter passado batido.

Este projeto vai estar open-source no GitHub para quem quiser se localizar melhor pelas pastas ou arquivos, ver o resultado final etc. Junto com ele, no README, estará uma cópia deste artigo, sendo possível contribuir tanto para a API quanto para o artigo em si (caso tenha alguma sugestão ou encontrado algum problema).

Espero realmente que todos tenham conseguido entender ou até aprendido algo novo, caso tenha alguma dúvida comentem aqui mesmo ou na aba de Discussão no GitHub.

[![GitHubDiscussions](https://img.shields.io/badge/Discussions-%23121011.svg?style=for-the-badge&logo=github&logoColor=white)](https://github.com/LuanRoger/AllInOneAspNet/discussions/categories/q-a)

**Obrigado a todos que leram até aqui 💖**.

[![Github-sponsors](https://img.shields.io/badge/sponsor-30363D?style=for-the-badge&logo=GitHub-Sponsors&logoColor=#EA4AAA)](https://github.com/sponsors/LuanRoger)

### Keep in touch:
[![LinkedIn](https://img.shields.io/badge/linkedin-%230077B5.svg?style=for-the-badge&logo=linkedin&logoColor=white)](https://www.linkedin.com/in/luan-roger) [![GitHub](https://img.shields.io/badge/github-%23121011.svg?style=for-the-badge&logo=github&logoColor=white)](
https://github.com/LuanRoger)

*Peace*✌️

### Referencias

[REST – Wikipédia, a enciclopédia livre (wikipedia.org)](https://pt.wikipedia.org/wiki/REST)

[Métodos de requisição HTTP - HTTP | MDN (mozilla.oerg)](https://developer.mozilla.org/pt-BR/docs/Web/HTTP/Methods)

[O que é API REST? (redhat.com)](https://www.redhat.com/pt-br/topics/api/what-is-a-rest-api)

[Criar APIs Web com o ASP.NET Core | Microsoft Learn](https://learn.microsoft.com/pt-br/aspnet/core/web-api/?WT.mc_id=dotnet-35129-website&view=aspnetcore-7.0)

[Suporte em tempo real do ASP.NET com SignalR | .NET (microsoft.com)](https://dotnet.microsoft.com/pt-br/apps/aspnet/signalr)

[ASP.NET | Framework web de código aberto para o .NET (microsoft.com)](https://dotnet.microsoft.com/pt-br/apps/aspnet)

[WebApplication Class (Microsoft.AspNetCore.Builder) | Microsoft Learn](https://learn.microsoft.com/en-us/dotnet/api/Microsoft.AspNetCore.Builder.WebApplication?view=aspnetcore-7.0&viewFallbackFrom=net-7.0)

[EndpointRouteBuilderExtensions Class (Microsoft.AspNetCore.Builder) | Microsoft Learn](https://learn.microsoft.com/en-us/dotnet/api/microsoft.aspnetcore.builder.endpointroutebuilderextensions?view=aspnetcore-7.0)

[RouteGroupBuilder Class (Microsoft.AspNetCore.Routing) | Microsoft Learn](https://learn.microsoft.com/en-us/dotnet/api/Microsoft.AspNetCore.Routing.RouteGroupBuilder?view=aspnetcore-7.0&viewFallbackFrom=net-7.0)

[Object and Collection Initializers - C# Programming Guide | Microsoft Learn](https://learn.microsoft.com/en-us/dotnet/csharp/programming-guide/classes-and-structs/object-and-collection-initializers)

[Dependency injection - Wikipedia](https://en.wikipedia.org/wiki/Dependency_injection)

[Dependency injection - .NET | Microsoft Learn](https://learn.microsoft.com/en-us/dotnet/core/extensions/dependency-injection)

[Introdução – EF Core | Microsoft Learn](https://learn.microsoft.com/pt-br/ef/core/get-started/overview/first-app?tabs=netcore-cli)

[Attributes and reflection | Microsoft Learn](https://learn.microsoft.com/en-us/dotnet/csharp/advanced-topics/reflection-and-attributes/)

[DbContext Class (Microsoft.EntityFrameworkCore) | Microsoft Learn](https://learn.microsoft.com/en-us/dotnet/api/Microsoft.EntityFrameworkCore.DbContext?view=efcore-7.0&viewFallbackFrom=net-6.0)

[Entendendo o Repository Pattern. Hoje venho apresentar o Repository… | by Renicius Pagotto Fostaini | Medium](https://renicius-pagotto.medium.com/entendendo-o-repository-pattern-fcdd0c36b63b)

[SQLite connection strings - ConnectionStrings.com](https://www.connectionstrings.com/sqlite/)

[Designing the infrastructure persistence layer | Microsoft Learn](https://learn.microsoft.com/en-us/dotnet/architecture/microservices/microservice-ddd-cqrs-patterns/infrastructure-persistence-layer-design)

[Registro em log em C# - .NET | Microsoft Learn](https://learn.microsoft.com/pt-br/dotnet/core/extensions/logging?tabs=command-line)

[Serilog — simple .NET logging with fully-structured events](https://serilog.net/)

[FluentValidation — FluentValidation documentation](https://docs.fluentvalidation.net/en/latest/)

[Built-in Validators — FluentValidation documentation](https://docs.fluentvalidation.net/en/latest/built-in-validators.html)

[Middleware do ASP.NET Core | Microsoft Learn](https://learn.microsoft.com/pt-br/aspnet/core/fundamentals/middleware/?view=aspnetcore-7.0)

[JSON Web Tokens - jwt.io](https://jwt.io/)

[JSON Web Token Introduction - jwt.io](https://jwt.io/introduction)

[Home - OpenAPI Initiative (openapis.org)](https://www.openapis.org/)

[Documentação da API Web ASP.NET Core com o Swagger/OpenAPI | Microsoft Learn](https://learn.microsoft.com/pt-br/aspnet/core/tutorials/web-api-help-pages-using-swagger?view=aspnetcore-7.0)
