fun main(){
    try{
        println("Введите x")
        var x = readln()!!.toDouble()
        println("Введите y")
        var y = readln()!!.toDouble()
        println("Введите z")
        var z = readln()!!.toDouble()
        var a = 0.0
        var b = 0.0

        when {
            (x == 0.0) -> println("Делить на ноль нельзя!")
            (y == 0.0) -> println("Делить на ноль нельзя!")
            (z == 0.0) -> println("Делить на ноль нельзя!")
            else -> {
                a = (Math.sqrt(Math.abs(x - 1)) - Math.pow(Math.abs(y), 1.0 / 3.0)) / (1 + (Math.pow(x, 2.0) / 2) + (Math.pow(y, 2.0) / 4))
                b = x * Math.atan(z) + Math.pow(Math.E, -(x + 3))
                println("a = $a, b = $b")
            }
        }
    }catch(e:Exception){
        println("Некорректные данные")
    }
}