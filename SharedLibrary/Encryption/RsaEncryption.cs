using System.Security.Cryptography;
using System.Text;

namespace SharedLibrary.Encryption;

public static class RsaEncryption
{
    private static  readonly RSA _rsa;

    static RsaEncryption()
    {
        _rsa = RSA.Create(2048);
    }

    public static byte[] Encrypt(byte[] dataToEncrypt)
    {
        return _rsa.Encrypt(dataToEncrypt, RSAEncryptionPadding.OaepSHA256);
    }

    public static byte[] Decrypt(byte[] dataToDecrypt)
    {
        return _rsa.Decrypt(dataToDecrypt, RSAEncryptionPadding.OaepSHA256);
    }
    public static void ImportEncryptedPrivateKey(byte[] encryptedKey, string password)
    {
        _rsa.ImportEncryptedPkcs8PrivateKey(Encoding.UTF8.GetBytes(password), encryptedKey, out _);
    }

    public static byte[] ExportPublicKey()
    {
        return _rsa.ExportRSAPublicKey();
    }

    public  static void ImportPublicKey(byte[] publicKey)
    {
        _rsa.ImportRSAPublicKey(publicKey, out _);
    }
}
