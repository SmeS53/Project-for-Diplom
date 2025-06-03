import java.util.ArrayList;

// Класс для сохранения и обработки значений игрового поля.
// Наследует класс Поля
public class GameField extends Field implements RoundToNum {

    // Переменная списка конечных вероятностей
    private ArrayList<Double> _finalPlaces;

    // Переменная списка выигрышей
    protected ArrayList<Double> _winList;

    // Числовой коэффициент для оптимального значения выигрыша
//    private final double mult = Math.PI/2d;

    // Конструктор класса
    public GameField(int size) {
        super(size);

        // Начальное значение вероятности
        double q = 0.5d;
        create(q);

        _finalPlaces = new ArrayList<>();
        createFinalPlaces();

        createWinList();
    }

    // Метод создания (заполнения) списка конечных значений вероятностей _finalPlaces
    protected void createFinalPlaces() {
        _finalPlaces = new FinalPlaces(this).getFinalPlaces();
    }

    // Методы создания списка выигрышей
    // Значения без зоны
    private void createWinList() {
        _winList = new ArrayList<>();

        for (int k = 0; k <= _size; k++) {

            // Значение выигрыша
            // обратно пропорциональное вероятности попадания в ячейку
            double win = roundToNum(1d/_finalPlaces.get(k), 0);

            _winList.add(win);
        }
    }
    // Значения с зоной
    protected void createWinList(ZoneField zoneField, int n, int zone) {
        _winList = new ArrayList<>();

        int size = zoneField._size-1;
        for (int k = 0; k <= size; k++) {

            // Значение выигрыша
            // обратно пропорциональное вероятности попадания в ячейку
            double win = roundToNum(1d/_finalPlaces.get(k), 0);

            // Изменение значения выигрыша в зависимости от зоны
            // Коэффициенты прибавки
            // 1 ==> 20
            // -1 ==> -5
            if (zoneField.getToIndex(size, k) == (double) zone) {
                _winList.add(win + (25 * zone + 15) / 2d * (_size - 1 - n) * n);
            }
            else {
                _winList.add(win);
            }
        }
    }

    // Вывод значений игрового поля
    public void print() {
        System.out.println("\nGame Field");
        for (ArrayList<Double> field : _field) {
            System.out.println(field);
        }
    }

    // Вывод конечных вероятностей
    public void printFinalPlaces() {
        System.out.println("\nFinalPlaces\n" + _finalPlaces);
    }

    // Вывод списка выигрышей
    public void printWinList() {
        System.out.println("\nWinList\n" + _winList);
    }
}
