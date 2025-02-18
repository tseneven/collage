fun main(){
    try {
        println("Введите N")
        var N = readln()!!.toInt()

        println("Введите M")
        var M = readln()!!.toInt()
        var temp = 0
        var sum = 0

        when{
            (M > N) ->  {
                println("M больше N, произвожу смену местами")
                temp = M
                M = N
                N = temp
            }
        }

        when{
            (N < 0)||(M < 0) -> println("Индекс не может быть меньше нуля!")
            else -> {
                sum = (N - M + 1) * (N + M) / 2
                println("Сумма чисел от $M до $N равна $sum")
            }
        }
    }catch (e: Exception){
        println("неверный формат данных")
    }
}