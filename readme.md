# API con C#

>NOTA: para este proyecto vamos a usar postman, VSCode y SQL Server

Antes de comenzar hablemos de un tema en particular **Rest** (Representational State Trasfer).

Rest es un estilo de arquitectura para el diseño de paginas web ¿que significa esto? significa que tiene toda una base teorica la cual tenemos que aplicar al momento de crear nuestras aplicaciones.
Una de sus reglas es que debemos unos verbos al momento de interacutar con nuestras Apis conocido como verbos http o http verbs

- GET: Es utilizado únicamente para consultar información al servidor, muy parecidos a realizar un SELECT a la base de datos. No soporta el envío del payload
- POST: Es utilizado para solicitar la creación de un nuevo registro, es decir, algo que no existía previamente, es decir, es equivalente a realizar un INSERT en la base de datos. Soporta el envío del payload.
- PUT: Se utiliza para actualizar por completo un registro existente, es decir, es parecido a realizar un UPDATE a la base de datos. Soporta el envío del payload.
- PATCH: Este método es similar al método PUT, pues permite actualizar un registro existente, sin embargo, este se utiliza cuando actualizar solo un fragmento del registro y no en su totalidad, es equivalente a realizar un UPDATE a la base de datos. Soporta el envío del payload
- DELETE: Este método se utiliza para eliminar un registro existente, es similar a DELETE a la base de datos. No soporta el envío del payload.
- HEAD: Este método se utilizar para obtener información sobre un determinado recurso sin retornar el registro. Este método se utiliza a menudo para probar la validez de los enlaces de hipertexto, la accesibilidad y las modificaciones recientes.

Otro de los mandamientos de Rest es que debemos manejar una URL por cada uno de nuestro recurcos dentro de nuestra base de datos.

Esta url nos va a permitir realizar las acciones dentro de nuestra BD, por lo tanto no puede ser dinamica y debe indicarme cual es el recurso al que estamos apuntando por ejemplo ".../api/user/1".

Y por ultimo hablemos de las respuestas HTTP. Estas nos permite decirle al usuario que es lo que paso con nuestras consultas:
Respuestas HTTP

- Informativas 100-199
- Satisfactorias 200-299
- Redirecciones 300-399
- Error Cliente 400-499
- Error Server 500-599

Con esto en mente creemos nuestra API con .net

    dotnet new webapi

Al crear nuestra API vamos a ir a nuestro controlador y vamos a hacer algunas modificaciones simples para entender lo que estabamos hablando sobre los mandamientos Rest.

    using Microsoft.AspNetCore.Mvc;

    namespace API.Controllers;

    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {
        private static readonly string[] Summaries = new[]
        {
            "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        };

        private readonly ILogger<WeatherForecastController> _logger;
        List<WeatherForecast> listWF = new List<WeatherForecast>();

        public WeatherForecastController(ILogger<WeatherForecastController> logger)
        {
            _logger = logger;
            if (listWF == null || !listWF.Any())
            {
                listWF = Enumerable.Range(1, 5).Select(index => new WeatherForecast
                {
                    Date = DateTime.Now.AddDays(index),
                    TemperatureC = Random.Shared.Next(-20, 55),
                    Summary = Summaries[Random.Shared.Next(Summaries.Length)]
                })
                .ToList();
            }
        }

        [HttpGet(Name = "GetWeatherForecast")]
        public IEnumerable<WeatherForecast> Get()
        {
            return listWF;
        }

        [HttpPost]
        public IActionResult Post(WeatherForecast watherf)
        {
            listWF.Add(watherf);
            return Ok();
        }


        [HttpDelete("{index}")]
        public IActionResult Delete(int index)
        {
            listWF.RemoveAt(index);
            return Ok();
        }


    }

Como puedes ver hicimos uso de los atributos para verbos http. en el cual despues de extraer la logica que obtenia la lista con los elementos en un array. lo instanciamos en constructor para que se obtubiera cuando iniciara la aplicacion.

Luego simplemente devolvismo la lista, de esta manera presentamos el codigo de una manera un poco más amigable. Despues creamos dos metodos mas, para agregar un nuevo elemento y otro para eliminar.

Con esto podemos abrir Postman y ver el resultado que nos trae.

Ahora supongamos que queremos cambiar la ruta de nuestras api. Basta con ir a al atributo [Route("[controller]")] y cambiarlo por [Route("api/[controller]") con esto nuestra ruta cambia de GetWeatherForecast "{"localhost}/weatherforecast" a "{localhost}/api/weatherforecast"

Esto nos permitiria tener multiples rutas para cada accion, por ejemplo si agregamos en el metodo Get.

    [Route("Get/weatherforecast")]

Ahora tendremos dos ruta que nos devuelven valores.
Tambien podemos hacer rutas de formas dinamica de la misma forma que el controller, pero usando la palabra reservada Action

    [Route("[action]")]

Para comprobarlo cambiemos el nombre de nuestro metodo Get. Sabiendo lo basico de lo basico ahora podemos entrar un poco mas profundo en este tema.

## ¿Qué son los middlewares?

Es un software que se ensambla en una canalización de una aplicación para controlar las solicitudes y las respuestas. Cada componente puede hacer lo siguiente:

- Elegir si se pasa la solicitud al siguiente componente de la canalización.
- Realizar trabajos antes y después del siguiente componente de la canalización.

Los delegados de solicitudes se usan para crear la canalización de solicitudes. Estos también controlan las solicitudes HTTP. para mas informacion [aqui](https://learn.microsoft.com/es-es/aspnet/core/fundamentals/middleware/?view=aspnetcore-6.0)

 ![](contents/img/Middlewares.png)

EStos middlewares se encuentran en nuestro archivo program.cs o en versiones anteriores al 6.0 startup.cs

Este orden es importante porque algunos de ellos tienen una dependencia del anterior. por ejemplo agreguemos un Middlewares de una pagina de vienvenida.

    app.UseWelcomePage();

El codigo tiene que estar despues de la autorizacion (app.UseAuthorization()) y antes del Endpoin (app.MapControllers())

Ahora tal vez vea mejor lo que es un Middlewares, son simplemente intrucciones de codigo que se iran agregando una tras otro durante el ciclo de vida de nuestra aplicacion.

Creemos nuestro propio Middlewares para entender un poco mas el tema. Empecemos con agregar una nueva carpeta para alojar el archivo. 

