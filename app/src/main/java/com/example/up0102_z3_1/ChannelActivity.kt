package com.example.up0102_z3_1

import android.content.Context
import android.content.Intent
import androidx.appcompat.app.AppCompatActivity
import android.os.Bundle
import android.util.Log
import android.view.LayoutInflater
import android.view.View
import android.view.ViewGroup
import android.widget.Button
import android.widget.EditText
import android.widget.ImageView
import android.widget.LinearLayout
import android.widget.TextView
import androidx.recyclerview.widget.GridLayoutManager
import androidx.recyclerview.widget.RecyclerView
import com.squareup.picasso.Picasso

class ChannelActivity : AppCompatActivity() {
    var comedy = arrayListOf<String>("Superbad", "The Hangover", "Step Brothers", "1+1", "The Grand Budapest Hotel", "The 40-Year-Old Virgin", "Dumb and Dumber", "Mean Girls", "Home Alone", "Shaun of the Dead")
    var dram = arrayListOf<String>("The Green Mile", "The Shawshank Redemption", "Fight Club", "Forrest Gump", "A Beautiful Mind", "The Pursuit of Happyness", "The Social Network", "The Help", "Whiplash", "A Star Is Born")
    var horror = arrayListOf<String>("The Conjuring", "Insidious", "Hereditary", "The Exorcist", "It", "Sinister", "A Nightmare on Elm Street", "The Ring", "Paranormal Activity", "Midsommar")
    lateinit var title : TextView
    lateinit var search : Button
    lateinit var titleEdit : EditText
    lateinit var currentFilms: ArrayList<Films>
    override fun onCreate(savedInstanceState: Bundle?) {
        super.onCreate(savedInstanceState)
        setContentView(R.layout.activity_channel)
        val theme : String? = intent.getStringExtra("theme")

        search = findViewById(R.id.search_button)
        titleEdit = findViewById(R.id.titleEdit)

        val searchButton: Button = findViewById(R.id.search_button)
        val searchResults: LinearLayout = findViewById(R.id.search_results)
        val titleEdit: EditText = findViewById(R.id.titleEdit)

        searchButton.setOnClickListener {
            val titleToSearch = titleEdit.text.toString().lowercase().trim()
            if (titleToSearch.isNotEmpty() && ::currentFilms.isInitialized) {
                val filtered = currentFilms.filter { it.title.lowercase().contains(titleToSearch) }

                searchResults.removeAllViews()  
                searchResults.invalidate()
                searchResults.requestLayout()

                val inflater = LayoutInflater.from(this)
                filtered.forEach { film ->
                    val itemView = inflater.inflate(R.layout.element, searchResults, false)
                    val img: ImageView = itemView.findViewById(R.id.img)
                    val titleView: TextView = itemView.findViewById(R.id.title)
                    val btDetails: TextView = itemView.findViewById(R.id.bt_details)

                    titleView.text = film.title
                    btDetails.text = ""

                    if (film.poster.isNotEmpty() && film.poster != "N/A") {
                        Picasso.get().load(film.poster).placeholder(R.drawable.fon).into(img)
                    } else {
                        img.setImageResource(R.drawable.fon)
                    }

                    searchResults.addView(itemView)
                }

                if (filtered.isEmpty()) {
                    val notFound = TextView(this)
                    notFound.text = "Фильм не найден"
                    notFound.setTextColor(resources.getColor(R.color.aqua))
                    searchResults.addView(notFound)
                }
            }
        }



        when(theme){
            "Horror" -> {
                val api  = API(this, horror)
                api.searchFilms { posters ->
                    Log.d("POSTERS", posters.toString())
                    currentFilms = posters
                    val rec : RecyclerView = findViewById(R.id.recyclers)
                    rec.layoutManager = GridLayoutManager(this, 3)
                    rec.adapter = ChannelRecycler(this, posters)
                }
            }
            "Dram" -> {
                val api  = API(this, dram)
                api.searchFilms { posters ->
                    Log.d("POSTERS", posters.toString())
                    currentFilms = posters
                    val rec : RecyclerView = findViewById(R.id.recyclers)
                    rec.layoutManager = GridLayoutManager(this, 3)
                    rec.adapter = ChannelRecycler(this, posters)
                }
            }
            "Comedy" -> {
                val api  = API(this, comedy)
                api.searchFilms { posters ->
                    Log.d("POSTERS", posters.toString())
                    currentFilms = posters
                    val rec : RecyclerView = findViewById(R.id.recyclers)
                    rec.layoutManager = GridLayoutManager(this, 3)
                    rec.adapter = ChannelRecycler(this, posters)
                }
            }
        }

        title = findViewById(R.id.title)

        title.setText(theme)
    }
}
class ChannelRecycler(val context: Context, val list: ArrayList<Films>) : RecyclerView.Adapter<ChannelRecycler.MyVH>(){
    override fun onCreateViewHolder(parent: ViewGroup, viewType: Int): ChannelRecycler.MyVH {
        val root = LayoutInflater.from(context).inflate(R.layout.element,parent,false)
        return MyVH(root)
    }
    class MyVH(val itemView : View) : RecyclerView.ViewHolder(itemView){
        val imageView: ImageView = itemView.findViewById(R.id.img)
        val title:TextView = itemView.findViewById(R.id.title)
        val descr : TextView = itemView.findViewById(R.id.descr)
    }

    override fun onBindViewHolder(holder: MyVH, position: Int) {
        val posterUrl = list[position].poster
        Log.e("POSTER", posterUrl)
        if (posterUrl.isNotEmpty() && posterUrl != "N/A") {
            Picasso.get()
                .load(posterUrl)
                .placeholder(R.drawable.fon)
                .into(holder.imageView)
        } else {
            holder.imageView.setImageResource(R.drawable.fon)
        }
        holder.title.setText(list[position].title)
        holder.descr.setText(list[position].descrp)
    }

    override fun getItemCount(): Int {
        return list.size
    }
}