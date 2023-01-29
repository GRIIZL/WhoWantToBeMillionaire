using System;
namespace Кто_хочет_стать_миллионером_НАСЛЕДОВВАНИЕ
{
    abstract class Answer
    {
       public Answer (string textAnswer)
        {
            if (textAnswer.StartsWith('+') || textAnswer.StartsWith('-'))
            {
                textAnswer = textAnswer[1..].Trim();
                
            }
            this.TextAnswer = textAnswer;
        }

        private string TextAnswer;

        public string textAnswer { get { return this.TextAnswer; } }
        public void Display(int number)
        {
            Console.WriteLine(number +")"+ this.TextAnswer);
        } 
    }
}
