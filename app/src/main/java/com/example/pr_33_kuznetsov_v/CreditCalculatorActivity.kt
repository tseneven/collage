package com.example.pr_33_kuznetsov_v

import android.content.Intent
import android.os.Bundle
import android.widget.Button
import androidx.appcompat.app.AppCompatActivity

class CreditCalculatorActivity : AppCompatActivity(){
    override fun onCreate(savedInstanceState: Bundle?) {
        super.onCreate(savedInstanceState)
        setContentView(R.layout.credit_calculator_activity)
        val backButton : Button = findViewById(R.id.button_back)

        backButton.setOnClickListener{
            back()
        }
    }

    fun back(){
        var intent = Intent(this, MainActivity::class.java)
        startActivity(intent)
    }
}