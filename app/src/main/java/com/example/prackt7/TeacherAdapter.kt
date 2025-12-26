package com.example.prackt7

import android.view.LayoutInflater
import android.view.View
import android.view.ViewGroup
import android.widget.ImageButton
import android.widget.PopupMenu
import android.widget.TextView
import androidx.recyclerview.widget.RecyclerView

class TeacherAdapter(
    private var teachers: MutableList<Teacher>,
    private val listener: OnTeacherActionListener
) : RecyclerView.Adapter<TeacherAdapter.TeacherViewHolder>() {

    interface OnTeacherActionListener {
        fun onEdit(teacher: Teacher)
        fun onDelete(teacher: Teacher)
        fun onTeacherClick(teacher: Teacher)
    }

    class TeacherViewHolder(view: View) : RecyclerView.ViewHolder(view) {
        val name: TextView = view.findViewById(R.id.teacherName)
        val position: TextView = view.findViewById(R.id.teacherPosition)
        val hours: TextView = view.findViewById(R.id.teacherHours)
        val menuBtn: ImageButton = view.findViewById(R.id.menuButton)
    }

    override fun onCreateViewHolder(parent: ViewGroup, viewType: Int): TeacherViewHolder {
        val view = LayoutInflater.from(parent.context)
            .inflate(R.layout.item_teacher, parent, false)
        return TeacherViewHolder(view)
    }

    override fun onBindViewHolder(holder: TeacherViewHolder, position: Int) {
        val teacher = teachers[position]
        holder.name.text = teacher.fullName
        holder.position.text = teacher.position
        holder.hours.text = "Макс. часов: ${teacher.maxHoursPerYear}"

        holder.itemView.setOnClickListener {
            listener.onTeacherClick(teacher)
        }

        holder.menuBtn.setOnClickListener { view ->
            val popup = PopupMenu(view.context, view)
            popup.inflate(R.menu.teacher_item_menu)
            popup.setOnMenuItemClickListener { item ->
                when(item.itemId) {
                    R.id.editTeacher -> listener.onEdit(teacher)
                    R.id.deleteTeacher -> listener.onDelete(teacher)
                }
                true
            }
            popup.show()
        }
    }

    override fun getItemCount(): Int = teachers.size

    fun updateList(newList: List<Teacher>) {
        teachers.clear()
        teachers.addAll(newList)
        notifyDataSetChanged()
    }
}
