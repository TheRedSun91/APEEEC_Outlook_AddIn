using APEEEC_Outlook_AddIn.src.Forms.KeyExchange;
using System;
using System.Drawing;
using System.IO;
using System.Reflection;
using System.Runtime.InteropServices;
using Office = Microsoft.Office.Core;
using Outlook = Microsoft.Office.Interop.Outlook;
using APEEEC_Outlook_AddIn.src.Encryption;
using APEEEC_Outlook_AddIn.src.WorkflowHandler;
using NLog;
using GpgApi;
using APEEEC_Outlook_AddIn.src.Forms;

namespace APEEEC_Outlook_AddIn
{
    [ComVisible(true)]
    public class Ribbon : Office.IRibbonExtensibility
    {
        private Office.IRibbonUI ribbon;
        public static bool encryptionWanted;
        private static Logger logger = LogManager.GetCurrentClassLogger();

        public Ribbon()
        {
            encryptionWanted = false;
        }

        #region IRibbonExtensibility-Member

        public string GetCustomUI(string ribbonID)
        {
            string ribbonXML = String.Empty;

            //to ensure that the custom ribbon is only displayed when creating a new message
            if (ribbonID == "Microsoft.Outlook.Mail.Compose")
            {
                ribbonXML = GetResourceText("APEEEC_Outlook_AddIn.ComposeMailRibbon.xml");
            }
            else if (ribbonID == "Microsoft.Outlook.Mail.Read")
            {
                ribbonXML = GetResourceText("APEEEC_Outlook_AddIn.ReadMailRibbon.xml");
            }

            return ribbonXML;
            
        }

        #endregion

        #region Menübandrückrufe
        public void OnEncryptEmailToggleButton(Office.IRibbonControl control, bool isPressed)
        {
            //if button is pressed (toggled) it needs to be defined here. should be accessible from ThisAddIn.cs
            encryptionWanted = isPressed;
        }

        public void OnClickImportCertificateButton(Office.IRibbonControl control)
        {
            String isPositiveResponse = "APEEEC - Positive Encrypted Communication Response";
            String isNegativeResponse = "APEEEC - Negative Encrypted Communication Response";
            String isEncryptionRequest = "APEEEC - Encrypted Communication Request";

            //get current active explorer and selected mail item
            if (Globals.ThisAddIn.Application.ActiveExplorer().Selection.Count > 0)
            {
                //access currently selected item
                Object selObject = Globals.ThisAddIn.Application.ActiveExplorer().Selection[1];
                if (selObject is Outlook.MailItem)
                {
                    logger.Info("Certification import started.");
                    KeyManager keyManager = APEEEC_Broker.GetSingletonBroker().GetKeyManager();
                    Outlook.MailItem currentMailItem = (selObject as Outlook.MailItem);
                    if (currentMailItem.Subject == isPositiveResponse)
                    {
                        logger.Info("Import Key from positive response.");
                        ImportKeyFromPositiveResponse(currentMailItem, keyManager);
                    }
                    else if (currentMailItem.Subject == isEncryptionRequest)
                    {
                        logger.Info("Import Key from encryption request.");
                        ImportKeyFromRequest(currentMailItem, keyManager);
                    }
                    else if (currentMailItem.Subject == isNegativeResponse)
                    {
                        logger.Info("Import from negative response --> not possible. User notified.");
                        UserDeniedCommunicationForm userDeniedCommunicationForm = new UserDeniedCommunicationForm();
                        userDeniedCommunicationForm.Show();
                    }
                    else
                    {
                        logger.Info("Import from external mail item not possible. User notified.");
                        NoAPEEECMailItemForm noAPEEECMailItemForm = new NoAPEEECMailItemForm();
                        noAPEEECMailItemForm.Show();
                    }
                }
                else
                {
                    //no currently selected object
                    logger.Warn("No item selected to import a public key from.");
                }
            }
            else
            {
                //no active explorer
                logger.Warn("No active explorer.");
            }
        }

        private void ImportKeyFromPositiveResponse(Outlook.MailItem currentMailItem, KeyManager keyManager)
        {
            keyManager.ImportKeyFromMessageAttachments(currentMailItem.Attachments, currentMailItem.SenderEmailAddress);
            FinalKeyImport finalKeyImport = new FinalKeyImport();
            finalKeyImport.Show();
            logger.Info("Importing key from positive response successful.");
        }

