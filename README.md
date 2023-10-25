# SW Tools NET

<div style="text-align:center">
  <img src="https://camo.githubusercontent.com/e73b75666b69c6e362ccc4f684ba0f180bebd8971b07821eb76163d3b1e84ba3/68747470733a2f2f646b613537356f666d34616f302e636c6f756466726f6e742e6e65742f70616765732d7472616e73616374696f6e616c5f6c6f676f732f726574696e612f36383731322f53575f736d61727465722d536572766963696f735f7765622e706e67" alt="SW Sapien" width="200" height="60">
</div>

Librería de clases que proporciona herramientas y utilidades aplicables para la facturación electrónica.

## Contenido

- [Compatibilidad](#compatibilidad)
- [Caracteristicas](#características)
- [Instalacion](#instalación)
  - [Nuget Package](#nuget-package)
  - [Referencia](#referencia)
- [Uso](#uso)
  - [Sellar CFDI](#sellar-cfdi)
  - [Sellar Retencion](#sellar-retención)
- [Contribuir](#contribuir)
- [Creadores](#creadores)
- [Licencia](#licencia)

## Compatibilidad

Esta biblioteca es compatible con .NET 6.0 y .NET 7.0.

## Características

- Sellar CFDI: Función para sellar Comprobantes CFDI 4.0 y sus complementos.
- Sellar Retención: Función para sellar Comprobantes de Retenciones 2.0 y sus complementos.
- Firmar XML: Función para generar y agregar el signature a documentos XML.
- Crear PFX: Función para crear un certificado PFX.

## Instalación

### Nuget Package

Para utilizar esta librería en tu proyecto, puedes instalarla a través de NuGet Package Manager. Ejecuta el siguiente comando en la consola de NuGet:

```bash
Por el momento no se encuentra disponible esta opción.
```

### Referencia

También puedes agregar la librería manualmente como referencia en tu proyecto. Sigue las siguientes instrucciones:

**1. Descargar compilado DLL:**

- Ve a la sección [Releases](https://github.com/lunasoft/sw-tools-net/releases) de este repositorio.
- Selecciona la versión que deseas agregar, haz clic en la sección "Assets" y selecciona el .zip con el nombre sw-tools-net para descargar el compilado.

**2. Descomprimir el archivo ZIP:**

- Descomprime el archivo ZIP descargado en una ubicación de tu elección en tu sistema.

**3. Añadir Referencias a tu Proyecto:**

- Abre tu proyecto.

- Haz clic con el botón derecho en tu proyecto en el Explorador de Soluciones y selecciona "Agregar", se desplegará un menú y deberás seleccionar "Referencia De Proyecto".

- En la ventana que se abrió, haz clic en "Examinar" y navega a la ubicación donde descomprimiste la biblioteca. Dentro de la carpeta, busca el archivo DLL correspondiente y selecciona "Aceptar".

- Asegúrate de que la referencia se agregue correctamente a tu proyecto.

## Uso

El response de cada una de las funciones es un objeto de una clase heredada que siempre contendrá los siguientes miembros:

- Status: Valor _success_ o _error_, dependiendo del resultado de la operación.
- Message: Si Status igual a _error_, Message contendrá un valor con información del error.
- Data: Si Status igual a _success_, el valor será un objeto que contendrá miembros dependiendo de la función que se esté consumiendo.

```csharp
public class BaseResponse<T>
{
    public string Status { get; private set; }
    public string Message { get; private set; }
    public T Data { get; private set; }
}
```

### Sellar CFDI

```csharp
using SW.Tools.Services.Sign;

string cfdi = Resource.GetInvoice("cfdi40.xml");
byte[] pfx = Resource.GetPfx("h&e951128469.pfx");
string password = "12345678a";

Sign sign = new();
var result = sign.SignCfdi(cfdi, pfx, password);

var signedCfdi = result.Data.Xml;
```


### Sellar Retención

```csharp
using SW.Tools.Services.Sign;

string retention = Resource.GetInvoice("retention20.xml");
byte[] pfx = Resource.GetPfx("h&e951128469.pfx");
string password = "12345678a";

Sign sign = new();
var result = sign.SignRetention(retention, pfx, password);

var signedRetencion = result.Data.Xml;
```

## Contribuir

Si deseas contribuir, puedes hacerlo a través de pull requests y reportando el issue [aqui](https://github.com/lunasoft/sw-tools-net/issues).

Si requieres soporte, envíanos un correo a [soporte@sw.com.mx](mailto:soporte@sw.com.mx).

## Creadores

**Aeyrton Villalobos**

- <https://github.com/SwAeyrton>
- <https://www.linkedin.com/in/aeyrtonvs/>

## Licencia

Este proyecto se distribuye bajo los términos de la [Licencia Pública General de GNU, versión 3](https://www.gnu.org/licenses/gpl-3.0.html).

Para obtener una copia completa del texto de la licencia, consulte el archivo [LICENSE](LICENSE) incluido en este repositorio.
