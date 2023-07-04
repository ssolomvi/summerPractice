namespace summer_practice_domain;

public class Student :
    IEquatable<Student>
{
    private readonly string _surname, _name, _patronymic;
    private readonly string _group;

    public enum ChosenCourseEnum
    {
        CSharp = 1,
        Go = 2,
        Yandex = 3,
        DataCollectionAndLabelling = 4,
        InfrastructureActivities = 5,
        Flood = 6
    }
    private readonly ChosenCourseEnum _chosenCourse;

    public Student(string? surname = null, string? name = null, string? patronymic = null, 
        string? group = null, string? chosenCourse = null)
    {
        /*Surname = string.IsNullOrEmpty(surname)
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
            : chosenCourse;*/
        Surname = surname ?? throw new ArgumentNullException(nameof(surname));
        Name = name ?? throw new ArgumentNullException(nameof(name));
        Patronymic = patronymic ?? throw new ArgumentNullException(nameof(patronymic));
        Group = group ?? throw new ArgumentNullException(nameof(group));
        if (string.IsNullOrEmpty(chosenCourse)) { throw new ArgumentNullException(nameof(chosenCourse)); }

        ChosenCourseEnum chosenCourseValue;
        if (Enum.TryParse(chosenCourse, true, out chosenCourseValue))
        {
            ChosenCourse = chosenCourseValue;
        }
        else
        {
            ChosenCourse = ChosenCourseEnum.Flood;
        }
    }

    public int CourseNumber
    {
        // М8О-213Б-21
        get => (_group[4] - '0');
    }

    // public int GetCourseNumber()
    // {
    //     int courseNumber;
    //     if (!(int.TryParse(_group.Substring(_group.Length - 2), out courseNumber))) { return 0; }
    //     return ((DateTime.Now.Year - (2000 + courseNumber)) + (DateTime.Now.Month > 8 ? 1 : 0)) % 5;
    // }

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
    public ChosenCourseEnum ChosenCourse
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
        return String.Format("[ SNP: {0} {1} {2}{3}Group: {4}{3}Chosen course for practice: {5}]", 
            _surname, _name, _patronymic, Environment.NewLine, _group, _chosenCourse);
        // return $"[ SNP: {Surname} {Name} {Patronymic}\nGroup: {Group}\nChosen course for practice: {ChosenCourse}]";
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
        return (((Surname.GetHashCode() * 2 + Name.GetHashCode()) * 3 + Patronymic.GetHashCode()) * 5 +
                Group.GetHashCode()) * 7 + ChosenCourse.GetHashCode();
    }
}