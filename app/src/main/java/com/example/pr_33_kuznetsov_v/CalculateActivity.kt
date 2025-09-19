package com.example.pr_33_kuznetsov_v

import android.content.Intent
import android.os.Bundle
import android.widget.TextView
import androidx.appcompat.app.AppCompatActivity
import androidx.appcompat.widget.AppCompatButton

class CalculateActivity() : AppCompatActivity() {


    override fun onCreate(savedInstanceState: Bundle?) {
        super.onCreate(savedInstanceState)
        setContentView(R.layout.calculate)
        val sum : String? = intent.getStringExtra("sum")
        val srok: String? = intent.getStringExtra("srok")
        val result: String? = intent.getStringExtra("result")

        val resultButton : AppCompatButton = findViewById(R.id.buttonResult)

        resultButton.setOnClickListener{
            back()
        }


        val sumText : TextView = findViewById(R.id.sum)
        val srokText : TextView = findViewById(R.id.srock)
        val resultText : TextView = findViewById(R.id.mPlat)


        sumText.text = sum
        srokText.text = srok
        resultText.text = result

    }

    fun back(){
        var intent = Intent(this, MainActivity::class.java)
        startActivity(intent)
    }

}