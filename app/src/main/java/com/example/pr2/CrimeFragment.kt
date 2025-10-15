package com.example.pr2

import android.content.SharedPreferences
import android.os.Bundle
import android.text.Editable
import android.text.TextWatcher
import android.util.Log
import android.view.LayoutInflater
import android.view.View
import android.view.ViewGroup
import android.widget.Button
import android.widget.CheckBox
import android.widget.EditText
import androidx.appcompat.app.AppCompatActivity
import androidx.fragment.app.Fragment

class CrimeFragment : Fragment(){
    private lateinit var crime : CrimeData
    private lateinit var titleField: EditText
    private lateinit var dateButton: Button
    private lateinit var solvedCheckBox: CheckBox
    private lateinit var sp : SharedPreferences

    override fun onCreate(savedInstanceState: Bundle?) {
        super.onCreate(savedInstanceState)
        crime = CrimeData()
    }

    override fun onCreateView(
        inflater: LayoutInflater,
        container: ViewGroup?,
        savedInstanceState: Bundle?
    ): View? {
        val view = inflater.inflate(R.layout.fragment_crime, container, false)
        titleField = view.findViewById(R.id.crime_title)
        dateButton = view.findViewById(R.id.crime_date)
        dateButton.apply {
            isEnabled = false
            text = crime.name
        }
        solvedCheckBox = view.findViewById(R.id.crime_solved)



        dateButton.setOnClickListener{
            if (crime.isSolved == true){
                sp = requireActivity().getSharedPreferences("UserData", AppCompatActivity.MODE_PRIVATE)
                val editor = sp.edit()
                editor.putString("title", titleField.text.toString())
                editor.apply()
                (activity as? MainActivity)?.openSecondFragment()
            }
        }

        return view
    }

    override fun onStart() {
        super.onStart()
        val titleWatcher = object : TextWatcher
        {

            override fun beforeTextChanged(                 sequence: CharSequence?,                 start: Int,                 count: Int,                 after: Int
            ) {
                // Это пространство оставлено пустым специально
            }

            override fun onTextChanged(                 sequence: CharSequence?,                 start: Int,                 before: Int,                 count: Int
            ) {
                crime.name = sequence.toString()
            }

            override fun afterTextChanged(sequence: Editable?) {
                }
        }
        titleField.addTextChangedListener(titleWatcher)
        solvedCheckBox.apply {
            setOnCheckedChangeListener { _, isChecked ->
                crime.isSolved = isChecked
                Log.d("tag", "1")
                dateButton.apply {
                    isEnabled = isChecked
                    text = crime.name
                }

            }
        }
    }
    }

