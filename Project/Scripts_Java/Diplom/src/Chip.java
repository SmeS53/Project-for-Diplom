import java.time.LocalTime;
import java.util.Random;

// Класс, реализующий движение фишки по игровому полю
public class Chip implements RoundToNum {

    // Переменная игрового поля
    private final GameField _gameField;

    // Поля координат фишки
    private int _n, _k;

    // Конструктор класса
    public Chip(GameField gameField) {
        _gameField = gameField;
    }

    // Метод запуска движения фишки (начала игры)
    public void startPlay() {
        _n = 0;
        _k = 0;
        calcPath();
    }

    // Метод расчета пути движения фишки
    private void calcPath() {
        while (_n < _gameField._size) {
//            double rand = LocalTime.now().getNano()/Math.pow(10, 9);
            double rand = new Random().nextDouble();
            if (rand >= _gameField.getToIndex(_n, _k)) {
                _n++;
                _k++;
            }
            else {
                _n++;
            }
        }
    }

    // Метод "получить" для значения выигрыша из списка
    public double getWin() {
        return _gameField._winList.get(_k);
    }

    // Метод "получить" для _k
    public int getK() {
        return _k;
    }
}
