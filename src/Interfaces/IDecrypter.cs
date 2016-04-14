using GpgApi;
using System;

namespace APEEEC_Outlook_AddIn.src.Interfaces
{
    interface IDecrypter
    {
        GpgInterfaceResult DecryptFile(String encryptedFileName, String decryptedFileName);
    }
}
