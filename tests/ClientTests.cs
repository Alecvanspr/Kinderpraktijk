using Xunit;

public class ClientTests{
    private MijnContext GetContext(){
        MockDatabase m = new MockDatabase();
        return m.CreateContext();
    }
    //Deze methode is nog niet af doordat het gekoppeld moet worden aan bepaalde componenten van de applicatie
    [Fact]
    public void testclass1(){

    }
}