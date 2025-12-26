package com.example.prackt7

import android.os.Bundle
import android.view.View
import android.widget.Button
import androidx.fragment.app.Fragment
import androidx.navigation.fragment.NavHostFragment.Companion.findNavController
import androidx.navigation.fragment.findNavController

class MenuFragment : Fragment(R.layout.fragment_menu) {

    override fun onViewCreated(view: View, savedInstanceState: Bundle?) {
        super.onViewCreated(view, savedInstanceState)

        view.findViewById<Button>(R.id.btnStudents).setOnClickListener {
            findNavController().navigate(R.id.action_menu_to_students)
        }

        view.findViewById<Button>(R.id.btnTeachers).setOnClickListener {
            findNavController().navigate(R.id.action_menu_to_teachers)
        }
    }
}
