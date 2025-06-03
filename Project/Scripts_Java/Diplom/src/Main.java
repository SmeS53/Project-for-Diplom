import java.util.ArrayList;

// Основной класс программы
public class Main {
    public static void main(String[] args) {

        // Задаем размер игрового поля
        // Размер может быть следующим:
        // size = {5, 6, 7, 8, 9, 10}
        int size = 5;

        // Создаем игровое поле с указанным размером
        GameField gameField = new GameField(size);

        // Создаем зону как объект на основе игрового поля
        ZoneField zoneField = new ZoneField(gameField);

        // Устанавливаем зеленую зону в (2, 1)
        zoneField.setZone(2, 1, 1);

        // Устанавливаем красную зону в (3, 2)
        zoneField.setZone(3, 2, -1);

        // Выводим значения зоны
        // и зону в виде "+-0"
        zoneField.printValue();
        zoneField.print();

        // Выводим значения поля,
        // значения вероятностей в конечных ячейках
        // и значения выигрышей
        gameField.print();
        gameField.printFinalPlaces();
        gameField.printWinList();





        // Симуляция

        // Создаем новое игровое поле
        GameField gameField2 = new GameField(5);

        // Создаем новое поле зоны
        ZoneField zoneField2 = new ZoneField(gameField2);

        // Устанавливаем зеленую зону в (2, 1)
        zoneField2.setZone(2, 1, 1);

        // Создаем фишку как объект с указанием игрового поля
        Chip chip = new Chip(gameField2);
        
        // Создаем массив для результатов игр
        ArrayList<ArrayList<Integer>> countGame = new ArrayList<>();
        countGame.add(new ArrayList<>());
        countGame.add(new ArrayList<>());

        // Основа моделирования
        // Производится 10 000 симуляций в каждой из которых происходит 1000 игр подряд
        for (int i = 0; i < 10000; i++) {
            // Начальное количество монет
            double cash = 500;
            // Стоимость игры
            double pay = zoneField2.pay;
            // Цикл для реализации "бросков" фишки
            int game = 0;
            for (; game < 1000 && cash >= pay; game++) {
                cash -= pay;
                chip.startPlay();
                cash += chip.getWin();
            }
            // Запись данных об игре в массив
            if (game > 150) {
                if (countGame.get(0).size() == 0) {
                    countGame.get(0).add(game);
                    countGame.get(1).add(1);
                }

                if (!countGame.get(0).contains(game)) {
                    countGame.get(0).add(game);
                    countGame.get(1).add(1);
                } else {
                    countGame.get(1).set(countGame.get(0).indexOf(game), countGame.get(1).get(countGame.get(0).indexOf(game)) + 1);
                }
            }
        }

        // Вывод полученных данных
        System.out.println("\nКоличество игр в одной симуляции:\n" + countGame.get(0));
        System.out.println("\nКоличество симуляций:\n" + countGame.get(1));
    }
}