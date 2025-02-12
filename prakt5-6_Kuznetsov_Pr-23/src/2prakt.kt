fun main(){
    try {
        println("Введите число от 0 до 9")
        val x = readln()!!.toInt()
        when(x){
            0 -> println("Ноль")
            1 -> println("Один")
            2 -> println("Два")
            3 -> println("Три")
            4 -> println("Четыре")
            5 -> println("Пять")
            6 -> println("Шесть")
            7 -> println("Семь")
            8 -> println("Восемь")
            9 -> println("Девять")
            else ->println("Неверное число")
        }
    }
    catch (e:Exception)
    {
        println("Неправильный формат данных")
    }
}