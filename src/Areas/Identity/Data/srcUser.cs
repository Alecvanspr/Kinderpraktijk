using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace src.Areas.Identity.Data;

// Add profile data for application users by adding properties to the srcUser class
public class srcUser : IdentityUser
{
    [ForeignKey("srcUser")]
    public string ParentId { get; set; } 

    [PersonalData]
    [Column(TypeName = "nvarchar(100)")]
    public string FirstName { get; set; }

    [PersonalData]
    [Column(TypeName = "nvarchar(100)")]
    public string LastName { get; set; }

    [PersonalData]
    public int Age { get; set; }

    [Column(TypeName = "nvarchar(100)")]
    public string Specialism { get; set; }

    [Column(TypeName = "nvarchar(256)")]
    public string Description { get; set; }

    public ICollection<srcUser> Childeren { get; set;}
}

