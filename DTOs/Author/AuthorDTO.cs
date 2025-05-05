namespace LibraryManagementSystem.DTOs.Author
{
    public class AuthorDto
    {
        public int Id { get; set; }
        public string Name { get; set; }

        // Optional: include if Admins should see this field
        public bool IsDeleted { get; set; }
    }
}