        private void ImportKeyFromRequest(Outlook.MailItem currentMailItem, KeyManager keyManager)
        {
            //create new mail
            Outlook.MailItem newMail = new Outlook.Application().CreateItem(Outlook.OlItemType.olMailItem) as Outlook.MailItem;

            //open window
            ImportKeyForm importKeyForm = new ImportKeyForm();
            System.Windows.Forms.DialogResult dresult = importKeyForm.ShowDialog();
            if (dresult.Equals(System.Windows.Forms.DialogResult.OK))
            {
                logger.Info("User accepts encrypted communication request.");
                //check if user has public key, else start certification
                if (keyManager.GetPublicKey(keyManager.getEmailOfCurrentUser()) != null)
                {
                    if (currentMailItem.Attachments.Count > 0)
                    {
                        keyManager.ImportKeyFromMessageAttachments(currentMailItem.Attachments, currentMailItem.SenderEmailAddress);

                        //send response with new key
                        keyManager.SendPositiveKeyExchangeResponse(newMail, currentMailItem.Sender, keyManager.getEmailOfCurrentUser());
                    }
                }
                else
                {
                    keyManager.StartCertification();
                }
            }
            else
            {
                //send cancel response
                keyManager.SendNegativeKeyExchangeResponse(newMail, currentMailItem.Sender, keyManager.getEmailOfCurrentUser());
            }
        }

        public void OnClickDecryptMessageButton(Office.IRibbonControl control)
        {
            if (Globals.ThisAddIn.Application.ActiveExplorer().Selection.Count > 0)
            {
                //access currently selected item
                Object selObject = Globals.ThisAddIn.Application.ActiveExplorer().Selection[1];
                if (selObject is Outlook.MailItem)
                {
                    Outlook.MailItem currentMailItem = (selObject as Outlook.MailItem);
                    String encryptedFileName = Path.GetTempFileName();
                    String decryptedFileName = Path.GetTempFileName();

                    foreach (Outlook.Attachment attachment in currentMailItem.Attachments)
                    {
                        if (attachment.FileName.Contains("encryptedMessage"))
                        {
                            attachment.SaveAsFile(encryptedFileName);
                        }
                    }

                    File.WriteAllText(encryptedFileName, currentMailItem.Body);
                    //verify signature and decrypt file
                    verifySignatureAndDecryptMessage(encryptedFileName, decryptedFileName);

                    currentMailItem.Body = File.ReadAllText(decryptedFileName);
                    File.Delete(encryptedFileName);
                    File.Delete(decryptedFileName);
                }
            }
        }

        private void verifySignatureAndDecryptMessage(String encryptedFileName, String decryptedFileName)
        {
            GpgInterfaceResult signatureResult = APEEEC_Broker.GetSingletonBroker().GetSignatureHandler().getSignature().VerifySignature(encryptedFileName);
            if (CallbackHandler.Callback(signatureResult, logger) == false)
            {
                ErrorForm errorForm = new ErrorForm();
                errorForm.Show();
            }
            GpgInterfaceResult result = APEEEC_Broker.GetSingletonBroker().GetEncryptionHandler().getDecrypter().DecryptFile(encryptedFileName, decryptedFileName);
            if (CallbackHandler.Callback(result, logger) == false)
            {
                ErrorForm errorForm = new ErrorForm();
                errorForm.Show();
            }
        }

        public void Ribbon_Load(Office.IRibbonUI ribbonUI)
        {
            this.ribbon = ribbonUI;
        }

        #endregion

        #region Hilfsprogramme

        private static string GetResourceText(string resourceName)
        {
            Assembly asm = Assembly.GetExecutingAssembly();
            string[] resourceNames = asm.GetManifestResourceNames();
            for (int i = 0; i < resourceNames.Length; ++i)
            {
                if (string.Compare(resourceName, resourceNames[i], StringComparison.OrdinalIgnoreCase) == 0)
                {
                    using (StreamReader resourceReader = new StreamReader(asm.GetManifestResourceStream(resourceNames[i])))
                    {
                        if (resourceReader != null)
                        {
                            return resourceReader.ReadToEnd();
                        }
                    }
                }
            }
            return null;
        }

        #endregion
        
        public Bitmap logoGetImage(Office.IRibbonControl control)
        {
            return Properties.Resources.encryptedMessageLogo;
        }

        public Bitmap importCertificateGetImage(Office.IRibbonControl control)
        {
            return Properties.Resources.importCertificateImage;
        }

        public Bitmap decryptMessageGetImage(Office.IRibbonControl control)
        {
            return Properties.Resources.decryptMessageImage;
        }
    }
}
