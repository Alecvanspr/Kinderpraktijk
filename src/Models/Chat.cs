using System.Collections.Generic;
public class Chat{
    public int Id{get;set;}
    public string Onderwerp{get; set;}
    public string Naam{get;set;}
    public string Beschrijving{get;set;}
    public ICollection<Message> Messages{get;set;} //Dit zijn de berichten die zijn geplaatst
    public ICollection<ChatUser> Users{get;set;} //Dit zijn de Users voor de app
    public ChatType type {get;set;} //Dit is het sype voor de app
}
