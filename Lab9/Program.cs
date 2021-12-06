using System;

namespace Lab9
{
    class Program
    {
        static void Print<T>(DynamicList<T> dynamicList)
        {
            var j = 0;
            foreach (var i in dynamicList)
            {
                Console.WriteLine("dynamicList[{0}] = {1}", j++, i);
            }

            Console.WriteLine("dynamicList.Count = {0}", dynamicList.Count);
        }

        static void Main(string[] args)
        {
            // Инициализировать
            var dynamicList = new DynamicList<int>();
            for (int i = 0; i < 50; i++)
            {
                dynamicList.Add(i);
            }

            // Вывести
            Console.WriteLine("После инициализации");
            Print(dynamicList);
            // Удалить несколько последних элементов
            dynamicList.Remove();
            dynamicList.Remove();
            dynamicList.Remove();
            // Вывести
            Console.WriteLine("После удаления с конца");
            Print(dynamicList);
            // Удалить по индексу
            dynamicList.RemoveAt(4);
            dynamicList.RemoveAt(5);
            // Вывести
            Console.WriteLine("После удаления по индексу");
            Print(dynamicList);
            // Очистить
            dynamicList.Clear();
            // Вывести
            Console.WriteLine("После очистки");
            Print(dynamicList);
        }
    }
}