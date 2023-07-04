namespace summer_practice_domain;

public class Student :
    IEquatable<Student>
{
    private readonly string _surname, _name, _patronymic;
    private readonly string _group;
    private readonly string _chosenCourse;

    public Student(string? surname = null, string? name = null, string? patronymic = null, 
        string? group = null, string? chosenCourse = null)
    {
        Surname = string.IsNullOrEmpty(surname)
            ? throw new ArgumentNullException(nameof(surname))
            : surname;
        Name = string.IsNullOrEmpty(name)
            ? throw new ArgumentNullException(nameof(name))
            : name;
        Patronymic = string.IsNullOrEmpty(patronymic)
            ? throw new ArgumentNullException(nameof(patronymic))
            : patronymic;
        Group = string.IsNullOrEmpty(group)
            ? throw new ArgumentNullException(nameof(group))
            : group;
        ChosenCourse = string.IsNullOrEmpty(chosenCourse)
            ? throw new ArgumentNullException(nameof(chosenCourse))
            : chosenCourse;
        // Surname = surname ?? throw new ArgumentNullException(nameof(surname));
        // Name = name ?? throw new ArgumentNullException(nameof(name));
        // Patronymic = patronymic ?? throw new ArgumentNullException(nameof(patronymic));
        // Group = group ?? throw new ArgumentNullException(nameof(group));
        // ChosenCourse = chosenCourse ?? throw new ArgumentNullException(nameof(chosenCourse));
    }

    public int GetCourseNumber()
    {
        // todo: checks up for course (some specific way?)
        int courseNumber;
        if (!(int.TryParse(_group.Substring(_group.Length - 2), out courseNumber))) { return 0; }
        return ((DateTime.Now.Year - (2000 + courseNumber)) + (DateTime.Now.Month > 8 ? 1 : 0)) % 5;
    }

    public string Surname
    {
        get => _surname;

        private init => _surname = value;
    }
    
    public string Name
    {
        get => _name;

        private init => _name = value;
    }
    
    public string Patronymic
    {
        get => _patronymic;

        private init => _patronymic = value;
    }
    
    public string Group
    {
        get => _group;

        private init => _group = value;
    }
    public string ChosenCourse
    {
        get => _chosenCourse;

        private init => _chosenCourse = value;
    }

    public bool Equals(Student? other)
    {
        Console.WriteLine("Student equals student method called");
        
        if (other == null)
        {
            return false;
        }

        return (_surname.Equals(other._surname) &&
                _name.Equals(other._name) &&
                _patronymic.Equals(other._patronymic) &&
                _group.Equals(other._group) &&
                _chosenCourse.Equals(other._chosenCourse));
    }

    public override string ToString()
    {
        return $"[ SNP: {Surname} {Name} {Patronymic}\nGroup: {Group}\nChosen course for practice: {ChosenCourse}]";
    }

    public override bool Equals(object? obj)
    {
        Console.WriteLine("Student equals object method called");

        if (obj == null)
        {
            return false;
        }

        if (obj is Student student)
        {
            return Equals(student);
        }

        return false;
    }

    public override int GetHashCode()
    {
        return (Surname.GetHashCode() + Name.GetHashCode() + Patronymic.GetHashCode()) * 17 + 
               Group.GetHashCode() * 5 + ChosenCourse.GetHashCode();
    }
}