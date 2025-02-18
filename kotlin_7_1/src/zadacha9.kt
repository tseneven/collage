fun main(){
    try {
        println("Введите a")
        var a = readln()!!.toDouble()
        println("Введите b")
        var b = readln()!!.toDouble()
        println("Введите c")
        var c = readln()!!.toDouble()

        val p = (a + b + c) / 3
        println("Среднее арифметическое значение = $p")

        val abc = a * b * c
        val q = Math.pow(abc, 1.0 / 3.0)
        println("Среднее геометрическое значение = $q")
    }catch (e: Exception){
        println("Некорректные данные")
    }
}