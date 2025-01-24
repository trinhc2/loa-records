// db.rs

use rusqlite::{Connection, Result};
use serde::Serialize;

#[derive(Serialize)]
pub struct BossRecordResult {
    pub boss_name: String,
    pub best_dps: i32,
}

pub fn get_encounter_previews(local_player: String) -> Result<Vec<BossRecordResult>, String> {
    let conn = Connection::open("C:/Users/Christian/AppData/Local/LOA Logs/encounters.db")
        .map_err(|e| format!("Failed to open database: {}", e))?;

    let mut stmt = conn
        .prepare(
            "SELECT current_boss AS boss_name, MAX(my_dps) AS best_dps
         FROM encounter_preview
         WHERE local_player = ? AND cleared = 1
         GROUP BY current_boss",
        )
        .map_err(|e| format!("Failed to prepare statement: {}", e))?;

    let boss_records = stmt
        .query_map([local_player], |row| {
            Ok(BossRecordResult {
                boss_name: row.get(0)?,
                best_dps: row.get(1)?,
            })
        })
        .map_err(|e| format!("Query error: {}", e))?;

    let mut result = Vec::new();
    for record in boss_records {
        result.push(record.map_err(|e| format!("Error reading record: {}", e))?);
    }

    Ok(result)
}
