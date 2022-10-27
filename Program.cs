using System;
TranslateDictionary td = new TranslateDictionary();

char vvod;
do
{
    Console.Clear();
    Console.WriteLine("1 - Создание словаря");
    Console.WriteLine("2 - Добавление");
    Console.WriteLine("3 - Вывод словника");
    Console.WriteLine("4 - Поиск");
    Console.WriteLine("5 - Поиск по слогану для фамилии");
    Console.WriteLine("6 - Посмотреть книгу");
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
                td.search();
                Console.ReadKey();
                break;
            }
        case '5':
            {
                Console.Clear();
             
                Console.ReadKey();
                break;
            }
        case '6':
            {
                Console.Clear();
              
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
    public string UA { get; set; }
    public string ENG { get; set; }
    
}
class TranslateDictionary
{
    public List<Word>? words { get; }
    public TranslateDictionary()
    {
        words = new List<Word>();
    }
    public void print()
    {
        var WPua = from word in words
                 orderby word.ENG
                 select word;
        foreach(var word in WPua)
        {
            Console.WriteLine(word.ENG + "\t" + word.UA);
        }
    }
    public void search()
    {
        Console.WriteLine("Введите искомое слово на ??? языке");
        string slovosearch = Console.ReadLine();
        var search = from word in words
                     where word.ENG == slovosearch
                     select word;

        foreach(var word in search)
        {
            Console.WriteLine(word.ENG + "\t" + word.UA);
        }

        var search2 = from word2 in words
                     where word2.ENG != slovosearch
                     select word2;
        foreach(var word2 in search2)
        {
            Console.WriteLine("Совпадений нет.");
        }


    }

}