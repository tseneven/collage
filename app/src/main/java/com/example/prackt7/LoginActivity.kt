package com.example.prackt7

import androidx.appcompat.app.AppCompatActivity
import android.os.Bundle
import androidx.room.Room

class LoginActivity : AppCompatActivity() {
    override fun onCreate(savedInstanceState: Bundle?) {
        super.onCreate(savedInstanceState)
        val db = Room.databaseBuilder(
            applicationContext,
            CollegeDatabase::class.java, "college-db"
        ).build()
        setContentView(R.layout.activity_login)
    }
}
