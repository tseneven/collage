package com.example.kuznetsov_up0102_z2

import android.content.Intent
import androidx.appcompat.app.AppCompatActivity
import android.os.Bundle
import android.widget.ImageButton

class mainScrean : AppCompatActivity() {
    lateinit var but1 : ImageButton
    lateinit var but2 : ImageButton

    override fun onCreate(savedInstanceState: Bundle?) {
        super.onCreate(savedInstanceState)
        setContentView(R.layout.activity_main_screan)
        but1 = findViewById(R.id.but1)
        but2 = findViewById(R.id.but2)


        but1.setOnClickListener{
            val intent : Intent  = Intent(this, list::class.java)
            startActivity(intent)
        }

        but2.setOnClickListener{
            val intent : Intent  = Intent(this, izb::class.java)
            startActivity(intent)
        }

    }
}