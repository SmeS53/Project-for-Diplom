using System;
using System.Collections.Generic;

namespace Assets.Scripts
{
    // Класс для сохранения и обработки значений игрового поля.
    // Наследует класс Поля
    public class GameField : Field
    {

        // Переменная списка конечных вероятностей
        private List<double> _finalCells;

        // Переменная списка выигрышей
        internal List<int> _winList;

        // Переменные минимального и максимального значения выигрыша
        private int winMax = 10000;
        private int winMin = -100;

        // Конструктор класса
        public GameField(int size) : base(size)
        {
            // Начальное значение вероятности
            double q = 0.5d;
            create(q);

            _finalCells = new List<double>();
            createFinalCells();

            createWinList();
        }

        // Метод создания (заполнения) списка конечных значений вероятностей _finalCells
        internal void createFinalCells()
        {
            _finalCells = new FinalCells(this).getFinalCells();
        }

        // Методы создания списка выигрышей
        // Значения без зоны
        private void createWinList()
        {
            _winList = new List<int>();

            for (int k = 0; k <= _size; k++)
            {

                // Значение выигрыша
                // обратно пропорциональное вероятности попадания в ячейку
                int win = (int)Math.Round(1d / _finalCells[k], 0, MidpointRounding.AwayFromZero);

                win = winMinMax(win);
                _winList.Add(win);
            }
        }
        // Значения с зоной
        internal void createWinList(ZoneField zoneField, int n, int zone)
        {
            _winList = new List<int>();

            int size = zoneField._size - 1;
            for (int k = 0; k <= size; k++)
            {

                // Значение выигрыша
                // обратно пропорциональное вероятности попадания в ячейку
                int win = (int)Math.Round(1d / _finalCells[k], 0, MidpointRounding.AwayFromZero);

                // Изменение значения выигрыша в зависимости от зоны
                // Коэффициенты прибавки
                // 1 ==> 20
                // -1 ==> -5
                if (zoneField.getToIndex(size, k) == (double)zone)
                {
                    int newWin = (int)(win + (25 * zone + 15) / 2d * (_size - 1 - n) * n);
                    newWin = winMinMax(newWin);
                    _winList.Add(newWin);
                }
                else
                {
                    win = winMinMax(win);
                    _winList.Add(win);
                }
            }
        }

        // Проверка значение выигрыша
        private int winMinMax(int win)
        {
            // Если значение выигрыша выходит за границы,
            // то возвращается значение ближайшей границы
            // Иначе, само значение
            if (win < winMin)
            {
                return winMin;
            }
            else if (win > winMax)
            {
                return winMax;
            }
            return win;
        }
    }
}
