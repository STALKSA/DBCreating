using System;

namespace StudentsManager.Entities
{
    public class Student
    {
        public Guid Id { get; init; }  //айди неизменяем
        public string? Name { get; set; }
        public DateTime Birthday { get; set; }
        public string? Email { get; set; }

        public override string ToString()
        {
            return Name;
        }
    }

}
