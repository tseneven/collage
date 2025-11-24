package com.example.kuznetsov_up0102_z2

import androidx.room.Dao
import androidx.room.Insert
import androidx.room.Query

@Dao
interface CityDao {
    @Insert
    fun insertCity(city: City)

    @Query("SELECT * FROM cities")
    fun getAllCities(): List<City>

    @Query("DELETE FROM cities WHERE id = :cityId")
    fun deleteCityById(cityId: Int)
}