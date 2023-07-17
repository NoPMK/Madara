using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RealEstateApp.Models
{
    public class User 
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.None)]
        public string Id { get; set; } = string.Empty;
        public List<Property>? Properties { get; set; }   
    }
}