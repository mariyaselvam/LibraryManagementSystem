using AutoMapper;
using LibraryManagementSystem.DTOs.Author;
using LibraryManagementSystem.DTOs.Book;
using LibraryManagementSystem.Models;


namespace LibraryManagementSystem.Helpers
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            // Author mappings
            CreateMap<Author, AuthorDTO>();
            CreateMap<AuthorCreateDTO, Author>();
            CreateMap<AuthorUpdateDTO, Author>();

            // Book mappings
            CreateMap<Book, BookResponseDTO>();
            CreateMap<BookCreateDTO, Book>();
            CreateMap<BookUpdateDTO, Book>();
        }
    }
}
