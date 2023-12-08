namespace SW.Tools.Validations;

internal partial class Validation
{
    internal static void ValidateSignParams(string xml, byte[] pfx, string password)
    {
        if (String.IsNullOrEmpty(xml) || pfx == null || String.IsNullOrEmpty(password))
            throw new Exception("Los parámetros xml, pfx y password son requeridos.");
    }
    internal static void ValidateInvoice(string tag, bool isRetention = false)
    {
        if (!isRetention && (tag != "cfdi:Comprobante" && tag != "Cancelacion" && tag != "SolicitudAceptacionRechazo"))
        {
            throw new ArgumentException("El XML no es un comprobante CFDI, Cancelacion o AceptacionRechazo válido.");
        }
        else if (isRetention && tag != "retenciones:Retenciones")
        {
            throw new ArgumentException("El XML no es una retención válida.");
        }
    }
    internal static void ValidateInvoiceVersion(string tag, string version, bool isRetention) 
    {
        if (!isRetention && (tag == "cfdi:Comprobante" && version != "4.0") 
            || (isRetention && (tag == "retenciones:Retenciones" && version != "2.0")))
        {
            throw new ArgumentException("La versión del comprobante no es un CFDI 4.0 o Retención 2.0.");
        }
    }
}
