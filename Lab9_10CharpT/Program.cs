using System;
using System.Collections.Generic;
using System.Text;
using System.Collections;


class Program
{
    static string ProcessString(string input)
    {
        Stack<char> stack = new Stack<char>();

        foreach (char c in input)
        {
            if (c == '#')
            {
                if (stack.Count > 0)
                {
                    stack.Pop();
                }
            }
            else
            {
                stack.Push(c);
            }
        }

        // Формуємо вихідний рядок
        StringBuilder result = new StringBuilder();
        while (stack.Count > 0)
        {
            result.Insert(0, stack.Pop());
        }

        return result.ToString();
    }

    static string ProcessStringArrayList(string input)
    {
        ArrayList list = new ArrayList();

        foreach (char c in input)
        {
            if (c == '#')
            {
                if (list.Count > 0)
                {
                    list.RemoveAt(list.Count - 1);
                }
            }
            else
            {
                list.Add(c);
            }
        }

        char[] result = (char[])list.ToArray(typeof(char));
        return new string(result);
    }

    class Student
    {
        public string LastName { get; set; }
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        public int GroupNumber { get; set; }
        public List<int> Grades { get; set; }

        public Student(string lastName, string firstName, string middleName, int groupNumber, List<int> grades)
        {
            LastName = lastName;
            FirstName = firstName;
            MiddleName = middleName;
            GroupNumber = groupNumber;
            Grades = grades;
        }

        public bool IsSuccessful()
        {
            foreach (var grade in Grades)
            {
                if (grade < 4) // Перевіряємо, чи є хоча б одна оцінка менше 4
                {
                    return false;
                }
            }
            return true;
        }
    }

    class Student2 : IComparable
    {
        public string LastName { get; set; }
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        public int GroupNumber { get; set; }
        public ArrayList Grades { get; set; }

        public Student2(string lastName, string firstName, string middleName, int groupNumber, ArrayList grades)
        {
            LastName = lastName;
            FirstName = firstName;
            MiddleName = middleName;
            GroupNumber = groupNumber;
            Grades = grades;
        }

        public bool IsSuccessful()
        {
            foreach (int grade in Grades)
            {
                if (grade < 4)
                {
                    return false;
                }
            }
            return true;
        }

        public int CompareTo(object obj)
        {
            if (obj == null) return 1;

            Student2 otherStudent = obj as Student2;
            if (otherStudent != null)
            {
                return this.LastName.CompareTo(otherStudent.LastName);
            }
            else
            {
                throw new ArgumentException("Object is not a Student");
            }
        }
    }


    class Song
    {
        public string Title { get; set; }
        public string Artist { get; set; }

        public Song(string title, string artist)
        {
            Title = title;
            Artist = artist;
        }

        public override string ToString()
        {
            return $"{Title} by {Artist}";
        }
    }

    class MusicCD
    {
        public string Title { get; set; }
        public List<Song> Songs { get; set; }

        public MusicCD(string title)
        {
            Title = title;
            Songs = new List<Song>();
        }

        public override string ToString()
        {
            return $"CD: {Title}";
        }
    }

    class MusicCatalog
    {
        private Hashtable catalog = new Hashtable();

        public void AddCD(string title)
        {
            if (!catalog.ContainsKey(title))
            {
                catalog[title] = new MusicCD(title);
            }
            else
            {
                Console.WriteLine($"CD with title '{title}' already exists.");
            }
        }

        public void RemoveCD(string title)
        {
            if (catalog.ContainsKey(title))
            {
                catalog.Remove(title);
            }
            else
            {
                Console.WriteLine($"CD with title '{title}' not found.");
            }
        }

        public void AddSong(string cdTitle, string songTitle, string artist)
        {
            if (catalog.ContainsKey(cdTitle))
            {
                var cd = (MusicCD)catalog[cdTitle];
                cd.Songs.Add(new Song(songTitle, artist));
            }
            else
            {
                Console.WriteLine($"CD with title '{cdTitle}' not found.");
            }
        }

        public void RemoveSong(string cdTitle, string songTitle)
        {
            if (catalog.ContainsKey(cdTitle))
            {
                var cd = (MusicCD)catalog[cdTitle];
                var song = cd.Songs.Find(s => s.Title == songTitle);
                if (song != null)
                {
                    cd.Songs.Remove(song);
                }
                else
                {
                    Console.WriteLine($"Song with title '{songTitle}' not found in CD '{cdTitle}'.");
                }
            }
            else
            {
                Console.WriteLine($"CD with title '{cdTitle}' not found.");
            }
        }

        public void DisplayCatalog()
        {
            Console.WriteLine("Music Catalog:");
            foreach (DictionaryEntry entry in catalog)
            {
                var cd = (MusicCD)entry.Value;
                Console.WriteLine(cd);
                foreach (var song in cd.Songs)
                {
                    Console.WriteLine($"- {song}");
                }
            }
        }

