fun main(){
    try {
        println("Введите кол-во пятерок")
        var p = readln()!!.toDouble()
        println("Введите кол-во четверок")
        var ch = readln()!!.toDouble()
        println("Введите кол-во троек")
        var tr = readln()!!.toDouble()
        println("Введите кол-во двоек")
        var dv = readln()!!.toDouble()
        var sr = ((p * 5.0) + (ch * 4.0) + (tr * 3.0) + (dv * 2.0))/(p+ch+tr+dv)
        println("Средний балл: "+ sr)
        println(2+2)
    }
    catch (e:Exception){
        println("Неверный формат данных")
    }
}