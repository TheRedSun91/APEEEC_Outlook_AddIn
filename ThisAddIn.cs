﻿using APEEEC_Outlook_AddIn.src.Encryption;
using APEEEC_Outlook_AddIn.src.Forms.Certification;
using APEEEC_Outlook_AddIn.src.Forms.KeyExchange;
using APEEEC_Outlook_AddIn.src.WorkflowHandler;
using GpgApi;
using NLog;
using System;
using System.Collections.Generic;
using System.IO;
using Outlook = Microsoft.Office.Interop.Outlook;

namespace APEEEC_Outlook_AddIn
{
    public partial class ThisAddIn
    {

        Outlook.Inspectors _inspectors;
        private APEEEC_Broker _broker;
        private static Logger logger = LogManager.GetCurrentClassLogger();
        
        private void ThisAddIn_Startup(object sender, System.EventArgs e)
        {
            logger.Info("Add-In starts loading.");
            //access the Application_ItemSend method to access the currently active mail item
            Application.ItemSend += new Outlook.ApplicationEvents_11_ItemSendEventHandler(Application_ItemSend);

            //necessary???
            _inspectors = Application.Inspectors;
            _inspectors.NewInspector += new Outlook.InspectorsEvents_NewInspectorEventHandler(Inspectors_NewInspector);

            //initialize broker
            _broker = APEEEC_Broker.GetSingletonBroker();
            logger.Info("Add-In loaded.");
        }

