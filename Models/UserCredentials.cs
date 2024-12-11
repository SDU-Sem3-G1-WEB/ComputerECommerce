namespace ComputerECommerce.Models
{
    public class UserCredentials
    {
        public required string hashedEmail { get; set; }
        public required string hashedPassword { get; set; }
        
    }
}