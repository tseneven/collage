package com.example.kuznetsovaa_01_04

import android.content.Intent
import androidx.appcompat.app.AppCompatActivity
import android.os.Bundle
import android.util.Log
import android.widget.EditText
import android.widget.Toast
import androidx.appcompat.widget.AppCompatButton
import com.google.android.material.snackbar.Snackbar


class BankActivity : AppCompatActivity() {

    private lateinit var log : EditText
    private lateinit var pas : EditText
    private lateinit var but : AppCompatButton
    override fun onCreate(savedInstanceState: Bundle?) {
        super.onCreate(savedInstanceState)
        setContentView(R.layout.activity_bank)
        but = findViewById(R.id.log_button)
        but.setOnClickListener {
            action()
        }

    }

    private fun action(){
        log = findViewById(R.id.log_editText)
        pas = findViewById(R.id.pas_editText)

        if(log.text.toString() == "" || pas.text.toString() == "") {

            var toast : Toast = Toast.makeText(this, "Нужно ввести логин и пароль!", Toast.LENGTH_SHORT)
            toast.show()
            return
        }

        val intent = Intent(this, CreditCalcActivity::class.java)
        startActivity(intent)
    }
}