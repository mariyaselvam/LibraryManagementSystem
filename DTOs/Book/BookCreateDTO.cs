// DTOs/Book/BookCreateDTO.cs
using System;
using System.ComponentModel.DataAnnotations;

namespace LibraryManagementSystem.DTOs.Book
{
    public class BookCreateDTO
    {
        [Required, StringLength(200)]
        public string Title { get; set; }
        [Required, StringLength(20)]
        public string ISBN { get; set; }
        public DateTime PublishedDate { get; set; }
        [Required]
        public int AuthorId { get; set; }
    }
}