using GpgApi;
using System;
using System.Collections.Generic;

namespace APEEEC_Outlook_AddIn.src.Interfaces
{
    interface IEncrypter
    {
        GpgInterfaceResult EncryptFile(String fileName, String encryptedFileName, Boolean armored, Boolean hideUserIds, KeyId signatureKeyId, IEnumerable<KeyId> recipients, CipherAlgorithm cipherAlgorithm);
    }
}
