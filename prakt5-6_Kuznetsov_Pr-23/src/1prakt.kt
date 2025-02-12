fun main() {
        try {
            println("Введите первое число")
            var x = readLine()!!.toDouble()
            println("Введите второе число")
            var y = readLine()!!.toDouble()
            when{
                (x>y) -> x+1
                (y>x) -> y+1
            }
            when{
                (x==y) -> x = Math.pow(x, 3.0)
                (x!=y) -> println("соответствующее сообщение")
            }
        }catch (e:Exception){
            println("Неверный формат данных")
        }
    }
