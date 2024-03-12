namespace LTS.Models;

public class Segment
{
    public string Time { get; set; }  

    public decimal Price { get; set; }

    public Node Start { get; set; } = new Node();

    public Way Way { get; set; } = new Way();   

    public Node End { get; set; } = new Node();  

}