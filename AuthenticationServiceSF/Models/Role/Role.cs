namespace AuthenticationServiceSF
{
    public class Role
    {
        public int id { get; set; }
        public string name { get; set; }

        public Role(int id, string name)
        {
            this.id = id;
            this.name = name;
        }
    }
}
