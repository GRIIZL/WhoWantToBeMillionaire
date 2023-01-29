using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;

namespace Кто_хочет_стать_миллионером_НАСЛЕДОВВАНИЕ
{
    public class Game
    {

        private User user;
        Collection<Question> questions;

        public void StartGame()
        {
            TryLoadGame();
            if(user == null)
            {
                PrintRules();
                AskName();
            }
            InitQuestions();
            AskQuestion();
        }

        private void TryLoadGame()
        {
            Console.WriteLine("Хотите загрузить сохранение? (y/n)");
            do
            {
                string userChoice = Console.ReadLine();
                if (userChoice == "y" || userChoice == "n")
                {
                    if (userChoice == "y")
                    {
                        LoadGame();
                        Console.WriteLine("Файл загружен успешно!");
                        break;
                    }
                    else { return; }
                }
                else
                {
                    Console.WriteLine("Вы ввели неправильно! Пожалуйста введите y или n ");
                }
            } while (true);
        }
        private void PrintRules()
        {
            Console.WriteLine(FileHelper.ReadTextFromFile("C:\\Users\\нива20\\source\\C#\\" +
                "dz\\Кто+хочет+стать+миллионером+НАСЛЕДОВВАНИЕ\\" +
                "Кто+хочет+стать+миллионером+НАСЛЕДОВВАНИЕ\\" +
                "GameRules.txt"));
        }
        private void AskName()
        {
            Console.WriteLine("Как вас зовут?");
            user = new User(Console.ReadLine());
        }
        private void InitQuestions()
        {

            string allQuestionsText = FileHelper.ReadTextFromFile("C:\\Users\\нива20\\source\\C#\\" +
                "dz\\Кто+хочет+стать+миллионером+НАСЛЕДОВВАНИЕ\\" +
                "Кто+хочет+стать+миллионером+НАСЛЕДОВВАНИЕ\\" +
                "Questions.txt");
            string[] lines = allQuestionsText.Split(Environment.NewLine);
            this.questions = new Collection<Question>();
            Collection<Answer> answers = new Collection<Answer>();
            string currentQuestion = string.Empty;


            foreach (string line in lines)
            {
                if (string.Equals(line, string.Empty))
                {
                    if (!string.IsNullOrEmpty(currentQuestion) && answers.Count > 0)
                    {
                        
                        questions.Add(new Question(currentQuestion, answers.ToArray()));
                    }
                    continue;
                }
                if (line.StartsWith('+') || line.StartsWith('-'))
                {
                    if (string.IsNullOrEmpty(currentQuestion))
                    {
                        continue;
                    }
                    if (line.StartsWith('+'))
                    {
                        answers.Add(new RightAnswer(line));
                    }
                    else
                    {
                        answers.Add(new WrongAnswer(line));
                    }
                }
                else
                {
                    currentQuestion = line;
                    answers.Clear();
                }
            }

            if (!string.IsNullOrEmpty(currentQuestion) && answers.Count > 0)
            {               
                questions.Add(new Question(currentQuestion, answers.ToArray()));               
            }

        }
        private void AskQuestion()
        {
            int i = 0;
            int questionsToSkip = QuestionsToSkip();
            i = questionsToSkip;
            bool canSaveGame = false;
            foreach (Question question in questions)
            {
                if (questionsToSkip>0)
                {
                    questionsToSkip--;                    
                    continue;
                }
                
                if (canSaveGame)
                {
                    TrySaveGame();
                }
                
                question.Display();
                int userChoice = user.Choose(question.Answers.Length);
                if (question.IsCorrect(userChoice))
                {
                    user.Score.Increase();
                    canSaveGame = true;
                        i++;
                    if (questions.Count == i)
                    {
                        break;
                    }
                    Console.WriteLine("Ответ верный!!! Продолжайте в том же духе!!! Ваш вигрыш Состовляет: " + user.Score.Money + "Byn");

                }
                else
                {
                    user.Score.ResetScore();
                    Console.WriteLine("К сожалению ответ неверный. Приходите в другой раз.!");
                    return;
                }
            }
            Console.WriteLine("Вы ответили на все вопросы! Ваш вигрыш Составляет: " + user.Score.Money + "Byn");
        }

        
        
        private void TrySaveGame()
        {
            Console.WriteLine($"Не жеалете ли сохранить текущий прогресс?, {user.Name}?(y/n)");
            do
            {
                string userChoice = Console.ReadLine();
                if (userChoice == "y" || userChoice == "n")
                {
                    if(userChoice == "y")
                    {
                        SaveGame();
                        Console.WriteLine("Файл сохранен успешно!");
                        break;
                    }
                    else { return; }
                }
                else
                {
                    Console.WriteLine("Вы ввели неправильно! Пожалуйста введите y или n ");
                }
            } while (true);
        }
        private bool ValidFileName(string filename)
         {
            bool valid = true;
            List<string> Pattern = new List<string> { "^", "<", ">", ";", "|", "'", "/", ",", "\\", ":", "=", "?", "\"", "*" };
              for (int i = 0; i < Pattern.Count; i++)
              {
                if (filename.Contains(Pattern[i]))
                {
                    valid = false;
                    Console.WriteLine("Вы ввели недопустимые символы! Пожалуйста повторите попытку");
                    break;
                }
              }
                return valid;   
         }
        private int QuestionsToSkip()
        {
            int skipQuestions = 0;
            Score counter = new Score();
            if (user.Score.Money > 0)
            {
                do
                {
                    skipQuestions++;
                    counter.Increase();
                } while (counter.Money < user.Score.Money);
            }
            return skipQuestions;
        }
        private int AskUser(int max)
        {
            do
            {
                int choice = 0;
                Console.WriteLine("Введите номер вашего сохранения: ");
                string userInput = Console.ReadLine();
                if (int.TryParse(userInput, out choice) && choice <= max && choice > 0)
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
        private void LoadGame()
        {
            DirectoryInfo directory = new DirectoryInfo(FileHelper.GetDirectoryName());
            FileInfo[] saves = directory.GetFiles("*.txt");
            for (int i = 0; i < saves.Length; i++)
            {
                Console.WriteLine((i+1) + ")" + saves[i].Name);
            }
            FileInfo chosenFile = saves[AskUser(saves.Length) - 1];
            string content = File.ReadAllText(chosenFile.FullName);
            string[] parts = content.Split(Environment.NewLine);
            user = new User(parts[0], decimal.Parse(parts[1]));
        }
        private void SaveGame()
        {
            Console.WriteLine("Введите имя сохранения: ");
            do
            {             
                string fileName = Console.ReadLine();
                string fullFileName = Path.Combine(FileHelper.GetDirectoryName(), fileName + ".txt");
                if (ValidFileName(fileName))
                {
                    File.WriteAllText(fullFileName,$"{user.Name}{Environment.NewLine}{user.Score.Money}");
                    return;
                }
            } while (true);
        }
    }
}