        //this method provides access to the currently active mail item, taken from the application when pressed on the "Send"-Button
        private void Application_ItemSend(object Item, ref bool Cancel)
        {
            KeyManager keyManager = _broker.GetKeyManager();
            //check if item itself is a mail item and the button in the ribbon is toggled
            //only then this should be processed
            if (Item is Outlook.MailItem && Ribbon.encryptionWanted)
            {
                Outlook.MailItem currentMailItem = Item as Outlook.MailItem;
                String senderEmail = keyManager.getEmailOfCurrentUser();
                if (KeyAvailableForEmail(senderEmail))
                {
                    KeyId signKeyID = keyManager.GetSignKeyIDForEmail(senderEmail);
                    //check for each recipient of the current mail item if a public key is available
                    foreach (Outlook.Recipient currentRecipient in currentMailItem.Recipients)
                    {

                        //get real smtp email address from exchange address
                        Outlook.PropertyAccessor propAccessorForRecipient = currentRecipient.PropertyAccessor;
                        String recipientEmail = keyManager.getEmailFromPropertyAccessor(propAccessorForRecipient);
                        if (KeyAvailableForEmail(recipientEmail))
                        {
                            //remove current recipient from mail item
                            currentMailItem.Recipients.Remove(currentRecipient.Index);

                            //create new mail item
                            Outlook.Application app = new Outlook.Application();
                            Outlook.MailItem newMailItem = app.CreateItem(Outlook.OlItemType.olMailItem);

                            //add recipient to new mail item
                            newMailItem.Recipients.Add(currentRecipient.Name);

                            //convert all data from current mail to new mail item
                            //newMailItem.Sender.Address = currentMailItem.Sender.Address;

                            //ok, start encryption, sign email, send email
                            Encrypter encrypter = _broker.GetEncryptionHandler().getEncrypter();
                            //save file and set filename
                            String fileName = Path.GetTempFileName();
                            File.WriteAllText(fileName, currentMailItem.Body);

                            //set encrypted filename from normal filename plus encrypted
                            String encryptedFileName = Path.GetTempFileName();

                            //get recipient key and add it to a list. if there are more recipients, add more
                            Key key = keyManager.GetPublicKey(recipientEmail);
                            IList<KeyId> recipients = new List<KeyId>();
                            recipients.Add(key.Id);

                            //create encrypter and set all values accordingly
                            GpgInterfaceResult gpgInterfaceResult = encrypter.EncryptFile(fileName, encryptedFileName, true, false, signKeyID, recipients, CipherAlgorithm.Aes256);

                            //add all encrypted value to new mail body
                            newMailItem.Body = File.ReadAllText(encryptedFileName);

                            //delete temporary files
                            File.Delete(fileName);
                            File.Delete(encryptedFileName);

                            /*
                            newMailItem.Body += "___________________________________";
                            newMailItem.Body += "Encryption added by APEEEC-Protocol";
                            */
                            newMailItem.Send();
                        }
                        else
                        {
                            List<Outlook.Recipient> remainingRecipients = new List<Outlook.Recipient>();
                            //remove all recipients which are working and save item
                            foreach (Outlook.Recipient recipientToDelete in currentMailItem.Recipients)
                            {
                                if (!recipientToDelete.Equals(currentRecipient))
                                {
                                    currentMailItem.Recipients.Remove(recipientToDelete.Index);
                                    remainingRecipients.Add(recipientToDelete);
                                }
                            }
                            currentMailItem.Save();

                            //put remaining recipients back so the currentmail still has them
                            foreach (Outlook.Recipient deletedRecipient in remainingRecipients)
                            {
                                currentMailItem.Recipients.Add(deletedRecipient.Name);
                            }

                            //start key exchange
                            //get Public Key and send it to the recipient for importing the key in a new mail!


                            //add some usability (ask user if he wants to do that etc.)
                            KeyExchangeStartForm keyExchangeStartForm = new KeyExchangeStartForm();
                            System.Windows.Forms.DialogResult dresult = keyExchangeStartForm.ShowDialog();
                            if (dresult.Equals(System.Windows.Forms.DialogResult.OK))
                            {
                                keyExchangeStartForm.Dispose();
                                //create a new mail
                                Outlook.MailItem newMail = this.Application.CreateItem(Outlook.OlItemType.olMailItem) as Outlook.MailItem;

                                //start key exchange process (send encrypted communication request)
                                keyManager.StartKeyExchange(newMail, senderEmail, currentRecipient);

                                ConfirmationKeyExchangeForm confirmationKeyExchangeForm = new ConfirmationKeyExchangeForm();
                                confirmationKeyExchangeForm.Show();
                            }
                            else
                            {
                                ErrorKeyExchangeForm errorKeyExchangeForm = new ErrorKeyExchangeForm();
                                errorKeyExchangeForm.Show();
                            }
                        }
                    }
                    if (currentMailItem.Recipients.Count > 0)
                    {
                        Cancel = true; //do not send current mail
                        currentMailItem.Save();
                    }
                    else
                    {
                        currentMailItem.Delete();
                    }
                }
                else
                {
                    //abort sending email, start certification
                    keyManager.StartCertification();
                    currentMailItem.Save();
                    currentMailItem = null;
                    Cancel = true;
                }
            }
        }

        private bool KeyAvailableForEmail (String email)
        {
            //check if key is available for email
            KeyManager keyManager = _broker.GetKeyManager();
            if (keyManager.GetPublicKey(email) != null)
            {
                return true;
            }
            return false;
        }

        private void Inspectors_NewInspector(Outlook.Inspector Inspector)
        {
            //necessary???
        }
        
        private void ThisAddIn_Shutdown(object sender, System.EventArgs e)
        {
            // Hinweis: Outlook löst dieses Ereignis nicht mehr aus. Wenn Code vorhanden ist, der 
            //    ausgeführt werden muss, wenn Outlook geschlossen wird, informieren Sie sich unter http://go.microsoft.com/fwlink/?LinkId=506785
        }

        protected override Microsoft.Office.Core.IRibbonExtensibility CreateRibbonExtensibilityObject()
        {
            return new Ribbon();
        }

        #region Von VSTO generierter Code

        /// <summary>
        /// Erforderliche Methode für die Designerunterstützung.
        /// Der Inhalt der Methode darf nicht mit dem Code-Editor geändert werden.
        /// </summary>
        private void InternalStartup()
        {
            this.Startup += new System.EventHandler(ThisAddIn_Startup);
            this.Shutdown += new System.EventHandler(ThisAddIn_Shutdown);
        }
        
        #endregion
    }
}