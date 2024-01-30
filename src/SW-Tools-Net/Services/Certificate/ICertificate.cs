namespace SW.Tools.Services.Certificate;

internal interface ICertificate<T>
{
    T CreatePfx(byte[] publicCertificate, byte[] privateKey, string password);
}
