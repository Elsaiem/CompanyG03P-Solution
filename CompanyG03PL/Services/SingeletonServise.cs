namespace CompanyG03PL.Services
{
    public class SingeletonServise:ISingeletonService
    {
        public Guid Guid { get; set; }

        public SingeletonServise()
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
