# Pro Addins

Una coleccion de botones y herramientas para ArcGIS Pro, empaquetados en un solo add-in.

## Instalacion

Requisitos: ArcGIS Pro 3.3 o superior (compilado y probado con la version 3.5.52636).

### Opcion 1: usar el archivo .esriAddinX ya compilado

1. Compila el proyecto siguiendo la Opcion 2 (abajo) o consigue el archivo `ProAddins.esriAddinX` ya compilado.
2. Haz doble clic sobre el archivo `.esriAddinX`. ArcGIS Pro abrira el instalador del add-in.
3. Sigue las instrucciones en pantalla para completar la instalacion.
4. Abre (o reinicia) ArcGIS Pro. La herramienta "Google Streetview" aparece en la pestana **Map**, dentro del grupo de navegacion. El boton "Panel Street View" permite volver a abrir el panel si lo cierras.

### Opcion 2: compilar desde el codigo fuente

1. Clona este repositorio.
2. Instala el [.NET 8 SDK](https://dotnet.microsoft.com/download) y asegurate de tener ArcGIS Pro (con su SDK para .NET) instalado en el equipo.
3. Abre `ProAddins.sln` en Visual Studio con la carga de trabajo del ArcGIS Pro SDK, y compila en modo `Release`. Visual Studio empaqueta e instala el add-in automaticamente.

   > Si compilas desde la linea de comandos con `dotnet build` en lugar de Visual Studio, el paso final de empaquetado de Esri (`Esri.ProApp.SDK.Desktop.targets`) puede fallar, porque usa tareas de MSBuild clasico que no son compatibles con el SDK de .NET multiplataforma (errores `MSB4801` / `MSB4036`). En ese caso:
   > 1. Compila igual con `dotnet build ProAddins.sln -c Release` (el .dll se genera correctamente, solo falla el empaquetado).
   > 2. Arma manualmente el `.esriAddinX` (es un archivo .zip): copia el contenido de `ProAddins\bin\Release\net8.0-windows10.0.17763.0\` dentro de una carpeta `Install\`, y copia `Config.daml`, `Images\` y `DarkImages\` en la raiz de esa misma carpeta temporal; luego comprime todo el contenido (no la carpeta) en un archivo `ProAddins.esriAddinX`.
   > 3. Instalalo de forma silenciosa con `RegisterAddIn.exe <ruta al .esriAddinX> /s`, ejecutado desde `C:\Program Files\ArcGIS\Pro\bin\`.

4. Reinicia ArcGIS Pro para ver los cambios.

## Street View

Haz clic en el mapa con la herramienta "Google Streetview" para abrir la imagen de Google Street View correspondiente a ese punto.

La vista de Street View se muestra directamente dentro de ArcGIS Pro, en un panel acoplable junto al mapa (sin salir a un navegador externo). El panel tambien muestra las coordenadas del punto consultado y un enlace al perfil de GitHub de la autora de esta version, debajo de la visualizacion.

## Botones de consulta de definicion (Definition Query Buttons)

Botones para establecer una expresion de definicion sobre una capa, a partir de las entidades seleccionadas. Es similar a "Crear capa a partir de la seleccion", con la diferencia de que la consulta de definicion se aplica sobre la capa seleccionada en lugar de crear una capa nueva.

## Boton de visor externo (External Viewer Button)

Un boton configurable que permite abrir una aplicacion externa usando una propiedad de ID y una URL.

Este boton debe habilitarse primero desde el cuadro de dialogo de Opciones de Pro, y tiene las siguientes configuraciones disponibles:

- **URL de la aplicacion**: la URL de la aplicacion externa. Debe incluir el texto `{0}`, que sera reemplazado por la propiedad ID de la entidad seleccionada.
- **Campo de ID de capa**: el nombre del campo a consultar en la entidad seleccionada.
- **Nombre de capa**: el nombre de la capa a consultar en el mapa.
