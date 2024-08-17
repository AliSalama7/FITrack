namespace FITrack.Models
{
    public class RegisterModel
    {
        [StringLength(100)]
        public string FirstName {  get; set; }
        [StringLength(100)]
        public string LastName { get; set; }
        [StringLength(100)]
        public string UserName {  get; set; }
        [StringLength(256)]
        public string Email { get; set; }
        [StringLength(256)]
        public string Password { get; set; }
    }
}