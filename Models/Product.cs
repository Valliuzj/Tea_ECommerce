using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

public class Product{
    [Key]
    public int ProductId {get; set;}
    public string ProductCode {get; set;}
    public string ProductType {get; set;}
    public string? Description {get; set;} 
    public decimal UnitPrice {get; set;}

    public int QtyInStock {get; set;}
}