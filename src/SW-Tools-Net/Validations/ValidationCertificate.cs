namespace SW.Tools.Validations;

internal partial class ValidationCertificate
{
    internal static void ValidateParams(byte[] publicCertificate, byte[] privateKey, string password)
    {
        if (publicCertificate == null || privateKey == null || String.IsNullOrEmpty(password))
            throw new Exception("Los parámetros Certificado, llave privada y contraseña son requeridos.");
    }
    internal static void ValidateParamsPfx(byte[] pfxBytes, string password)
    {
        if (pfxBytes == null || pfxBytes.Length == 0 || string.IsNullOrEmpty(password))
            throw new Exception("Los parámetros PFX y contraseña son requeridos."); 
    }
}
