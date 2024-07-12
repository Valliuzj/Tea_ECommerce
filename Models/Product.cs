using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System;
using System.IO;
using System.Threading.Tasks;

public class Product{
    [Key]
    public int ProductId {get; set;}
    public string ProductCode {get; set;}
    public string Name {get; set;}
    public string? Description {get; set;} 
    public decimal UnitPrice {get; set;}

    public int QtyInStock {get; set;}

    public string? ImageUrl {get; set;}    
    
}