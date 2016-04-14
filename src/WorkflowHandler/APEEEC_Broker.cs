using APEEEC_Outlook_AddIn.src.Encryption;

namespace APEEEC_Outlook_AddIn.src.WorkflowHandler
{

    class APEEEC_Broker
    {
        private EncryptionHandler encryptionHandler;
        private SignatureHandler signatureHandler;
        private KeyManager keyManager;

        private static APEEEC_Broker _apeeecBroker = null;

        private APEEEC_Broker ()
        {
            encryptionHandler = new EncryptionHandler();
            signatureHandler = new SignatureHandler();
            keyManager = new KeyManager();
        }

        public static APEEEC_Broker GetSingletonBroker()
        {
            if (_apeeecBroker == null)
            {
                _apeeecBroker = new APEEEC_Broker();
            }
            return _apeeecBroker;
        }

        public EncryptionHandler GetEncryptionHandler()
        {
            if (encryptionHandler != null)
            {
                return encryptionHandler;
            }
            return null;
        }

        public SignatureHandler GetSignatureHandler()
        {
            if (signatureHandler != null)
            {
                return signatureHandler;
            }
            return null;
        }

        public KeyManager GetKeyManager()
        {
            if (keyManager != null)
            {
                return keyManager;
            }
            return null;
        }
    }
}
