
using System.Security.Cryptography;

namespace SharedLibrary.Encryption;

public static class AesEncryption
{
    public static byte[] Encrypt(byte[] dataToEncrypt, byte[] key, byte[] iv)
    {
        using var aes = Aes.Create();
        aes.Key = key;
        return aes.EncryptCbc(dataToEncrypt, iv);
    }
    public static byte[] Decrypt(byte[] dataToDecrypt, byte[] key, byte[] iv)
    {
        using var aes = Aes.Create();
        aes.Key = key;
        return aes.DecryptCbc(dataToDecrypt, iv);

    }
    public static (byte[] key, byte[] IV) GenerateSymmetricKey()
    {
        byte[] key;
        byte[] iv;
        using (Aes aesAlg = Aes.Create())
        {
            aesAlg.KeySize = 256;
            aesAlg.GenerateKey();
            key = aesAlg.Key;
            iv = aesAlg.IV;
        }
        return (key, iv);
    }
}

