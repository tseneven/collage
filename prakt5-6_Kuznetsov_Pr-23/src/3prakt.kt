fun main(){
    try {
        println("Введите число от 0 до 100")
        val x = readln()!!.toInt()
        when(x){
            in 0..2 -> println("Младенец")
            in 2..7 -> println("Ребенок")
            in 7..13 -> println("Школьник")
            in 13..18 -> println("Подросток")
            in 18..55 -> println("Взрослый")
            in 55..70 -> println("Престарелый")
            in 80..100 -> println("Старец")
            else ->println("Неверное число")
        }
    }
    catch (e:Exception)
    {
        println("Неправильный формат данных")
    }
}