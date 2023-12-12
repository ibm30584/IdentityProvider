using System.Security.Cryptography.X509Certificates;

namespace IdentityProvider.Configuration;

public class IdentityServerConfig
{
    public string? CertificateThumbprint { get; set; }
}

public static class IdentityServerConfigExtention
{
    public static X509Certificate2? GetSigningCertificate(this IdentityServerConfig identityServerConfig)
    {
        var certStore = new X509Store(StoreName.Root, StoreLocation.LocalMachine);
        certStore.Open(OpenFlags.ReadOnly);
        var certs = certStore.Certificates.Find(
            X509FindType.FindByThumbprint,
            identityServerConfig.CertificateThumbprint!,
            true);
        var cert = certs.FirstOrDefault();
        certStore.Close();
        return cert;
    }
}

