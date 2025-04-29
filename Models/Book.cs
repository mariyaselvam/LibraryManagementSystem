// Models/Book.cs
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LibraryManagementSystem.Models
{
    public class Book
    {
        public int Id { get; set; }
        [Required, StringLength(200)]
        public string Title { get; set; }
        [Required, StringLength(20)]
        public string ISBN { get; set; }
        public DateTime PublishedDate { get; set; }
        [ForeignKey("Author")]
        public int AuthorId { get; set; }
        public Author Author { get; set; }
    }
}