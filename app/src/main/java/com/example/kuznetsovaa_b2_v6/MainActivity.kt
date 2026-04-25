package com.example.kuznetsovaa_b2_v6

import android.content.Intent
import android.content.SharedPreferences
import androidx.appcompat.app.AppCompatActivity
import android.os.Bundle
import android.widget.EditText
import android.widget.Toast
import androidx.appcompat.widget.AppCompatButton

class MainActivity : AppCompatActivity() {

    lateinit var sh : SharedPreferences

    lateinit var btn : AppCompatButton
    lateinit var loginEditText: EditText
    lateinit var passwordEditText: EditText

    var isLogined : Boolean = false

    override fun onCreate(savedInstanceState: Bundle?) {
        super.onCreate(savedInstanceState)
        setContentView(R.layout.activity_main)
        sh = getSharedPreferences("userData", MODE_PRIVATE)

        btn = findViewById(R.id.btnLogin)
        loginEditText = findViewById(R.id.loginEdit)
        passwordEditText = findViewById(R.id.passEdit)

        if(sh.contains("login") && sh.contains("password")){
            isLogined = true;

        }

        btn.setOnClickListener({
            action()
        })


    }

    fun action(){

        if(loginEditText.text.toString() =="" && passwordEditText.text.toString() == ""){
            Toast.makeText(this, "Введены неверные данные", Toast.LENGTH_SHORT).show()
            return;
        }
        if(isLogined){
            val loginSh : String? = sh.getString("login", "")
            val passSh : String? = sh.getString("password", "")

            if(loginEditText.text.toString() == loginSh && passwordEditText.text.toString() == passSh){
                var intent = Intent(this, ChatsAcivity::class.java)
                startActivity(intent)
            }
            else{
                Toast.makeText(this, "Введены неверные данные", Toast.LENGTH_SHORT).show()
            }
        }
        else{
            var edit = sh.edit()

            edit.putString("login", loginEditText.text.toString())
            edit.putString("password", passwordEditText.text.toString())
            edit.apply()

            var intent = Intent(this, ChatsAcivity::class.java)
            startActivity(intent)
        }
    }

}