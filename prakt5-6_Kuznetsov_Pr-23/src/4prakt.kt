fun main(){
    try {
        println("Введите число")
        val x = readln()!!.toDouble()
        var f = 0.0
        when{
            (x>=1) -> f = Math.sin(Math.pow(x, 2.0) + (1/x))
            (x>-3)&&(x<1) -> f = Math.pow(x, 2.0)
            (x<=-3) -> f = Math.sqrt(Math.abs(Math.pow(x,2.0)-2))
        }
        println("F(x)= "+f)
    }
    catch (e:Exception)
    {
        println("Неправильный формат данных")
    }
}