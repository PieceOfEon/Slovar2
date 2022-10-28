using System;
using System.Text.Json;
using System.Xml.Linq;

TranslateDictionary td = new TranslateDictionary();

char vvod;
do
{
    Console.Clear();
    Console.WriteLine("1 - Создание словаря");
    Console.WriteLine("2 - Добавление");
    Console.WriteLine("3 - Вывод словника");
    Console.WriteLine("4 - Поиск");
    Console.WriteLine("5 - Save");
    Console.WriteLine("6 - Load");
    Console.WriteLine("Выход - Esc");

    vvod = Console.ReadKey().KeyChar;
    switch (vvod)
    {
        case '1':
            {
                Console.Clear();

                Console.ReadKey();
                break;
            }
        case '2':
            {
                Console.Clear();
                Console.WriteLine("Введите слово на английском");
                string engWord = Console.ReadLine();
                Console.WriteLine("Введите его перевод на украинском");
                string UaWord = Console.ReadLine();
                td.words.Add(new Word() { ENG = engWord, UA = UaWord });

                Console.ReadKey();
                break;
            }
        case '3':
            {
                Console.Clear();
                td.print();
                Console.ReadKey();
                break;
            }
        case '4':
            {
                Console.Clear();
                td.AddTrs();
                Console.ReadKey();
                break;
            }
        case '5':
            {
                Console.Clear();
                td.CreateSettings();
                Console.ReadKey();
                break;
            }
        case '6':
            {
                Console.Clear();
                td.LoadSettings();
                Console.ReadKey();
                break;
            }
        case '7':
            {
                Console.Clear();


                Console.ReadKey();
                break;
            }
    }
} while (vvod != 27);
class Word
{
    public Word()
    {

    }
    public Word(string enG, string Ua)
    {
        ENG = enG;
        UA = Ua;
    }
    public string UA { get; set; }
    public string ENG { get; set; }

}
class TranslateDictionary
{
    string[] splits;
    public List<Word>? words { get; set; }
    
    public TranslateDictionary()
    {
        words = new List<Word>();
    }
    public void print()
    {
        var WPua = from word in words
                   orderby word.ENG
                   select word;
        
        foreach (var word in WPua)
        {
            Console.WriteLine("Слово\tПеревод");
            Console.WriteLine(word.ENG + "\t" + word.UA);
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
                    Word? folder = new(ve.ENG, ve.UA);
                    await JsonSerializer.SerializeAsync<Word>(f, folder);
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
                var i = new JsonSerializerOptions { WriteIndented = true };
                foreach (var p in words)
                    json += JsonSerializer.Serialize<Word>(p/*, i*/) + "\n";
                Console.WriteLine(json);
                Console.WriteLine();
            }
        }
        catch (Exception e) { Console.WriteLine(e.Message); }
    }
    public void search()
    {
        
        Console.WriteLine("Введите искомое слово на ??? языке");
        string slovosearch = Console.ReadLine();
        var search = from word in words
                     where word.ENG == slovosearch
                     select word;
        foreach (var word in search)
        {
            Console.WriteLine("Cлово->  " + word.ENG + "\t"+"Вариант(ы) перевода" + word.UA);
        }

        var search2 = from word2 in words
                      where word2.ENG != slovosearch
                      select word2;
        foreach (var word2 in search2)
        {
            Console.WriteLine("Совпадений нет.");
        }

    }
    public void AddTrs()
    {
        //search();
      
        
            char vvod;
            do
            {
        re1: Console.Clear();
                Console.WriteLine("1 - Добавить перевод");
                Console.WriteLine("2 - Удаление");
                Console.WriteLine("3 - Удаление перевода");
                Console.WriteLine("Выход - Esc");

                vvod = Console.ReadKey().KeyChar;
                switch (vvod)
                {
                    case '1':
                        {
                            Console.Clear();
                            Console.WriteLine("Добавить перевод");
                            Console.WriteLine("Введите искомое слово на ??? языке");
                            string slovosearch = Console.ReadLine();

                        var search = from word in words
                                     where word.ENG == slovosearch
                                     select word;
                        foreach (var word in search)
                        {
                            Console.WriteLine("Слово\tПеревод");
                            Console.WriteLine(word.ENG + "\t" + word.UA);
                            re2: Console.WriteLine("Введите перевод чтобы добавить");
                            string per = Console.ReadLine();
                            if(per=="" || per==" " || per=="   ")
                            {
                                goto re2;
                            }
                            word.UA = word.UA + " " + per;
                            Console.WriteLine(word.ENG + "\t" + word.UA);
                            perSearch = false;
                        }
                        if(perSearch==true)
                        {
                            var search2 = from word in words
                                          where word.ENG != slovosearch
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
                        Console.WriteLine("Добавить перевод");
                        Console.WriteLine("Введите искомое слово на ??? языке для удаления слова и его переводов");
                        string slovosearch = Console.ReadLine();
                            Console.WriteLine("Слово\tПеревод");
                            words.Remove(words.Single(s => s.ENG == slovosearch));
                        Console.ReadKey();
                            break;
                        }
                    case '3':
                    {
                        Console.Clear();
                        string newPerevod="";
                        string[] split;
                        Console.WriteLine("Введите искомое слово на ??? языке");
                        string slovosearch = Console.ReadLine();
                        var f = words.Find(p => p.ENG == slovosearch);
                        Console.WriteLine(f.ENG + "\t"+ f.UA);
                        Console.WriteLine("Введите перевод который хотите удалить");
                        string perDel = Console.ReadLine();
                        Console.WriteLine(f.UA.Contains(perDel));
                        //splits = f.UA.Split(perDel);
                        splits=f.UA.Split(new char[] {});
                        Console.WriteLine(splits.Length);
                        Console.WriteLine("Drasti "+f.UA+" OH");
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
                        
                        //if (f.UA.Contains(perDel)==true)
                        //{
                            foreach (string s in splits)
                            {
                                Console.WriteLine(s);
                                if (s==perDel)
                                {
                                    //newPerevod += "";
                                    Console.WriteLine(s);
                                }
                                else if (s!= perDel)
                                {
                                    newPerevod += s+" ";
                                }
                                //Console.WriteLine(s);

                            }
                            Console.WriteLine(newPerevod);
                        //}
                        splits = f.UA.Split(new char[] { });
                        split = newPerevod.Split(new char[] {});
                        Console.WriteLine("Размер нового сплита-> " + split.Length);
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
                        f.UA = n;
                        Console.WriteLine("Last chanse: " + f.UA);
                        Console.ReadKey();
                        break;
                    }
                }
            } while (vvod != 27);

    }
}