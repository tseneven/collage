package com.example.prackt7

import android.content.Context
import android.content.SharedPreferences
import android.os.Bundle
import android.text.TextWatcher
import android.view.LayoutInflater
import android.view.View
import android.widget.Button
import android.widget.EditText
import android.widget.Toast
import androidx.fragment.app.Fragment
import androidx.lifecycle.lifecycleScope
import androidx.recyclerview.widget.LinearLayoutManager
import androidx.recyclerview.widget.RecyclerView
import kotlinx.coroutines.launch

class TeachersFragment : Fragment(R.layout.fragment_teachers), TeacherAdapter.OnTeacherActionListener {

    private lateinit var adapter: TeacherAdapter
    private lateinit var sp: SharedPreferences
    private val teacherList = mutableListOf<Teacher>()

    override fun onViewCreated(view: View, savedInstanceState: Bundle?) {
        super.onViewCreated(view, savedInstanceState)

        sp = requireActivity().getSharedPreferences("login_data", Context.MODE_PRIVATE)
        val role = sp.getString("role", "")


        if(role == "student" || role == "teacher"){
            view.findViewById<Button>(R.id.addTeacherBtn).isEnabled = false
        }


        val recycler = view.findViewById<RecyclerView>(R.id.teachersRecycler)
        adapter = TeacherAdapter(teacherList, this)
        recycler.layoutManager = LinearLayoutManager(requireContext())
        recycler.adapter = adapter

        val addBtn = view.findViewById<Button>(R.id.addTeacherBtn)
        if(role == "teacher") addBtn.isEnabled = false

        addBtn.setOnClickListener { showTeacherDialog(null) }

        val searchEdit = view.findViewById<EditText>(R.id.searchTeacher)
        searchEdit.addTextChangedListener(object : TextWatcher {
            override fun beforeTextChanged(s: CharSequence?, start: Int, count: Int, after: Int) {}
            override fun onTextChanged(s: CharSequence?, start: Int, before: Int, count: Int) {
                filterTeachers(s.toString())
            }
            override fun afterTextChanged(s: android.text.Editable?) {}
        })

        loadTeachers()
    }

    private fun loadTeachers() {
        lifecycleScope.launch {
            val db = CollegeDatabase.getInstance(requireContext())
            val teachers = db.teacherDao().getAll()
            teacherList.clear()
            teacherList.addAll(teachers)
            adapter.notifyDataSetChanged()
        }
    }

    private fun filterTeachers(query: String) {
        val db = CollegeDatabase.getInstance(requireContext())
        lifecycleScope.launch {
            val allTeachers = db.teacherDao().getAll()
            val filtered = allTeachers.filter {
                it.fullName.contains(query, true) || it.position.contains(query, true)
            }
            teacherList.clear()
            teacherList.addAll(filtered)
            adapter.notifyDataSetChanged()
        }
    }

    override fun onEdit(teacher: Teacher) {
        val role = sp.getString("role", "")
        if(role != "student" || role != "teacher") showTeacherDialog(teacher)
        else Toast.makeText(requireContext(), "Нет доступа!", Toast.LENGTH_LONG).show()
    }

    override fun onDelete(teacher: Teacher) {
        val role = sp.getString("role", "")
        if(role != "student" || role != "teacher") {
            lifecycleScope.launch {
                val db = CollegeDatabase.getInstance(requireContext())
                db.teacherDao().delete(teacher)
                loadTeachers()
            }
        } else Toast.makeText(requireContext(), "Нет доступа!", Toast.LENGTH_LONG).show()
    }

    override fun onTeacherClick(teacher: Teacher) {
    }

    private fun showTeacherDialog(teacher: Teacher? = null) {
        val dialogView = LayoutInflater.from(requireContext()).inflate(R.layout.dialog_teacher, null)
        val editName = dialogView.findViewById<EditText>(R.id.editTeacherName)
        val editPosition = dialogView.findViewById<EditText>(R.id.editTeacherPosition)
        val editHours = dialogView.findViewById<EditText>(R.id.editTeacherHours)
        val saveBtn = dialogView.findViewById<Button>(R.id.saveTeacherButton)

        teacher?.let {
            editName.setText(it.fullName)
            editPosition.setText(it.position)
            editHours.setText(it.maxHoursPerYear.toString())
        }

        val dialog = android.app.AlertDialog.Builder(requireContext())
            .setView(dialogView)
            .create()

        saveBtn.setOnClickListener {
            val name = editName.text.toString()
            val position = editPosition.text.toString()
            val hours = editHours.text.toString().toIntOrNull() ?: 0

            if(hours > 1439){
                Toast.makeText(requireContext(), "Слишком много часов", Toast.LENGTH_LONG).show()
            }
            else{
                lifecycleScope.launch {
                    val db = CollegeDatabase.getInstance(requireContext())
                    if(teacher == null){
                        db.teacherDao().insert(Teacher(fullName = name, position = position, maxHoursPerYear = hours))
                    } else {
                        db.teacherDao().update(teacher.copy(fullName = name, position = position, maxHoursPerYear = hours))
                    }
                    loadTeachers()
                    dialog.dismiss()
                }

            }

        }

        dialog.show()
    }
}
