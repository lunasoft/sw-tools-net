namespace SW.Tools.Validations;

internal partial class ValidationCertificate
{
    internal static void ValidateParams(byte[] publicCertificate, byte[] privateKey, string password)
    {
        if (publicCertificate == null || privateKey == null || String.IsNullOrEmpty(password))
            throw new Exception("Los parámetros Certificado, llave privada y contraseña son requeridos.");
    }
}
