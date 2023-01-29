using System;

namespace Кто_хочет_стать_миллионером_НАСЛЕДОВВАНИЕ
{
    class User
    {
       public User(string name, decimal money=0)
        {
            this.name = name;
            this.score = new Score(money);
        }


        private Score score;
        private string name;


        
      

        public Score Score { get { return this.score; } }
        public string Name { get { return this.name; } }

        public int Choose(int max)
        {
            do
            {
                int choice = 0;
                Console.WriteLine("Номер вашего ответа?");
                string userInput = Console.ReadLine();
                if (int.TryParse(userInput, out choice) && choice <= max && choice>0)
                {
                    return choice;
                }
                else
                {
                    Console.WriteLine("Введите лигитимное число!");
                }
            }
            while (true);
        } 
    }
}
