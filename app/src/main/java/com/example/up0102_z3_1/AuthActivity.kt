package com.example.up0102_z3_1

import android.content.Intent
import androidx.appcompat.app.AppCompatActivity
import android.os.Bundle
import android.view.View
import android.widget.EditText
import androidx.appcompat.app.AlertDialog

class AuthActivity : AppCompatActivity() {
    lateinit var email:EditText
    lateinit var password : EditText
    override fun onCreate(savedInstanceState: Bundle?) {
        super.onCreate(savedInstanceState)
        setContentView(R.layout.activity_auth)
        email = findViewById(R.id.email)
        password = findViewById(R.id.password)
    }

    fun login(view : View){
        if(email.text.toString().isNotEmpty() && password.text.toString().isNotEmpty()){
             val intent = Intent(this@AuthActivity, MenuActivity::class.java )
            startActivity(intent)
         }
        else{
            val alert = AlertDialog.Builder(this)
                .setTitle("Error")
                .setMessage("У вас пустые поля")
                .setPositiveButton("ok", null)
                .create()
                .show()
         }
    }
}