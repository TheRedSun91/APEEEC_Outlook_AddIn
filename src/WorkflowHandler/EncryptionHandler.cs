using APEEEC_Outlook_AddIn.src.Encryption;

namespace APEEEC_Outlook_AddIn.src.WorkflowHandler
{
    class EncryptionHandler
    {
        private Encrypter _encrypter;
        private Decrypter _decrypter;

        public EncryptionHandler()
        {
            _encrypter = new Encrypter();
            _decrypter = new Decrypter();
        }

        public Encrypter getEncrypter()
        {
            if (_encrypter != null)
            {
                return _encrypter;
            }
            return null;
        }

        public Decrypter getDecrypter()
        {
            if (_decrypter != null)
            {
                return _decrypter;
            }
            return null;
        }
    }
}
