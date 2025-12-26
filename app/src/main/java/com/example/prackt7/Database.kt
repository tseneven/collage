package com.example.prackt7


import android.content.Context
import androidx.room.Database
import androidx.room.Room
import androidx.room.RoomDatabase

@Database(entities = [Student::class, Teacher::class, Specialty::class], version = 1)
abstract class CollegeDatabase : RoomDatabase() {
    abstract fun studentDao(): StudentDao
    abstract fun teacherDao(): TeacherDao

    companion object {
        @Volatile
        private var INSTANCE: CollegeDatabase? = null

        fun getInstance(context: Context): CollegeDatabase {
            return INSTANCE ?: synchronized(this) {
                val instance = Room.databaseBuilder(
                    context.applicationContext,
                    CollegeDatabase::class.java,
                    "college-db"
                ).build()
                INSTANCE = instance
                instance
            }
        }
    }
}
