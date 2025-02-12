fun main(){
    try{
        println("Введите первое число")
       var first = readln()!!.toDouble()
        println("Введите второе число")
        var second = readln()!!.toDouble()
        println("Введите третье число")
        var three = readln()!!.toDouble()

        when{
            (first < second)&&(first > three)||(first > second)&&(first < three) -> println("Среднее число:" + first)
            (second < first)&&(second > three)||(second > first)&&(second < three) -> println("Среднее число:" + second)
            (three < first)&&(three > second)||(three > first)&&(three < second) -> println("Среднее число:" + three)
        }

    }
    catch(e:Exception){
        println("Неверный формат данных")
    }
}