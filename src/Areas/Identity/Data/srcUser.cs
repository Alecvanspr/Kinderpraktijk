using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using System.Xml.Linq;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

// Add profile data for application users by adding properties to the srcUser class
public class srcUser : IdentityUser
{
    [ForeignKey("srcUser")]
    [Column(TypeName = "nvarchar(450)")]
    public string ParentId { get; set; }

    [ForeignKey("srcUser")]
    [Column(TypeName = "nvarchar(450)")]
    public string SpecialistId { get; set; }

    public bool UserBlocked { get; set; }

    [PersonalData]
    [Display(Name = "Voornaam")]
    [Column(TypeName = "nvarchar(100)")]
    public string FirstName { get; set; }

    [PersonalData]
    [Display(Name = "Achternaam")]
    [Column(TypeName = "nvarchar(100)")]
    public string LastName { get; set; }

    [PersonalData]
    public DateTime Age { get; set; }

    [Column(TypeName = "nvarchar(100)")]
    [Display(Name = "Specialisme")]
    public string Specialism { get; set; }

    [Column(TypeName = "nvarchar(256)")]
    [Display(Name = "Beschrijving")]
    public string Description { get; set; }

    public ICollection<srcUser> Childeren { get; set;}
    public ICollection<srcUser> Clients { get; set; }
    public ICollection<ChatUser> Chats{get;set;}
}

