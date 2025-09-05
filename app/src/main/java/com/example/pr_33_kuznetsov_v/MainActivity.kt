package com.example.pr_33_kuznetsov_v

import android.content.SharedPreferences
import android.os.Bundle
import android.widget.Button
import android.widget.EditText
import android.widget.Toast
import androidx.appcompat.app.AlertDialog
import androidx.appcompat.app.AppCompatActivity


class MainActivity : AppCompatActivity() {
    lateinit var settings: SharedPreferences
    lateinit var LoginInShared : String
    lateinit var PasswordInShared : String

    lateinit var ButtonLogin : Button
    lateinit var LoginText : EditText
    lateinit var PasswordText : EditText

    override fun onCreate(savedInstanceState: Bundle?) {
        super.onCreate(savedInstanceState)
        settings = getSharedPreferences("UserData", MODE_PRIVATE)
        setContentView(R.layout.bank_activity)

        // Подгрузка с UI
        ButtonLogin = findViewById(R.id.button_login)
        LoginText = findViewById(R.id.login)
        PasswordText = findViewById(R.id.password)

        // Подгрузка из SP
        LoginInShared = settings.getString("Login", "").toString()
        PasswordInShared = settings.getString("Password", "").toString()


        if(PasswordInShared == "" && LoginInShared == ""){
            ClickLoginFirst()
        }
        else{
            ClickLogin()
        }

    }

    // Функция перехода на другой экран в первый раз
    fun ClickLoginFirst(){
        // Проверяем поля, если они пустые, то выдаем ошибку, если нет, то сохраняем в SP и переходим на другой экран
        ButtonLogin.setOnClickListener{
            if(!LoginText.text.toString().isEmpty() && !PasswordText.text.toString().isEmpty()){
                val editor = settings.edit()

                editor.putString("Login", LoginText.text.toString())
                editor.putString("Password", PasswordText.text.toString())

                editor.apply()
            }
            else{
                var toast : Toast = Toast.makeText(this, "Поля пусты", Toast.LENGTH_LONG)
                toast.show()
            }
        }
    }
    // Функция перехода на другой экран
    fun ClickLogin(){
        // Проверяем поля, если они пустые, то выдаем ошибку, если нет, то сохраняем в SP и переходим на другой экран
        ButtonLogin.setOnClickListener{
            if(LoginText.text.toString() == LoginInShared && PasswordText.text.toString() == PasswordInShared && LoginText.text.toString() == "ects" && PasswordText.text.toString() == "ects2023"){

            }
            else{
                var toast : Toast = Toast.makeText(this, "Ошибка, данные не совпадают", Toast.LENGTH_LONG)
                toast.show()





                // Удаление данных из SharedPreferencs
                var editor = settings.edit()
                editor.remove("Login")
                editor.remove("Password")
                editor.apply()

                LoginInShared = ""
                PasswordInShared = ""

            }
        }
    }

}