using GpgApi;
using System;

namespace APEEEC_Outlook_AddIn.src.Signature
{
    class OpenPGPSignature
    {
        private GpgSign gpgSignature;
        public GpgInterfaceResult sign(KeyId signatureKeyId, String fileName, String signedFileName, Boolean armored)
        {
            gpgSignature = new GpgSign(signatureKeyId, fileName, signedFileName, armored);
            return gpgSignature.Execute();
            ///todo implement
        }
    }
}
