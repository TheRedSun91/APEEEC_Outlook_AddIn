using GpgApi;
using System;

namespace APEEEC_Outlook_AddIn.src.Signature
{
    class OpenPGPSignature
    {
        private GpgSign gpgSignature;
        private GpgVerifySignature gpgVerifySignature;
        public GpgInterfaceResult Sign(KeyId signatureKeyId, String fileName, String signedFileName, Boolean armored)
        {
            gpgSignature = new GpgSign(signatureKeyId, fileName, signedFileName, armored);
            return gpgSignature.Execute();
        }

        public GpgInterfaceResult VerifySignature(String fileName)
        {
            gpgVerifySignature = new GpgVerifySignature(fileName);
            return gpgVerifySignature.Execute();
        }
    }
}
