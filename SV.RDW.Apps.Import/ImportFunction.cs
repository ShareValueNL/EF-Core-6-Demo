using System;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Host;
using Microsoft.Extensions.Logging;

namespace SV.RDW.Apps.Import
{
    public class ImportFunction
    {
        [FunctionName("ImportFunction")]
        public void Run([TimerTrigger("0 */5 * * * *")]TimerInfo myTimer, ILogger log)
        {
            log.LogInformation($"Timer trigger function executed at: {DateTime.Now}");
        }
    }
}
