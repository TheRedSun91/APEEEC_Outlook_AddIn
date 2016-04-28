using APEEEC_Outlook_AddIn.src.Encryption;

namespace APEEEC_Outlook_AddIn.src.WorkflowHandler
{

    class APEEEC_Broker
    {
        private EncryptionHandler _encryptionHandler;
        private SignatureHandler _signatureHandler;
        private KeyManager _keyManager;

        private static APEEEC_Broker _apeeecBroker = null;

        private APEEEC_Broker ()
        {
            _encryptionHandler = new EncryptionHandler();
            _signatureHandler = new SignatureHandler();
            _keyManager = new KeyManager();
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
            if (_encryptionHandler != null)
            {
                return _encryptionHandler;
            }
            return null;
        }

        public SignatureHandler GetSignatureHandler()
        {
            if (_signatureHandler != null)
            {
                return _signatureHandler;
            }
            return null;
        }

        public KeyManager GetKeyManager()
        {
            if (_keyManager != null)
            {
                return _keyManager;
            }
            return null;
        }
    }
}
