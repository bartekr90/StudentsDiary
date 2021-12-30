namespace StudentsDiary
{
    public class Student : Person
    {
        
        public string Math { get; set; }
        public string Technology { get; set; }
        public string Physics { get; set; }
        public string PolishLang { get; set; }
        public string ForeignLang { get; set; }

        public override string GetInfo()
        {
            return $"Student: {Id} {FirstName} {LastName} {Comments}";
        }
    }
}
