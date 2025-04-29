// Models/Author.cs
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace LibraryManagementSystem.Models
{
    public class Author
    {
        public int Id { get; set; }
        [Required, StringLength(100)]
        public string Name { get; set; }
        public DateTime DateOfBirth { get; set; }
        public ICollection<Book> Books { get; set; } = new List<Book>();
    }
}