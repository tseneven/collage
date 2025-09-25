package com.example.pr2

import android.content.Context.PRINT_SERVICE
import android.content.SharedPreferences
import android.graphics.Color
import androidx.appcompat.app.AppCompatActivity
import android.os.Bundle
import android.util.Log
import android.view.View
import android.widget.TextView
import androidx.appcompat.widget.AppCompatButton
import com.google.android.material.floatingactionbutton.FloatingActionButton
import com.google.android.material.snackbar.Snackbar

class MainActivity : AppCompatActivity() {

    lateinit var settings: SharedPreferences
    lateinit var enums: Enums
    lateinit var chet : TextView
    lateinit var value : String

    override fun onCreate(savedInstanceState: Bundle?) {
        super.onCreate(savedInstanceState)
        setContentView(R.layout.activity_main)

        enums = Enums.default

        // Инициализация SP
        settings = getSharedPreferences("UserData", MODE_PRIVATE)

        // Поиск виджетов по id
        chet = findViewById(R.id.chet)

        // Подгрузка из SP
        val valueInSP = settings.getInt("value", 0)

        chet.text = valueInSP.toString()

    }

    fun ClickFAB(view:View){
        value = chet.text.toString()

        Log.d("TAG", enums.toString())

        var valueInt : Int = value.toInt()

        valueInt += 1

        val editor = settings.edit()

        editor.putInt("value", valueInt)
        editor.apply()

        chet.text = valueInt.toString()

        when(enums){
            Enums.default -> Snackbar.make(view, "+1", Snackbar.LENGTH_LONG).show()
            Enums.withButton -> {
                val snack = Snackbar.make(view, "+1", Snackbar.LENGTH_LONG)
                snack.setAction("Отменить", View.OnClickListener {
                    valueInt -=1
                    editor.putInt("value", valueInt)
                    editor.apply()
                    chet.text = valueInt.toString()
                }).show()
            }

            Enums.undefault -> {
                val snack = Snackbar.make(view, "+1", Snackbar.LENGTH_LONG)
                snack.setBackgroundTint(Color.WHITE)
                snack.setTextColor(Color.BLACK)
                snack.setAction("Отменить", View.OnClickListener {
                    valueInt -=1
                    editor.putInt("value", valueInt)
                    editor.apply()
                    chet.text = valueInt.toString()
                }).show()
            }
        }
    }

    fun ClickDefault(view: View){
        Snackbar.make(view, "Нажата ДЕФОЛТ кнопка", Snackbar.LENGTH_LONG).show()
        enums = Enums.default
    }


    fun ClickWithButton(view: View){
        var snackbar = Snackbar.make(view, "Нажата C КНОПКОЙ кнопка", Snackbar.LENGTH_LONG)
        snackbar.setAction("Назад", View.OnClickListener { ClickDefault(view)}).show()
        enums = Enums.withButton
    }

    fun ClickUndefault(view: View){
        var snackbar = Snackbar.make(view, "Нажата НЕДЕФОЛТ кнопка", Snackbar.LENGTH_LONG)
        snackbar.setBackgroundTint(Color.WHITE)
        snackbar.setTextColor(Color.BLACK)
        snackbar.setAction("Назад", View.OnClickListener { ClickDefault(view)}).show()
        enums = Enums.undefault
    }
}