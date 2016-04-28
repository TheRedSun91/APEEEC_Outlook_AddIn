using APEEEC_Outlook_AddIn.src.Signature;

namespace APEEEC_Outlook_AddIn.src.WorkflowHandler
{
    class SignatureHandler
    {
        private OpenPGPSignature _signature;

        public SignatureHandler()
        {
            _signature = new OpenPGPSignature();
        }

        public OpenPGPSignature getSignature()
        {
            if (_signature != null)
            {
                return _signature;
            }
            return null;
        }
    }
}
