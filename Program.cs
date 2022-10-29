using System;
using System.Runtime.ConstrainedExecution;
using System.Text.Json;
using System.Xml.Linq;

TranslateDictionary td = new TranslateDictionary();
Console.WriteLine("Создание");
Console.WriteLine("Введите имя словаря");
Console.ReadKey();
string namesl = Console.ReadLine();
char vvod;
do
{
    Console.Clear();
    Console.WriteLine("1 - Добавление");
    Console.WriteLine("2 - Вывод словника");
    Console.WriteLine("3 - Добавление/Удаление слов/перевода");
    Console.WriteLine("4 - Save");
    Console.WriteLine("5 - Load");
    Console.WriteLine("6 - Поиск");
    Console.WriteLine("Выход - Esc");

    vvod = Console.ReadKey().KeyChar;
    switch (vvod)
    {
        case '1':
            {
                Console.Clear();
                re3: Console.WriteLine("Введите слово");
                string slov = Console.ReadLine();
                if (slov == "" || slov == " " || slov == "   ")
                {
                    goto re3;
                }
                re4: Console.WriteLine("Введите перевод");
                string perev = Console.ReadLine();
                if (perev == "" || perev == " " || perev == "   ")
                {
                    goto re4;
                }
                td.words.Add(new Word() { Slovo = slov, perevod = perev });
                Console.ReadKey();
                break;
            }
        case '2':
            {
                Console.Clear();
                Console.WriteLine("Название словаря-> " +namesl);
                td.print();
                Console.ReadKey();
                break;
            }
        case '3':
            {
                Console.Clear();
                td.AddTrs();
                Console.ReadKey();
                break;
            }
        case '4':
            {
                Console.Clear();
                td.CreateSettings();
                Console.ReadKey();
                break;
            }
        case '5':
            {
                Console.Clear();
                td.LoadSettings();
                Console.ReadKey();
                break;
            }
        case '6':
            {
                Console.Clear();
                td.search();
                Console.ReadKey();
                break;
            }
    }
} while (vvod != 27);
class Word
{
    public Word(){}
    public Word(string enG, string Ua) { Slovo = enG;perevod = Ua; }
    public string perevod { get; set; }
    public string Slovo { get; set; }
}
class TranslateDictionary
{
    string[] splits;
    public List<Word>? words { get; set; }
    public int language;
    public TranslateDictionary()
    {
        words = new List<Word>();
    }
    public void print()
    {
        var WPua = from word in words
                   orderby word.Slovo
                   select word;
        
        foreach (var word in WPua)
        {
            Console.WriteLine("Слово \tПеревод");
            Console.WriteLine(word.Slovo + "\t" + word.perevod);
        }
    }
    public bool perSearch = true;
    public async void CreateSettings()
    {
        try
        {
            using (FileStream f = new("Perevodik.json", FileMode.Create))
            {
                foreach (var ve in words)
                {
                    Word? folder = new(ve.perevod, ve.Slovo);
                    await JsonSerializer.SerializeAsync<Word>(f, folder);
                    Console.WriteLine("Save +");
                }
            }
        }
        catch (Exception e) { Console.WriteLine(e.Message); }
    }
    public void LoadSettings()
    {
        try
        {
            using (FileStream f2 = new("Perevodik.json", FileMode.Open))
            {
                string json = null;
                foreach (var p in words)
                    json += JsonSerializer.Serialize<Word>(p) + "\n";
                Console.WriteLine(json);
                Console.WriteLine();
            }
        }
        catch (Exception e) { Console.WriteLine(e.Message); }
    }
    public void search()
    {
        
        Console.WriteLine("Введите искомое слово");
        string slovosearch = Console.ReadLine();
        var search = from word in words
                     where word.Slovo.ToLower() == slovosearch.ToLower()
                     select word;
        foreach (var word in search)
        {
            Console.WriteLine("Cлово->  " + word.Slovo + "\t"+"Вариант(ы) перевода ->>" + word.perevod);
        }
    }
    public void AddTrs()
    {
            char vvod;
            do
            {
        re1: Console.Clear();
                Console.WriteLine("1 - Добавить перевод");
                Console.WriteLine("2 - Удаление слова");
                Console.WriteLine("3 - Удаление перевода");
                Console.WriteLine("Выход - Esc");

                vvod = Console.ReadKey().KeyChar;
                switch (vvod)
                {
                    case '1':
                        {
                            Console.Clear();
                            Console.WriteLine("Добавить перевод");
                            Console.WriteLine("Введите искомое слово");
                            string slovosearch = Console.ReadLine();

                        var search = from word in words
                                     where word.Slovo.ToLower() == slovosearch.ToLower()
                                     select word;
                        foreach (var word in search)
                        {
                            Console.WriteLine("Слово\tПеревод");
                            Console.WriteLine(word.Slovo + "\t" + word.perevod);
                            re2: Console.WriteLine("Введите перевод чтобы добавить");
                            string per = Console.ReadLine();
                            if(per=="" || per==" " || per=="   ")
                            {
                                goto re2;
                            }
                            word.perevod = word.perevod + " " + per;
                            Console.WriteLine(word.Slovo + "\t" + word.perevod);
                            perSearch = false;
                        }
                        if(perSearch==true)
                        {
                            var search2 = from word in words
                                          where word.Slovo != slovosearch
                                          select word;
                            foreach (var word2 in search2)
                            {
                                Console.WriteLine("Совпадений нет.");
                                Console.ReadKey();
                                goto re1;
                            }
                        }
                        perSearch = true;
                        Console.ReadKey();
                            break;
                        }
                    case '2':
                        {
                            Console.Clear();
                        Console.WriteLine("Введите искомое слово для удаления слова и его переводов");
                        string slovosearch = Console.ReadLine();
                            Console.WriteLine("Слово\tПеревод");
                        try
                        {
                            words.Remove(words.Single(s => s.Slovo == slovosearch));
                        }catch(Exception e) { Console.WriteLine(e.Message); }
                            
                        Console.ReadKey();
                            break;
                        }
                    case '3':
                    {
                        Console.Clear();
                        try
                        {
                            string newPerevod = "";
                            string[] split;
                            Console.WriteLine("Введите искомое слово");
                            string slovosearch = Console.ReadLine();

                            var f = words.Find(p => p.Slovo == slovosearch);
                            Console.WriteLine(f.Slovo + "\t" + f.perevod);
                            Console.WriteLine("Введите перевод который хотите удалить");
                            string perDel = Console.ReadLine();
                            splits = f.perevod.Split(new char[] { });
                            if (splits.Length < 2)
                            {
                                Console.ReadKey();
                                break;
                            }
                            if (perDel.Length == 0)
                            {
                                Console.ReadKey();
                                break;
                            }
                            foreach (string s in splits)
                            {
                                Console.WriteLine(s);
                                if (s == perDel)
                                {
                                    Console.WriteLine(s);
                                }
                                else if (s != perDel)
                                {
                                    newPerevod += s + " ";
                                }
                            }
                            splits = f.perevod.Split(new char[] { });
                            split = newPerevod.Split(new char[] { });
                            string n = "";
                            for (int i = 0; i < split.Length; i++)
                            {
                                n += split[i];
                            }
                            if (split.Length < 2)
                            {
                                Console.ReadKey();
                                break;
                            }
                            f.perevod = n;
                        }
                        catch (Exception e) { Console.WriteLine(e.Message); }

                        Console.ReadKey();
                        break;
                    }
                }
            } while (vvod != 27);
    }
}