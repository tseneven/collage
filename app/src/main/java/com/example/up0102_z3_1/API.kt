package com.example.up0102_z3_1

import android.content.Context
import com.android.volley.Request
import com.android.volley.toolbox.StringRequest
import com.android.volley.toolbox.Volley
import org.json.JSONObject

class API(private val context: Context, private val theme: ArrayList<String>) {

    val films = arrayListOf<Films>()

    fun searchFilms(callback: (ArrayList<Films>) -> Unit) {
        val queue = Volley.newRequestQueue(context)
        var completed = 0

        for (i in 0 until theme.size) {
            val url = "https://www.omdbapi.com/?apikey=daad32aa&t=${theme[i]}"

            val request = StringRequest(
                Request.Method.GET, url,
                { response ->
                    val obj = JSONObject(response)
                    val posterUrl = obj.optString("Poster", "")
                    val title = obj.optString("Title", "No title")
                    val decsrp = obj.optString("Plot", "")

                    val film = Films(title, decsrp, posterUrl)
                    films.add(film)
                    completed++

                    if (completed == theme.size) {
                        callback(films)
                    }
                },
                { error ->
                    completed++
                    if (completed == theme.size) {
                        callback(films)
                    }
                }
            )
            queue.add(request)
        }
    }
}
