using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace WorkerProcess.ReportService
{
    public class MessageQueueType
    {
        public int UUID { get; set; }
        public DateTime RequestDate { get; set; } = DateTime.Now;
        public int StatusID { get; set; }
    }
}