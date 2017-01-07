using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Ticketing.Models
{
    public class StatusTrace
    {
        public int Id { get; set; }
        public DateTime LogDateTime { get; set; }
        public string Status { get; set; }
        public string IssueId { get; set; }

        public Issue Issue { get; set; }
    }
}