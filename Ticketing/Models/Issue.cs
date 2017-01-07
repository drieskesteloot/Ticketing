using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Ticketing.Models
{
    public class Issue
    {
        public int Id { get; set; } // = PK

        [Display(Name = "IssueSubject", ResourceType = typeof(Resources.TicketingUI))]
        [Required(ErrorMessageResourceName = "IssueSubjectIsRequired",
            ErrorMessageResourceType = typeof(Resources.TicketingUI))]
        public String Onderwerp { get; set; }

        [Display(Name = "IssueStatus", ResourceType = typeof(Resources.TicketingUI))]
        public int StatusInfoId { get; set; } // FK

        [Display(Name = "IssueStatus", ResourceType = typeof(Resources.TicketingUI))]
        public virtual StatusInfo StatusInfo { get; set; }

        [Display(Name = "IssueDescription", ResourceType = typeof(Resources.TicketingUI))]
        [Required(ErrorMessageResourceName = "IssueDescriptionIsRequired",
            ErrorMessageResourceType = typeof(Resources.TicketingUI))]
        public String Omschrijving { get; set; }

        [Display(Name = "IssuePriority", ResourceType = typeof(Resources.TicketingUI))]
        public int PrioriteitInfoId { get; set; } //FK

        [Display(Name = "IssuePriority", ResourceType = typeof(Resources.TicketingUI))]
        public virtual PrioriteitInfo PrioriteitInfo { get; set; }

        [Display(Name = "IssueDateChanged", ResourceType = typeof(Resources.TicketingUI))]
        public DateTime DatumAanpassing { get; set; }

        [Display(Name = "IssueDateCreated", ResourceType = typeof(Resources.TicketingUI))]
        public DateTime DatumAangemaakt { get; set; }

        public Boolean Teruggestuurd { get; set; }
        public String RedenTeruggestuurd { get; set; }


        public string ApplicationUserId { get; set; }
        [ForeignKey("ApplicationUserId")]
        public virtual ApplicationUser Gebruiker { get; set; }



        [Display(Name = "IssueCreator", ResourceType = typeof(Resources.TicketingUI))]
        public String CreationGebruikerId { get; set; }
        [ForeignKey("CreationGebruikerId")]
        [Display(Name = "IssueCreator", ResourceType = typeof(Resources.TicketingUI))]
        public ApplicationUser CreationGebruiker { get; set; }

        [Display(Name = "IssueSolver", ResourceType = typeof(Resources.TicketingUI))]
        public String SolverGebruikerId { get; set; }
        [ForeignKey("SolverGebruikerId")]
        [Display(Name = "IssueSolver", ResourceType = typeof(Resources.TicketingUI))]
        public ApplicationUser SolverGebruiker { get; set; }

        public int Rating { get; set; }

        public ICollection<StatusTrace> StatusTraces { get; set; }

        public ICollection<IssueTrace> IssueTraces { get; set; }

    }
}