// DTOs/Author/AuthorUpdateDTO.cs
using System;
using System.ComponentModel.DataAnnotations;

namespace LibraryManagementSystem.DTOs.Author
{
    public class AuthorUpdateDTO
    {
        [Required, StringLength(100)]
        public string Name { get; set; }
        public DateTime DateOfBirth { get; set; }
    }
}