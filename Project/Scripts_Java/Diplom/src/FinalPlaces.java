import java.util.ArrayList;

// Класс для расчета конечных вероятностей
public class FinalPlaces implements RoundToNum {

    // Переменная игрового поля
    private final GameField _field;

    // Переменная списка конечных вероятностей
    private final ArrayList<Double> _finalPlaces = new ArrayList<>();

    // Конструктор класса
    public FinalPlaces(GameField gameField) {
        _field = gameField;

        // Заполнение списка конечных вероятностей
        for (int k = 0; k <= _field._size; k++) {
            _finalPlaces.add(R(_field._size, k));
        }
    }

    // Метод "получить" для _finalPlaces
    public ArrayList<Double> getFinalPlaces() {
        return _finalPlaces;
    }

    // Рекуррентный метод для расчета вероятностей
    private double R(int n, int k) {

        if (n == 0) {
            return 1d;
        }
        if (n < k || n < 0 || k < 0) {
            return 0d;
        }
        if (k == 0) {
            return roundToNum(R(n-1, k) * q(n-1, k), 10);
        }
        if (n == k) {
            return roundToNum(R(n-1, k-1) * p(n-1, k-1), 10);
        }
        return roundToNum(R(n-1, k-1) * p(n-1, k-1) + R(n-1, k) * q(n-1, k), 10);
    }

    // Методы для получения соответствующих вероятностей из поля
    private double q(int n, int k) {
        return _field.getToIndex(n, k);
    }
    private double p(int n, int k) {
        return 1 - _field.getToIndex(n, k);
    }
}
