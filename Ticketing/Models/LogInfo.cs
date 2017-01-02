using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Ticketing.Models
{
    public class LogInfo
    {
        public int Id { get; set; }
        public string CreatedBy { get; set; }
        public string ChangedBy { get; set; }
        public DateTime CreatedOn { get; set; }
        public DateTime ChangedOn { get; set; }
    }
}