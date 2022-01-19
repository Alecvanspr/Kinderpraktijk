using src.Interfaces;
using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace src.Models
{
    public class AanmeldingClient : IAanmelding
    {
        public int Id { get; set; }

        [ForeignKey("srcUser")]
        [Column(TypeName = "nvarchar(450)")]
        public string ClientId { get; set; }
        public bool IsAangemeld { get; set; }
        public bool IsAfgemeld { get; set; }
        public DateTime Aanmelding { get; set; }
        public DateTime Afmelding { get; set; }
        public string srcUserId { get; set; }
    }
}
