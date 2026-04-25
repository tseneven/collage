package com.example.kuznetsovaa_b2_v6

import androidx.appcompat.app.AppCompatActivity
import android.os.Bundle
import android.widget.TextView

class DetailActivity : AppCompatActivity() {

    lateinit var nameText : TextView
    lateinit var descText : TextView
    val people = arrayOf("Thanks for your service", "Alright, I wll be waiting", "Thanks for your service", "Thanks for your service")
    override fun onCreate(savedInstanceState: Bundle?) {
        super.onCreate(savedInstanceState)
        setContentView(R.layout.activity_detail)

        var name = intent.getStringExtra("name")
        val index = intent.getIntExtra("index", 0)
        nameText = findViewById(R.id.name)
        descText = findViewById(R.id.desc)

        nameText.setText(name)
        descText.setText(people[index])
    }
}