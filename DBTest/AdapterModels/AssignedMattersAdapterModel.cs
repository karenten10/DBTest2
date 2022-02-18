using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace InspectionBlazor.AdapterModels
{
    public class AssignedMattersAdapterModel
    {
        public long Id { get; set; }
        public int? Sender { get; set; }
        public int? Receiver { get; set; }
        public string SenderName { get; set; }
        public string ReceiverName { get; set; }
        public string Message { get; set; }
        public DateTime AnnounceTime { get; set; }
    }
}
