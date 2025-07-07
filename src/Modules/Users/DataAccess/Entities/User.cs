using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Entities
{
    // I was originally going to use SQL but decided to go with Postgres on Suprabase.
    // Column attributes are being used to match with Postgres snake case conventions.
    public partial class User
    {
        [Key]
        [Column("id")]
        public required Guid Id { get; set; }

        [Column("first_name")]
        public required string FirstName { get; set; }

        [Column("last_name")]
        public required string LastName { get; set; }

        [Column("email")]
        public required string Email { get; set; }

        [Column("phone")]
        public required string Phone { get; set; }

        [Column("address_line_1")]
        public required string AddressLine1 { get; set; }

        [Column("address_line_2")]
        public required string AddressLine2 { get; set; }

        [Column("city")]
        public required string City { get; set; }

        [Column("state")]
        public required string State { get; set; }

        [Column("postal_code")]
        public required string PostalCode { get; set; } // Postal code is a string because international postal codes can contain letters

        [Column("date_of_birth")]
        public required DateTime DateOfBirth { get; set; }

        [Column("created_at")]
        public required DateTime CreatedAt { get; set; }

        [Column("updated_at")]
        public required DateTime UpdatedAt { get; set; }
    }
}
