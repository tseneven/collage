package com.example.prackt7

import androidx.room.*

@Dao

interface StudentDao {

    @Query("SELECT * FROM Student")
    suspend fun getAll(): List<Student>

    @Query("SELECT * FROM Student WHERE fullName LIKE :query OR groupName LIKE :query")
    suspend fun search(query: String): List<Student>

    @Insert
    suspend fun insert(student: Student): Long

    @Update
    suspend fun update(student: Student)

    @Delete
    suspend fun delete(student: Student)

    @Query("SELECT * FROM Student WHERE id = :studentId")
    suspend fun getById(studentId: Long): Student?
}