        public void DisplayCD(string title)
        {
            if (catalog.ContainsKey(title))
            {
                var cd = (MusicCD)catalog[title];
                Console.WriteLine(cd);
                foreach (var song in cd.Songs)
                {
                    Console.WriteLine($"- {song}");
                }
            }
            else
            {
                Console.WriteLine($"CD with title '{title}' not found.");
            }
        }

        public void SearchByArtist(string artist)
        {
            Console.WriteLine($"Search results for artist '{artist}':");
            bool found = false;
            foreach (DictionaryEntry entry in catalog)
            {
                var cd = (MusicCD)entry.Value;
                foreach (var song in cd.Songs)
                {
                    if (song.Artist.Equals(artist, StringComparison.OrdinalIgnoreCase))
                    {
                        Console.WriteLine($"- {song} (CD: {cd.Title})");
                        found = true;
                    }
                }
            }
            if (!found)
            {
                Console.WriteLine($"No songs found for artist '{artist}'.");
            }
        }
    }

    static void Main(string[] args)
    {
        Console.WriteLine("Lab#9");
        Console.WriteLine("Task 1");
        string input = "abc#d##c";
        string output1 = ProcessString(input);
        string output2 = ProcessStringArrayList(input);
        Console.WriteLine("Input: " + input);
        Console.WriteLine("Output stack: " + output1);
        Console.WriteLine("Output array list: " + output2);

        Console.WriteLine("Task 2 (Using Queue)");
        Queue<Student> successfulStudents = new Queue<Student>();
        Queue<Student> otherStudents = new Queue<Student>();

        string filePath = "students.txt";
        string[] lines = File.ReadAllLines(filePath);
        foreach (var line in lines)
        {
            string[] parts = line.Split(',');
            string lastName = parts[0].Trim();
            string firstName = parts[1].Trim();
            string middleName = parts[2].Trim();
            int groupNumber = int.Parse(parts[3].Trim());
            List<int> grades = new List<int>();
            for (int i = 4; i < parts.Length; i++)
            {
                grades.Add(int.Parse(parts[i].Trim()));
            }

            Student student = new Student(lastName, firstName, middleName, groupNumber, grades);
            if (student.IsSuccessful())
            {
                successfulStudents.Enqueue(student);
            }
            else
            {
                otherStudents.Enqueue(student);
            }
        }

        Console.WriteLine("Successful Students:");
        while (successfulStudents.Count > 0)
        {
            Student student = successfulStudents.Dequeue();
            Console.WriteLine($"{student.LastName} {student.FirstName} {student.MiddleName}, Group: {student.GroupNumber}, Grades: {string.Join(", ", student.Grades)}");
        }

        Console.WriteLine("\nOther Students:");
        while (otherStudents.Count > 0)
        {
            Student student = otherStudents.Dequeue();
            Console.WriteLine($"{student.LastName} {student.FirstName} {student.MiddleName}, Group: {student.GroupNumber}, Grades: {string.Join(", ", student.Grades)}");
        }

        Console.WriteLine("Task 2 (Using ArrayList)");

        ArrayList successfulStudentsArrayList = new ArrayList();
        ArrayList otherStudentsArrayList = new ArrayList();
        foreach (var line in lines)
        {
            string[] parts = line.Split(',');
            string lastName = parts[0].Trim();
            string firstName = parts[1].Trim();
            string middleName = parts[2].Trim();
            int groupNumber = int.Parse(parts[3].Trim());
            ArrayList grades = new ArrayList();
            for (int i = 4; i < parts.Length; i++)
            {
                grades.Add(int.Parse(parts[i].Trim()));
            }

            Student2 student = new Student2(lastName, firstName, middleName, groupNumber, grades);
            if (student.IsSuccessful())
            {
                successfulStudentsArrayList.Add(student);
            }
            else
            {
                otherStudentsArrayList.Add(student);
            }
        }

        Console.WriteLine("Successful Students (Using ArrayList):");
        foreach (Student2 student in successfulStudentsArrayList)
        {
            Console.WriteLine($"{student.LastName} {student.FirstName} {student.MiddleName}, Group: {student.GroupNumber}, Grades: {string.Join(", ", student.Grades.ToArray())}");
        }

        Console.WriteLine("Other Students (Using ArrayList):");
        foreach (Student2 student in otherStudentsArrayList)
        {
            Console.WriteLine($"{student.LastName} {student.FirstName} {student.MiddleName}, Group: {student.GroupNumber}, Grades: {string.Join(", ", student.Grades.ToArray())}");
        }

        //task4
        Console.WriteLine("Task 4");
        MusicCatalog catalog = new MusicCatalog();

        catalog.AddCD("Best of 90s");
        catalog.AddSong("Best of 90s", "Song 1", "Artist 1");
        catalog.AddSong("Best of 90s", "Song 2", "Artist 2");

        catalog.AddCD("Rock Classics");
        catalog.AddSong("Rock Classics", "Rock Song 1", "Artist 3");
        catalog.AddSong("Rock Classics", "Rock Song 2", "Artist 4");

        catalog.DisplayCatalog();
        catalog.RemoveCD("Best of 90s");

        catalog.DisplayCatalog();

        catalog.SearchByArtist("Artist 3");

    }
}
