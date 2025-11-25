package com.example.up0102_z3_1

import android.content.Intent
import androidx.appcompat.app.AppCompatActivity
import android.os.Bundle
import android.widget.Button

class MenuActivity : AppCompatActivity() {
    override fun onCreate(savedInstanceState: Bundle?) {
        super.onCreate(savedInstanceState)
        setContentView(R.layout.activity_menu)

        val expoButton : Button = findViewById(R.id.expo)
        val horrorButton : Button = findViewById(R.id.horror)
        val dramButton : Button = findViewById(R.id.dram)
        val comedyButton : Button = findViewById(R.id.comedy)

        expoButton.setOnClickListener{
            val intent = Intent(this@MenuActivity, QuestsActivity::class.java)
            startActivity(intent)
        }

        horrorButton.setOnClickListener{
            val intent = Intent(this@MenuActivity, ChannelActivity::class.java)
            intent.putExtra("theme", "Horror")
            startActivity(intent)
        }

        dramButton.setOnClickListener{
            val intent = Intent(this@MenuActivity, ChannelActivity::class.java)
            intent.putExtra("theme", "Dram")
            startActivity(intent)
        }

        comedyButton.setOnClickListener{
            val intent = Intent(this@MenuActivity, ChannelActivity::class.java)
            intent.putExtra("theme", "Comedy")
            startActivity(intent)
        }

    }
}