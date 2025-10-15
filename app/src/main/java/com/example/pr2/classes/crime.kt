package com.example.pr2

import java.util.UUID

data class CrimeData(val id: UUID = UUID.randomUUID()) {
    var name : String = ""
    var date : String = ""
    var isSolved : Boolean = false

}