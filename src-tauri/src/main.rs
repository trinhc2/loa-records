#![cfg_attr(not(debug_assertions), windows_subsystem = "windows")]

mod db;

use tauri::command;

#[tauri::command]
fn get_encounter_previews(local_player: String) -> Result<Vec<db::BossRecordResult>, String> {
    println!("Received local_player: {}", local_player);
    match db::get_encounter_previews(local_player) {
        Ok(results) => Ok(results),
        Err(e) => Err(e), // Since we're already returning a String error in db, we can just pass it through
    }
}

fn main() {
    tauri::Builder::default()
        .invoke_handler(tauri::generate_handler![get_encounter_previews])
        .run(tauri::generate_context!())
        .expect("error while running tauri application");
}
