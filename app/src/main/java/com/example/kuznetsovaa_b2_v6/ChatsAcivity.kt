package com.example.kuznetsovaa_b2_v6

import android.content.Intent
import androidx.appcompat.app.AppCompatActivity
import android.os.Bundle
import android.util.Log
import android.view.View
import android.widget.AdapterView
import android.widget.ArrayAdapter
import android.widget.Spinner
import android.widget.Toast
import androidx.appcompat.widget.AppCompatImageButton
import java.util.logging.Logger


class ChatsAcivity : AppCompatActivity() {
    lateinit var btnback : AppCompatImageButton

    lateinit var spin : Spinner

    var name : String = ""

    var index : Int = 0

    var isSelected = 0

    override fun onCreate(savedInstanceState: Bundle?) {
        super.onCreate(savedInstanceState)
        setContentView(R.layout.activity_chats_acivity)

        btnback = findViewById(R.id.btnback)

        btnback.setOnClickListener({
            val intent = Intent(this, MainActivity::class.java)
            startActivity(intent)
        })


        spin = findViewById(R.id.spin)

        val people = arrayOf("John Joshua", "Chinonso James", "Raph Ron", "Joy Ezekiel")

        var adapter = ArrayAdapter(this, android.R.layout.simple_spinner_item, people)

        adapter.setDropDownViewResource(android.R.layout.simple_spinner_dropdown_item)

        spin.adapter = adapter

        spin.onItemSelectedListener = object :
            AdapterView.OnItemSelectedListener {
            override fun onItemSelected(p0: AdapterView<*>?, p1: View?, p2: Int, p3: Long) {
                var selectedItem = p0?.selectedItem;

                name = selectedItem.toString()
                index = p0?.selectedItemId?.toInt() ?: 0
                isSelected++;
                if(isSelected > 1)
                    action()
            }

            override fun onNothingSelected(p0: AdapterView<*>?) {

            }

        }
    }

    fun action(){
        var intent = Intent(this, DetailActivity::class.java).apply {
            putExtra("name", name)
            putExtra("index", index)
        }

        startActivity(intent)
    }
}