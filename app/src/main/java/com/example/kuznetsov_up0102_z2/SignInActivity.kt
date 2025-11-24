package com.example.kuznetsov_up0102_z2

import android.content.Intent
import android.content.SharedPreferences
import androidx.appcompat.app.AppCompatActivity
import android.os.Bundle
import android.widget.EditText
import androidx.appcompat.widget.AppCompatButton

class SignInActivity : AppCompatActivity() {
    lateinit var but : AppCompatButton
    lateinit var email : EditText
    lateinit var pas : EditText
    lateinit var sp : SharedPreferences
    override fun onCreate(savedInstanceState: Bundle?) {
        super.onCreate(savedInstanceState)
        setContentView(R.layout.activity_sign_in)
        but = findViewById(R.id.log_but)
        email = findViewById(R.id.email_edit)
        pas = findViewById(R.id.pas_edit)
        sp = getSharedPreferences("userData", MODE_PRIVATE)

        but.setOnClickListener{
            action()
        }

    }
    fun action(){
        if(email.text.toString() == "" || pas.text.toString() == ""){
            return
        }
        val editor = sp.edit()
        editor.putString("email", email.text.toString())
        editor.putString("password", pas.text.toString())
        editor.apply()
        var intent : Intent = Intent(this, mainScrean::class.java)
        startActivity(intent)
    }
}