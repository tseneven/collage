package com.example.prackt7


import androidx.room.*

@Dao
interface TeacherDao {
    @Insert
    suspend fun insert(teacher: Teacher): Long

    @Update
    suspend fun update(teacher: Teacher)

    @Delete
    suspend fun delete(teacher: Teacher)

    @Query("SELECT * FROM Teacher ORDER BY fullName")
    suspend fun getAll(): List<Teacher>
}
