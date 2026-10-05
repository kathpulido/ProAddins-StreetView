# Pro Addins

Una coleccion de botones y herramientas para ArcGIS Pro, empaquetados en un solo add-in.

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
