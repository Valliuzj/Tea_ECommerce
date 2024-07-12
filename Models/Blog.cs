using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

public class Blog{
    [Key]
    public int BlogId {get; set;}
    public string Title {get; set;}
    public string Content {get; set;} 
    public string? Author {get; set;}
    public string? ImageUrl {get; set;}    

}