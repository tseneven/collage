package com.example.prackt7

import android.content.Context
import android.content.SharedPreferences
import android.os.Bundle
import android.view.View
import android.widget.CheckBox
import android.widget.EditText
import android.widget.Toast
import androidx.fragment.app.Fragment
import androidx.navigation.fragment.findNavController
import androidx.appcompat.widget.AppCompatButton

class LoginFragment : Fragment(R.layout.fragment_login) {

    private lateinit var sp: SharedPreferences
    private lateinit var but: AppCompatButton
    private lateinit var log: EditText
    private lateinit var pas: EditText
    private lateinit var check1: CheckBox
    private lateinit var check2: CheckBox
    private lateinit var check3: CheckBox

    override fun onViewCreated(view: View, savedInstanceState: Bundle?) {
        super.onViewCreated(view, savedInstanceState)

        sp = requireActivity().getSharedPreferences("login_data", Context.MODE_PRIVATE)

        log = view.findViewById(R.id.email_edit)
        pas = view.findViewById(R.id.pas_edit)
        check1 = view.findViewById(R.id.check1)
        check2 = view.findViewById(R.id.check2)
        check3 = view.findViewById(R.id.check3)
        but = view.findViewById(R.id.log_but)

        check1.setOnClickListener {
            check2.isChecked = false
            check3.isChecked = false
        }

        check2.setOnClickListener {
            check1.isChecked = false
            check3.isChecked = false
        }

        check3.setOnClickListener {
            check1.isChecked = false
            check2.isChecked = false
        }

        but.setOnClickListener {
            action()
        }
    }

    private fun action() {
        val emailText = log.text.toString()
        val passText = pas.text.toString()

        if (emailText.isEmpty() || passText.isEmpty()) {
            Toast.makeText(requireContext(), "Введите email и пароль", Toast.LENGTH_SHORT).show()
            return
        }

        val role = when {
            check1.isChecked -> "student"
            check2.isChecked -> "teacher"
            check3.isChecked -> "coms"
            else -> ""
        }

        if (role.isEmpty()) {
            Toast.makeText(requireContext(), "Выберите роль", Toast.LENGTH_SHORT).show()
            return
        }

        val editor = sp.edit()
        editor.putString("email", emailText)
        editor.putString("password", passText)
        editor.putString("role", role)
        editor.apply()

        findNavController().navigate(R.id.action_login_to_menu)
    }
}
