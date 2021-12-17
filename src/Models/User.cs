using System.Collections.Generic;
using Microsoft.AspNetCore.Identity;

public class User : IdentityUser{
    public ICollection<ChatUser> Chats{get;set;}
}