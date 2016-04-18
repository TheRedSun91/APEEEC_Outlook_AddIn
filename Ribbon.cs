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

// TODO:  Führen Sie diese Schritte aus, um das Element auf dem Menüband (XML) zu aktivieren:

// 1: Kopieren Sie folgenden Codeblock in die ThisAddin-, ThisWorkbook- oder ThisDocument-Klasse.

//  protected override Microsoft.Office.Core.IRibbonExtensibility CreateRibbonExtensibilityObject()
//  {
//      return new Ribbon();
//  }

// 2. Erstellen Sie Rückrufmethoden im Abschnitt "Menübandrückrufe" dieser Klasse, um Benutzeraktionen
//    zu behandeln, z.B. das Klicken auf eine Schaltfläche. Hinweis: Wenn Sie dieses Menüband aus dem Menüband-Designer exportiert haben,
//    verschieben Sie den Code aus den Ereignishandlern in die Rückrufmethoden, und ändern Sie den Code für die Verwendung mit dem
//    Programmmodell für die Menübanderweiterung (RibbonX).

// 3. Weisen Sie den Steuerelementtags in der Menüband-XML-Datei Attribute zu, um die entsprechenden Rückrufmethoden im Code anzugeben.  

// Weitere Informationen erhalten Sie in der Menüband-XML-Dokumentation in der Hilfe zu Visual Studio-Tools für Office.


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
                    KeyManager keyManager = APEEEC_Broker.GetSingletonBroker().GetKeyManager();
                    Outlook.MailItem currentMailItem = (selObject as Outlook.MailItem);
                    if (currentMailItem.Subject == isPositiveResponse)
                    {
                        ImportKeyFromPositiveResponse(currentMailItem, keyManager);
                    }
                    else if (currentMailItem.Subject == isEncryptionRequest)
                    {
                        ImportKeyFromRequest(currentMailItem, keyManager);
                    }
                    else if (currentMailItem.Subject == isNegativeResponse)
                    {
                        //alert: user does not want to create secure communication
                    }
                    else
                    {
                        //no apeeec mail item
                        //nothing can be imported
                    }
                }
                else
                {
                    //no currently selected object
                }
            }
            else
            {
                //no active explorer
            }
        }

        private void ImportKeyFromPositiveResponse(Outlook.MailItem currentMailItem, KeyManager keyManager)
        {
            keyManager.ImportKeyFromMessageAttachments(currentMailItem.Attachments, currentMailItem.SenderEmailAddress);
            FinalKeyImport finalKeyImport = new FinalKeyImport();
            finalKeyImport.Show();
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
                //check if user has public key, else start certification
                if (keyManager.GetPublicKey(keyManager.getEmailOfCurrentUser()) != null)
                {
                    if (currentMailItem.Attachments.Count > 0)
                    {
                        keyManager.ImportKeyFromMessageAttachments(currentMailItem.Attachments, currentMailItem.SenderEmailAddress);

                        //send response with new key (newMail created, add values and send)
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
                //send cancel response (newMail created, add values and send)

                keyManager.SendNegativeKeyExchangeResponse(newMail, currentMailItem.Sender, keyManager.getEmailOfCurrentUser());
            }
        }

        public void OnClickDecryptMessageButton(Office.IRibbonControl control)
        {
            EncryptionHandler encryptionHandler = APEEEC_Broker.GetSingletonBroker().GetEncryptionHandler();

            if (Globals.ThisAddIn.Application.ActiveExplorer().Selection.Count > 0)
            {
                //access currently selected item
                Object selObject = Globals.ThisAddIn.Application.ActiveExplorer().Selection[1];
                if (selObject is Outlook.MailItem)
                {
                    Outlook.MailItem currentMailItem = (selObject as Outlook.MailItem);
                    String encryptedFileName = Path.GetTempFileName();
                    String decryptedFileName = Path.GetTempFileName();
                    File.WriteAllText(encryptedFileName, currentMailItem.Body);
                    GpgInterfaceResult result = encryptionHandler.getDecrypter().DecryptFile(encryptedFileName, decryptedFileName);
                    CallbackHandler.Callback(result, logger);
                    currentMailItem.Body = File.ReadAllText(decryptedFileName);

                    //only delete if mail is being closed? or doesnt matter?
                    File.Delete(encryptedFileName);
                    File.Delete(decryptedFileName);
                }
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
