package com.example.kuznetsovaa_01_04

import android.content.Intent
import android.os.Bundle
import android.widget.TextView
import androidx.appcompat.app.AppCompatActivity
import androidx.appcompat.widget.AppCompatButton

class ResultActivity() : AppCompatActivity() {


    override fun onCreate(savedInstanceState: Bundle?) {
        super.onCreate(savedInstanceState)
        setContentView(R.layout.activity_result)
        val sum : String? = intent.getStringExtra("sum")
        val srok: String? = intent.getStringExtra("srok")
        val result: String? = intent.getStringExtra("result")

        val resultButton : AppCompatButton = findViewById(R.id.regButton)

        resultButton.setOnClickListener{
            back()
        }


        val sumText : TextView = findViewById(R.id.sum)
        val srokText : TextView = findViewById(R.id.srock)
        val resultText : TextView = findViewById(R.id.resultEdit)


        sumText.text = sum
        srokText.text = srok
        resultText.text = result

    }

    fun back(){
        var intent = Intent(this, BankActivity::class.java)
        startActivity(intent)
    }

}