using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Ticketing.Models
{
    public class IssueTrace
    {
        public int Id { get; set; }

        public DateTime LogDateTime { get; set; }

        public String Onderwerp { get; set; }

        public string Status { get; set; }

        public String Omschrijving { get; set; }

        public string Prioriteit { get; set; }

        public DateTime DatumAangemaakt { get; set; }

        public DateTime DatumAanpassing { get; set; }

        public Boolean Teruggestuurd { get; set; }

        public String RedenTeruggestuurd { get; set; }

        public string ApplicationUser { get; set; }

        public string CreationGebruiker { get; set; }

        public string SolverGebruiker { get; set; }

        //public string IssueId { get; set; }
        public Issue Issue { get; set; }
    }
}