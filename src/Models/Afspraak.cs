using System;
using System.ComponentModel.DataAnnotations;

namespace Kinderpraktijk.Models
{
    public class Afspraak
    {
        public long Id { get; set; }
        public string Beschrijving { get; set; }
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy HH:mm}")]
        public DateTime startTijd { get; set; }
        //De Duur is in minuten
        public int Duur { get; set; }
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy HH:mm}")]
        public DateTime eindTijd { get; set;}
        public string SpecialistId { get; set; }
        public srcUser Specialist { get; set; }
    }
}