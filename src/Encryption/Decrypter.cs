using APEEEC_Outlook_AddIn.src.Interfaces;
using GpgApi;
using System;

namespace APEEEC_Outlook_AddIn.src.Encryption
{
    class Decrypter : IDecrypter
    {
        private GpgDecrypt _gpgDecrypter;

        public GpgInterfaceResult DecryptFile(String encryptedFileName, String decryptedFileName)
        {
            _gpgDecrypter = new GpgDecrypt(encryptedFileName, decryptedFileName);
            return _gpgDecrypter.Execute();
        }
    }
}
