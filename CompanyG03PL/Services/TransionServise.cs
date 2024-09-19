namespace CompanyG03PL.Services
{
    public class TransionServise:ITransientService
    {
        public Guid Guid { get; set; }

        public TransionServise()
        {
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
