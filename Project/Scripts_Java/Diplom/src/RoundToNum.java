// Интерфейс с методом для округления числа
public interface RoundToNum {

    // Метод округления числа до n знаков после запятой
    default double roundToNum(double value, int num) {
        return Math.round(value*Math.pow(10, num))/Math.pow(10, num);
    }
}
