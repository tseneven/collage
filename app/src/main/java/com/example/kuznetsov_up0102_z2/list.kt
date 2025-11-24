package com.example.kuznetsov_up0102_z2



import android.content.Context
import android.os.Bundle
import android.text.Layout
import android.util.Log
import android.widget.ArrayAdapter
import android.widget.LinearLayout
import android.widget.ListView
import android.widget.TextView
import androidx.appcompat.app.AppCompatActivity
import androidx.appcompat.widget.AppCompatImageButton
import androidx.room.Room
import kotlinx.coroutines.CoroutineScope
import kotlinx.coroutines.Dispatchers
import kotlinx.coroutines.launch


class list : AppCompatActivity() {
    lateinit var list: ListView

    override fun onCreate(savedInstanceState: Bundle?) {
        super.onCreate(savedInstanceState)
        setContentView(R.layout.activity_list)


        list = findViewById(R.id.list)

        val names = arrayOf(
            "\uD83C\uDDEA\uD83C\uDDF8 Мадрид", "\uD83C\uDDE8\uD83C\uDDF3 Пекин", "\uD83C\uDDEB\uD83C\uDDF7 Париж", "\uD83C\uDDEC\uD83C\uDDE7 Лондон", "\uD83C\uDDF5\uD83C\uDDF9 Лиссабон", "\uD83C\uDDE8\uD83C\uDDFF Прага"
        )

        val opis = arrayOf(
            "Cтолица Испании, где энергия улиц не затихает ни днём, ни ночью. Музеи мирового уровня, уютные площадки и атмосфера настоящей испанской страсти.",
            "Современный мегаполис с тысячелетней историей. Здесь небоскрёбы соседствуют с Запретным городом и древними храмами.",
            "Город романтики и утончённой красоты. Эйфелева башня, уютные бистро и искусство на каждом шагу создают особую магию.",
            "Туманная столица, сочетающая классику и хай-тек. Красные автобусы, Биг-Бен и атмосфера спокойной аристократичности",
            "Солнечный и тёплый город у океана. Узкие улочки, жёлтые трамваи и ощущение лёгкой морской свободы.",
            "Сказочный город на берегах Влтавы. Готические башни, старинные мосты и чувство настоящей европейской средневековой магии."
        )

        val dost = arrayOf(
            "Достопримечательности:\n " +
                    "• Королевский дворец\n" +
                    "• Парк Ретиро\n" +
                    "• Музей Прадо",
            "Достопримечательности:\n  " +
                    "• Великая Китайская стена\n" +
                    "• Запретный город\n" +
                    "• Храм Неба",
            "Достопримечательности:\n " +
                    "• Эйфелева башня\n" +
                    "• Лувр\n" +
                    "• Нотр-Дам де Пари",
            "Достопримечательности:\n " +
                    "• Биг-Бен\n" +
                    "• Тауэрский мост\n" +
                    "• Букингемский дворец",
            "Достопримечательности:\n " +
                    "• Башня Белен\n" +
                    "• Замок Сан-Жоржи\n" +
                    "• Монастырь Жеронимуш",
            "Достопримечательности:\n " +
                    "• Карлов мост\n" +
                    "• Пражский град\n" +
                    "• Староместская площадь",
        )

        val adapter = ChatAdapter(this, names, opis, dost)
        list.adapter = adapter
    }
}
class ChatAdapter(
    private val context: AppCompatActivity,
    private val names: Array<String>,
    private val opis: Array<String>,
    private val dost: Array<String>
) : ArrayAdapter<String>(context, R.layout.liner, names) {

    private val backgrounds = arrayOf(
        R.drawable.g1,
        R.drawable.g2,
        R.drawable.g3,
        R.drawable.g4,
        R.drawable.g5,
        R.drawable.g6
    )

    fun addCity(city: City){
        CoroutineScope(Dispatchers.IO).launch {
            val db = AppDatabase.getInstance(context)
            db.cityDao().insertCity(city)
        }
    }

    override fun getView(position: Int, convertView: android.view.View?, parent: android.view.ViewGroup): android.view.View {
        val inflater = context.layoutInflater
        val rowView = convertView ?: inflater.inflate(R.layout.liner, parent, false)

        val bg = rowView.findViewById<LinearLayout>(R.id.main_liner)
        val textView = rowView.findViewById<TextView>(R.id.textName)
        val opisView = rowView.findViewById<TextView>(R.id.textOpis)
        val dostView = rowView.findViewById<TextView>(R.id.textDost)
        val but = rowView.findViewById<AppCompatImageButton>(R.id.like)

        textView.text = names[position]
        opisView.text = opis[position]
        dostView.text = dost[position]

        bg.setBackgroundResource(backgrounds[position])

        but.setOnClickListener{
            addCity(City(name = names[position], description = opis[position], attractions = dost[position], position = position + 1 ))
        }



        return rowView
    }
}