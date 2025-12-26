package com.example.prackt7

import android.content.Context
import android.content.SharedPreferences
import android.os.Bundle
import android.view.LayoutInflater
import android.view.View
import android.view.ViewGroup
import android.widget.Button
import android.widget.CheckBox
import android.widget.DatePicker
import android.widget.EditText
import android.widget.Toast
import androidx.fragment.app.Fragment
import androidx.lifecycle.lifecycleScope
import androidx.navigation.fragment.findNavController
import androidx.recyclerview.widget.LinearLayoutManager
import androidx.recyclerview.widget.RecyclerView
import kotlinx.coroutines.launch
import java.util.Calendar


class StudentsFragment : Fragment(R.layout.fragment_students), StudentAdapter.OnStudentActionListener {

    private lateinit var adapter: StudentAdapter
    private lateinit var sp: SharedPreferences

    private val studentList = mutableListOf<Student>()

    override fun onViewCreated(view: View, savedInstanceState: Bundle?) {
        super.onViewCreated(view, savedInstanceState)

        val recycler = view.findViewById<RecyclerView>(R.id.studentsRecycler)
        adapter = StudentAdapter(studentList, this)
        recycler.layoutManager = LinearLayoutManager(requireContext())
        recycler.adapter = adapter
        sp = requireActivity().getSharedPreferences("login_data", Context.MODE_PRIVATE)
        val role : String = sp.getString("role", "").toString()

        if(role == "student"){
            view.findViewById<Button>(R.id.addStudentBtn).isEnabled = false
        }

        val searchEdit = view.findViewById<EditText>(R.id.searchStudent)
        searchEdit.addTextChangedListener(object : android.text.TextWatcher {
            override fun beforeTextChanged(s: CharSequence?, start: Int, count: Int, after: Int) {}
            override fun onTextChanged(s: CharSequence?, start: Int, before: Int, count: Int) {
                filterStudents(s.toString())
            }
            override fun afterTextChanged(s: android.text.Editable?) {}
        })

        view.findViewById<Button>(R.id.addStudentBtn).setOnClickListener {
            showStudentDialog(null)
        }

        loadStudents()
    }

    private fun filterStudents(query: String) {
        val db = CollegeDatabase.getInstance(requireContext())
        lifecycleScope.launch {
            val allStudents = db.studentDao().getAll()
            val filtered = allStudents.filter {
                it.fullName.contains(query, ignoreCase = true) ||
                        it.groupName.contains(query, ignoreCase = true)
            }
            studentList.clear()
            studentList.addAll(filtered)
            adapter.notifyDataSetChanged()
        }
    }


    private fun loadStudents() {
        lifecycleScope.launch {
            val db = CollegeDatabase.getInstance(requireContext())
            val students = db.studentDao().getAll()
            studentList.clear()
            studentList.addAll(students)
            adapter.notifyDataSetChanged()
        }
    }

    override fun onEdit(student: Student) {
        val role : String = sp.getString("role", "").toString()

        if(role != "student"){
            showStudentDialog(student)
        }
        else{
            Toast.makeText(requireContext(), "Нету доступа!", Toast.LENGTH_LONG).show()
        }
    }

    override fun onStudentClick(student: Student) {
            val bundle = Bundle()
            bundle.putLong("studentId", student.id)
            findNavController().navigate(R.id.studentDetailFragment, bundle)


    }



    override fun onDelete(student: Student) {
        val role : String = sp.getString("role", "").toString()

        if(role != "student"){
            lifecycleScope.launch {
                val db = CollegeDatabase.getInstance(requireContext())
                db.studentDao().delete(student)
                loadStudents()
            }
        }
        else{
            Toast.makeText(requireContext(), "Нету доступа!", Toast.LENGTH_LONG).show()
        }
    }
    private fun showStudentDialog(student: Student? = null) {
        val dialogView = LayoutInflater.from(requireContext()).inflate(R.layout.dialog_student, null)
        val editFullName = dialogView.findViewById<EditText>(R.id.editFullName)
        val editGroup = dialogView.findViewById<EditText>(R.id.editGroup)
        val editCourse = dialogView.findViewById<EditText>(R.id.editCourse)
        val editSpecialty = dialogView.findViewById<EditText>(R.id.editSpecialtyId)
        val datePicker = dialogView.findViewById<DatePicker>(R.id.datePickerBirth)
        val checkBudget = dialogView.findViewById<CheckBox>(R.id.checkBudget)
        val saveButton = dialogView.findViewById<Button>(R.id.saveButton)

        student?.let {
            editFullName.setText(it.fullName)
            editGroup.setText(it.groupName)
            editCourse.setText(it.course.toString())
            editSpecialty.setText(it.specialtyId.toString())
            student?.let {
                val calendar = Calendar.getInstance()
                calendar.timeInMillis = it.birthDate

                datePicker.updateDate(
                    calendar.get(Calendar.YEAR),
                    calendar.get(Calendar.MONTH),
                    calendar.get(Calendar.DAY_OF_MONTH)
                )
            }
            checkBudget.isChecked = it.isBudget
        }

        val dialog = android.app.AlertDialog.Builder(requireContext())
            .setView(dialogView)
            .create()

        saveButton.setOnClickListener {
            val fullName = editFullName.text.toString()
            val group = editGroup.text.toString()
            val course = editCourse.text.toString().toIntOrNull() ?: 1
            val specialtyId = editSpecialty.text.toString().toIntOrNull() ?: 0
            val calendar = Calendar.getInstance()
            calendar.set(
                datePicker.year,
                datePicker.month,
                datePicker.dayOfMonth
            )

            val birthDate = calendar.timeInMillis


            val isBudget = checkBudget.isChecked

            lifecycleScope.launch {
                val db = CollegeDatabase.getInstance(requireContext())
                if (student == null) {
                    db.studentDao().insert(
                        Student(
                            fullName = fullName,
                            groupName = group,
                            course = course,
                            specialtyId = specialtyId,
                            birthDate = birthDate,
                            isBudget = isBudget,
                            photoPath = null
                        )
                    )
                } else {
                    db.studentDao().update(
                        student.copy(
                            fullName = fullName,
                            groupName = group,
                            course = course,
                            specialtyId = specialtyId,
                            birthDate = birthDate,
                            isBudget = isBudget
                        )
                    )
                }
                loadStudents()
                dialog.dismiss()
            }
        }

        dialog.show()
    }
}

