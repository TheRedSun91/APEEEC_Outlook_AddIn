using APEEEC_Outlook_AddIn.src.Interfaces;
using GpgApi;
using System;
using System.Collections.Generic;

namespace APEEEC_Outlook_AddIn.src.Encryption
{
    class Encrypter : IEncrypter
    {
        private GpgEncrypt gpgEncrypter;

        public GpgInterfaceResult EncryptFile(String fileName, String encryptedFileName, Boolean armored, Boolean hideUserIds, KeyId signatureKeyId, IEnumerable<KeyId> recipients, CipherAlgorithm cipherAlgorithm)
        {
            gpgEncrypter = new GpgEncrypt(fileName, encryptedFileName, armored, hideUserIds, signatureKeyId, recipients, cipherAlgorithm);
            return gpgEncrypter.Execute();
        }
    }
}
