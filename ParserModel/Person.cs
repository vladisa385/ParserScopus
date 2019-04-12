namespace ParserModel
{
    public class Person
    {
        public string Email { get; }
        public string Fio { get; }

        public Person(string fio, string email)
        {
            Fio = fio;
            Email = email;
        }

        public string ToString(bool isOnlyEmail)
        {
            if (isOnlyEmail)
                return $"{Email}";
            return $"{Fio}  {Email}";
        }


    }
}
