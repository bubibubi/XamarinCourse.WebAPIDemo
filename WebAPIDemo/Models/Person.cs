using System;
using System.ComponentModel.DataAnnotations;

namespace WebAPIDemo.Models
{
    public class Person
    {
        public int Id { get; set; }
        [MaxLength(50)]
        public string Name { get; set; }
        public int? Age { get; set; }
    }
}