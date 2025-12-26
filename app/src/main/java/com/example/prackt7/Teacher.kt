package com.example.prackt7


import androidx.room.Entity
import androidx.room.PrimaryKey

@Entity
data class Teacher(
    @PrimaryKey(autoGenerate = true)
    val id: Int = 0,
    val fullName: String,
    val position: String,
    val maxHoursPerYear: Int
)
