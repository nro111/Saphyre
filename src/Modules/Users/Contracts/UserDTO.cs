namespace Contracts
{
    public class UserDTO
    {
        public Guid Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; } 
        public string AddressLine1 { get; set; }
        public string AddressLine2 { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string PostalCode { get; set; } // Postal code is a string because international postal codes can contain letters
        public DateTime DateOfBirth { get; set; }

        // Initialization of properties to prevent null values
        public UserDTO() 
        {
            Id = Guid.Empty;
            FirstName = string.Empty;
            LastName = string.Empty;
            Email = string.Empty;
            Phone = string.Empty;
            AddressLine1 = string.Empty;
            AddressLine2 = string.Empty;
            City = string.Empty;
            State = string.Empty;
            PostalCode = string.Empty;
            DateOfBirth = DateTime.MinValue;
        }
    }
}
