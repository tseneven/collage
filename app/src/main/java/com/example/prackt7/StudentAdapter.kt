package com.example.prackt7

import android.view.LayoutInflater
import android.view.View
import android.view.ViewGroup
import android.widget.ImageButton
import android.widget.PopupMenu
import android.widget.TextView
import androidx.recyclerview.widget.RecyclerView
import com.example.prackt7.R

class StudentAdapter(
    private var students: MutableList<Student>,
    private val listener: OnStudentActionListener
) : RecyclerView.Adapter<StudentAdapter.StudentViewHolder>() {

    interface OnStudentActionListener {
        fun onEdit(student: Student)
        fun onDelete(student: Student)
        fun onStudentClick(student: Student)
    }

    class StudentViewHolder(view: View) : RecyclerView.ViewHolder(view) {
        val name: TextView = view.findViewById(R.id.studentName)
        val group: TextView = view.findViewById(R.id.studentGroup)
        val course: TextView = view.findViewById(R.id.studentCourse)
        val menuBtn: ImageButton = view.findViewById(R.id.menuButton)
    }


    override fun onCreateViewHolder(parent: ViewGroup, viewType: Int): StudentViewHolder {
        val view = LayoutInflater.from(parent.context)
            .inflate(R.layout.item_student, parent, false)
        return StudentViewHolder(view)
    }

    override fun onBindViewHolder(holder: StudentViewHolder, position: Int) {
        val student = students[position]
        holder.name.text = student.fullName
        holder.group.text = student.groupName
        holder.course.text = "Курс: ${student.course}"

        holder.itemView.setOnClickListener {
            listener.onStudentClick(student)
        }

        holder.menuBtn.setOnClickListener { view ->
            val popup = PopupMenu(view.context, view)
            popup.inflate(R.menu.student_item_menu)
            popup.setOnMenuItemClickListener { item ->
                when(item.itemId) {
                    R.id.editStudent -> listener.onEdit(student)
                    R.id.deleteStudent -> listener.onDelete(student)
                }
                true
            }
            popup.show()
        }
    }

    override fun getItemCount(): Int = students.size

    fun updateList(newList: List<Student>) {
        students.clear()
        students.addAll(newList)
        notifyDataSetChanged()
    }

}


