using System.Security.Cryptography.X509Certificates;

namespace IdentityProvider.Application.Models;

public class IdentityServerConfig
{
    public string ConnectionString { get; set; } = null!;
    public string CertificateThumbprint { get; set; } = null!;
    public X509Certificate2 Certificate { get; private set; } = null!;

    public static IdentityServerConfig Validate(IdentityServerConfig? identityServerConfig)
    {
        ArgumentNullException.ThrowIfNull(identityServerConfig);
        ArgumentException.ThrowIfNullOrWhiteSpace(identityServerConfig.ConnectionString);
        ArgumentException.ThrowIfNullOrWhiteSpace(identityServerConfig.CertificateThumbprint);
        var certificate = GetSigningCertificate(identityServerConfig.CertificateThumbprint);
        if (certificate == null)
            throw new InvalidOperationException("No certificate found for configured thumbprint");
        identityServerConfig.Certificate = certificate;
        return identityServerConfig!;
    }

    private static X509Certificate2 GetSigningCertificate(string certificateThumbprint)
    {
        var certStore = new X509Store(StoreName.Root, StoreLocation.LocalMachine);
        certStore.Open(OpenFlags.ReadOnly);
        var certs = certStore.Certificates.Find(
            X509FindType.FindByThumbprint,
            certificateThumbprint,
            true);
        var cert = certs.FirstOrDefault();
        certStore.Close();
        return cert;
    }
}

