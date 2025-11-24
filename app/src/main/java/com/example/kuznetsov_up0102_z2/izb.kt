package com.example.kuznetsov_up0102_z2

import android.app.Activity
import android.os.Bundle
import android.view.View
import android.view.ViewGroup
import android.widget.ArrayAdapter
import android.widget.ImageButton
import android.widget.LinearLayout
import android.widget.ListView
import android.widget.TextView
import androidx.appcompat.app.AppCompatActivity
import kotlinx.coroutines.CoroutineScope
import kotlinx.coroutines.Dispatchers
import kotlinx.coroutines.launch
import kotlinx.coroutines.withContext

class izb : AppCompatActivity() {

    lateinit var listView: ListView

    override fun onCreate(savedInstanceState: Bundle?) {
        super.onCreate(savedInstanceState)
        setContentView(R.layout.activity_izb)

        listView = findViewById(R.id.list)

        CoroutineScope(Dispatchers.IO).launch {
            val cities = AppDatabase.getInstance(this@izb).cityDao().getAllCities().toMutableList()

            withContext(Dispatchers.Main) {
                val adapter = IzbAdapter(this@izb, cities)
                listView.adapter = adapter
            }
        }
    }
}


class IzbAdapter(
    private val context: Activity,
    private val cities: MutableList<City>
) : ArrayAdapter<City>(context, R.layout.liner, cities) {

    private val backgrounds = arrayOf(
        R.drawable.g1, R.drawable.g2, R.drawable.g3, R.drawable.g4, R.drawable.g5, R.drawable.g6
    )

    override fun getView(position: Int, convertView: View?, parent: ViewGroup): View {
        val inflater = context.layoutInflater
        val rowView = convertView ?: inflater.inflate(R.layout.liner, parent, false)

        val bg = rowView.findViewById<LinearLayout>(R.id.main_liner)
        val nameView = rowView.findViewById<TextView>(R.id.textName)
        val descView = rowView.findViewById<TextView>(R.id.textOpis)
        val attrView = rowView.findViewById<TextView>(R.id.textDost)
        val deleteButton = rowView.findViewById<ImageButton>(R.id.like) // переиспользуем кнопку

        val city = cities[position]

        nameView.text = city.name
        descView.text = city.description
        attrView.text = city.attractions

        val bgIndex = (city.position - 1).coerceIn(backgrounds.indices)
        bg.setBackgroundResource(backgrounds[bgIndex])

        deleteButton.setOnClickListener {
            CoroutineScope(Dispatchers.IO).launch {
                AppDatabase.getInstance(context).cityDao().deleteCityById(city.id)
                withContext(Dispatchers.Main) {
                    cities.removeAt(position)
                    notifyDataSetChanged()
                }
            }
        }

        return rowView
    }
}