using System;
using System.Collections.Generic;

namespace Assets.Scripts
{
    // Класс для расчета конечных вероятностей
    public class FinalCells
    {
        // Переменная игрового поля
        private GameField _field;

        // Переменная списка конечных вероятностей
        private List<double> _finalCells = new List<double>();

        // Конструктор класса
        public FinalCells(GameField gameField)
        {
            _field = gameField;

            // Заполнение списка конечных вероятностей
            for (int k = 0; k <= _field._size; k++)
            {
                _finalCells.Add(R(_field._size, k));
            }
        }

        // Метод "получить" для _finalCells
        public List<double> getFinalCells()
        {
            return _finalCells;
        }

        // Рекуррентный метод для расчета вероятностей
        private double R(int n, int k)
        {

            if (n == 0)
            {
                return 1d;
            }
            if (n < k || n < 0 || k < 0)
            {
                return 0d;
            }
            if (k == 0)
            {
                return Math.Round(R(n - 1, k) * Q(n - 1, k), 10, MidpointRounding.AwayFromZero);
            }
            if (n == k)
            {
                return Math.Round(R(n - 1, k - 1) * P(n - 1, k - 1), 10, MidpointRounding.AwayFromZero);
            }
            return Math.Round(R(n - 1, k - 1) * P(n - 1, k - 1) + R(n - 1, k) * Q(n - 1, k), 10, MidpointRounding.AwayFromZero);
        }

        // Методы для получения соответствующих вероятностей из поля
        private double Q(int n, int k)
        {
            return _field.getToIndex(n, k);
        }
        private double P(int n, int k)
        {
            return 1 - _field.getToIndex(n, k);
        }
    }
}
