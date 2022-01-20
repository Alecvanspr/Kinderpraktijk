using System;
using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace src.Models
{
    public class ClientListAanmelding
    {
        [Display(Name = "Naam")]
        public string ClientName { get; set; }
        public string ClientId { get; set; }
        [Display(Name = "Aangemeld")]
        public bool IsAangemeld { get; set; }
        [Display(Name = "Afgemeld")]
        public bool IsAfgemeld { get; set; }
        [Display(Name = "Aanmelding")]
        public DateTime DateAanmelding { get; set; }
        [Display(Name = "Datum Afmelding")]
        public DateTime DateAfmelding { get; set; }
    }
}
