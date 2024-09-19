
namespace CompanyG03PL.Services
{
    public class ScoobedServise : IScoopedService
    {
        public Guid Guid { get; set; }

        public ScoobedServise() {
         Guid = Guid.NewGuid();
        
        }
        public string getGuid()
        {
           return Guid.ToString();
        }
        public override string ToString()
        {
            return Guid.ToString();
        }
    }
}
