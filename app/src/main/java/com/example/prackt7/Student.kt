package com.example.prackt7


import androidx.room.Entity
import androidx.room.PrimaryKey

@Entity
data class Student(
    @PrimaryKey(autoGenerate = true)
    val id: Long = 0,
    val fullName: String,
    val groupName: String,
    val course: Int,
    val specialtyId: Int,
    val birthDate: Long,
    val isBudget: Boolean,
    val photoPath: String? = null
)
