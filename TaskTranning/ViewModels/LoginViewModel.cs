namespace TaskTranning.ViewModels
{
    public class LoginViewModel
    {
        public string Email { get; set; }
        
        public string PassWord { get; set; }
        
        public int Role { get; set; }
        
        public string RequestPath { get; set; }
        
        public bool IsActive { get; set; }
    }
}