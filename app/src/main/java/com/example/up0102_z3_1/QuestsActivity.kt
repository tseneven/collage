package com.example.up0102_z3_1

import android.content.Context
import androidx.appcompat.app.AppCompatActivity
import android.os.Bundle
import android.view.LayoutInflater
import android.view.View
import android.view.ViewGroup
import android.widget.ImageView
import android.widget.TextView
import androidx.recyclerview.widget.GridLayoutManager
import androidx.recyclerview.widget.RecyclerView

class QuestsActivity : AppCompatActivity() {
    override fun onCreate(savedInstanceState: Bundle?) {
        super.onCreate(savedInstanceState)
        setContentView(R.layout.activity_quests)
        val rec : RecyclerView = findViewById(R.id.recyclers)
        rec.layoutManager = GridLayoutManager(this, 3)
        rec.adapter = QuestRecycler(this, MyObj().list)
    }
}

class QuestRecycler(val context: Context, val list: ArrayList<Quests>) : RecyclerView.Adapter<QuestRecycler.MyVH>(){
    override fun onCreateViewHolder(parent: ViewGroup, viewType: Int): QuestRecycler.MyVH {
        val root = LayoutInflater.from(context).inflate(R.layout.element,parent,false)
        return MyVH(root)
    }
    class MyVH(val itemView : View) : RecyclerView.ViewHolder(itemView){
        val imageView:ImageView = itemView.findViewById(R.id.img)
        val title:TextView = itemView.findViewById(R.id.title)
        val descr : TextView = itemView.findViewById(R.id.descr)
    }

    override fun onBindViewHolder(holder: MyVH, position: Int) {
        holder.imageView.setImageResource(list[position].image)
        holder.title.setText(list[position].title)
        holder.descr.setText(list[position].text)
    }

    override fun getItemCount(): Int {
        return list.size
    }
}