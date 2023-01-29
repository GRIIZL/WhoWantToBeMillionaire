using System;

namespace Кто_хочет_стать_миллионером_НАСЛЕДОВВАНИЕ
{
    class Question
    {
       
       public Question(string text, Answer[] answers)
        {
            this.TextQuestion = text;
            this.answers = answers;
        }
        private string TextQuestion;
        private Answer[] answers; 




        public string textQuestion {get { return this.TextQuestion; } }
        public Answer[] Answers { get { return this.answers; } }

        public void Display()
        {
            Console.WriteLine(TextQuestion);
            for (int i = 0; i < answers.Length; i++)
            {
                answers[i].Display(i + 1);
            }
            
        }
        public bool IsCorrect(int userChoice)
        {
            return answers[userChoice - 1] is RightAnswer;
            
        }
    }
}
