using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

//[Table("products")]
public class Product
{
    //[JsonPropertyName("id")]
    public int id { get; set; } // PK
    //[JsonPropertyName("name")]
    public string name { get; set; } = string.Empty;
    //[JsonPropertyName("description")]
    public string description { get; set; } = string.Empty;
    //[JsonPropertyName("price")]
    public decimal price { get; set; }
    //[JsonPropertyName("createdat")]
    public DateTime createdat { get; set; } = DateTime.Now;
}
