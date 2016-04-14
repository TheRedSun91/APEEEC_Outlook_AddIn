using APEEEC_Outlook_AddIn.src.Interfaces;
using GpgApi;
using System;

namespace APEEEC_Outlook_AddIn.src.Encryption
{
    class Decrypter : IDecrypter
    {
        private GpgDecrypt _gpgDecrypter;
        //private static String _tempPath = System.IO.Path.GetTempPath();

        public GpgInterfaceResult DecryptFile(String encryptedFileName, String decryptedFileName)
        {
            _gpgDecrypter = new GpgDecrypt(encryptedFileName, decryptedFileName);
            return _gpgDecrypter.Execute();
        }
    }
}
