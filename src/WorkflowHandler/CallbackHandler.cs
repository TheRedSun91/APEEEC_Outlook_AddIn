using GpgApi;
using NLog;

namespace APEEEC_Outlook_AddIn.src.WorkflowHandler
{
    class CallbackHandler
    {

        public static bool Callback(GpgInterfaceResult result, Logger logger)
        {
            if (result.Status == GpgInterfaceStatus.Success)
            {
                logger.Info(result.Message);
                return true;
            }
            else
            {
                logger.Error(result.Message);
                return false;
            }
        }
    }
}
