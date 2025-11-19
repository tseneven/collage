package com.example.kuznetsovaa_01_04

import android.content.Intent
import android.os.Bundle
import android.util.Log
import android.widget.EditText
import android.widget.SeekBar
import android.widget.SeekBar.OnSeekBarChangeListener
import android.widget.Toast
import androidx.appcompat.app.AppCompatActivity
import androidx.appcompat.widget.AppCompatButton


class CreditCalcActivity : AppCompatActivity() {
    lateinit var result : AppCompatButton
    override fun onCreate(savedInstanceState: Bundle?) {
        super.onCreate(savedInstanceState)
        setContentView(R.layout.activity_kreditcalc)

        result = findViewById(R.id.resultButton)

        result.setOnClickListener{
            calculate()
        }
    }

    fun calculate() {
        val seekBar : SeekBar = findViewById(R.id.seekBar)
        val srok : EditText = findViewById(R.id.srockEdit)

        if(srok.text.toString() != ""){
            val srokText : String = srok.text.toString()
            val Srock: Int = srokText.toInt()

            val sum : String = seekBar.progress.toString()

            val sumInt : Int = sum.toInt()
            var res : String = ""
            if (Srock <= 12) {
                val s1 = (sumInt / Srock) + (sumInt * 0.059)
                res = s1.toString()
            } else if (Srock > 12 && Srock <= 24) {
                val s1 = (sumInt / 12) + (sumInt * 0.059)
                val additionalPayment = (sumInt / Srock) + ((sumInt - (sumInt / 12 * 12)) * 0.051)
                res = (s1 + additionalPayment).toString()
            } else {
                val s1 = (sumInt / 12) + (sumInt * 0.059)
                val additionalPayment = (sumInt / Srock) + ((sumInt - (sumInt / 12 * 24)) * 0.042)
                res = (s1 + additionalPayment).toString()
            }

            val intent = Intent(this, ResultActivity:: class.java).apply {
                putExtra("sum", sum)
                putExtra("srok", Srock.toString())
                putExtra("result", res)
            }
            startActivity(intent)

        }
        else{
            var toast : Toast = Toast.makeText(this, "Ошибка, не все поля заполнены", Toast.LENGTH_LONG)
            toast.show()
        }
    }
}


