using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace Entities
{
    public partial class User
    {
        [Key]
        public required Guid Id { get; set; }
        public required string FirstName { get; set; }
        public required string LastName { get; set; }
        public required string Email { get; set; }
        public required string Phone { get; set; } 
        public required string AddressLine1 { get; set; }
        public required string AddressLine2 { get; set; }
        public required string City { get; set; }
        public required string State { get; set; }
        public required string PostalCode { get; set; } // Postal code is a string because international postal codes can contain letters
        public required DateTime DateOfBirth { get; set; }
    }
}
