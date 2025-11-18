class Tasks
{
    static void Main(string[] args)
    {   
        var person1 = ("Michał", "Madeiski", 21, 1000);
        PrintSeparator(1, true);PrintTuple(person1);PrintSeparator(1, false);

        PrintSeparator(2, true);ClassVarTest();PrintSeparator(2, false);

        int[] arr = {1, 2, 3, 4, 5};
        PrintSeparator(3, true); ArrayPresentation(arr); Console.WriteLine($"Tablica po operacjach: {ArrayToString<int>(arr)}"); PrintSeparator(3, false);

        var person2 = new {name = "Michał", surname = "Madeiski", age = 21, wage = 1000};
        PrintSeparator(4, true); PrintAnonymous(person2); PrintSeparator(4, false);

        PrintSeparator(5, true); DrawCard("Michał", "Madeiski"); PrintSeparator(5, false);

        PrintSeparator(6, true); PrintCountMyTypes(CountMyTypes(1, 2, 4, 3.2, -2.8, "abc", "abcde", 'x', 10, new int[] {1, 2, 3})); PrintSeparator(6, false);
    }

    static void PrintSeparator(int n, bool startOrEnd)
    {
        string separator = "===============================";
        string task = "TASK";
        task += n;

        separator += startOrEnd ? (task + separator) : (separator + "=====");
        Console.WriteLine(separator);
    }

    static void PrintTuple((string name, string surname, int age, int wage) person)
    {
        Console.WriteLine("Różne opcje dostępu do elementów krotki: ");

        Console.WriteLine("1. Dostęp przez nazwy elementów: ");
        Console.WriteLine($"imię: {person.name}, nazwisko: {person.surname}, wiek: {person.age}, płaca: {person.wage}");

        Console.WriteLine("2. Dostęp przez kolejne itemy: ");
        Console.WriteLine($"imię: {person.Item1}, nazwisko: {person.Item2}, wiek: {person.Item3}, płaca: {person.Item4}");

        var (n, s, a, w) = person;
        Console.WriteLine("3. Dostęp przez dekonstrukcję krotki: ");
        Console.WriteLine($"imię: {n}, nazwisko: {s}, wiek: {a}, płaca: {w}");
    }

    static void ClassVarTest()
    {
        // var class = "class test";
        // Console.WriteLine(class);
        var @class = "@class test";
        Console.WriteLine(@class);
    }

    static void ArrayPresentation(int[] arr)
    {
        Console.WriteLine($"Przekazana tablica: {ArrayToString<int>(arr)}");

        Array.Reverse(arr);
        Console.WriteLine($"1. Reverse(): {ArrayToString<int>(arr)}");

        int idx = Array.IndexOf(arr, 4);
        Console.WriteLine($"2. IndexOf(4): {idx}");

        Array.Sort(arr);
        Console.WriteLine($"3. Sort(): {ArrayToString<int>(arr)}");
        
        int[] filtered = Array.FindAll(arr, x => x > 3);
        Console.WriteLine($"4. FindAll(x => x > 3): {ArrayToString<int>(filtered)}");

        Array.Clear(arr, 2, 2);
        Console.WriteLine($"5. Clear(2, 2): {ArrayToString<int>(arr)}");
    }

    static string ArrayToString<T>(T[] arr)
    {
        string arrStr = "(";
        foreach (var item in arr)
        {
            arrStr += item;
            arrStr += ", ";
        }
        arrStr += ")";
        return arrStr;
    }

    static void PrintAnonymous(dynamic person)
    {
        Console.WriteLine("Wyświetlanie typu anonimowego jak krotki: ");
        Console.WriteLine($"imię: {person.name}, nazwisko: {person.surname}, wiek: {person.age}, płaca: {person.wage}");
    }

    static void DrawCard(string first_line, string second_line="Ryś", char border_char='X', int border_width=2, int min_width=20)
    {
        int first_line_length = first_line.Length + 2 + border_width * 2;
        int second_line_length = second_line.Length + 2 + border_width * 2;
        int width = Math.Max(first_line_length, second_line_length);

        int first_line_space_amount = width - first_line_length + 2;
        int second_line_space_amount = width - second_line_length + 2;
        int first_line_space_amount_p1 = first_line_space_amount / 2;
        int first_line_space_amount_p2 = first_line_space_amount - first_line_space_amount_p1;
        int second_line_space_amount_p1 = second_line_space_amount / 2;
        int second_line_space_amount_p2 = second_line_space_amount - second_line_space_amount_p1;

        if (width < min_width)
        {
            width = min_width;

            first_line_space_amount = width - first_line_length + 2;
            second_line_space_amount = width - second_line_length + 2;
            first_line_space_amount_p1 = first_line_space_amount / 2;
            first_line_space_amount_p2 = first_line_space_amount - first_line_space_amount_p1;
            second_line_space_amount_p1 = second_line_space_amount / 2;
            second_line_space_amount_p2 = second_line_space_amount - second_line_space_amount_p1;
        }
        
        string card_first_line = new string(border_char, border_width) + new string(' ', first_line_space_amount_p1) + first_line + new string(' ', first_line_space_amount_p2) + new string(border_char, border_width);
        string card_second_line = new string(border_char, border_width) + new string(' ', second_line_space_amount_p1) + second_line + new string(' ', second_line_space_amount_p2) + new string(border_char, border_width);

        string border_line = new string(border_char, width);
        for (int i = 0; i < border_width; i++) { Console.WriteLine(border_line); }
        Console.WriteLine(card_first_line);
        Console.WriteLine(card_second_line);
        for (int i = 0; i < border_width; i++) { Console.WriteLine(border_line); }
    }

    static (int, int, int, int) CountMyTypes(params object[] items)
    {
        int int_even = 0;
        int double_positive = 0;
        int string_length_above_5 = 0;
        int other = 0;

        foreach (var item in items)
        {
            switch (item)
            {
                case int i when i % 2 == 0:
                    int_even++;
                    break;
                case double d when d > 0:
                    double_positive++;
                    break;
                case string s when s.Length >= 5:
                    string_length_above_5++;
                    break;
                default:
                    other++;
                    break;
            }
        }

        var result = (int_even, double_positive, string_length_above_5, other);
        return result;
    }

    static void PrintCountMyTypes((int, int, int, int) result)
    {
        Console.WriteLine("1. Liczby całkowite parzyste: " + result.Item1);
        Console.WriteLine("2. Liczby rzeczywiste dodatnie: " + result.Item2);
        Console.WriteLine("3. Napisy co najmnniej 5-znakowe: " + result.Item3);
        Console.WriteLine("4. Elementy innych typów: " + result.Item4);
    }
}