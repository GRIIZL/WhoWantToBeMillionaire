namespace Кто_хочет_стать_миллионером_НАСЛЕДОВВАНИЕ
{
    class Score
    {
        private decimal money;

        public Score(decimal money = 0)
        {
            this.money = money;
        }
        public void Increase()
        {
            if (money == 0)
            {
                money = 100;
            }
            else
            {
                money *= 2;
            }
        }

        public void ResetScore()
        {
            money = 0;
        } 

        public decimal Money { get { return money; } }
    }
}
