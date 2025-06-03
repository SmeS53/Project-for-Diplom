using System.Collections.Generic;

namespace Assets.Scripts
{
    // Родительский класс Поля
    // Содержит основные переменные и методы поля
    public class Field
    {

        // Переменная размера поля
        // Размер может быть следующим:
        // size = {5, 6, 7, 8, 9, 10}
        internal int _size;

        // Переменная поля, как список списков
        internal List<List<double>> _field;
        

        // Конструктор класса
        public Field(int size)
        {
            _size = size;
            _field = new List<List<double>>();
        }

        // Метод создания поля со значениями value
        internal void create(double value)
        {
            for (int n = 0; n < _size; n++)
            {
                _field.Add(new List<double>());
                for (int k = 0; k <= n; k++)
                {
                    _field[n].Add(value);
                }
            }
        }

        // Методы "получить", "установить" для значений поля
        public double getToIndex(int n, int k)
        {
            return _field[n][k];
        }
        internal void setToIndex(int n, int k, double value)
        {
            _field[n][k] = value;
        }

    }
}
