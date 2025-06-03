import java.util.ArrayList;

// Класс для создания поля зоны
// Список значений, которые принимают ячейки зоны {-2, -1, 0, 1, 2}
public class ZoneField extends Field implements RoundToNum {

    // Переменная начального значения вероятности
    private final double _q = 0.5d;

    // Переменная коэффициента эффекта
    private final double _effect;
    
    // Переменная игрового поля
    protected GameField _gameField;

    // Переменная платы за игру
    public double pay;

    // Конструктор класса
    public ZoneField(GameField gameField) {
        super(gameField._size+1);

        _gameField = gameField;
        _effect = getEffect();

        // Начальная плата за игру (без модификаторов)
        pay = 4 + _size;

        create(0);
    }

    // Метод получения коэффициента эффекта
    // в зависимости от размера игрового поля
    private double getEffect() {
        return 0.4d/(_size - 2);
    }

    // Метод создания зоны с проверками наложения разных зон (зеленой или красной)
    public void setZone(int zN, int zK, int zoneType) {

        // zoneType - переменная типа зоны
        // 1 - это зеленая зона
        // -1 - это красная зона

        // zN, zK переменные начальной позиции зоны

        // Переменная стоимости зоны
        double _zonePrice = 10;
        // Формула для расчета платы за игру
        pay += (_gameField._size - zN) * _zonePrice;


        // Условия, при которых нельзя установить зону
        // с выводом предупреждения
        if (zN > _gameField._size-1 || zK > zN || zK < 0) {
            System.out.println(
                    """
                            
                    ==========================================
                    Зона не может быть установлена в это место
                    ==========================================
                    """);
            return;
        }

        // Основная работа метода
        for (int n = 0; n < _size; n++) {
            for (int k = 0; k <= n; k++) {

                // Проверка вертикального края зоны
                if (k == zK - 1 && n >= zN - 1) {
                    if (getToIndex(n, k) == 0d) {

                        // Проверка конечных ячеек,
                        // так как _gameField меньше по размеру
                        // Далее эта проверка будет везде
                        if (n != _gameField._size) {
                            _gameField.setToIndex(n, k, getMinOrMax(zoneType));
                        }
                        setToIndex(n, k, 2d * zoneType);
                    }
                    else if (getToIndex(n, k) == -2d * zoneType) {
                        // Проверка конечных ячеек
                        if (n != _gameField._size) {
                            _gameField.setToIndex(n, k, _q);
                        }
                        setToIndex(n, k, 0d);
                    }
                }

                // Проверка диагонального края зоны
                else if (k >= zK && zN - zK - n + k == 1) {
                    if (getToIndex(n, k) == 0d) {
                        // Проверка конечных ячеек
                        if (n != _gameField._size) {
                            _gameField.setToIndex(n, k, getMinOrMax(-zoneType));
                        }
                        setToIndex(n, k, 2d * zoneType);
                    }
                    else if (getToIndex(n, k) == -2d * zoneType) {
                        // Проверка конечных ячеек
                        if (n != _gameField._size) {
                            _gameField.setToIndex(n, k, _q);
                        }
                        setToIndex(n, k, 0d);
                    }
                }

                // Проверка внутри зоны и симметричной области
                else if ((k >= zK && zN - zK - n + k <= 0) ||
                        (k < zK && zN - zK - n + k > 0)) {
                    if (getToIndex(n, k) == (double) -zoneType) {
                        // Проверка конечных ячеек
                        if (n != _gameField._size) {
                            _gameField.setToIndex(n, k, _q);
                        }
                        setToIndex(n, k, 0d);
                    }
                    else if (getToIndex(n, k) != (double) zoneType) {
                        // Проверка конечных ячеек
                        if (n != _gameField._size) {
                            refreshGameField(n, k);
                        }
                        setToIndex(n, k, zoneType);
                    }
                }
            }
        }
        // Создание конечных вероятностей после применения зоны
        _gameField.createFinalPlaces();

        // Создание списка выигрышей после применения зоны
        _gameField.createWinList(this, zN, zoneType);
    }

    // Метод обновления значения вероятности игрового поля
    private void refreshGameField(int n, int k) {

        // Переменная поля движения в ячейке (n, k)
        double mf = 2*k - n;

        _gameField.setToIndex(n, k, roundToNum(_q + mf * _effect, 3));
    }

    // Метод получения min или max значения вероятности
    // на границе зоны в зависимости от типа зоны
    private double getMinOrMax(double value) {
        double _min = 0.1d;
        if (value == 1d) {
            return 1 - _min;
        }
        return _min;
    }

    // Вывод зоны в виде значений
    public void printValue() {
        System.out.println("\nValue Zone Field");
        for (ArrayList<Double> zFieldArr : _field) {
            for (double zField : zFieldArr) {
                if (zField < 0) {
                    System.out.print("\b" + (int) zField + "  ");
                }
                else {
                    System.out.print((int) zField + "  ");
                }
            }
            System.out.println();
        }
    }

    // Вывод зоны в виде "+-0"
    public void print() {
        System.out.println("\nZone Field");
        for (ArrayList<Double> zFieldArr : _field) {
            for (double zField : zFieldArr) {
                if (zField == -1d) {
                    System.out.print("-  ");
                }
                else if (zField == 1d) {
                    System.out.print("+  ");
                }
                else {
                    System.out.print("0  ");
                }
            }
            System.out.println();
        }
    }
}
