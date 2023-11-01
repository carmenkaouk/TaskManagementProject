using System.Security.Cryptography;
using System.Text;

namespace SharedLibrary.Encryption;

public class RsaEncryption
{
    private readonly RSA _rsa;

    public RsaEncryption()
    {
        _rsa = RSA.Create(2048);
    }

    public byte[] Encrypt(byte[] dataToEncrypt)
    {
        return _rsa.Encrypt(dataToEncrypt, RSAEncryptionPadding.OaepSHA256);
    }

    public byte[] Decrypt(byte[] dataToDecrypt)
    {
        return _rsa.Decrypt(dataToDecrypt, RSAEncryptionPadding.OaepSHA256);
    }
    public void ImportEncryptedPrivateKey(byte[] encryptedKey, string password)
    {
        _rsa.ImportEncryptedPkcs8PrivateKey(Encoding.UTF8.GetBytes(password), encryptedKey, out _);
    }

    public byte[] ExportPublicKey()
    {
        return _rsa.ExportRSAPublicKey();
    }

    public void ImportPublicKey(byte[] publicKey)
    {
        _rsa.ImportRSAPublicKey(publicKey, out _);
    }
}
