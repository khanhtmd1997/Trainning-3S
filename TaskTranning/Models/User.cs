namespace TaskTranning.Models
{
    public class User
    {
        public int Id { get; set; }

        public string Email{ get; set; }        

        public string PassWord { get; set; }
        
        public string FullName { get; set; }        

        public int Phone { get; set; }
        
        public string Address { get; set; }
        
        public string Picture { get; set; }       

        public bool IsActive { get; set; }       

        public int StoreId { get; set; }
        
        public int Role { get; set; }
        
        public virtual Store Store { get; set; }
        
//        public virtual Role Role { get; set; }
    }
}