using AutoMapper;
using LibraryManagementSystem.DTOs.Author;
using LibraryManagementSystem.DTOs.Book;
using LibraryManagementSystem.DTOs.Genre;
using LibraryManagementSystem.Models;

namespace LibraryManagementSystem.Helpers
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            // Author mappings
            CreateMap<Author, AuthorDto>(); // Mapping Author to AuthorDto
            CreateMap<AuthorCreateDTO, Author>(); // Mapping AuthorCreateDTO to Author
            CreateMap<AuthorUpdateDTO, Author>(); // Mapping AuthorUpdateDTO to Author

            // Book mappings
            CreateMap<Book, BookResponseDTO>()
                .ForMember(dest => dest.AuthorName, opt => opt.MapFrom(src => src.Author.Name)); // Custom mapping for AuthorName

            //CreateMap<Book, BookReadDto>()
            //.ForMember(dest => dest.AuthorName, opt => opt.MapFrom(src => src.Author.Name));

            CreateMap<BorrowRecord, BorrowReadDto>()
                .ForMember(dest => dest.BookTitle, opt => opt.MapFrom(src => src.Book.Title));


            CreateMap<BookCreateDTO, Book>(); // Mapping BookCreateDTO to Book
            CreateMap<BookUpdateDTO, Book>(); // Mapping BookUpdateDTO to Book
            CreateMap<BorrowCreateDto, BorrowRecord>();

            CreateMap<Genre, GenreDto>().ReverseMap();
            CreateMap<GenreCreateDto, Genre>();
            CreateMap<GenreUpdateDto, Genre>();


        }
    }
}
