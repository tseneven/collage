package com.example.prackt7

import android.os.Bundle
import android.view.LayoutInflater
import android.view.View
import android.view.ViewGroup
import android.widget.TextView
import androidx.fragment.app.Fragment
import androidx.lifecycle.lifecycleScope
import kotlinx.coroutines.launch

class StudentDetailFragment : Fragment(R.layout.student_detail_fragment) {

    private var studentId: Long = 0
    private var student: Student? = null

    override fun onViewCreated(view: View, savedInstanceState: Bundle?) {
        super.onViewCreated(view, savedInstanceState)

        studentId = arguments?.getLong("studentId") ?: 0

        if (studentId != 0L) {
            loadStudent(studentId)
        }
    }

    private fun loadStudent(id: Long) {
        lifecycleScope.launch {
            val db = CollegeDatabase.getInstance(requireContext())
            student = db.studentDao().getById(id)
            student?.let {
                view?.findViewById<TextView>(R.id.detailName)?.text = it.fullName
                view?.findViewById<TextView>(R.id.detailGroup)?.text = it.groupName
                view?.findViewById<TextView>(R.id.detailCourse)?.text = "Курс: ${it.course}"
                view?.findViewById<TextView>(R.id.detailSpecialty)?.text = "Специальность: ${it.specialtyId}"

                val date = java.text.SimpleDateFormat("yyyy-MM-dd").format(java.util.Date(it.birthDate))
                view?.findViewById<TextView>(R.id.detailBirthDate)?.text = "Дата рождения: $date"

                view?.findViewById<TextView>(R.id.detailBudget)?.text =
                    if (it.isBudget) "Бюджет: Да" else "Бюджет: Нет"
            }
        }
    }
}
