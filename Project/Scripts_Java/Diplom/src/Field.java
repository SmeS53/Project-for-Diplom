import java.util.ArrayList;

// Родительский класс Поля.
// Содержит основные переменные и методы поля
public class Field {

    // Переменная размера поля
    // Размер может быть следующим:
    // size = {5, 6, 7, 8, 9, 10}
    protected int _size;

    // Переменная поля, как список списков
    protected ArrayList<ArrayList<Double>> _field;

    // Конструктор класса
    public Field(int size) {
        _size = size;
        _field = new ArrayList<>();
    }

    // Метод создания поля со значениями value
    protected void create(double value) {

        for (int n = 0; n < _size; n++) {
            _field.add(new ArrayList<>());
            for (int k = 0; k <= n; k++) {
                _field.get(n).add(value);
            }
        }
    }

    // Методы "получить", "установить" для значений поля
    public double getToIndex(int n, int k) {
        return _field.get(n).get(k);
    }
    protected void setToIndex(int n, int k, double value) {
        _field.get(n).set(k, value);
    }
}