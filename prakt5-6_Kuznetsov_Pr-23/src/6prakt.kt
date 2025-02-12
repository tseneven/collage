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
        var col = p+ch+tr+dv;
        var oc = ((p * 5.0) + (ch * 4.0) + (tr * 3.0) + (dv * 2.0))
        var sr = 0.0
        when{
            (col > 0.0) -> sr = oc/col

        }
        println("Средний балл: "+ sr)
        println(2+2)
    }
    catch (e:Exception){
        println("Неверный формат данных")
    }
}